using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server
{
    public interface ITreeServices : IAppServices
    {
        List<ShowTree> Get(int pageNum);


        void Update(UpdateTree updateTree);
        void Delete(List<Guid> treeIdList);
        UpdateTree Get(Guid treeID);
    }
}