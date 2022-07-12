using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moamen.Training.APIs.Models;
using Moamen.Training.APIs.Services;

namespace Moamen.Training.APIs.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        public EmployeesController(IEmployeeService employeeService,
            IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = employeeService.GetAll();
            return Ok(mapper.Map<IEnumerable<EmployeeGet>>(employees));
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = employeeService.GetById(id);
            if (employee == null)
                return NotFound();

            return Ok(mapper.Map<EmployeeGet>(employee));
        }


    }
}
