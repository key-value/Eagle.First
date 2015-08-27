using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IAppServices : IDisposable
    {
        int Code { get; }

        string Message { get; }

        bool Flag { get; }

        int PageCount { get; }

        Cells GetResult();
    }
}