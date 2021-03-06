﻿using Eagle.Domain.Core.Model;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public abstract class ApplicationServices : DisposableObject
    {
        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        protected ApplicationServices()
        {
            Flag = false;
            PageSize = 10;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool Flag { get; set; }

        protected int _pageCount;

        public int PageCount { get { return _pageCount; } set { _pageCount = value; } }

        public int PageSize { get; set; }

        public Cells GetResult()
        {
            return new Cells(Flag, Message, Code);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}