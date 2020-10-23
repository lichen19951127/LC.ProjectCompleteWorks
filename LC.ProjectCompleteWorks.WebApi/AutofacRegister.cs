using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.ProjectCompleteWorks.WebApi
{
    public class AutofacRegister:Autofac.Module
    {
        #region 单个类和接口
        //直接注册某一个类和接口
        //左边的是实现类，右边的As是接口
        //containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>().SingleInstance();
        //这里就跟默认DI差不多
        //services.AddScoped<TestServiceE, ITestServiceE>();
        #endregion

        #region 方法1   Load 适用于无接口注入
        //var assemblysServices = Assembly.Load("Exercise.Services");

        //containerBuilder.RegisterAssemblyTypes(assemblysServices)
        //          .AsImplementedInterfaces()
        //          .InstancePerLifetimeScope();

        //var assemblysRepository = Assembly.Load("Exercise.Repository");

        //containerBuilder.RegisterAssemblyTypes(assemblysRepository)
        //          .AsImplementedInterfaces()
        //          .InstancePerLifetimeScope();

        #endregion

        #region 方法2  选择性注入 与方法1 一样
        //            Assembly Repository = Assembly.Load("Exercise.Repository");
        //            Assembly IRepository = Assembly.Load("Exercise.IRepository");
        //            containerBuilder.RegisterAssemblyTypes(Repository, IRepository)
        //.Where(t => t.Name.EndsWith("Repository"))
        //.AsImplementedInterfaces().PropertiesAutowired();

        //            Assembly service = Assembly.Load("Exercise.Services");
        //            Assembly Iservice = Assembly.Load("Exercise.IServices");
        //            containerBuilder.RegisterAssemblyTypes(service, Iservice)
        //.Where(t => t.Name.EndsWith("Service"))
        //.AsImplementedInterfaces().PropertiesAutowired();
        #endregion

        #region 方法3  使用 LoadFile 加载服务层的程序集  将程序集生成到bin目录 实现解耦 不需要引用
        //获取项目路径
        //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
        //var ServicesDllFile = Path.Combine(basePath, "Exercise.Services.dll");//获取注入项目绝对路径
        //var assemblysServices = Assembly.LoadFile(ServicesDllFile);//直接采用加载文件的方法
        //containerBuilder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

        //var RepositoryDllFile = Path.Combine(basePath, "Exercise.Repository.dll");
        //var RepositoryServices = Assembly.LoadFile(RepositoryDllFile);//直接采用加载文件的方法
        //containerBuilder.RegisterAssemblyTypes(RepositoryServices).AsImplementedInterfaces();
        #endregion

        #region 在控制器中使用属性依赖注入，其中注入属性必须标注为public
        //public ITestServiceE _testService {get;set }
        //注意 上方为属性注入  发现为Null  需要在Startup.cs  的 ConfigureServices 方法下加入如下代码
        //services.AddControllers().AddControllersAsServices();

        //在控制器中使用属性依赖注入，其中注入属性必须标注为public
        //        var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
        //.Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type)).ToArray();
        //        containerBuilder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        #endregion

        /// <summary>
        /// 当前使用
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            ////程序集范围注入 将匹配所有Service结尾的
            //builder.RegisterAssemblyTypes(typeof(SysUserService).Assembly)
            //     .Where(t => t.Name.EndsWith("Service"))
            //     .AsImplementedInterfaces().PropertiesAutowired();
            ////单个注册 工作单元 和数据库上下文
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();
            //builder.RegisterType<dbContext>().As<DbContext>().PropertiesAutowired();

            //在控制器中使用属性依赖注入，其中注入属性必须标注为public  就是不需要通过构造函数 直接
            //public ITestServiceE _testService {get;set }   可直接_testService.方法  不许在构造函数接收
            //注意属性注入 发现为Null  需要在Startup.cs  的 ConfigureServices 方法下加入如下代码
            //services.AddControllers().AddControllersAsServices();
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        }
    }
}
