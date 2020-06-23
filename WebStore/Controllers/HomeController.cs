using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
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

        public IActionResult Index() => View();

        public IActionResult Employees()
        {
            ViewBag.Title = "123";
            ViewData["TestValue"] = "Value - test";

            var employees = __Employees;
            return View(employees);
        }

        public IActionResult EmployeeInfo(int id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}
