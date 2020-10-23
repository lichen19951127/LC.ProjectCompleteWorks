using LC.ProjectCompleteWorks.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z.EntityFramework.Plus;

namespace LC.ProjectCompleteWorks.Common
{
    public static class IQueryExtensions
    {
        /// <summary>
        /// EF中“不使用”SQL语句查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static PageResult<T> Page<T>(this IQueryable<T> query, int page, int size) where T : class
        {
            var result = new PageResult<T>(page, size) { Data = new List<T>() };
            var listQuery = query.Skip((page - 1) * size).Take(size).AsNoTracking().Future();
            var count = query.AsNoTracking().DeferredCount().FutureValue();
            result.Data = listQuery.ToList() ?? new List<T>();
            result.RCount = count.Value;

            return result;
        }
        /// <summary>
        /// EF中使用SQL语句查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static PageResult<T> SqlPage<T>(this IQueryable<T> query, int page, int size)
        {
            var result = new PageResult<T>(page, size) { Data = new List<T>() };
            result.RCount = query.Count();
            result.Data = query.Skip((page - 1) * size).Take(size).ToList() ?? new List<T>();

            return result;
        }



        public static PageResult<T> EnumerablePage<T>(this IEnumerable<T> query, int page, int size)
        {
            var result = new PageResult<T>(page, size) { Data = new List<T>() };
            var listQuery = query.Skip((page - 1) * size).Take(size);
            var count = query.Count();

            result.Data = listQuery.ToList() ?? new List<T>();
            result.RCount = count;

            return result;
        }


    }
}
