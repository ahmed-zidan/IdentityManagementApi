using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LoginDto
    {
        [RegularExpression(pattern:@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" , ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
