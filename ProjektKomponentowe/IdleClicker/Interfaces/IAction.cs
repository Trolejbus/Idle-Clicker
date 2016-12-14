using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Delegat zdarzenia (ToDo: Zastanowić się czy potrzebuje parametry wejściowe)
    /// </summary>
    public delegate void ActionDelegate();

    public interface IAction : IComparable<Action>
    {
        event ActionDelegate Actions;
        long Tick { get; }
        long TickToExecute { get; }
        int ExecuteTimes { get; set; }
        long FrequencyTick { get; }
        bool FirstExecute { get; }
        void UpdateTick(long gameEngineTicks);
        void Execute();
    }
}
