using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFExpress.Models;

namespace SFExpress.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //public IActionResult Index()
        //{
        //    List<Employee> lstEmployee = new List<Employee>();
        //    lstEmployee = objemployee.GetAllEmployee().ToList();

        //    return View(lstEmployee);
        //}
    }
}
