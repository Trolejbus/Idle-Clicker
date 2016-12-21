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

    public interface IAction : IComparable<IAction>
    {
        event ActionDelegate Actions;
        long TriggerValue { get; }
        int ExecuteTimes { get; set; }
        long FrequencyValue { get; }
        bool FirstExecute { get; }
        void Execute();
        void OnAdd();
    }
}
