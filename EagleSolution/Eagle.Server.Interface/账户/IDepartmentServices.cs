using System;
using System.Collections.Generic;
using Eagle.ViewModel;

namespace Eagle.Server.Interface
{
    public interface IDepartmentServices : IAppServices
    {
        List<ShowDepartment> Get(int pageNum);
        ShowDepartment Get(Guid departmentId);
        void Update(UpdateDepartment updateDepartment);
        void Delete(List<Guid> departmentsList);
        List<Guid> GetWorkCard(Guid departmentsId);
        List<ShowDepartment> Get();
        Guid GetOwner(Guid departmentsId);
    }
}