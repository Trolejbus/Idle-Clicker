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
    /// Interaction logic for MenuPanel.xaml
    /// </summary>
    public partial class MenuPanel : UserControl
    {
        public MenuPanel()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            AudioPlayer.StopMusic();
            AudioPlayer.RemoveAllMusic();
            AudioPlayer.AddMusic("Resources/Music/main_menu_slaby_end.mp3");
            AudioPlayer.PlayMusic();
            ((MainWindow)Application.Current.MainWindow).sceneController.LoadScene(new MainMenuScene());
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            GameEngine.Game.Save();
        }
    }
}
