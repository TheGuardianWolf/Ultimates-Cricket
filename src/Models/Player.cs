using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ultimates_Cricket.Models
{
    public class Player
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Photo { get; set; }

        [Display(Name = "Batting Average")]
        [Range(0, float.MaxValue)]
        public float BattingAverage { get; set; }

        [Display(Name = "Catches Taken")]
        [Range(0, int.MaxValue)]
        public int CatchesTaken { get; set; }

        public ICollection<Stat> Stats { get; set; }

        public ICollection<Game> GamesWellPlayed { get; set; }
    }
}
