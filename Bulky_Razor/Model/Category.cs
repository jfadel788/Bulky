using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky_Razor.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "The value must be between 0 and 100")]
        public int DisplayOrder { get; set; }
    }
}
