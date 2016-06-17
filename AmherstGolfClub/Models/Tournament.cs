using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class Tournament
    {
        public int TournamentID { get; set; }    

        public string Year { get; set; }

        [Required]
        [Display(Name = "Tournament")]
        public string TournamentName { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TournamentDate { get; set; }

    }
}