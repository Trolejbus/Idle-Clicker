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
    public class Action : IComparable<Action>
    {
        /// <summary>
        /// Zdarzenia, które mają się wykonać
        /// </summary>
        public event ActionDelegate Actions;

        /// <summary>
        /// Numer Ticku, w którym mają się wykonać. UWAGA! Wartość musi być zwiększona o ilość ticków w momencie wykonania
        /// zdarzenia! (ToDo: Dorobić możliwość zmiany ilości ticków na bieżąco)
        /// </summary>
        public long TriggerValue { protected set; get; }
        public int ExecuteTimes { get; set; }
        public long FrequencyValue { get; private set; }
        public bool FirstExecute { get; private set; }

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="tickToExecute">Wartość, która mówi, że "za ile ticków ma się wykonać zdarzenie"</param>
        /// <param name="executeTimes">Wartość, która mówi "ile razy ma się wykonać zdarzenie"</param>
        /// <param name="frequencyTicks">Wartość, która mówi "co ile ma się wykonywać zdarzenie"</param>
        public Action(long triggerValue = 0, int executeTimes = 1, long frequencyTicks = 1)
        {
            //Tick = ticksToExecute;
            TriggerValue = triggerValue;

            // przechowuje częstotliwość z jaką jest wykonywana akcja
            FrequencyValue = frequencyTicks;

            // ile razy ma się wykonać akcja
            ExecuteTimes = executeTimes;
        }

        /// <summary>
        /// Wykonuje zdarzenia zapisane w tej klasie
        /// </summary>
        public void Execute()
        {
            Actions();
        }

        /// <summary>
        /// Implementacja interfacu IComparable. Służy do porównywania dwóch zdarzeń biorąc pod uwagę wartość Tick
        /// </summary>
        /// <param name="obj">Drugie zdarzenie</param>
        /// <returns> mniejsze od 0 jeśli mniejsze, równe 0 jeśli takie samo, większe od 0 jeżeli większe</returns>
        public int CompareTo(Action obj)
        {
            return Convert.ToInt32(TriggerValue - obj.TriggerValue);
        }
    }
}
