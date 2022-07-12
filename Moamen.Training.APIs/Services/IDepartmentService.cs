using Moamen.Training.APIs.Data;
using Moamen.Training.APIs.Models;

namespace Moamen.Training.APIs.Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext dataContext;
        public DepartmentService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Department> GetAll() => dataContext.Departments.ToList();
        public Department GetById(int id) => dataContext.Departments.Find(id);
    }
}
