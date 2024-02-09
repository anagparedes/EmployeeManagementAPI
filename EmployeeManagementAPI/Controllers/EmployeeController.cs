using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        
        public EmployeeController(IEmployeeService employeeService,DataContext context, IMapper mapper)
        {
            _employeeService = employeeService;
            _context = context;
            _mapper = mapper;
      
        }
       
        [HttpGet()]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeebyId(int id)
        {
            var result = await _employeeService.GetEmployeebyId(id);
            if (result is null)
                return NotFound("Employee doesn't exists.");

            return Ok(_mapper.Map<EmployeeDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(int id, Employee request)
        {
            var result = await _employeeService.UpdateEmployee(id, request);
            if (result is null)
                return NotFound("Employee not found.");

            return Ok(result.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (result is null)
                return NotFound("Employee not found.");

            return Ok(result.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }
    }

}
