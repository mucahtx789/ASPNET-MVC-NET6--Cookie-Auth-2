using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Models
{
    public class LoginViewModel
    {
        //[Display(Name ="Username",Prompt ="username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(16, ErrorMessage = "Pasword can be max 16 characters.")]
        public string Password { get; set; }
    }
}
