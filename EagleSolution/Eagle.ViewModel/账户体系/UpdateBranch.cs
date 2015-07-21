using System;

namespace Eagle.ViewModel.账户体系
{
    public class UpdateBranch
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public bool Enble { get; set; }

        public int Level { get; set; }

        public Guid PreBranch { get; set; }

        public int SortID { get; set; }

        public string Logo { get; set; }

        public string AreaName { get; set; }

        public string ActionName { get; set; }

        public string Description { get; set; }
    }
}