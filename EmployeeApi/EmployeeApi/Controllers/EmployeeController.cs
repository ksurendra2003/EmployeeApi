using EmployeeApi.Models;
using EmployeeApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    public class EmployeeController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("GetAllEmployees")]
        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            var employeeList = _employeeRepository.GetAllEmployees();

            return employeeList;
        }

        [Route("AddEmployee")]
        [HttpPost]
        public bool AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
            return true;
        }

        [Route("hc")]
        public string HC()
        {
            return "Sucess";
        }
    }
}
