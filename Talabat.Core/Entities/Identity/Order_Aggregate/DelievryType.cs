using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Identity.Oreder_Aggregate
{
    public class DelievryType : BaseEntitiy 
    {
        public DelievryType() { }
        public DelievryType(string name, string description, decimal cost, string delievryTime)
        {
            Name = name;
            Description = description;
            Cost = cost;
            DelievryTime = delievryTime;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }    
        public string DelievryTime { get; set; }
    }
}
