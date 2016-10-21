﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ultimates_Cricket.Models
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PlayerOfMatchId { get; set; }

        [Required]
        [Display(Name = "Game Number")]
        [Range(0, int.MaxValue)]
        public int GameNumber { get; set; }

        [Display(Name = "Player of Match")]
        [ForeignKey("PlayerOfMatchId")]
        public virtual Player PlayerOfMatch { get; set; }

        public ICollection<Stat> Stats { get; set; }
    }
}
