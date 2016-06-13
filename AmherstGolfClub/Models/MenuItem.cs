using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class MenuItem
    {
        [Display(Name = "Item ID")]
        public int MenuItemID { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("MenuCategory")]
        public int Category { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}