using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void OnTickDelegate(long Tick);
    public interface IGameEngine
    {
        bool Enabled { get; set; }
        void SetActionList(IActionList newActionList);
        long Ticks { get; }
        event OnTickDelegate OnTick;
        IActionList GetActionList();
    }
}
