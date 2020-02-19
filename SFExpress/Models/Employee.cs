using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFExpress.Models
{
    public class Employee
    {
        public Employee()
        {

        }
        public Employee(string firstName, string lastName, DateTime hireDate, List<Task> tasks, int employeeId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.HiredDate = hireDate;
            this.Task = tasks;
            this.EmployeeId = employeeId;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<Task> Task { get; set; }
        public int EmployeeId { get; set; }
    }
}
