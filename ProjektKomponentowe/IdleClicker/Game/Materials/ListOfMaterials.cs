using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void NewMaterialHandler(Material m);
    /// <summary>
    /// Klasa przechowująca listę surowców dostępnych w grze
    /// </summary>
    public class ListOfMaterials
    {
        private List<Material> Materials;

        public event NewMaterialHandler NewMaterial;

        /// <summary>
        /// Konstruktor klasy ListOfMaterials.
        /// </summary>
        public ListOfMaterials()
        {
            Materials = new List<Material>();
        }

        /// <summary>
        /// Metoda dodająca nowy surowiec do listy.
        /// </summary>
        /// <param name="m">Surowiec który chcemy dodać do listy.</param>
        /// <returns></returns>
        public Material AddNewMaterial(Material material)
        {
            foreach (Material item in Materials)
            {
                if (item.Key == material.Key) return (Material)item;
            }
            
            Materials.Add(material);
            NewMaterial((Material)Materials[Materials.Count - 1]);
            return (Material)Materials[Materials.Count - 1];
        }
    }
}
