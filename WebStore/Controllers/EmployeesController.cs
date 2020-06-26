using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> __Employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Age = 39
            },
            new Employee
            {
                Id = 2,
                Surname = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Age = 27
            },
            new Employee
            {
                Id = 3,
                Surname = "Сидоров",
                Name = "Сидор",
                Patronymic = "Сидорович",
                Age = 18
            },
        };

        public IActionResult Index() => View(__Employees);

        public IActionResult Details(int id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}
