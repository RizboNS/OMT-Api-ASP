using OMT_Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace OMT_Api.Models
{
    public class EmployeeRegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}