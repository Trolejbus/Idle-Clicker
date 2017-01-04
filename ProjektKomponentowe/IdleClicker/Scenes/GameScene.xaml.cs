using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for GameScene.xaml
    /// </summary>
    public partial class GameScene : Scene
    {   
        public GameScene()
        {
            InitializeComponent();

            MainPanel.MenuButton.Click += (o, i) => { menuPanel.Visibility = menuPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            buildButton.Click += (o, i) => { buildPanel.Visibility = buildPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            menuPanel.exitButton.Click += (o, i) => { sceneController.LoadScene(new MainMenuScene());  };
            //  villageBackground.Source = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/VillageBackground.png", UriKind.Relative));

            GameEngine.Enabled = true;

            ListOfMaterials lista = new ListOfMaterials();
            lista.NewMaterial += this.MainPanel.UpdateKindOfMaterials;

            Material m = new Material();
            m.Key = "WOOD";
            m.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/wood.png", UriKind.Relative));
            m.CurrentAmount = 200;
            m.BeginningIncreaseQuantity = 2;
            m.CurrentIncreaseQuantity = 2;
            m.onChangeMaterial += this.MainPanel.UpdateCountOfMaterials;
            lista.AddNewMaterial(m);


            TickAction bonus = new TickAction(10, 1, 0);
            bonus.Actions += delegate ()
            {
                m.AddBonusQuantity(1, 1);
            };
            GameEngine.ActionList.AddAction(bonus);

            TickAction bonus100 = new TickAction(10, 5, 3);
            bonus100.Actions += delegate ()
            {
                m.CurrentAmount += 1;
            };
            GameEngine.ActionList.AddAction(bonus100);

            TownHallPanel ratusz = new TownHallPanel("/IdleClicker;component/Resources/Images/wood.png", "Dupa");
            ratusz.AddNewParagraph("Aktualny przyrost surowców:", "Złoto", "200", "Drewno", "500", "Żywność", "1000");
            ratusz.AddNewParagraph("Aktualne poziomy budynków:", "Farma", "999","Leśniczówka", "600" );
            
            canvas.Children.Add(ratusz);

            // AK: Dodawanie budynków w celach testowych
            List<Building> listOfBuildings = new List<Building>();
            listOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Images/wood.png", 2, 200, 200, 999));
            listOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Images/gold.png", 1, 300, 300, 999));


            listOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Buildings/TownHall.png", 0, 0, 0, 999));
            listOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Images/food.png", 0, 0, 0, 999));


            Requirement req = new Requirement(100, m);
            req.requireAlgorithm = (level) => { return 100 * level; };
            listOfBuildings[1].Requirements.Add(req);
            listOfBuildings[1].AddRequirement(5, listOfBuildings[0]);

            listOfBuildings[3].AddRequirement(1, listOfBuildings[2]);
            // ----------------------------------------

            buildPanel.ImportBuildings(listOfBuildings);
            buildingsLayer.UpdateBuildingsOnLayer(listOfBuildings);


            // AK: odtwarzacz
            AudioPlayer.Play("Resources/Music/bratanki.mp3");
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
