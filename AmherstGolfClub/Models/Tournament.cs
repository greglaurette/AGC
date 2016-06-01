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
        public virtual ICollection<TournamentPlayers> TournamentPlayers { get; set; }

        public int TournamentID { get; set; }

        [Required]
        [Display(Name = "Tournament")]
        public string TournamentName { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TournamentDate { get; set; }

        //[Display(Name = "File Name")]
        //public File FileName { }

    }
}