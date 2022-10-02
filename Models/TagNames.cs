using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class TagNames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Tag Name")]
        [StringLength(50)]
        public string TagName { get; set; }
    }
}
