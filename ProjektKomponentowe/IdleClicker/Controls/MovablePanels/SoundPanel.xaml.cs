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
    /// Interaction logic for SoundPanel.xaml
    /// </summary>
    public partial class SoundPanel : UserControl
    {
        public SoundPanel()
        {
            InitializeComponent();
            musicVolume.Value = (int)(AudioPlayer.Volume * 100);
            soundsVolume.Value = (int)(AudioPlayer.QuickSoundVolume * 100);
        }

        private void musicVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (musicLevel != null)
            {
                AudioPlayer.Volume = (float)(musicVolume.Value / 100);
                musicLevel.Text = (int)musicVolume.Value + "%";
            }
        }

        private void soundsVolume_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (soundsLevel != null)
            {
                AudioPlayer.QuickSoundVolume = (float)(soundsVolume.Value / 100);
                soundsLevel.Text = (int)soundsVolume.Value + "%";
            }
        }
    }
}
