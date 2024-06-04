using System.ComponentModel.DataAnnotations;

namespace JwtAuthMvcApp.Models
{
    public class loginDto
    {
  
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
