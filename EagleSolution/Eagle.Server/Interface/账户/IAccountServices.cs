using System;

namespace Eagle.Server.Interface
{
    public interface IAccountServices : IAppServices
    {
        Guid Login(string loginID, string pwd);
    }
}