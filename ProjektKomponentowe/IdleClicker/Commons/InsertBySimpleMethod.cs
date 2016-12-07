using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Klasa przechowująca metodę, która wstawia obiekt do posortowanej LinkedList w odpowiednie miejsce
    /// </summary>
    static class InsertBySimpleMethod
    {
        /// <summary>
        /// Metoda wywoływana. Sprawdza czy obiekt nie jest większy niż ostatni element (wtedy wyjątkowo musi wstawić po ostatnim
        /// elemencie) i jeżeli nie, to wywołuje właściwą metodą umieszczającą.
        /// </summary>
        /// <typeparam name="T">Typ danych (IComparable)</typeparam>
        /// <param name="values">LinkedList</param>
        /// <param name="item">Wstawiany element</param>
        public static void Insert<T>(LinkedList<T> values, T item) where T : IComparable<T>
        {
            if (values.Count == 0 || item.CompareTo(values.Last.Value) > 0) 
                values.AddLast(item);
            else
                Insert(values, item, values.First);
        }

        /// <summary>
        /// Metoda działająca w rekurencji, która sprawdza czy następny element jest większy od aktualnie sprawdzanego. Jeżeli tak
        /// to musi wstawić przed tym elementem, jeżeli nie metoda wykona się dla następnego elementu
        /// </summary>
        /// <typeparam name="T">Typ danych (IComparable)</typeparam>
        /// <param name="values">LinkedList</param>
        /// <param name="item">Wstawiany element</param>
        /// <param name="node">Aktualnie sprawdzany element</param>
        private static void Insert<T>(LinkedList<T> values, T item, LinkedListNode<T> node) where T : IComparable<T>
        {
            if (item.CompareTo(node.Value) < 0)
                values.AddBefore(node, item);
            else
            {
                Insert(values, item, node.Next);
            }
        }
    }
}
