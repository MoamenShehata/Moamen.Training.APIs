namespace Moamen.Training.APIs.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }

    public class EmployeeGet
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public string Department { get; set; }
    }

    public class DepartmentGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentPost
    {
        public string Name { get; set; }
        public ICollection<EmployeePost> Employees { get; set; } = new List<EmployeePost>();

    }

    public class EmployeePost
    {
        public int Salary { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
    }


}
