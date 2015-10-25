using System;
using System.Security.Cryptography;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class TrackTarget : IEntity
    {
        public Guid ID
        {
            get; set;
        }

        public string NiceName
        {
            get; set;
        }

        public Guid PlanId
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }
    }
}