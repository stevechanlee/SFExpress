using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFExpress.Models
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }
    }
}
