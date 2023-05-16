using System.ComponentModel.DataAnnotations;

namespace SalesPageAPI.Data.DTOS.UserDtos
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
