using System.ComponentModel.DataAnnotations;

namespace Model.Dtos
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
