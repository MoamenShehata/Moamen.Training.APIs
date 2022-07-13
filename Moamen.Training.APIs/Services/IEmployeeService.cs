using Microsoft.EntityFrameworkCore;
using Moamen.Training.APIs.Data;
using Moamen.Training.APIs.Models;

namespace Moamen.Training.APIs.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAllByDepartment(int departmentId);
        Employee GetById(int id);
        Employee GetByIdAndDepartmentId(int id, int departmentId);
        bool Create(Employee employee);
        bool CreateRange(IEnumerable<Employee> employees);
        bool Update(Employee employee);
    }


    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext dataContext;
        public EmployeeService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool Create(Employee employee)
        {
            dataContext.Employees.Add(employee);
            dataContext.SaveChanges();
            return employee.Id != 0;
        }

        public bool CreateRange(IEnumerable<Employee> employees)
        {
            dataContext.Employees.AddRange(employees);
            dataContext.SaveChanges();
            return employees.All(e => e.Id != 0);
        }
        public IEnumerable<Employee> GetAll() => dataContext.Employees.ToList();
        public IEnumerable<Employee> GetAllByDepartment(int departmentId) => dataContext.Employees.Where(e => e.DepartmentId == departmentId).ToList();
        public Employee GetById(int id) => dataContext.Employees.Include(e => e.Department).SingleOrDefault(e => e.Id == id);
        public Employee GetByIdAndDepartmentId(int id, int departmentId) => dataContext.Employees.Include(e => e.Department).SingleOrDefault(e => e.Id == id && e.DepartmentId == departmentId);
        public bool Update(Employee employee)
        {
            var entry1 = dataContext.Entry(employee);
            dataContext.Employees.Update(employee);
            var affectedRows = dataContext.SaveChanges();
            return affectedRows == 1;
        }
    }
}
