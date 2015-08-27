using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF;
using Eagle.Infrastructrue;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    public class TreeServices : ApplicationServices, ITreeServices
    {
        public List<ShowTree> Get(int pageNum)
        {
            using (var monitorContext = new MonitorContext())
            {
                var treelist = monitorContext.Trees.OrderByDescending(x => x.CreateTime)
                    .Pageing(pageNum, PageSize, ref _pageCount).ToList();

                var showTrees = new List<ShowTree>();
                foreach (var tree in treelist)
                {
                    var showTree = new ShowTree();
                    showTree.ID = tree.ID;
                    showTree.Name = tree.Name;
                    showTree.Description = tree.Description;
                    showTree.CreateTime = tree.CreateTime.ToString(SystemConst.TimeStyle);
                    showTrees.Add(showTree);
                }
                return showTrees;
            }
        }

        public void Edit(UpdateTree updateTree)
        {
            using (var monitorContext = new MonitorContext())
            {
                var tree = monitorContext.Trees.FirstOrDefault(x => x.ID == updateTree.ID);
                if (tree.Null())
                {
                    Message = "请选择要修改的服务器";
                    return;
                }
                tree.Name = updateTree.Name;
                tree.Description = updateTree.Description;
                monitorContext.ModifiedModel(tree);
                monitorContext.SaveChanges();
            }
            Flag = true;
        }

        public void Add(UpdateTree updateTree)
        {
            var tree = new Tree();
            tree.ID = Guid.NewGuid();
            tree.Name = updateTree.Name;
            tree.Description = updateTree.Description;
            tree.CreateTime = DateTime.Now;
            using (var monitorContext = new MonitorContext())
            {
                monitorContext.Trees.Add(tree);
                monitorContext.SaveChanges();
            }
            Flag = true;
        }

        public void Update(UpdateTree updateTree)
        {
            if (updateTree.ID == Guid.Empty)
            {
                Add(updateTree);
            }
            else
            {
                Edit(updateTree);
            }
        }

        public void Delete(List<Guid> treeIdList)
        {
            using (var defalutContent = new MonitorContext())
            {
                var trees = defalutContent.Trees.Where(x => treeIdList.Contains(x.ID));
                if (!trees.Any())
                {
                    Message = "请选择要删除的数据";
                    return;
                }
                defalutContent.Trees.RemoveRange(trees);
                defalutContent.SaveChanges();
                Flag = true;
            }
        }
    }
}