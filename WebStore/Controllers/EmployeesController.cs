using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("All")]
        public IActionResult Index() => View(_EmployeesData.Get());

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new EmployeesViewModel());

            if (id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel Model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
