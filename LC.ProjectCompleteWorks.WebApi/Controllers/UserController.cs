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
    public class UserController : ControllerBase
    {
        private readonly IUserService _iUserService;
        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public PageResult<Users> SearchUsers([FromQuery]SearchUserDto model)
        {
            var result = _iUserService.SearchUsers(model);
            return result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ReturnResult Login([FromBody]UsersDto model)
        {
            var result = _iUserService.Login(model);
            return result;
        }
    }
}
