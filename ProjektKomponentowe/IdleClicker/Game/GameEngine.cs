using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IdleClicker
{
    delegate void OnTickDelegate(long Tick);

    class GameEngine
    {
        /// <summary>
        /// GameTimer - zegar gry. Wywołuje zdarzenia na które ma się coś dziać
        /// </summary>
        private System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();

        /// <summary>
        /// Zdarzenie wywołujące się przy ticku zegara
        /// </summary>
        public event OnTickDelegate OnTick;

        /// <summary>
        /// Informacja czy gra jest aktywna
        /// </summary>
        private bool enabled;

        /// <summary>
        /// Ilość ticków wykonanych przez zegar do tej pory
        /// </summary>
        private long ticks;

        /// <summary>
        /// Lista akcji
        /// </summary>
        ActionList actionList;

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public GameEngine()
        {
            // Tworzy obiekt GameTimera
            gameTimer.Tick += GameTimer_Tick;

            // Określa wstępną częstotliwość zegara
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            // Tworzy obiekt listy zdarzeń
            actionList = new ActionList(this);
        }

        /// <summary>
        /// Zdarzenie wywołujące się przy każdym Ticku
        /// </summary>
        /// <param name="sender">Domyślnie Game Timer</param>
        /// <param name="e"></param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            ticks++; // Przy każdym ticku zwiększa ticks
            OnTick(ticks); // Wywołuje zdarzenie
        }

        /// <summary>
        /// Właściwość określająca czy gra jest aktywna. Dzięki temu można pauzować grę
        /// </summary>
        public bool Enabled
        {
            set
            {
                enabled = value;
                // Odpowiednio zatrzymuje lub wznawia pracę zegara
                if (value)
                    gameTimer.Start();
                else
                    gameTimer.Stop();
            }
            get
            {
                return enabled;
            }
        }

        /// <summary>
        /// Zwraca instancję listy zdarzeń
        /// </summary>
        public ActionList ActionList
        {
            get
            {
                return actionList;
            }
        }

        /// <summary>
        /// Zwraca ilość ticków
        /// </summary>
        public long Ticks
        {
            get
            {
                return ticks;
            }
        }
    }
}
