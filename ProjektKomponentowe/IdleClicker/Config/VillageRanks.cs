using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    static class VillageRanks
    {
        public static List<Rank> Ranks { get; private set; }

        static VillageRanks()
        {
            Ranks = new List<Rank>();
            Ranks.Add(new Rank() { Score = 0, Name = "Osada" });
            Ranks.Add(new Rank() { Score = 1000, Name = "Wioska" });
            Ranks.Add(new Rank() { Score = 2000, Name = "Miasteczko" });
            Ranks.Add(new Rank() { Score = 5000, Name = "Miasto" });
            Ranks.Add(new Rank() { Score = 10000, Name = "Metropolia" });
        }
    }

    class Rank
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
