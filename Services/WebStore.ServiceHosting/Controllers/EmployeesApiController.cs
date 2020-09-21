using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")] // http://localhost:5001/api/EmployeesApi
    [Route("api/employees")]      // http://localhost:5001/api/employees - наш выбор
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;

        [HttpGet]        // GET http://localhost:5001/api/employees
        //[HttpGet("all")] // GET http://localhost:5001/api/employees/all
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")]        // GET http://localhost:5001/api/employees/5
        public Employee GetById(int id) => _EmployeesData.GetById(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            var id = _EmployeesData.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut]
        public void Edit(Employee employee)
        {
            _EmployeesData.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")] // DELETE http://localhost:5001/api/employees/5
        //[HttpDelete("delete/{id}")]    // DELETE http://localhost:5001/api/employees/delete/5
        //[HttpDelete("delete({id})")]    // DELETE http://localhost:5001/api/employees/delete(5)
        public bool Delete(int id)
        {
            var result = _EmployeesData.Delete(id);
            SaveChanges();
            return result;
        }

        // Будет ошибка при автоматизированной генерации документации по WebAPI
        //[NonAction]
        public void SaveChanges() => _EmployeesData.SaveChanges();
    }
}
