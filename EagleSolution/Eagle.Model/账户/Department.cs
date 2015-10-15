using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class Department : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }


    }
}