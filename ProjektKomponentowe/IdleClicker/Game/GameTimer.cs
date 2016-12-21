using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public class GameTimer : IGameTimer
    {
        protected System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        bool enabled;
        public DateTime GameDate { get; private set; }

        /// <summary>
        /// Zdarzenie wywołujące się przy ticku zegara
        /// </summary>
        public event OnTickDelegate OnTick;

        public GameTimer()
        {
            GameDate = new DateTime(1, 1, 1, 8, 0, 0);
            gameTimer.Tick += GameTimer_Tick;           
        }

        /// <summary>
        /// Zwraca ilość ticków
        /// </summary>()
        public long Ticks
        {
            get; private set;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Ticks++;
            GameDate = GameDate.AddMinutes(1);
            if (OnTick != null)
                OnTick(new TickEventArgs(Ticks,GameDate));
        }

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

        public int Interval
        {
            get
            {
                return gameTimer.Interval.Milliseconds;
            }
            set
            {
                gameTimer.Interval = new TimeSpan(0, 0, 0, 0, value);
            }
        }    
    }
}
