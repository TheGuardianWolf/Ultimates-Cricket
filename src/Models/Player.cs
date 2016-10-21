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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Photo { get; set; }

        public ICollection<Stat> Stats { get; set; }

        public ICollection<Game> GamesWellPlayed { get; set; }
    }
}
