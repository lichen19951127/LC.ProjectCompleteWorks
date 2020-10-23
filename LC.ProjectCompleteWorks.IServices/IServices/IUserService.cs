using LC.ProjectCompleteWorks.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.IServices
{
    public interface IUserService:IBaseService<Users>
    {
        PageResult<Users> SearchUsers(SearchUserDto model);


        ReturnResult Login(UsersDto model);
    }
}
