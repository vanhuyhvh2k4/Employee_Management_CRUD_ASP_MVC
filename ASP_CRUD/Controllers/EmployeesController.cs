using ASP_CRUD.Data;
using ASP_CRUD.Models;
using ASP_CRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoContext mvcDemoContext;

        //Constructor
        public EmployeesController(MVCDemoContext mvcDemoContext)
        {
            this.mvcDemoContext = mvcDemoContext;
        }

        [HttpGet]
        //Get all employees
        public IActionResult Index()
        {
            var employees = mvcDemoContext.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        //Display form to add new employee
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        //Add a new employee
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            //Get data from input
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };

            //Add a new employee to db
            await mvcDemoContext.Employees.AddAsync(employee);
            await mvcDemoContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        //Get an employee by Id
        public async Task<IActionResult> View(Guid id)
        {
            var employee = mvcDemoContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };

                return await Task.Run(() => View("View", viewModel));
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        // Update an employee
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await mvcDemoContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        //Delete an employee
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = mvcDemoContext.Employees.Find(model.Id);

            if (employee != null)
            {
                mvcDemoContext.Employees.Remove(employee);
                await mvcDemoContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
