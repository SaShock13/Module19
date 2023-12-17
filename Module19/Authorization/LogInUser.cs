using System.ComponentModel.DataAnnotations;

namespace Module19.Authorization
{
    public class LogInUser
    {
        [Required, MaxLength(20)]
        public string LoginProp { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } 

    }
}
