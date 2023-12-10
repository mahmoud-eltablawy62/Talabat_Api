using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class AddressDto
    {
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        
        public string State { get; set; }
    }
}