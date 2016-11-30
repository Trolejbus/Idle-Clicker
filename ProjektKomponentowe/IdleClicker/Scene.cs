using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace IdleClicker
{
    /// <summary>
    /// Klasa określająca budowę obiektu Scene. Każda utworzona przez nas Scena dziedziczy właśnie po tej klasie
    /// </summary>
    public class Scene : UserControl
    {
        protected SceneController sceneController;

        public virtual void SetSceneController(SceneController sc)
        {
            sceneController = sc;
        }

        /// <summary>
        /// Metoda odpowiedzialna za wczytanie sceny
        /// </summary>
        public virtual void Load() { }

        /// <summary>
        /// Metoda odpowiedzialna za zamknięcie sceny
        /// </summary>
        public virtual void Close() { }
    }
}
