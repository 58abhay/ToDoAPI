using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Application.Configuration
{
    public class AppSettings
    {
        [Required]
        public string ApplicationName { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "MaxTasksPerUser must be greater than 0.")]
        public int MaxTasksPerUser { get; set; }

        [Required]
        public JwtSettings Jwt { get; set; } = new JwtSettings();
    }

    public class JwtSettings
    {
        [Required]
        public string Key { get; set; } = string.Empty;

        [Required]
        public string Issuer { get; set; } = string.Empty;

        [Required]
        public string Audience { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "ExpiryMinutes must be greater than 0.")]
        public int ExpiryMinutes { get; set; }
    }
}