using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required]
        public int Price { get; set; }

        public string Image { get; set; } 
        [Display(Name="Product Color")]
        public string ProductColor { get; set; }

        [Required]
        [Display(Name="Available")]
        public bool IsAvailable { get; set; }

        [Display(Name="Product Type")]
        [Required]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }

        [Display(Name = "Tag Name")]
        [Required]
        public int TagNameId { get; set; }
        [ForeignKey("TagNameId")]
        public TagNames TagNames { get; set; }
    }
}
