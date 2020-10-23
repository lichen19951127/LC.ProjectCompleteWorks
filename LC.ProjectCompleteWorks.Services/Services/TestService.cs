using LC.ProjectCompleteWorks.Common;
using LC.ProjectCompleteWorks.Entitys;
using LC.ProjectCompleteWorks.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.Services
{
    public class TestService : ITestService
    {
        public string GetMD5(string pwd,string salt, MD5Type type)
        {
            if (type == MD5Type.加密)
            {
                return MD5Helper.MD5Encoding(pwd, salt);
            }
            else
            {
                return MD5Helper.MD5Encoding(pwd, salt);
            }
        }
    }
}
