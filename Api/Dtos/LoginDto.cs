using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class LoginDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [RegularExpression(pattern:@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" , ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
