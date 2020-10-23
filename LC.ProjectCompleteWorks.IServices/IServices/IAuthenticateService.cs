using LC.ProjectCompleteWorks.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.IServices
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(UsersDto request, out string token);
    }
}
