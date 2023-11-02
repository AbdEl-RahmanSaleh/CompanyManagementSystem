using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {

        //Department GetDepartmentById(int? id);
        //IEnumerable<Department> GetAllDepartments();
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);

    }
}
