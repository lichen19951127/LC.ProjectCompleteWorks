using LC.ProjectCompleteWorks.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.IServices
{
    public interface IBaseService<T> where T : class,new()
    {
        ReturnResult Edit(T model);

    }
}
