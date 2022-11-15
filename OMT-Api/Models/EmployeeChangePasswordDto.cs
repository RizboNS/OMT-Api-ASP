namespace OMT_Api.Models
{
    public class EmployeeChangePasswordDto
    {
        public string oldPassword { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }
}