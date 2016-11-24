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
    /// Interaction logic for SlotScene.xaml
    /// </summary>
    public partial class SlotScene : Scene
    {
        public SlotScene()
        {
            InitializeComponent();
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            Slot newSlot = new IdleClicker.Slot();
            newSlot.Margin = new Thickness(0, 10, 0, 0);
            stackPanel.Children.Insert(stackPanel.Children.Count - 1, newSlot);
            scrollviewer.ScrollToEnd();
        }
    }
}
