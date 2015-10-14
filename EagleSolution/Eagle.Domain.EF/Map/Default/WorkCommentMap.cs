using System.Data.Entity.ModelConfiguration;
using Eagle.Model;

namespace Eagle.Domain.EF.Map.Default
{
    public class WorkCommentMap : EntityTypeConfiguration<WorkComment>
    {
        public WorkCommentMap()
        {
            this.HasKey(x => x.ID);
        }
    }
}