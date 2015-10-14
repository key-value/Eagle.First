using System.Data.Entity;
using Eagle.Model;

namespace Eagle.Domain.EF.DataContext
{
    public partial class DefaultContext
    {
        public DbSet<WeekSummary> WeekSummaries
        {
            get; set;
        }

        public DbSet<WeekTarget> WeekTargets
        {
            get; set;
        }

        public DbSet<WeekComment> WeekComments
        {
            get; set;
        }

        public DbSet<WorkComment> WorkComments
        {
            get; set;
        }

        public DbSet<WorkRecord> WorkRecords
        {
            get; set;
        }
    }
}