using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void OnTickDelegate(TickEventArgs e);
    public interface IGameTimer
    {
        DateTime GameDate { get; }
        long Ticks { get; }
        bool Enabled { get; set; }
        int Interval { get; set; }

        event OnTickDelegate OnTick;
    }
}
