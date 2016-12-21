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
            List<Requirement> buildingRequirements = building.Requirements;
            for (int i = 0; i < buildingRequirements.Count; i++)
            {
                if (!buildingRequirements[i].CheckIfCompleted())
                {
                    // AK: Teraz sprawdź typ i działaj.
                }
            }
        }
    }
}
