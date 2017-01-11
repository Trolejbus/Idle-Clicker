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
    /// Interaction logic for SettingsScene.xaml
    /// </summary>
    public partial class SettingsScene : Scene
    {

        public SettingsScene()
        {
            InitializeComponent();
            musicSlider.Value = (int)(AudioPlayer.Volume * 100.0f);
            //soundSlider.Value = (int)(player.SoundVolume * 100.0f);
        }

        private void changeGrid(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            changeButtonStyleToDefault();
            btn.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A4A27"));

            String gridName = btn.Name.Substring(3, btn.Name.Length - 3);

            Grid gridToDisplay = (Grid)FindName("grid" + gridName);

            GridVisibilityOff();
            gridToDisplay.Visibility = Visibility.Visible;

        }

        public void GridVisibilityOff()
        {
            gridSound.Visibility = Visibility.Hidden;
            gridGraphic.Visibility = Visibility.Hidden;
            gridGame.Visibility = Visibility.Hidden;
            gridUpdate.Visibility = Visibility.Hidden;
        }

        public void changeButtonStyleToDefault()
        {
            btnGame.Background = btnSound.Background = btnGraphic.Background = btnUpdate.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#492412"));
        }

        private void musicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (musicValueTextBlock != null)
            {
                AudioPlayer.Volume = (float)(musicSlider.Value / 100);
                musicValueTextBlock.Text = (int)musicSlider.Value + "%";
            }

        }

        private void soundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (soundValueTextBlock != null)
            {
                //player.SoundVolume = (float)(soundSlider.Value / 100);
                soundValueTextBlock.Text = (int)soundSlider.Value + "%";
            }

        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            sceneController.LoadScene(new MainMenuScene());
        }
    }
}
