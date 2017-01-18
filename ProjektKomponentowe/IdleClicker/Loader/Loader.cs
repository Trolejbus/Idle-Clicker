using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void OnStatusTextChangeDelegate(string StatusText);
    static class Loader
    {
        public static List<GameResource> ResourceList { get; set; }
        private static string statusText;
        public static event OnStatusTextChangeDelegate OnStatusTextChange;
        public static string StatusText
        {
            get
            {
                return statusText;
            }
            private set
            {
                statusText = value;
                if (OnStatusTextChange != null)
                    OnStatusTextChange(value);
            }
        }
        
        public static void LoadAllResources()
        {
            StatusText = "Wczytywanie zasobów";
            foreach (GameResource item in ResourceList)
            {
                if (item.LoadTextVisible)
                    StatusText = item.LoadText;
                item.LoadResource();
            }
            StatusText = "Wczytano";
        }
    }
}
