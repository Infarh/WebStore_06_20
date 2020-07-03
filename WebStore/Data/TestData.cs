using System;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Age = 39,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(300 * 7))
            },
            new Employee
            {
                Id = 2,
                Surname = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Age = 27,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(512 * 3))
            },
            new Employee
            {
                Id = 3,
                Surname = "Сидоров",
                Name = "Сидор",
                Patronymic = "Сидорович",
                Age = 18,
                EmployementDate = DateTime.Now.Subtract(TimeSpan.FromDays(200 * 1))
            },
        };
    }
}
