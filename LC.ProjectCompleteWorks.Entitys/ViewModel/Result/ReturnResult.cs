using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ProjectCompleteWorks.Entitys
{
    public class ReturnResult
    {
        public ReturnResult()
        {
            Code = 0;
            Msg = "";
            Obj = "";
        }

        public ReturnResult(int code, string msg = "", object obj = null)
        {
            Code = code;
            Msg = msg;
            Obj = obj ?? "";
        }

        public int Code { get; set; }

        public string Msg { get; set; }

        public object Obj { get; set; }
    }

    public class ReturnResult<T>
    {
        public ReturnResult()
        {
            Code = 0;
            Msg = "";
            Obj = default(T);
        }

        public ReturnResult(int code, string msg = "", T obj = default(T))
        {
            Code = code;
            Msg = msg;
            Obj = obj;
        }

        public int Code { get; set; }

        public string Msg { get; set; }

        public T Obj { get; set; }
    }

    public class PageResult
    {
        /// <summary>
        /// 状态码
        /// 0：成功，1：失败，其他自定义
        /// </summary>
        public int Code { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RCount { get; set; }
        public object Data { get; set; }
        public string Msg { get; set; }
    }

    public class PageResult<T>
    {
        public PageResult()
        { }
        public PageResult(int page, int size)
        {
            Page = page;
            Size = size;
        }
        /// <summary>
        /// 状态码
        /// 0：成功，1：失败，其他自定义
        /// </summary>
        public int Code { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

        private int pages;
        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages
        {
            set { pages = value; }

            get
            {
                if (RCount > 0 && Size > 0)
                {
                    return (int)Math.Ceiling((double)RCount / (double)Size);
                }
                else if (pages > 0)
                {
                    return pages;
                }
                else
                    return 0;
            }
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RCount { get; set; }
        public List<T> Data { get; set; }
        public string Msg { get; set; }
    }
}
