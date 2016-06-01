using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class ContactForm
    {
        [Required]
        public string emailName { get; set; }
        [Required]
        public string emailSubject { get; set; }
        [Required]
        public string emailMessage { get; set; }
        [Required]
        public string emailAddress { get; set; }
    }
}