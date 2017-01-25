using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IdleClicker
{
    static class GameLoader
    {
        public static List<Game> games  {get; private set; }
        static GameLoader()
        {
            games = new List<Game>();
        }

        public static void LoadGames()
        {
            games.Clear();

            foreach(string file in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\SavedGames"))
            {
                Game newGame = new Game();
                newGame.Load(file);
                games.Add(newGame);
            }
        }

        static void SetStandardParameters()
        {
            
        }
    }
}
