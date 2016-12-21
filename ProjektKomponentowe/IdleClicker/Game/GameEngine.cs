using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IdleClicker
{
    public static class GameEngine
    {
        /// <summary>
        /// GameTimer - zegar gry. Wywołuje zdarzenia na które ma się coś dziać
        /// </summary>
        public static IGameTimer GameTimer;

        /// <summary>
        /// Informacja czy gra jest aktywna
        /// </summary>
        private static bool enabled;

        /// <summary>
        /// Lista akcji
        /// </summary>
        public static IActionList ActionList;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        static GameEngine()
        {
            //Tworzy obiekt GameTimera
            GameTimer = new GameTimer();
            ActionList = new ActionList();
            GameTimer.OnTick += GameTimer_OnTick;
            Enabled = false;          

            // Określa wstępną częstotliwość zegara
            GameTimer.Interval = 1;
        }

        /// <summary>
        /// Metoda wywołująca sprawdzanie zdarzeń w ActionList
        /// </summary>
        /// <param name="e">Aktualny czas</param>
        private static void GameTimer_OnTick(TickEventArgs e)
        {
            ActionList.Execute(e.Ticks);
        }

        /// <summary>
        /// Pauzuje i uruchamia z powrotem grę
        /// </summary>
        public static bool Enabled
        {
            set
            {
                enabled = value;
                GameTimer.Enabled = value;
            }
            get
            {
                return enabled;
            }
        }

        
    }
}
