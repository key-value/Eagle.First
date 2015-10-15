using System;
using System.Collections.Generic;
using System.Linq;
using Eagle.Domain.EF.DataContext;
using Eagle.Infrastructrue.Aop.Attribute;
using Eagle.Infrastructrue.Utility;
using Eagle.Model;
using Eagle.Server.Interface;
using Eagle.ViewModel;

namespace Eagle.Server.Services
{
    [Injection(typeof(IDepartmentServices))]
    public class DepartmentServices : ApplicationServices, IDepartmentServices
    {
        public List<ShowDepartment> Get(int pageNum)
        {
            var showDepartments = new List<ShowDepartment>();
            using (var accountContext = new DefaultContext())
            {
                var departments = accountContext.Departments.OrderByDescending(x => x.CreateTime)
                     .Pageing(pageNum, PageSize, ref _pageCount).ToList();

                showDepartments.AddRange(departments.Select(ShowDepartment.CreateShowDepartment));

            }
            Flag = true;
            return showDepartments;
        }
        public List<ShowDepartment> Get()
        {
            var showDepartments = new List<ShowDepartment>();
            using (var accountContext = new DefaultContext())
            {
                var departments = accountContext.Departments.OrderByDescending(x => x.CreateTime).ToList();
                showDepartments.AddRange(departments.Select(ShowDepartment.CreateShowDepartment));
            }
            Flag = true;
            return showDepartments;
        }

        public ShowDepartment Get(Guid departmentId)
        {
            using (var accountContext = new DefaultContext())
            {
                var department = accountContext.Departments.SingleOrDefault(x => x.ID == departmentId);
                if (department == null)
                {
                    return null;
                }
                var showDepartments = ShowDepartment.CreateShowDepartment(department);

                Flag = true;
                return showDepartments;
            }
        }

        public void Update(UpdateDepartment updateDepartment)
        {
            if (updateDepartment.ID == Guid.Empty)
            {
                Add(updateDepartment);
            }
            else
            {
                Edit(updateDepartment);
            }
        }

        private void Edit(UpdateDepartment updateDepartment)
        {
            using (var accountContext = new DefaultContext())
            {
                var department = accountContext.Departments.SingleOrDefault(x => x.ID == updateDepartment.ID);
                if (department.Null())
                {
                    return;
                }
                department = updateDepartment.UpdateShowDepartment(department);
                accountContext.ModifiedModel(department);
                var workCard =
                    accountContext.WorkCards.FirstOrDefault(x => x.Owner && x.DepartmentId == updateDepartment.ID);

                if (!workCard.Null())
                {
                    if (workCard.AccountId != updateDepartment.ID)
                    {
                        workCard.Owner = false;
                        accountContext.ModifiedModel(workCard);
                    }
                }
                var newWorkCard =
                        accountContext.WorkCards.FirstOrDefault(x => x.AccountId == updateDepartment.OwnerId && x.DepartmentId == updateDepartment.ID);
                if (!newWorkCard.Null())
                {
                    newWorkCard.Owner = true;
                    accountContext.ModifiedModel(newWorkCard);
                }



                accountContext.SaveChanges();
                Flag = true;
            }
        }

        private void Add(UpdateDepartment updateDepartment)
        {
            using (var accountContext = new DefaultContext())
            {
                var department = accountContext.Departments.SingleOrDefault(x => x.Name == updateDepartment.Name);
                if (!department.Null())
                {
                    Message = "创建部门已经存在";
                    return;
                }
                department = updateDepartment.CreateShowDepartment();
                accountContext.Departments.Add(department);
                accountContext.SaveChanges();
                Flag = true;
            }
        }

        public void Delete(List<Guid> departmentsList)
        {
            using (var accountContext = new DefaultContext())
            {
                var departmenst = accountContext.Departments.Where(x => departmentsList.Contains(x.ID));
                if (!departmenst.Any())
                {
                    Message = "创建部门已经存在";
                    return;
                }
                accountContext.Departments.RemoveRange(departmenst);
                accountContext.SaveChanges();
                Flag = true;
            }
        }

        public List<Guid> GetWorkCard(Guid departmentsId)
        {
            using (var accountContext = new DefaultContext())
            {
                var departmenst = accountContext.WorkCards.Where(x => x.DepartmentId == departmentsId).ToList();
                if (!departmenst.Any())
                {
                    return new List<Guid>();
                }
                var cardIdList = departmenst.Select(x => x.AccountId).ToList();

                Flag = true;
                return cardIdList;
            }
        }

        public Guid GetOwner(Guid departmentsId)
        {
            using (var accountContext = new DefaultContext())
            {
                var departmensts = accountContext.WorkCards.Where(x => x.DepartmentId == departmentsId && x.Owner).ToList();
                if (!departmensts.Any())
                {
                    return Guid.Empty;
                }
                if (departmensts.Count() != 1)
                {
                    return Guid.Empty;
                }
                var departmenst = departmensts.FirstOrDefault();
                Flag = true;
                return departmenst.AccountId;

            }
        }
    }
}