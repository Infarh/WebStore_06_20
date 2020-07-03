using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("All")]
        public IActionResult Index() => View(_EmployeesData.Get().Select(employee => new EmployeesViewModel
        {
            Id = employee.Id,
            FirstName = employee.Name,
            LastName = employee.Surname,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
            EmployementDate = employee.EmployementDate
        }));

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                EmployementDate = employee.EmployementDate
            });
        }

        #region Edit

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
                Age = employee.Age,
                EmployementDate = employee.EmployementDate
            });
        }

        [HttpPost]
        public IActionResult Edit(EmployeesViewModel Model)
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model));

            if(Model.Age < 18 || Model.Age > 75)
                ModelState.AddModelError(nameof(Employee.Age), "Возраст должен быть всё же в пределах от 18 до 75");

            if(Model.FirstName == "123" && Model.LastName == "QWE")
                ModelState.AddModelError(string.Empty, "Странный выбор для имени и фамилии");

            if (!ModelState.IsValid)
                return View(Model);

            var employee = new Employee
            {
                Id = Model.Id,
                Surname = Model.LastName,
                Name = Model.FirstName,
                Patronymic = Model.Patronymic,
                Age = Model.Age,
                EmployementDate = Model.EmployementDate
            };

            if (Model.Id == 0)
                _EmployeesData.Add(employee);
            else
                _EmployeesData.Edit(employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeesViewModel
            {
                Id = employee.Id,
                FirstName = employee.Name,
                LastName = employee.Surname,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                EmployementDate = employee.EmployementDate
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        } 

        #endregion
    }
}
