using System.ComponentModel.DataAnnotations;

namespace OMT_Api.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string State { get; set; } = string.Empty;

        [Required, MaxLength(5)]
        public string Zip { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}