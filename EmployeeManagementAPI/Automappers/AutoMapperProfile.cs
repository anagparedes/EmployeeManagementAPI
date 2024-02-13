using AutoMapper;
using EmployeeManagementAPI.DTOs;

namespace EmployeeManagementAPI.Automappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
