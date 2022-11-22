using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Models
{
    public class UserModel
{
       
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public bool Locked { get; set; } = false; //hesap pasif,aktif
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? ProfileImageFileNames { get; set; } = "no-image.jpg";
        public string Role { get; set; } = "user";
    }
    public class CreateUserModel
    {
        //[Display(Name ="Username",Prompt ="username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(50, ErrorMessage = "Fullname can be max 50 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(16, ErrorMessage = "Pasword can be max 16 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-Password is required.")]
        [MaxLength(16, ErrorMessage = "Re-Pasword can be max 16 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public String? Done { get; set; }
    }

    public class EditViewModel
    {
        //[Display(Name ="Username",Prompt ="username")]
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(50, ErrorMessage = "Fullname can be max 50 characters.")]
        public string FullName { get; set; }

        public bool Locked { get; set; }

        

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public String? Done { get; set; }
        public Guid Id { get; set; }
    }
}
