using System;
using System.Collections.Generic;
using System.Reflection;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class Branch : IEntity
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

        public string ActionButtons { get; set; }
    }
}