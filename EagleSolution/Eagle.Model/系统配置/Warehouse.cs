using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class Warehouse : IAggregateRoot
    {
        public Guid ID { get; set; }

        public SystemType KeyName { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public DateTime LastUpdateTime { get; set; }
    }

    public enum SystemType
    {
        LastUpdateRest = 1,
    }
}