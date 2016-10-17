using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ultimates_Cricket.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public ICollection<Stat> Stats { get; set; }

        public ICollection<Game> GamesWellPlayed { get; set; }
    }
}
