using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moamen.Training.APIs.Models;
using Moamen.Training.APIs.Services;

namespace Moamen.Training.APIs.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        public DepartmentsController(
            IDepartmentService departmentService,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = departmentService.GetAll();
            return Ok(mapper.Map<IEnumerable<DepartmentGet>>(departments));
        }

        [HttpGet("{id}", Name = nameof(GetDepartment))]
        public IActionResult GetDepartment(int id)
        {
            var department = departmentService.GetById(id);
            if (department == null)
                return NotFound();

            return Ok(mapper.Map<DepartmentGet>(department));
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentPost departmentPost)
        {
            if (departmentPost == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityResult(ModelState);

            var department = mapper.Map<Department>(departmentPost);

            var isCreated = departmentService.Create(department);
            if (!isCreated)
                throw new Exception("Failed to create Department instance");

            var departmentGet = mapper.Map<DepartmentGet>(department);
            return CreatedAtRoute(nameof(GetDepartment), new { id = department.Id },
                departmentGet);
        }


        [HttpGet("{departmentId}/employees")]
        public IActionResult GetDepartmentEmployees(int departmentId)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
                return NotFound();

            var employees = employeeService.GetAllByDepartment(departmentId);
            return Ok(mapper.Map<IEnumerable<EmployeeGet>>(employees));
        }

        [HttpPost("{departmentId}/employees")]
        public IActionResult CreateDepartmentEmployee(int departmentId, [FromBody] EmployeePost employeePost)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
                return NotFound();

            if (employeePost == null)
                return BadRequest();


            var employee = mapper.Map<Employee>(employeePost);
            employee.DepartmentId = departmentId;

            var isCreated = employeeService.Create(employee);
            if (!isCreated)
                throw new Exception("Failed to create Employee instance");

            var employeeGet = mapper.Map<EmployeeGet>(employee);
            return CreatedAtRoute(nameof(GetDepartmentEmployee),
                new { departmentId = departmentId, employeeId = employee.Id },
                employeeGet);
        }

        [HttpGet("{departmentId}/employees/{employeeId}", Name = nameof(GetDepartmentEmployee))]
        public IActionResult GetDepartmentEmployee(int departmentId, int employeeId)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
                return NotFound();

            var employee = employeeService.GetByIdAndDepartmentId(employeeId, departmentId);
            if (employee == null)
                return NotFound();

            return Ok(mapper.Map<EmployeeGet>(employee));
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            var department = departmentService.GetById(departmentId);
            if (department == null)
                return NotFound();

            var isDeleted = departmentService.Delete(department);
            if (!isDeleted)
                throw new Exception("Deletion was no possible");

            return NoContent();
        }

    }
}
