using LC.ProjectCompleteWorks.Entitys;
using LC.ProjectCompleteWorks.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LC.ProjectCompleteWorks.Common;

namespace LC.ProjectCompleteWorks.Services
{
    public class UserService : IUserService
    {
        private static EFDBContext _dbContext;
        public UserService(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ReturnResult Edit(Users model)
        {
            throw new NotImplementedException();
        }

        public ReturnResult Login(UsersDto model)
        {
            var users = _dbContext.Users.Where(x=>x.UserName.Equals(model.UserName)&&x.Password.Equals(model.Password)&&x.IsEnable&&!x.IsDelete).FirstOrDefault();
            if (users == null)
            {
                return new ReturnResult(1, "用户名或密码不正确");
            }
            else
            {
                return new ReturnResult(0,"",users);
            }
        }

        public PageResult<Users> SearchUsers(SearchUserDto model)
        {
            var users = _dbContext.Users.Where(x=>x.IsEnable&&!x.IsDelete);

            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                users= users.Where(x=>x.UserName.Contains(model.Text)||x.NickName.Contains(model.Text));
            }

            var result = users.Page(model.Page,model.Size);

            return result;
        }
    }
}
