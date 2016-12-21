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
    /// Interaction logic for BuildingsPanel.xaml
    /// </summary>
    public partial class BuildingsPanel : UserControl
    {
        public BuildingsPanel()
        {
            InitializeComponent();
        }

        public void ImportBuildings(List<Building> buildings)
        {
            ResourceInfo newResourceInfo;
            BuildingItem newItem;
            OtherRequirementLine line;

            foreach (Building item in buildings)
            {
                newItem = new BuildingItem();
                
                newItem.BuildingImage.Source = item.IconSource;
                newItem.BuildingName.Text = Dictionary.dictionary[item.Key];
                newItem.BuildingLevelValue.Text = item.Level.ToString();

                for (int i = 0; i < item.Requirements.Count; i++)
                {
                    // MP: Może nie działać
                    if (item.Requirements[i].requiredObject.RequireType.HasFlag(RequireType.BuildingOrMaterial))
                    {

                        newResourceInfo = new ResourceInfo();
                        newResourceInfo.ResourceIconME.Source = item.Requirements[i].requiredObject.GetIcon();
                        newResourceInfo.ResourceCountTB.Text = item.Requirements[i].RequireValue.ToString();

                        if (item.Requirements[i].requiredObject.RequireType == RequireType.Material)
                            newItem.BuildingRequireMaterials.Children.Add(newResourceInfo);
                        else
                            newItem.BuildingRequireMaterials.Children.Add(newResourceInfo);                   
                    }
                    else
                    {
                        line = new OtherRequirementLine();
                        line.ResourceTextTB.Text = item.Requirements[i].RequireValue.ToString();
                        if (item.Requirements[i].requiredObject.GetIcon() != null)
                        {
                            line.ResourceTextTB.Text = item.Requirements[i].RequireValue.ToString();
                        }
                        else
                        {
                            line.ResourceIconME.Visibility = Visibility.Hidden;
                        }
                    }
                }

                this.BuildingListStackPanel.Children.Add(newItem);
            }


        }
    }
}
