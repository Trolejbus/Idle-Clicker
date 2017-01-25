using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void OnTickDelegate(TickEventArgs e);
    public delegate void ChangeOnNightDelegate(int checkIfSunRise);
    public interface IGameTimer
    {
        DateTime GameDate { get; set; }
        long Ticks { get; }
        bool Enabled { get; set; }
        int Interval { get; set; }
        double NightState { get; set; }

        event OnTickDelegate OnTick;
        event ChangeOnNightDelegate CheckOnNight;
    }
}
