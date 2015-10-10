using System.Data.Entity;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF.DataContext
{
    public partial class DefaultContext
    {
        public DbSet<Heartbeat> Heartbeats { get; set; }

        public DbSet<Tree> Trees
        {
            get; set;
        }

        public DbSet<Journal> Journals { get; set; }
    }
}