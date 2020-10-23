using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using LC.ProjectCompleteWorks.Entitys;
using LC.ProjectCompleteWorks.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LC.ProjectCompleteWorks.WebApi
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EFDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlconn")));

            // 添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });
            });

            //允许一个或多个具体来源:
            services.AddCors(options =>
            {
                // Policy 名稱 CorsPolicy 是自訂的，可以自己改
                //跨域规则的名称
                options.AddPolicy("AllowSameDomain", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.WithOrigins("http://127.0.0.1:8011", "http://localhost:50383")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowAnyOrigin()//允许所有来源的主机访问
                    .AllowCredentials();
                });
            });
            
            //读取jwt配置
            services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }
        //集成Autofac
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("LC.ProjectCompleteWorks.Services");
            //接口层所在程序集命名空间
            Assembly repository = Assembly.Load("LC.ProjectCompleteWorks.IServices");
            //自动注入
            builder.RegisterAssemblyTypes(service, repository)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            ////注册仓储，所有IRepository接口到Repository的映射
            //builder.RegisterGeneric(typeof(Repository<>))
            //    //InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
            //    .As(typeof(IRepository<>)).InstancePerDependency();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Authentication（认证） 明确是你谁，确认是不是合法用户。常用的认证方式有用户名密码认证。
            app.UseAuthentication();

            app.UseRouting();

            app.UseCors("AllowSameDomain");//必须位于UserMvc之前 

            // 添加Swagger有关中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
            });

            //授权 明确你是否有某个权限。当用户需要使用某个功能的时候，系统需要校验用户是否需要这个功能的权限。
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
