namespace Talabat.Core.Entities.Identity
{
    public class Adress
    {
        public int Id { get; set; }
        public string   First_Name { get; set; }
        public string Last_Name { get; set;}
        public string Street { get; set;}
        public string City { get; set;}
        public string country { get; set;}  

        public string UsersId { get; set;}    
    }
}