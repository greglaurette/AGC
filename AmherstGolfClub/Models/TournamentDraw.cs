using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class TournamentDraw
    {
        [Key]
        public int TouramentDrawID { get; set; }

        [ForeignKey("Tournaments")]
        public int TournamentID { get; set; }

        [Display(Name = "Tee Time")]
        public string TeeTime { get; set; }

        public string GolfOne { get; set; }

        public string GolfTwo { get; set; }

        public string GolfThree { get; set; }

        public string GolfFour { get; set; }

        public virtual IEnumerable<TournamentDraw> TournmentDraws { get; set; }

        public virtual Tournament Tournaments { get; set; }
    }
}