using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Models
{
    public class DepartmanViewModel
    {
        //[Display(Name ="Username",Prompt ="username")]

       
            public Guid Id { get; set; }
            [StringLength(50, ErrorMessage = "Username can be max 50 characters.")]
            public string Position { get; set; }

            [StringLength(50, ErrorMessage = "Task can be max 50 characters.")]
            public string Task { get; set; }
        
            
            
            public string? FullName { get; set; }
            public string? Username { get; set; }
        
        
    }
}
