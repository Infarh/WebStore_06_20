using System;

namespace WebStore.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }

        public DateTime EmployementDate { get; set; }
    }
}
