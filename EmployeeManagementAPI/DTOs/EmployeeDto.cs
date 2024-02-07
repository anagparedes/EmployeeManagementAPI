namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeDto
    {
        public required string Name { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
    }
}
