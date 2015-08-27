using System.Data.Entity;
using Eagle.Domain.EF.DataContext;
using Eagle.Model;

namespace Eagle.Domain.EF
{
    public class MonitorContext : DefaultContext
    {
        public DbSet<Heartbeat> Heartbeats { get; set; }

        public DbSet<Tree> Trees { get; set; }
    }
}