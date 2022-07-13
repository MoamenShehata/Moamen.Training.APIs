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

        [HttpGet("{id}", Name = nameof(GetEmployee))]
        public IActionResult GetEmployee(int id)
        {
            var employee = employeeService.GetById(id);
            if (employee == null)
                return NotFound();

            return Ok(mapper.Map<EmployeeGet>(employee));
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateEmployee(int id, [FromBody] EmployeePut employeePut)
        //{
        //    if (employeePut == null)
        //        return BadRequest();

        //    var employee = employeeService.GetById(id);
        //    if (employee == null)
        //        return NotFound();

        //    mapper.Map(employeePut, employee);

        //    var isUpdated = employeeService.Update(employee);
        //    if (!isUpdated)
        //        throw new Exception("Failed to update Employee instance");

        //    return NoContent();
        //}


        //Upserting
        //Which means
        //1-if the resource with this id does not exist WE CREATE it
        //2-if the resource with this id does     exist WE Update it
        //So Insert + Update 
        //So UPSERT
        //Notice we wont be able to test the insert case for SQL Server cause it does not allow adding value to identity columns
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeePut employeePut)
        {
            if (employeePut == null)
                return BadRequest();

            var employee = employeeService.GetById(id);
            if (employee == null)
            {
                var employeeToCreate = mapper.Map<Employee>(employeePut);
                employeeToCreate.Id = id;
                var isCreated = employeeService.Create(employeeToCreate);
                if (!isCreated)
                    throw new Exception("Creation failed!");

                var createdDto = mapper.Map<EmployeeGet>(employeeToCreate);
                return CreatedAtRoute(nameof(GetEmployee), new { id }, createdDto);
            }
            else
            {
                mapper.Map(employeePut, employee);

                var isUpdated = employeeService.Update(employee);
                if (!isUpdated)
                    throw new Exception("Failed to update Employee instance");

                return NoContent();
            }
        }
    }
}
