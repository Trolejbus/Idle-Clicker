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

        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        /// <param name="tick">Wartość, która mówi, że "za ile ticków ma się wykonać zdarzenie"</param>
        public Action(long tick = 0)
        {
            Tick = tick;
        }

        /// <summary>
        /// Przy dodawaniu zdarzenia do listy ta metoda zwiększa ilość ticków w tym zdarzeniu o ilość ticków w silniku gry
        /// </summary>
        /// <param name="tickValue"></param>
        public void UpdateTick(long tickValue)
        {
            Tick += tickValue;
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
