using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.ProjectCompleteWorks.Entitys;
using LC.ProjectCompleteWorks.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LC.ProjectCompleteWorks.WebApi.Controllers
{
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _iTestService;
        public TestController(ITestService iTestService)
        {
            _iTestService = iTestService;
        }

        /// <summary>
        /// MD5加密/解密
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="salt">盐值</param>
        /// <param name="type">1加密 2解密</param>
        /// <returns></returns>
        [HttpGet]
        public string GetMd5(string pwd,string salt, MD5Type type)
        {
            var result = _iTestService.GetMD5(pwd,salt,type);
            return result;
        }
    }
}
