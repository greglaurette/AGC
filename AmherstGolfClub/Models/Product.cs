using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }

        
        public int ProductID { get; set; }

        [Required]
        public decimal Price { get; set; }

        
        public int Quantity { get; set; }

        
        public string SubDepartment { get; set; }

        
        public string ItemCategory { get; set; }

        public string Vendor { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}