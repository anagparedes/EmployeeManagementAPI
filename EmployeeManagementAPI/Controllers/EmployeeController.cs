using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Automappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmployeeController([FromKeyedServices("employeeService")] IEmployeeService employeeService, DataContext context, IMapper mapper)
        {
            _employeeService = employeeService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetEmployeebyId(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Employee doesn't exists.");
            }

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpGet("search/{searchbyName}")]
        public async Task<ActionResult<List<Employee>>> Get(string name)
        {
            var employee = await _context.Employees
                .Where(e => e.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            if (!_employeeService.Validate(employee))
            {
                return BadRequest("Employee data is not correct.");
            }
            _context.Employees.Add(employee);
            _context.SaveChanges();

            var employees = await _context.Employees.ToListAsync();

            return Ok(employees.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee employee)
        {
            var dbEmployee = await _context.Employees.FindAsync(employee.Id);
            if (dbEmployee is null)
                return NotFound("Employee not found.");

            dbEmployee.Name = employee.Name;
            dbEmployee.Role = employee.Role;
            dbEmployee.Company = employee.Company;

            await _context.SaveChangesAsync();

            var employees = await _context.Employees.ToListAsync();
            return Ok(employees.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }

        [HttpDelete]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                return NotFound("Employee not found.");
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            var employees = await _context.Employees.ToListAsync();
            return Ok(employees.Select(employee => _mapper.Map<EmployeeDto>(employee)));
        }
    }

}
