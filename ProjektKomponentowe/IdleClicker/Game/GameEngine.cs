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
        public static GameTimer GameTimer;

        /// <summary>
        /// Informacja czy gra jest aktywna
        /// </summary>
        private static bool enabled;

        /// <summary>
        /// Lista akcji
        /// </summary>
        public static ActionList ActionList;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        static GameEngine()
        {
            //Tworzy obiekt GameTimera
            GameTimer = new GameTimer();
            GameTimer.OnTick += GameTimer_Tick;
            Enabled = false;
            ActionList = new ActionList();

            // Określa wstępną częstotliwość zegara
            GameTimer.Interval = 1000;
        }

        /// <summary>
        /// Zdarzenie wywołujące się przy każdym Ticku
        /// </summary>
        /// <param name="sender">Domyślnie Game Timer</param>
        /// <param name="e"></param>
        private static void GameTimer_Tick(long Ticks)
        {
            ActionList.Execute(Ticks);
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
