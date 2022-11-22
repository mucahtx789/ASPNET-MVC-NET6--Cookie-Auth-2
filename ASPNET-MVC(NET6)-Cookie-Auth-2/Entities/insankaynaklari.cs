using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Entities
{
    [Table("departman_insankaynaklari")]
    public class insankaynaklari
{
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Task { get; set; }
    }
}
