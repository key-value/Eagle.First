using System;
using System.Collections.Generic;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Interface
{
    public interface IBranchServices : IAppServices
    {
        List<Branch> GetBranches();
        List<Branch> GetBranches(int pageNum);
        List<Branch> GetFirstBranches();
        void Update(UpdateBranch updateBranch);
        void Delete(List<Guid> branchIdList);
        UpdateBranch GetBranches(Guid id);
    }
}