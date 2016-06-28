using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class Events
    {
        public int EventsID { get; set; }
        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Start { get; set; }

        [ForeignKey("EventType")]
        public int Type { get; set; }

        public IEnumerable<Events> Event { get; set; }

        public virtual EventType EventType { get; set; }
        
    }
}