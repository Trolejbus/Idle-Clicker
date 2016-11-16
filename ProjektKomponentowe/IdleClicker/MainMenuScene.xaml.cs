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
    /// Interaction logic for MainMenuScene.xaml
    /// </summary>
    public partial class MainMenuScene : Scene
    {
        public MainMenuScene()
        {
            InitializeComponent();
        }

        private void authorsButton_Click(object sender, RoutedEventArgs e)
        {
            sceneController.LoadScene(new AuthorsScene());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            sceneController.LoadScene(new GameScene());
        }
    }
}
