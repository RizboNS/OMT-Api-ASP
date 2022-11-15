using System.ComponentModel.DataAnnotations;

namespace OMT_Api.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Required, MinLength(8), MaxLength(8)]
        public string EmployeeId { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public Role Role { get; set; }
    }

    public enum Role
    {
        agent, admin
    }
}