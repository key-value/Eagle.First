using System;
using System.Collections.Generic;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowBranch
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public List<ShowBranch> Branches { get; set; }

        public string Logo { get; set; }

        public string AreaName { get; set; }

        public string ActionName { get; set; }

        public string Description { get; set; }

        public string ActionButtons { get; set; }

        public int SortID { get; set; }

        public bool Enble { get; set; }

        public static ShowBranch CreateShowBranch(Branch branch)
        {
            var showBranch = new ShowBranch();
            showBranch.ID = branch.ID;
            showBranch.Title = branch.Title;
            showBranch.Logo = branch.Logo;
            showBranch.AreaName = branch.AreaName;
            showBranch.ActionName = branch.ActionName;
            showBranch.Description = branch.Description;
            showBranch.ActionButtons = branch.ActionButtons;
            showBranch.Enble = branch.Enble;
            showBranch.SortID = branch.SortID;
            return showBranch;
        }
    }
}