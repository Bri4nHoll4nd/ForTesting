using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueChampion.Data
{
    public class Info
    {
        public int InfoId { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }

        public Guid ChampionId { get; set; }
        public Champion Champion { get; set; }
    }
}
