using System.Collections.Generic;
using Eagle.Model;
using Eagle.ViewModel.账户体系;

namespace Eagle.Server.Interface
{
    public interface IBranchServices : IAppServices
    {
        List<Branch> GetBranches();
        List<Branch> GetBranches(int pageNum);
        List<Branch> GetFirstBranches();
        void Update(UpdateBranch updateBranch);
    }
}