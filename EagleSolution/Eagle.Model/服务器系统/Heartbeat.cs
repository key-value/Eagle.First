using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class Heartbeat : IEntity
    {
        public Guid ID { get; set; }

        public Guid MachineId { get; set; }

        public double MaxNum { get; set; }

        public double AvgNum { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LogTime { get; set; }

        public int HourTime { get; set; }

    }
}