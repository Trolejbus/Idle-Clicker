using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Delegat, który umieszcza element w posortowanej liście
    /// </summary>
    /// <typeparam name="T">Typ danych(IComparable)</typeparam>
    /// <param name="values">Linked List</param>
    /// <param name="item">Wstawiany element</param>
    delegate void InsertToSortTableDelegate<T>(LinkedList<T> values, T item) where T : IComparable<T>;

    /// <summary>
    /// Klasa, która przechowuje posortowaną tabelę w LinkedList
    /// </summary>
    /// <typeparam name="T">Typ danych</typeparam>
    class SortLinkedList<T> where T : IComparable<T>
    {
        /// <summary>
        /// Lista przechowująca elementy
        /// </summary>
        LinkedList<T> items = new LinkedList<T>();

        /// <summary>
        /// Metoda umożliwiająca dodanie obiektu korzystając z podstawowego algorymu
        /// </summary>
        /// <param name="newitem">Nowy obiekt</param>
        public void AddItem(T newitem)
        {
            InsertBySimpleMethod.Insert<T>(items, newitem);
        }

        /// <summary>
        /// Metoda umożliwiająca dodanie obiektu korzystając z wybranego algorytmu
        /// </summary>
        /// <param name="newitem">Nowy obiekt</param>
        /// <param name="insertToSortTableDelegate">Algorytm dodawania obiektu</param>
        public void AddItem(T newitem, InsertToSortTableDelegate<T> insertToSortTableDelegate)
        {
            insertToSortTableDelegate(items, newitem);
        }

        /// <summary>
        /// Metoda zwracająca listę
        /// </summary>
        public LinkedList<T> List
        {
            get
            {
                return items;

            }
        }
    }
}
