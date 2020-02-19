using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFExpress.Models.Requests
{
    public class TaskUpdateRequest : TaskAddRequest
    {
        public int Id { get; set; }
    }
}
