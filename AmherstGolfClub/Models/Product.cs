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
        [StringLength(60)]
        public string Name { get; set; }

        
        public int ProductID { get; set; }

        [Required]
        public string Price { get; set; }

        
        public string Quantity { get; set; }

        
        public string SubDepartment { get; set; }

        
        public string ItemCategory { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}