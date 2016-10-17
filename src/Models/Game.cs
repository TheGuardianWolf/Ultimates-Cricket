using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ultimates_Cricket.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int? PlayerOfMatchId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int GameNumber { get; set; }

        [ForeignKey("PlayerOfMatchId")]
        public virtual Player PlayerOfMatch { get; set; }

        public ICollection<Stat> Stats { get; set; }
    }
}
