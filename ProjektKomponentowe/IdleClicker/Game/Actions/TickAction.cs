using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Klasa przechowująca zdarzenie zaplanowane w przyszłości, które ma się wykonać
    /// </summary>
    public class TickAction : Action
    {     
        public long TicksToExecute { private set; get; }

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="tickToExecute">Wartość, która mówi, że "za ile ticków ma się wykonać zdarzenie"</param>
        /// <param name="executeTimes">Wartość, która mówi "ile razy ma się wykonać zdarzenie"</param>
        /// <param name="frequencyTicks">Wartość, która mówi "co ile ma się wykonywać zdarzenie"</param>
        public TickAction(long ticksToExecute = 0, int executeTimes = 1, long frequencyTicks = 1) 
            :base(0,executeTimes,frequencyTicks)
        {
            TicksToExecute = ticksToExecute;
        }

        /// <summary>
        /// Przy dodawaniu zdarzenia do listy ta metoda zwiększa ilość ticków w tym zdarzeniu o ilość ticków w silniku gry
        /// </summary>
        /// <param name="gameEngineTicks">Tiki zegara gry</param>
        public void UpdateTick(long gameEngineTicks)
        {
            if (TicksToExecute == 0)
            {
                TriggerValue = gameEngineTicks;
            }
            else if(TicksToExecute > 0)
            {
                TriggerValue = gameEngineTicks + TicksToExecute;
            }

            // gdy wstawię akcję w liście akcji już nie muszę czekać na rozpoczęcie wykonywania
            TicksToExecute = FrequencyValue;
            return;
        }
    }
}
