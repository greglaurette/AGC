using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class EventType
    {
        [Required]
        [Display(Name = "Event ID")]
        public int EventTypeID { get; set; }
        [Required]
        [Display(Name = "Event Type")]
        public string Type { get; set; }
    }
}