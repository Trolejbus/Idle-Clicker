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
    /// Interaction logic for Building.xaml
    /// </summary>
    public partial class BuildingControl : Button
    {
        Building building;
        public BuildingControl(Building building)
        {
            InitializeComponent();
            this.building = building;
            this.building.OnChangeLevel += UpdateState;
        }

        public void SetPosition(int x, int y)
        {
            this.Margin = new Thickness((double)x, (double)y, 0, 0);
        }

        public void SetImage(ImageSource Image)
        {
            BitmapSource btsource = (BitmapSource)Image;
            buildingImage.Source = Image;
            this.Width = btsource.PixelWidth;
            this.Height = btsource.PixelHeight;
        }

        public void UpdateState(int level)
        {
            if (level == 1)
            {
                this.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            GameScene gameScene = (GameScene)mainWindow.sceneController.CurrentScene;

            TownHallPanel buildingWindow = new TownHallPanel(building.IconSource, Dictionary.dictionary[building.Key]);
            buildingWindow.AddNewParagraph("Podstawowe informacje", "Obecny level budynku:", building.Level.ToString(), "Maksymalny level budynku:", building.MaxLevel.ToString());
            buildingWindow.AddNewParagraph("Inne informacje");

            for (int i = 0; i < building.BuildingInfos.Count; i++)
            {
                buildingWindow.AddNewLine(building.BuildingInfos[i].Label, building.BuildingInfos[i].Value);
            }

            gameScene.canvas.Children.Add(buildingWindow);
        }
    }
}
