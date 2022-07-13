using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moamen.Training.APIs.Models;
using Moamen.Training.APIs.Services;

namespace Moamen.Training.APIs.Controllers
{
    [ApiController]
    [Route("api/departments/{departmentId}/employeeCollection")]
    public class DepartmentEmployeeCollectionController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        public DepartmentEmployeeCollectionController(
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }


        [HttpPost()]
        public IActionResult AddEmployeesToDepartment(int departmentId,
            [FromBody] IEnumerable<EmployeePost> employeesPost)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
                return NotFound();

            if (employeesPost == null)
                return BadRequest();


            var employees = mapper.Map<IEnumerable<Employee>>(employeesPost);
            foreach (var employee in employees)
                employee.DepartmentId = departmentId;

            var isCreated = employeeService.CreateRange(employees);
            if (!isCreated)
                throw new Exception("Failed to create employees for this department");

            var employeesGet = mapper.Map<IEnumerable<EmployeeGet>>(employees);

            return Ok(employeesGet);
        }

        //We need to add model binder for IEnumerable<int>
        [HttpDelete("({employeesIds})")]
        public IActionResult DeleteEmployeesForDepartment(int departmentId,
            IEnumerable<int> employeesIds)
        {
            throw new NotImplementedException();
        }
    }
}
