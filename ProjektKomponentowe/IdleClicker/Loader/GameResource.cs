using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    delegate void LoadResourceDelegate();
    class GameResource
    {
        public string name;
        public object Resource { get; set; }
        public string LoadText { get; set; }
        public bool LoadTextVisible { get; set; }
        public LoadResourceDelegate LoadResource { get; set; }
    }
}
