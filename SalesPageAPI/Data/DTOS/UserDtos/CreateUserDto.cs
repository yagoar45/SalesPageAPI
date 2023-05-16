using System.ComponentModel.DataAnnotations;

namespace SalesPageAPI.Data.DTOS.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        public DateTime DayBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
