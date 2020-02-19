using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFExpress.Models;

namespace SFExpress.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDataAccessLayer _dataAccessLayer;

        public EmployeeController()
        {
            _dataAccessLayer = new EmployeeDataAccessLayer();
        }

        public ActionResult List()
        {
            List<Employee> employees = _dataAccessLayer.GetAllEmployee();
            foreach (var emp in employees)
            {
                List<Task> tasks = _dataAccessLayer.GetAllTasks(emp.EmployeeId);
                emp.Task = tasks;
            }

            return View(employees);
        }
    }
}
