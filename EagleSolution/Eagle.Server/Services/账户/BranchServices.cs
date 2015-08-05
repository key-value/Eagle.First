using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server.Interface;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IBranchServices))]
    public class BranchServices : ApplicationServices, IBranchServices
    {
        public List<ShowBranch> GetBranches()
        {
            Dictionary<Guid, List<Branch>> allBranch;
            using (var context = new DefaultContext())
            {
                allBranch = context.Branches.AsNoTracking().Where(x => x.Enble).GroupBy(x => x.PreBranch).ToDictionary(x => x.Key, x => x.OrderBy(y => y.SortID).ToList());
            }
            if (!allBranch.Any() || !allBranch.ContainsKey(Guid.Empty))
            {
                return new List<ShowBranch>();
            }
            var firstBranch = allBranch[Guid.Empty].ToList();
            var resultBranch = new List<ShowBranch>();
            foreach (var branch in firstBranch)
            {
                var dataBranch = ShowBranch.CreateShowBranch(branch);
                if (allBranch.ContainsKey(branch.ID))
                {
                    dataBranch.Branches = new List<ShowBranch>();
                    foreach (var branch1 in allBranch[branch.ID])
                    {
                        branch1.ActionButtons = (new[] { 0, 1, 2 }).ToJson();
                        dataBranch.Branches.Add(ShowBranch.CreateShowBranch(branch1));
                    }
                }
                else
                {
                    dataBranch.Branches = new List<ShowBranch>();
                }
                dataBranch.ActionButtons = (new[] { 0, 1, 2 }).ToJson();
                resultBranch.Add(dataBranch);
            }
            Flag = true;
            return resultBranch;
        }


        public UpdateBranch GetBranches(Guid id)
        {
            var branch = new Branch();
            if (id != Guid.Empty)
            {
                using (var context = new DefaultContext())
                {
                    branch = context.Branches.AsNoTracking().FirstOrDefault(x => x.ID == id);
                    if (branch.Null())
                    {
                        return new UpdateBranch();
                    }
                }
            }
            var updateBranch = new UpdateBranch();
            updateBranch.ID = branch.ID;
            updateBranch.Title = branch.Title;
            updateBranch.Enble = branch.Enble;
            updateBranch.Level = branch.Level;
            updateBranch.PreBranch = branch.PreBranch;
            updateBranch.Logo = branch.Logo;
            updateBranch.AreaName = branch.AreaName;
            updateBranch.ActionName = branch.ActionName;
            updateBranch.Description = branch.Description;
            updateBranch.SortID = branch.SortID;
            return updateBranch;
        }


        public List<ShowBranch> GetBranches(int pageNum)
        {
            Flag = true;
            List<ShowBranch> allBranch = new List<ShowBranch>();
            using (var context = new DefaultContext())
            {
                var dataBranch = context.Branches.AsNoTracking().OrderBy(x => x.SortID).Pageing(pageNum, PageSize, ref _pageCount).ToList();
                if (dataBranch.Null())
                {
                    return new List<ShowBranch>();
                }
                foreach (var branch in dataBranch)
                {
                    allBranch.Add(ShowBranch.CreateShowBranch(branch));
                }
            }
            return allBranch;
        }

        public List<ShowBranch> GetFirstBranches()
        {
            Flag = true;
            List<ShowBranch> allBranch = new List<ShowBranch>();
            using (var context = new DefaultContext())
            {
                var dataBranch = context.Branches.AsNoTracking().Where(x => x.Level == 1).OrderBy(x => x.SortID).ToList();
                if (dataBranch.Null())
                {
                    return new List<ShowBranch>();
                }
                foreach (var branch in dataBranch)
                {
                    allBranch.Add(ShowBranch.CreateShowBranch(branch));
                }
            }
            return allBranch;
        }


        private readonly List<ActionButton> _actionButtons = new List<ActionButton>()
                                                   {
new ActionButton(){ActionName = "Create",Name = "新增",Dialog = 1,Post = false,Callback = true,ParamNum =  0},
new ActionButton(){ActionName = "Create",Name = "修改",Dialog = 1,Post = false,Callback = true,ParamNum = 1},
new ActionButton(){ActionName = "Delete",Name = "删除",Dialog = 0,Post = true,Callback = false,ParamNum = -1},
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
                updateBranch.AreaName = parentBranch.AreaName;
                updateBranch.Logo = string.Empty;
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
                branch.SortID = context.Branches.Max(x => x.SortID) + 1;
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


        public void Delete(List<Guid> branchIdList)
        {

            using (var context = new DefaultContext())
            {
                var branchList = context.Branches.Where(x => branchIdList.Contains(x.ID)).ToList();
                if (!branchList.Any())
                {
                    Message = "请选择正确的数据";
                    return;
                }
                context.Branches.RemoveRange(branchList);
                context.SaveChanges();
            }
            Flag = true;
        }
    }
}