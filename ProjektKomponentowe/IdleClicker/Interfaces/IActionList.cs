using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public interface IActionList
    {
        void SetGameEngine(IGameEngine ge);
        void AddAction(Action newAction);
    }
}
