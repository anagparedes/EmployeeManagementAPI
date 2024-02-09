using AutoMapper;
using EmployeeManagementAPI.Controllers;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using EmployeeManagementAPI.Services.EmployeeService;

namespace EmployeeManagementUnitTesting
{
    public class EmployeeTest
    {
        private readonly EmployeeController _employeeController;
        private readonly Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public EmployeeTest()
        {
            _employeeController = new EmployeeController(_employeeServiceMock.Object, null, _mapperMock.Object);
        }
        
        [Fact]
        public async Task GetAllEmployees_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Role = "Business Analyst", Company = "Banco Popular", UserName="jdoe", PasswordHash="sowir39jfjoij" },
                new Employee { Id = 2, Name = "Jane Cruz", Role = "Optical Consultant", Company = "Optica Baez", UserName="jcruz", PasswordHash="sfssfrewfvdfd" },
                new Employee { Id = 1, Name = "Ramon Ruiz", Role = "Assistant", Company = "CompanyTest", UserName="rruiz", PasswordHash="wfeere31313"},
            };
            _employeeServiceMock.Setup(service => service.GetAllEmployees()).ReturnsAsync(employees);

            var employeeDtos = employees.Select(e => new Employee { Name = e.Name });
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<Employee>>(employees)).Returns(employeeDtos);

            // Act
            var result = await _employeeController.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Equal(employees.Count, returnedEmployees.Count());

        }
       
        [Fact]
        public async Task GetEmployeeById_ExistingId_ReturnsOkResultWithEmployeeDto()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John" };
            _employeeServiceMock.Setup(service => service.GetEmployeebyId(It.IsAny<int>())).ReturnsAsync(employee);

            var employeeDto = new EmployeeDto { Name = employee.Name };
            _mapperMock.Setup(mapper => mapper.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _employeeController.GetEmployeebyId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<EmployeeDto>(okResult.Value);
            Assert.Equal(employeeDto.Name, returnedEmployee.Name);
        }

        [Fact]
        public async Task AddEmployee_ValidEmployee_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John" };
            _employeeServiceMock.Setup(service => service.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(new List<Employee> { employee });

            var employeeDto = new EmployeeDto { Name = employee.Name };
            _mapperMock.Setup(mapper => mapper.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _employeeController.AddEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Single(returnedEmployees); // Assuming only one employee is returned
        }
        [Fact]
        public async Task DeleteEmployee_ExistingId_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John" };
            _employeeServiceMock.Setup(service => service.DeleteEmployee(It.IsAny<int>())).ReturnsAsync(new List<Employee> { employee });

            var employeeDto = new EmployeeDto { Name = employee.Name };
            _mapperMock.Setup(mapper => mapper.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _employeeController.DeleteEmployee(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Single(returnedEmployees);
        }


    }
}