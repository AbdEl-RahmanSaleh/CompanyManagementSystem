using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PresentationLayer.Helper;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchValue = "",int? deptId = null)
        {
            //var Employees = _unitOfWork.EmployeeRepository.GetAll();
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll().OrderBy(d => d.Name);
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> mappedEmployees;
            if (string.IsNullOrEmpty(SearchValue) && deptId == null)
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
                mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else if(deptId == null)
            {
                employees = _unitOfWork.EmployeeRepository.Search(SearchValue);
                mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else if (string.IsNullOrEmpty(SearchValue))
            {
                employees = _unitOfWork.EmployeeRepository.SearchByDepartment(deptId);
                mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.Search(SearchValue);
                employees = employees.Where(emp =>emp.DepartmentId == deptId);
                mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }

            foreach (var emp in mappedEmployees)
            {
                emp.Department = _unitOfWork.DepartmentRepository.GetById(emp.DepartmentId);
            }

            return View(mappedEmployees);
        }

        public IActionResult Create() 
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM) 
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

            //employee.Department = _unitOfWork.DepartmentRepository.GetById(employee.DepartmentId);
             if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<Employee>(employeeVM);
                    employee.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "Images"); 

                    _unitOfWork.EmployeeRepository.Add(employee);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message) ;
                }
            }

            return View(employeeVM);
        }

        public IActionResult Update(int? id)
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

            if (employee is null)
                return RedirectToAction("Error", "Home");

            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeVM);
        }
        [HttpPost]
        public IActionResult Update(int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(employeeVM);
                    employee.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    _unitOfWork.EmployeeRepository.Update(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                    return NotFound();

                var employee = _unitOfWork.EmployeeRepository.GetById(id);
                ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

                if (employee is null)
                    return NotFound();

                var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

                return View(employeeVM);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var employee = _unitOfWork.EmployeeRepository.GetById(id);
         
            if (employee is null)
                return RedirectToAction("Error", "Home");

            _unitOfWork.EmployeeRepository.Delete(employee);

            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return RedirectToAction("Index");
        }
    }
}
