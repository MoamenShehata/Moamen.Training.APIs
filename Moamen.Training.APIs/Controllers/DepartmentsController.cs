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

        [HttpGet("{id}")]
        public IActionResult GetDepartment(int id)
        {
            var department = departmentService.GetById(id);
            if (department == null)
                return NotFound();

            return Ok(mapper.Map<DepartmentGet>(department));
        }

        [HttpGet("{id}/employees")]
        public IActionResult GetDepartmentEmployees(int id)
        {
            var department = departmentService.GetById(id);
            if (department == null)
                return NotFound();

            var employees = employeeService.GetAllByDepartment(id);
            return Ok(mapper.Map<IEnumerable<EmployeeGet>>(employees));
        }

        [HttpGet("{id}/employees/{employeeId}")]
        public IActionResult GetDepartmentEmployee(int id, int employeeId)
        {
            var department = departmentService.GetById(id);
            if (department == null)
                return NotFound();

            var employee = employeeService.GetByIdAndDepartmentId(employeeId, id);
            if (employee == null)
                return NotFound();

            return Ok(mapper.Map<EmployeeGet>(employee));
        }


    }
}
