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
        public Scene CurrentScene { get; set; }
        private Scene InitialScene = new GameScene(); 

        public SceneController()
        {
            InitializeComponent();
            LoadInitialScene();
        }

        public void LoadScene(Scene scene)
        {
            if (CurrentScene != null)
            {
                grid.Children.Remove(CurrentScene);
                CurrentScene.Close();
            }
            CurrentScene = scene;
            CurrentScene.SetSceneController(this);
            grid.Children.Add(CurrentScene);
            scene.Load();
        }

        private void LoadInitialScene()
        {
            LoadScene(InitialScene);          
        }
    }
}
