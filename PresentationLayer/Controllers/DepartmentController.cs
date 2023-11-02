using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin , HR")]

    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        //private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepository,
                                     * IEmployeeRepository employeeRepository , */
                                     ILogger<DepartmentController> logger,
                                     IUnitOfWork unitOfWork,IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            //_employeeRepository = employeeRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Department> departments;
            IEnumerable<DepartmentViewModel> departmentVM;
            departments = _unitOfWork.DepartmentRepository.GetAll();
            departmentVM = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments); 
            //ViewData["Message"] = "Hello from view Data";
            //ViewBag.Hello = "Hello from view bag";

            //TempData.Keep("Message");
           
            return View(departmentVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM) 
        {
            if (ModelState.IsValid) 
            {
                var department = _mapper.Map<Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(department);

                //TempData["Message"] = "Department Created Successfully";
                return RedirectToAction("Index");
            }
            return View(departmentVM);
        }   

        public  IActionResult Details(int? id)
        {
            try
            {
                if (id is null)
                    //return RedirectToAction("Error","Home");
                    return NotFound();

                var department = _unitOfWork.DepartmentRepository.GetById(id);

                if (department is null)
                    //return RedirectToAction("Error","Home");
                    return NotFound();

                var departmentVM = _mapper.Map<DepartmentViewModel>(department);
                return View(departmentVM);
            }
            catch (Exception ex)
            {   
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home"); 
            }


        }

        public IActionResult Update(int? id) 
        {
            if (id is null)
                return RedirectToAction("Error", "Home");

            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department is null)
                return RedirectToAction("Error", "Home");

            var departmentVM = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentVM);
        }
        [HttpPost]
        public IActionResult Update(int id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(departmentVM);
                    _unitOfWork.DepartmentRepository.Update(department);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View(departmentVM); 
        }

        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department is null)
                return RedirectToAction("Error", "Home");

            _unitOfWork.DepartmentRepository.Delete(department);
            
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);
            return RedirectToAction("Index");
        }
    }
}
