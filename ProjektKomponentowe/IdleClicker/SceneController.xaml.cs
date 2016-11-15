using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IdleClicker
{
    /// <summary>
    /// Interaction logic for SceneController.xaml
    /// </summary>
    public partial class SceneController : UserControl
    {
        public object CurrentScene { get; set; }
        private IntroScene InitialScene = new IntroScene(); // Przy zmianie sceny początkowej należy w metodzie LoadInitialScene zmienić przekazywany typ ogólny

        public SceneController()
        {
            InitializeComponent();
            LoadInitialScene();
        }

        public void LoadScene<T>(IScene scene) where T : UserControl, IScene
        {
            if (CurrentScene != null)
            {
                grid.Children.Remove((UserControl)CurrentScene);
                ((IScene)CurrentScene).Close();
            }
            CurrentScene = scene;
            ((IScene)CurrentScene).SetSceneController(this);
            grid.Children.Add((UserControl)CurrentScene);
            scene.Load();
        }

        private void LoadInitialScene()
        {
            LoadScene<IntroScene>(InitialScene);          
        }
    }
}
