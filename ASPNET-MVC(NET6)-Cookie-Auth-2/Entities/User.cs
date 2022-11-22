﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string? FullName { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool Locked { get; set; } = false; //hesap pasif,aktif
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? ProfileImageFileNames { get; set; } = "no-image.jpg";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

    }
}