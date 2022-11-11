namespace OMT_Api.Models
{
    public class EmployeeAuth
    {
        //This model might not be needed, probably should use entity model.
        public string EmployeeId { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}