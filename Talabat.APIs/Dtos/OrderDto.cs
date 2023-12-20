using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
    public class OrderDto
    {
      
        [Required]
        public string Baasket_ID { get; set;}
        [Required]
        public int Deleivry_Method { get; set; }
         
        public AddressDto Addres { get; set; }


        ///string Basket_iD, int deleivryMethod, Address Ad

    }
}
