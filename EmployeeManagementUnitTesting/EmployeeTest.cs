using AutoMapper;
using EmployeeManagementAPI.Controllers;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
                new Employee { Id = 1, Name = "John Doe", Role = "Business Analyst", Company = "Banco Popular" },
                new Employee { Id = 2, Name = "Jane Cruz", Role = "Optical Consultant", Company = "Optica Baez"},
                new Employee { Id = 1, Name = "Ramon Ruiz", Role = "Assistant", Company = "CompanyTest"},
            };

            _employeeServiceMock.Setup(service => service.GetAllEmployees()).ReturnsAsync(employees);

            var employeeDtos = employees.Select(e => new EmployeeDto { Name = e.Name });

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
            var employee = new Employee { Id = 55, Name = "Laura Castillo" };
            _employeeServiceMock.Setup(service => service.GetEmployeebyId(It.IsAny<int>())).ReturnsAsync(employee);

            var employeeDto = new EmployeeDto { Name = employee.Name };
            _mapperMock.Setup(mapper => mapper.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _employeeController.GetEmployeebyId(55);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<EmployeeDto>(okResult.Value);
            Assert.Equal(employeeDto.Name, returnedEmployee.Name);
        }

        [Fact]
        public async Task GetEmployeeById_NonExistingId_ReturnsNotFound()
        {
            // Act
            var result = await _employeeController.GetEmployeebyId(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }


        [Fact]
        public async Task AddEmployee_ValidEmployee_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "José Romero" };
            _employeeServiceMock.Setup(service => service.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(new List<Employee> { employee });

            var employeeDto = new EmployeeDto { Name = employee.Name };

            // Act
            var result = await _employeeController.AddEmployee(employee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Single(returnedEmployees);
        }

        [Fact]
        public async Task UpdateEmployee_ExistingId_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var existingEmployee = new Employee { Id = 1, Name = "María Chavez" };
            _employeeServiceMock.Setup(service => service.UpdateEmployee(It.IsAny<int>(), It.IsAny<Employee>())).ReturnsAsync(new List<Employee> { existingEmployee });

            var updatedEmployee = new Employee { Id = 1, Name = "Updated María", Role = "Cleaner" };

            var employeeDto = new EmployeeDto { Name = updatedEmployee.Name, Role = updatedEmployee.Role };

            // Act
            var result = await _employeeController.UpdateEmployee(1, updatedEmployee);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);

            Assert.NotNull(employeeDto);
            Assert.Equal(updatedEmployee.Name, employeeDto.Name);

        }


        [Fact]
        public async Task UpdateEmployee_NonExistingId_ReturnsNotFound()
        {
            // Act
            var result = await _employeeController.UpdateEmployee(1, new Employee());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingId_ReturnsOkResultWithEmployeeDtos()
        {
            // Arrange
            var employee = new Employee { Id = 32, Name = "Juan Mendoza" };
            _employeeServiceMock.Setup(service => service.DeleteEmployeebyId(It.IsAny<int>())).ReturnsAsync(new List<Employee> { employee });

            // Act
            var result = await _employeeController.DeleteEmployee(32);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployees = Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(okResult.Value);
            Assert.Single(returnedEmployees);
        }

        [Fact]
        public async Task DeleteEmployee_NonExistingId_ReturnsNotFound()
        {   
            // Act
            var result = await _employeeController.DeleteEmployee(42);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}