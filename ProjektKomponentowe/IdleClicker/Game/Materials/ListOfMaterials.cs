using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    /// <summary>
    /// Klasa przechowująca listę surowców dostępnych w grze
    /// </summary>
    class ListOfMaterials
    {
        private List<Material> Materials;

        /// <summary>
        /// Konstruktor klasy ListOfMaterials.
        /// Tworzę instancję listy z surowcami i ustawiam licznik surowców na zero.
        /// </summary>
        public ListOfMaterials()
        {
            Materials = new List<Material>();
        }

        /// <summary>
        /// Metoda dodająca nowy surowiec do listy.
        /// </summary>
        /// <typeparam name="T">Typ ogólny surowca</typeparam>
        /// <param name="ge">GameEngine - instancja silnika gry podawana dla surowca</param>
        /// <returns>Zwraca instancję obiektu.</returns>
        public T AddNewMaterial<T>(GameEngine ge) where T : Material
        {
            foreach (Material item in Materials)
            {
                if (item is T) return (T)item;
            }
            Materials.Add((Material)Activator.CreateInstance(typeof(T), ge));
            return (T)Materials[Materials.Count - 1];
        }

    }
}
