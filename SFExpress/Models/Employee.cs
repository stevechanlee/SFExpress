using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFExpress.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime HiredDate { get; set; }
        public List<Task> Task { get; set; }
    }
}
