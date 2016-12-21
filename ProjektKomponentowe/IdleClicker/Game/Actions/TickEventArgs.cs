using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public class TickEventArgs : EventArgs
    {
        public long Ticks { get; private set; }
        public DateTime GameDate { get; private set; }

        public TickEventArgs(long ticks, DateTime gameDate)
        {
            Ticks = ticks;
            GameDate = gameDate;
        }
    }
}
