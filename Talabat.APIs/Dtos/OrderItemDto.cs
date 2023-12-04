namespace Talabat.APIs.Dtos
{
    public class OrderItemDto
    { 
        public int Id { get ; set; }
        public int Prod_Id { get; set; }
        public string Prod_Name { get; set; }
        public string Picture_Url { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}