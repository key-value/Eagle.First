using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server.Interface;
using Eagle.ViewModel.账户体系;

namespace Eagle.Server.Services
{
    [Injection(typeof(IBranchServices))]
    public class BranchServices : ApplicationServices, IBranchServices
    {
        public List<Branch> GetBranches()
        {
            Dictionary<Guid, List<Branch>> allBranch;
            using (var context = new DefaultContext())
            {
                allBranch = context.Branches.AsNoTracking().Where(x => x.Enble).GroupBy(x => x.PreBranch).ToDictionary(x => x.Key, x => x.OrderBy(y => y.SortID).ToList());
            }
            if (!allBranch.Any() || !allBranch.ContainsKey(Guid.Empty))
            {
                return new List<Branch>();
            }
            var firstBranch = allBranch[Guid.Empty].ToList();
            var resultBranch = new List<Branch>();
            foreach (var branch in firstBranch)
            {
                if (allBranch.ContainsKey(branch.ID))
                {
                    branch.Branches = new List<Branch>();
                    foreach (var branch1 in allBranch[branch.ID])
                    {
                        branch1.ActionButtons = _actionButtons.ToList();
                        branch.Branches.Add(branch1);
                    }
                }
                else
                {
                    branch.Branches = new List<Branch>();
                }
                branch.ActionButtons = _actionButtons.ToList();
                resultBranch.Add(branch);
            }
            Flag = true;
            return resultBranch;
        }

        public List<Branch> GetBranches(int pageNum)
        {
            Flag = true;
            List<Branch> allBranch;
            using (var context = new DefaultContext())
            {
                allBranch = context.Branches.AsNoTracking().OrderBy(x => x.SortID).Pageing(pageNum, PageSize, ref _pageCount).ToList();
            }
            return allBranch;
        }

        public List<Branch> GetFirstBranches()
        {
            Flag = true;
            List<Branch> allBranch;
            using (var context = new DefaultContext())
            {
                allBranch = context.Branches.AsNoTracking().Where(x => x.Level == 1).OrderBy(x => x.SortID).ToList();
            }
            return allBranch;
        }


        private readonly List<ActionButton> _actionButtons = new List<ActionButton>()
                                                   {
new ActionButton(){ActionName = "Create",Name = "新增",Dialog = 1,Post = false},
new ActionButton(){ActionName = "Edit",Name = "修改",Dialog = 1,Post = false},
new ActionButton(){ActionName = "Delete",Name = "删除",Dialog = 0,Post = true},
                                                   };

        public void Update(UpdateBranch updateBranch)
        {
            Branch parentBranch = null;
            if (updateBranch.Level == 2 && updateBranch.PreBranch != Guid.Empty)
            {
                using (var context = new DefaultContext())
                {
                    parentBranch = context.Branches.AsNoTracking().FirstOrDefault(x => x.ID == updateBranch.PreBranch);
                }
            }
            if (parentBranch.Null())
            {
                updateBranch.Level = 1;
                updateBranch.PreBranch = Guid.Empty;
            }
            else
            {
                updateBranch.AreaName = updateBranch.AreaName;
            }
            if (updateBranch.ID == Guid.Empty)
            {
                Add(updateBranch);
            }
            else
            {
                Edit(updateBranch);
            }
            Flag = true;
        }

        private void Add(UpdateBranch updateBranch)
        {
            var branch = new Branch();
            branch.ID = Guid.NewGuid();
            branch.Title = updateBranch.Title;
            branch.Enble = true;
            branch.Level = updateBranch.Level;
            branch.PreBranch = updateBranch.PreBranch;
            branch.SortID = updateBranch.SortID;
            branch.Logo = updateBranch.Logo;
            branch.ActionName = updateBranch.ActionName;
            branch.AreaName = updateBranch.AreaName;
            branch.Description = updateBranch.Description;
            using (var context = new DefaultContext())
            {
                context.Branches.Add(branch);
                context.SaveChanges();
            }
        }

        private void Edit(UpdateBranch updateBranch)
        {
            using (var context = new DefaultContext())
            {
                var editBranch = context.Branches.FirstOrDefault(x => x.ID == updateBranch.ID);
                if (editBranch.Null())
                {
                    Message = "修改菜单不存在";
                    return;
                }
                editBranch.Title = updateBranch.Title;
                editBranch.Level = updateBranch.Level;
                editBranch.PreBranch = updateBranch.PreBranch;
                editBranch.SortID = updateBranch.SortID;
                editBranch.Logo = updateBranch.Logo;
                editBranch.ActionName = updateBranch.ActionName;
                editBranch.AreaName = updateBranch.AreaName;
                editBranch.Description = updateBranch.Description;
                context.ModifiedModel(editBranch);
                context.SaveChanges();
            }
        }
    }
}