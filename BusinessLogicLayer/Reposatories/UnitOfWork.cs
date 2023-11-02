using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Reposatories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository)
        {
            EmployeeRepository = employeeRepository ;
            DepartmentRepository = departmentRepository ;
        }
    }
}
