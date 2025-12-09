using Assignment.DTOs;
using Assignment_BussinesLogicLayer.Interfaces;
using Assignment_BussinesLogicLayer.Reposatories;
using Assignment_DataAccesslayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentReposatory _departmentReposatory;  //null

        // ask CLR to create oject from DepartmentReposatory
        public DepartmentController(IDepartmentReposatory departmentReposatory)
        {
            _departmentReposatory = departmentReposatory;
        }
        [HttpGet]
        public async  Task<IActionResult> Index(String? SearchInput, int page = 1)
        {
            int pageSize = 5;
            IEnumerable<Department> departments;
            if (String.IsNullOrEmpty(SearchInput))
            {
               departments = await _departmentReposatory.GetAllAsync();
            }
            else
            {
                 departments = await _departmentReposatory.GetByNameAsync(SearchInput);
            }
           
            ViewBag.Count = departments.Count();
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)ViewBag.Count / pageSize);
            ViewBag.SearchInput = SearchInput;

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()     // open view html form 

        {
            return View();        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  // prevent any request out of application

        // submit form  
        public IActionResult Create(CreateDepartmentDto model)   //CreateDepartmentDto=> separate ojects from front even obj in Db
        {
            if (ModelState.IsValid)   // server side validation   (check data coming from isss valid throw validations in dtos(name,code,createAt )is req
            {
                //casting data from department dto to data from type department throw manual mapping 
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
              var count=  _departmentReposatory.Add(department);
                if (count > 0)
                {
                    TempData["message"] = "Depatment Is Created Sucssesfully :) ";

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        
        public async Task< IActionResult> RefactorActionFunction (int? id ,String ViewName )
        {
            if (id is null) { return BadRequest("Id Is Invalid! "); }
            var departments = await _departmentReposatory.GetAsync(id.Value);  // (value) because nullability progrition  
            if (departments == null) { return NotFound($"Department With Id {id} Is Not Found ! "); }
            else   return View(ViewName, departments);
        }

        [HttpGet]
        //get (open) form 
        public async Task<IActionResult> Details (int? id)
        {
            //if (id is null) { return BadRequest("Id Is Invalid! "); }
            //var departments = _departmentReposatory.Get(id.Value);  // (value) because nullability progrition  
            //if (departments == null) { return NotFound($"Department With Id {id} Is Not Found ! "); }
            return await RefactorActionFunction(id, "Details");
        }


            [HttpGet]
        //get (open) form 
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) { return BadRequest("Id Is Invalid! "); }
            //var departments = _departmentReposatory.Get(id.Value);  // (value) because nullability progrition  
            //if (departments == null) { return NotFound($"Department With Id {id} Is Not Found ! "); }
             return await RefactorActionFunction(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  // prevent any request out of application
        // submit form  
        public IActionResult Edit([FromRoute]int id,Department department)  //FromRoute => force app to get id val from route(URL)only
        {

            ModelState.Remove("Employees");  // because form not sent employees or use DTO doesnt have Employees
            if (ModelState.IsValid)
            {
                if (department.Id == id) {
                    var count = _departmentReposatory.Update(department);
                    if (count > 0) {
                        TempData["message"] = "Depatment Is Updated Sucssesfully :) ";

                        return RedirectToAction(nameof(Index)); 
                    }
                }
            }
             return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) { return BadRequest("Id Is Invalid! "); }
            //var departments = _departmentReposatory.Get(id.Value);  // (value) because nullability progrition  
            //if (departments == null) { return NotFound($"Department With Id {id} Is Not Found ! "); }
             return await RefactorActionFunction(id,"Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)  //FromRoute => force app to get id val from route(URL)only
        {
            ModelState.Remove("Employees"); // because form not sent employees  or use DTO doesnt have Employees
            if (ModelState.IsValid)
            {
                if (department.Id == id)
                {
                    var count = _departmentReposatory.Delete(department);
                    if (count > 0) {
                        TempData["message"] = "Depatment Is Deleted Sucssesfully :) ";

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(department);
        }
    }
}
