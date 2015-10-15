using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class MonitorConnectStepMap : EntityTypeConfiguration<MonitorConnectStep>
    {
        public MonitorConnectStepMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}