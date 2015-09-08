using System;
using Eagle.Domain.Core.Model;

namespace Eagle.Model
{
    public class SystemCard : IAggregateRoot
    {
        public int ID { get; set; }

        public string CardName { get; set; }

        public string Action { get; set; }

        public Guid BranchId { get; set; }

    }
}