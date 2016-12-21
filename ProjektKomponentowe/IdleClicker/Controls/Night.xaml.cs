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
    /// Interaction logic for Night.xaml
    /// </summary>
    public partial class Night : UserControl
    {
        public Night()
        {
            InitializeComponent();
            GameEngine.GameTimer.CheckOnNight += OnChangeTimeOfDay;
        }
        public void OnChangeTimeOfDay(bool CheckIfNight)
        {
            this.nightEffectGrid.Opacity = CheckIfNight ? 0.6 : 0;
        }
    }
}
