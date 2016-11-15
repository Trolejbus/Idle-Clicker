using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public interface IScene
    {
        void SetSceneController(SceneController sc);
        void Load();
        void Close();
    }
}
