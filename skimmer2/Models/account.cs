using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skimmer2.Models
{
    public class account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        public string first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [Required]
        [StringLength(255)]
        public string address { get; set; }

        [Required]
        [EnumDataType(typeof(role))]
        public role Role { get; set; }
    }

    public enum role
    {
        User,
        Admin
    }
}