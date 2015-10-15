using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class ShowDepartment
    {

        public Guid ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public DateTime CreateTime
        {
            get; set;
        }

        public Guid OwnerId
        {
            get; set;
        }

        public static ShowDepartment CreateShowDepartment(Department departments)
        {
            var showDepartment = new ShowDepartment();
            showDepartment.ID = departments.ID;
            showDepartment.Name = departments.Name;
            showDepartment.CreateTime = departments.CreateTime;
            return showDepartment;
        }

    }
}
