using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmherstGolfClub.Models
{
    public class TournamentPlayers
    {
        public virtual Tournament Tournament { get; set; }

        [Required]
        public int TournamentPlayersID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required]
        public string Club { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public int DayOne { get; set; }

        [Required]
        public int DayTwo { get; set; }

        public int Total {
            get {
                return DayOne + DayTwo; }
        }

    }
}