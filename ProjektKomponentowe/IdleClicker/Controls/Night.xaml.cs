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
        public void OnChangeTimeOfDay(int CheckIfRise)
        {
            if (CheckIfRise == 1)
            {
                if (this.nightEffectGrid.Opacity<0.9)
                {
                    this.nightEffectGrid.Opacity += 0.04;
                }
            }
            if (CheckIfRise == 2)
            {
                if (nightEffectGrid.Opacity>0)
                {
                    this.nightEffectGrid.Opacity -= 0.04;
                }
            }

            
            //this.nightEffectGrid.Opacity = CheckIfNight ? 0.6 : 0;
        }
    }
}
