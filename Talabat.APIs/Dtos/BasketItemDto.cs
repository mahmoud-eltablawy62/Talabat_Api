using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Product_Name { get; set; }
        [Required]
        public string Picture_Url { get; set; }
        [Required]
        [Range(0.1, double.MaxValue ,ErrorMessage = "Price Must Not Equal Zero")]
        public double Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Not Equal Zero")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
