using Assignment.DTOs;
using Assignment_BussinesLogicLayer.Interfaces;
using Assignment_BussinesLogicLayer.Reposatories;
using Assignment_DataAccesslayer.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Assignment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeReposatory _employeeReposatory;  //null
        private readonly IDepartmentReposatory _departmentReposatory;

        public EmployeeController( IMapper mapper ,IEmployeeReposatory employeeReposatory,IDepartmentReposatory departmentReposatory)
        {
            _mapper = mapper;
            _employeeReposatory = employeeReposatory;
           _departmentReposatory = departmentReposatory;
        }
        [HttpGet]
        public async Task< IActionResult >Index( String? SearchInput , int page = 1)
        {
            int pageSize = 5;

            IEnumerable<Employee> employees;
            if(String.IsNullOrEmpty(SearchInput))
            {
                 employees = await _employeeReposatory.GetAllAsync();
            }
            else
            {
                 employees = await _employeeReposatory.GetByNameAsync(SearchInput);
            }
            // View Storage (memory)=> Dictionary Accessd by key&value 
            //viewdata,viewbag like [indexer]:tranfer extra info from controller to view 
            //tempdata :tranfer data from requst to request
         
            ViewBag.Count = employees.Count();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)ViewBag.Count / pageSize);
            ViewBag.SearchInput = SearchInput;



            return View(employees);
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            var department = await _departmentReposatory.GetAllAsync();
            //return View(department);      // not valid model has data from type CreateEmployeeDte

            ViewData["departments"] = department;
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]  // prevent any request out of application

        public  IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)             // server side validation   (check data coming from isss valid throw validations in dtos(name,code,createAt )is req

            {//casting data from department dto to data from type department throw manual mapping 
                //var emplyee = new Employee()
                //{
                //    Name = model.Name,
                //    Age=model.Age,
                //    Email = model.Email,
                //    Phone=model.Phone,
                //    Salary=model.Salary,
                //    IsActivated=model.IsActivated,
                //    IsDeleted=model.IsDeleted,
                //    HiringDate=model.HiringDate,
                //    DepartmentId=model.DepartmentId,

                //};
                 var employee=_mapper.Map<Employee>(model);
                var count =  _employeeReposatory.Add(employee);
                if (count > 0)
                {
                    TempData["message"] = "Employee Is Created Sucssesfully :) ";
                    return RedirectToAction("Index");
                }
             }
                return View(model);
        }
        [HttpGet]

        public async  Task<IActionResult> RefactorActionFunction(int? id, String ViewName)
        {
            if (id is null) { return BadRequest("Id Is Invalid! "); }
            var employees =  await _employeeReposatory.GetAsync(id.Value);  // (value) because nullability progrition  
            if (employees == null) { return NotFound($"Employee With Id {id} Is Not Found ! "); }
            else return View(ViewName, employees);
        }
        [HttpGet]

        public Task<IActionResult> Details(int? id )
        {
            return RefactorActionFunction(id, "Details");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            var department =  await _departmentReposatory.GetAllAsync();
            ViewData["departments"] = department;
            return await RefactorActionFunction(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]  // prevent any request out of application
        //submit form 
        public IActionResult Edit([FromRoute]int? id,Employee employee)  //FromRoute => force app to get id val from route(URL)only
        {
            if (ModelState.IsValid)
            {
                if (id==employee.Id)
                {
                   var count=  _employeeReposatory.Update(employee);
                    if (count>0)
                    {
                        TempData["message"] = "Employee Is Updated Sucssesfully :) ";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(employee);
        }

        [HttpGet]
        public async  Task<IActionResult> Delete(int?id)
        {
          return  await RefactorActionFunction(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  // prevent any request out of application
        //submit form 
        public IActionResult Delete([FromRoute]int? id,Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(id==employee.Id)
                {
                   var count= _employeeReposatory.Delete(employee);
                    if(count>0)
                    {
                        TempData["message"] = "Employee Is Deleted Sucssesfully :) ";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(employee);
        }
    }


}
