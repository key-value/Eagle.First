using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server.Interface
{
    public interface IBranchServices : IAppServices
    {
        List<ShowBranch> GetBranches();
        List<ShowBranch> GetBranches(int pageNum);
        List<ShowBranch> GetFirstBranches();
        void Update(UpdateBranch updateBranch);
        void Delete(List<Guid> branchIdList);
        UpdateBranch GetBranches(Guid id);
    }
}