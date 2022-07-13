using AutoMapper;
using Moamen.Training.APIs.Models;

namespace Moamen.Training.APIs.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeGet>()
                .ForMember(eg => eg.Department, op => op.MapFrom(e => e.Department.Name))
                ;

            CreateMap<EmployeePost, Employee>()
                ;
        }
    }

    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentGet>()
                ;

            CreateMap<DepartmentPost, Department>()
                ;
        }
    }


}
