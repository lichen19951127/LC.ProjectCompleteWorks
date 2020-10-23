using LC.ProjectCompleteWorks.Entitys;
using LC.ProjectCompleteWorks.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.Services
{
    public class BaseService<T> where T:class, IBaseService<T>,new()
    {
        public ReturnResult Edit(T model)
        {
            throw new NotImplementedException();
        }
    }
}
