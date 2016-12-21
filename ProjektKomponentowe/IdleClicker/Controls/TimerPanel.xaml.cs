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
    /// Interaction logic for TimerPanel.xaml
    /// </summary>
    public partial class TimerPanel : UserControl
    {
        public TimerPanel()
        {
            InitializeComponent();
            IsVisibleChanged += TimerPanel_IsVisibleChanged;
        }

        private void TimerPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                GameEngine.GameTimer.OnTick += GameTimer_OnTick;
            }
            else if (Visibility == Visibility.Hidden)
            {
                GameEngine.GameTimer.OnTick -= GameTimer_OnTick;
            }
        }

        private void GameTimer_OnTick(long Tick)
        {
            DayTB.Text = GameEngine.GameTimer.GameDate.Day.ToString();
            HourTB.Text = GameEngine.GameTimer.GameDate.ToString("HH:mm");
        }       
    }
}
