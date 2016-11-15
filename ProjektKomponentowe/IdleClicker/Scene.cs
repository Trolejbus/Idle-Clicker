using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace IdleClicker
{
    public class Scene : UserControl
    {
        protected SceneController sceneController;

        public virtual void SetSceneController(SceneController sc)
        {
            sceneController = sc;
        }

        public virtual void Load() { }
        public virtual void Close() { }
    }
}
