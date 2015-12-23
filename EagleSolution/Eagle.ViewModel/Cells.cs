using System;

namespace Eagle.ViewModel
{
    public class Cells
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public Cells()
        {
        }
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public Cells(bool flag, string message, int code, int pageCount = 0)
        {
            Flag = flag;
            Message = message;
            Code = code;
            PageCount = pageCount;
        }

        public bool Flag
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }

        public int Code
        {
            get; set;
        }

        public int PageCount
        {
            get; set;
        }
    }

    public class Cells<T> : Cells
    {
        public Cells( int code, T fruit) : base(true, null, code)
        {
            Fruit = fruit;
        }
        public Cells( T fruit , int pageCount) : base(true, null, 0, pageCount)
        {
            Fruit = fruit;
        }

        public Cells(bool flag, string message, int code, T fruit, int pageCount) : base(flag, message, code, pageCount)
        {
            Fruit = fruit;
        }

        public T Fruit
        {
            get; set;
        }
    }
}