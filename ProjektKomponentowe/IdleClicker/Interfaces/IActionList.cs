using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public interface IActionList
    {
        void AddAction(IAction newAction);
        void Execute(long triggerValue);
    }
}
