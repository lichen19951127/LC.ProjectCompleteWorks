using LC.ProjectCompleteWorks.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.IServices
{
    public interface ITestService
    {
        string GetMD5(string pwd,string salt, MD5Type type);
    }
}
