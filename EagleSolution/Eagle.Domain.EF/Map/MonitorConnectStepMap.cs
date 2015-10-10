using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map
{
    public class MonitorConnectStepMap : EntityTypeConfiguration<MonitorConnectStep>
    {
        public MonitorConnectStepMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}