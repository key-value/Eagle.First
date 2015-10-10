using System;
using System.Collections.Generic;

namespace Eagle.ViewModel
{
    public class SplinterCell<T> : Cells
    {
        public SplinterCell(bool flag, string message, int code)
            : base(flag, message, code)
        {
        }

        public SplinterCell()
            : base(true, String.Empty, 0)
        {

        }

        public List<T> Data { get; set; }

        public int PageCount { get; set; }
    }
}