using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        //[Route("All")]
        public IActionResult Index() => View(TestData.Employees);

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = TestData.Employees.FirstOrDefault(e => e.Id == id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}
