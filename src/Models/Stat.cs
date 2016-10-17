using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ultimates_Cricket.Models
{
    public class Stat
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Batting { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Bowling { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}
