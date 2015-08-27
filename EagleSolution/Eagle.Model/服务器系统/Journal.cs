using System;
using System.Threading;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class Journal : IEntity
    {
        public Guid ID { get; set; }

        public string ProjectName { get; set; }

        public string Path { get; set; }

        public DateTime CreateTime { get; set; }

    }
}