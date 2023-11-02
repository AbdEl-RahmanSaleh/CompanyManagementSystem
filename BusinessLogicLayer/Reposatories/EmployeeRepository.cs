using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Reposatories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;

        public EmployeeRepository(ApplicationDBContext context) : base(context) 
        {
            _context = context;
        }


        //public IEnumerable<Employee> GetEmployeesByDepartmentId(int id)
        //    => _context.Employees.Where( emp => emp.DepartmentId == id).ToList();
        

        public IEnumerable<Employee> Search(string name)
          =>  _context.Employees.Where(emp => emp.Name.Trim().ToLower().Contains(name.Trim().ToLower()));

        public IEnumerable<Employee> SearchByDepartment(int? id)
             => _context.Employees.Where(emp => emp.DepartmentId == id);


        //public int Add(Employee employee)
        //{
        //    _context.Add(employee);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAllEmployees()
        //         => _context.Employees.ToList();

        //public Employee GetEmployeeById(int? id)
        //       => _context.Employees.FirstOrDefault(d => d.Id == id);

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
