using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace IdleClicker
{
    public class GameTimer : IGameTimer
    {
        protected DispatcherTimer gameTimer = new DispatcherTimer();
        bool enabled;
        public DateTime GameDate { get; private set; }
        int counterToIncreaseOpacity = 0;

        /// <summary>
        /// Zdarzenie wywołujące się przy ticku zegara
        /// </summary>
        public event OnTickDelegate OnTick;
        public event ChangeOnNightDelegate CheckOnNight;

        public GameTimer()
        {
            GameDate = new DateTime(1, 1, 1, 8, 0, 0);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Tick += CheckTimeOfDay;
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
        //1-zachód
        //2-wschód
        //3-nic
        public int CheckIfSunrise()
        {
            if (GameDate.Hour >= 18 && GameDate.Hour < 23)
            {
                return 1;
            }
            else
            {
                if (GameDate.Hour >= 5 && GameDate.Hour < 9)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }

        }
        public void CheckTimeOfDay(object sender, EventArgs a)
        {
                int checkIfNight = CheckIfSunrise();
                if (checkIfNight == 1 || checkIfNight == 2)
                {
                    counterToIncreaseOpacity += 1;
                    if (counterToIncreaseOpacity == 10)
                    {
                        CheckOnNight(checkIfNight);
                        counterToIncreaseOpacity = 0;
                    }
                }
        }
    }
}
