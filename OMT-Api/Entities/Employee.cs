using System.ComponentModel.DataAnnotations;

namespace OMT_Api.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MinLength(6), MaxLength(50)]
        public string Password { get; set; }

        [Required, MinLength(8), MaxLength(8)]
        public string EmployeeId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public Role Role { get; set; }
    }

    public enum Role
    {
        agent, admin
    }
}