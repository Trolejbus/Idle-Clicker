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
    /// Interaction logic for IntroScene.xaml
    /// </summary>
    public partial class IntroScene : Scene
    {
        public IntroScene()
        {
            InitializeComponent();

            mediaElement.Source = new Uri(Config.ApplicationExecutablePath + @"\data\Intro.avi");
            mediaElement.MediaEnded += MediaElement_MediaEnded;           
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            sceneController.LoadScene(new MainMenuScene());
        }

        public override void Close()
        {
            
        }

        public override void Load()
        {
            mediaElement.Play();
        }
    }
}
