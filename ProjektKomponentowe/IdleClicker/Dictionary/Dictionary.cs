using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Klasa przechowująca słownik surowców.
    /// </summary>
    static class Dictionary
    {
        /// <summary>
        /// Słownik przechowujący klucze surowców i ich polskie nazwy.
        /// </summary>
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {
            { "WOOD", "Drewno" },
            { "GOLD", "Złoto" },
            { "FOOD", "Pożywnienie"},
        };
    }
      
}
