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

    /// <summary>
    /// Klasa przechowująca zdarzenie zaplanowane w przyszłości, które ma się wykonać
    /// </summary>
    class Action : IComparable<Action>
    {     
        /// <summary>
        /// Zdarzenia, które mają się wykonać
        /// </summary>
        public event ActionDelegate Actions;

        /// <summary>
        /// Numer Ticku, w którym mają się wykonać. UWAGA! Wartość musi być zwiększona o ilość ticków w momencie wykonania
        /// zdarzenia! (ToDo: Dorobić możliwość zmiany ilości ticków na bieżąco)
        /// </summary>
        public long Tick { private set; get; }
        public long TickToExecute { private set; get; }
        public int ExecuteTimes { get; set; }
        public long FrequencyTick { get; private set; }

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="tickToExecute">Wartość, która mówi, że "za ile ticków ma się wykonać zdarzenie"</param>
        /// <param name="executeTimes">Wartość, która mówi "ile razy ma się wykonać zdarzenie"</param>
        /// <param name="frequencyTicks">Wartość, która mówi "co ile ma się wykonywać zdarzenie"</param>
        public Action(long ticksToExecute = 0, int executeTimes = 1, long frequencyTicks = 1)
        {
            Tick = ticksToExecute;
            TickToExecute = ticksToExecute;
            // przechowuje częstotliwość z jaką jest wykonywana akcja
            FrequencyTick = frequencyTicks;
            ExecuteTimes = executeTimes;
        }

        /// <summary>
        /// Przy dodawaniu zdarzenia do listy ta metoda zwiększa ilość ticków w tym zdarzeniu o ilość ticków w silniku gry
        /// </summary>
        /// <param name="gameEngineTicks">Tiki zegara gry</param>
        public void UpdateTick(long gameEngineTicks)
        {
            if (TickToExecute == 0)
            { 
                Tick = gameEngineTicks + FrequencyTick;
                return;
            }

            Tick += gameEngineTicks;

            // gdy wstawię akcję w liście akcji już nie muszę czekać na rozpoczęcie wykonywania
            TickToExecute = 0;
            return;
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
            if (!(obj is Action))
                throw new ArgumentException();

            return Convert.ToInt32(Tick - ((Action)obj).Tick);            
        }
    }
}
