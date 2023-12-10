using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository.Identity.Oreder_Aggregate
{
     public class Address
    {
        public Address() { }    
        public Address(string first_Name, string last_Name, string street, string city, string state)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            Street = street;
            City = city;
            State = state;
        }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }     
        public string Street { get; set; }  
        public string City { get; set; }
        public string? State { get; set; }
    }
}
