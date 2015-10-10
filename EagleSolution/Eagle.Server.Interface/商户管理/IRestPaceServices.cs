using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface IRestPaceServices : IAppServices
    {
        List<ShowRestPace> Get(int pageNum);
    }
}