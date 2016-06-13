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
        public int MenuItemID { get; set; }
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name ="Price")]
        public decimal ItemPrice { get; set; }

        [ForeignKey("MenuCategory")]
        public int Type { get; set; }

        public virtual MenuCategory MenuCategory { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}