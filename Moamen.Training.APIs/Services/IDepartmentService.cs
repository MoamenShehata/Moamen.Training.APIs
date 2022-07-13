using Moamen.Training.APIs.Data;
using Moamen.Training.APIs.Models;

namespace Moamen.Training.APIs.Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        bool Create(Department department);
        bool Delete(Department department);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext dataContext;
        public DepartmentService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool Create(Department department)
        {
            dataContext.Departments.Add(department);
            dataContext.SaveChanges();
            return department.Id != 0;
        }

        public bool Delete(Department department)
        {
            dataContext.Departments.Remove(department);
            var affectedRows = dataContext.SaveChanges();
            return affectedRows == 1;
        }
        public IEnumerable<Department> GetAll() => dataContext.Departments.ToList();
        public Department GetById(int id) => dataContext.Departments.Find(id);
    }
}
