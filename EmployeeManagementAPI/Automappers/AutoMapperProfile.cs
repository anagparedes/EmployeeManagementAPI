using AutoMapper;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Automappers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<UserDto, Employee>();
            CreateMap<Employee, UserDto>();
        
        }
    }
}
