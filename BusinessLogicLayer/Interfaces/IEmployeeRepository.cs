using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> Search(string name); 
        IEnumerable<Employee> SearchByDepartment(int? id);

        //Employee GetEmployeeById(int? id);
        //IEnumerable<Employee> GetAllEmployees();
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

    }
}
