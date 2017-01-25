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
    /// Interaction logic for BuildingsLayer.xaml
    /// </summary>
    public partial class BuildingsLayer : UserControl
    {
        public BuildingsLayer()
        {
            InitializeComponent();
        }

        public void UpdateBuildingsOnLayer(List<Building> buildings)
        {
            BuildingControl buildingInstance;
            foreach (Building item in buildings)
            {
                    buildingInstance = new BuildingControl(item);
                    buildingInstance.SetPosition(item.Location.X, item.Location.Y);
                    buildingInstance.SetImage(item.IconSource);
                    buildingsContainer.Children.Add(buildingInstance);
             
            }
        }
    }
}
