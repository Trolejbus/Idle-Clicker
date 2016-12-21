using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IdleClicker
{
    public class GameEngine
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
        }

        /// <summary>
        /// Ustawia instację action listy
        /// </summary>
        /// <param name="newActionList"></param>
        public void SetActionList(ActionList newActionList)
        {
            actionList = newActionList;
            newActionList.SetGameEngine(this);
        }

        /// <summary>
        /// Zdarzenie wywołujące się przy każdym Ticku
        /// </summary>
        /// <param name="sender">Domyślnie Game Timer</param>
        /// <param name="e"></param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Ticks++; // Przy każdym ticku zwiększa ticks
            actionList.Execute(Ticks);
            if(OnTick != null)
                OnTick(Ticks); // Wywołuje zdarzenie
        }

        /// <summary>
        /// Pauzuje i uruchamia z powrotem grę
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
        public IActionList GetActionList()
        {
            return actionList;
        }

        /// <summary>
        /// Zwraca ilość ticków
        /// </summary>()
        public long Ticks
        {
            get; private set;
        }
    }
}
