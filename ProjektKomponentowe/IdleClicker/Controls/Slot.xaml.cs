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
    /// Interaction logic for Slot.xaml
    /// </summary>
    public partial class Slot : UserControl
    {
        public Slot()
        {
            InitializeComponent();
        }

        public void AssignSlot(Game game)
        {
            slotName.Text = game.VillageType;
            slotDate.Text = game.CreatedDate.ToString("hh:mm:ss dd.MM.yyyy");
            slotPoints.Text = "Punkty: " + game.Points;

        }

        private void SlotMouseEnter(object sender, MouseEventArgs e)
        {
            Slot slot = sender as Slot;
            slot.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B0000"));
        }

        private void SlotMouseLeave(object sender, MouseEventArgs e)
        {
            Slot slot = sender as Slot;
            slot.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#492412"));
        }
    }
}
