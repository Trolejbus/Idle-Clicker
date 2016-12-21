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
    /// Interaction logic for BuildingItem.xaml
    /// </summary>
    public partial class BuildingItem : UserControl
    {
        Building building;
        Brush defaultBrush = new SolidColorBrush(Color.FromRgb(228, 181, 123));

        public BuildingItem(Building building)
        {
            InitializeComponent();
            this.building = building;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoPopup.IsOpen = true;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoPopup.IsOpen = false;
        }

        public void UpdateRequirements()
        {
            for (int i = 0; i < building.Requirements.Count; i++)
            {
                if (!building.Requirements[i].CheckIfCompleted())
                {
                    if ((building.Requirements[i].requiredObject.RequireType & RequireType.BuildingOrMaterial) != 0)
                        ((ResourceInfo)this.FindName("w" + i)).ResourceCountTB.Foreground = Brushes.Red;
                    else
                        ((OtherRequirementLine)this.FindName("w" + i)).ResourceTextTB.Foreground = Brushes.Red;
                }
                else
                {

                    if ((building.Requirements[i].requiredObject.RequireType & RequireType.BuildingOrMaterial) != 0)
                        ((ResourceInfo)this.FindName("w" + i)).ResourceCountTB.Foreground = defaultBrush;
                    else
                        ((OtherRequirementLine)this.FindName("w" + i)).ResourceTextTB.Foreground = defaultBrush;
                }
            }
        }
    }
}
