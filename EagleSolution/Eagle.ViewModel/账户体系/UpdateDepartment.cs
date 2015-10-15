using System;
using Eagle.Model;

namespace Eagle.ViewModel
{
    public class UpdateDepartment
    {

        public Guid ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Guid OwnerId
        {
            get; set;
        }

        public Department UpdateShowDepartment(Department department)
        {
            department.Name = Name;
            return department;
        }

        public Department CreateShowDepartment()
        {
            Department department = new Department();
            department.ID = Guid.NewGuid();
            department.Name = Name;
            department.CreateTime = DateTime.Now;
            return department;
        }
    }
}