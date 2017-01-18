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
    /// 

    public partial class GameScene : Scene
    {
        public GameScene()
        {
            InitializeComponent();
            MainPanel.MenuButton.Click += (o, i) => { menuPanel.Visibility = menuPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            buildButton.Click += (o, i) => { buildPanel.Visibility = buildPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            menuPanel.exitButton.Click += (o, i) => {
                AudioPlayer.StopMusic();
                AudioPlayer.RemoveAllMusic();
                AudioPlayer.AddMusic("Resources/Music/main_menu_slaby_end.mp3");
                AudioPlayer.PlayMusic();
                sceneController.LoadScene(new MainMenuScene());
            };
            menuPanel.exitButton.Click += (o, i) => { sceneController.LoadScene(new MainMenuScene()); };
            menuPanel.SoundButton.Click += (o, i) => { canvas.Children.Add(new SoundPanel()); };
            menuPanel.loadGameButton.Click += (o, i) => { canvas.Children.Add(new LoadGamePanel()); };

            //  villageBackground.Source = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/VillageBackground.png", UriKind.Relative));

            GameEngine.Enabled = true;

            ListOfMaterials lista = new ListOfMaterials();
            GameEngine.SetListOfMaterials(lista);

            //-------------------------------------------------------------- UTWORZENIE LISTY SUROWCÓW
            lista.NewMaterial += this.MainPanel.UpdateKindOfMaterials;

            //-------------------------------------------------------------- UTWORZENIE DREWNA
            Material wood = new Material();
            wood.Key = "WOOD";
            wood.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/wood.png", UriKind.Relative));
            wood.CurrentAmount = 0;
            wood.BeginningIncreaseQuantity = 0;
            wood.CurrentIncreaseQuantity = 0;
            wood.onChangeMaterial += this.MainPanel.UpdateCountOfMaterials;
            lista.AddNewMaterial(wood);

            //-------------------------------------------------------------- UTWORZENIE ZŁOTA
            Material gold = new Material();
            gold.Key = "GOLD";
            gold.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/gold.png", UriKind.Relative));
            gold.CurrentAmount = 0;
            gold.BeginningIncreaseQuantity = 0;
            gold.CurrentIncreaseQuantity = 0;
            gold.onChangeMaterial += this.MainPanel.UpdateCountOfMaterials;
            lista.AddNewMaterial(gold);

            //-------------------------------------------------------------- UTWORZENIE ŻYWNOŚCI
            Material food = new Material();
            food.Key = "FOOD";
            food.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/food.png", UriKind.Relative));
            food.CurrentAmount = 0;
            food.BeginningIncreaseQuantity = 0;
            food.CurrentIncreaseQuantity = 0;
            food.onChangeMaterial += this.MainPanel.UpdateCountOfMaterials;
            lista.AddNewMaterial(food);


            //-------------------------------------------------------------- UTWORZENIE LISTY BUDYNKÓW
            List<Building> listOfBuildings = new List<Building>();

            //-------------------------------------------------------------- UTWORZENIE BUDYNKÓW
            listOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Buildings/Woodshed.png", 0, -400, -400, 20, BuildingType.Productive));
            listOfBuildings.Add(new Building("FARM", "/IdleClicker;component/Resources/Buildings/Farm.png", 0, -100, 500, 999, BuildingType.Productive));
            listOfBuildings.Add(new Building("TOWNHALL", "/IdleClicker;component/Resources/Buildings/TownHall.png", 0, 0, 0, 999, BuildingType.TownHall));
            listOfBuildings.Add(new Building("WARHOUSE", "/IdleClicker;component/Resources/Buildings/Warehouse.png", 0, 400, -400, 999, BuildingType.Warehouse));
            listOfBuildings.Add(new Building("MINE", "/IdleClicker;component/Resources/Buildings/Mine.png", 0, 900, -400, 999, BuildingType.Productive));

            //-------------------------------------------------------------- DODANIE PRODUKCJI SUROWCÓW
            //------------------------------------------ DREWNO
            listOfBuildings[0].AddBonusCount(0, 0, 1, wood, 5, "Ilość produkowanego drewna");
            //------------------------------------------ ZŁOTO
            listOfBuildings[4].AddBonusCount(0, 0, 1, gold, 2, "Ilość produkowanego złota");
            //------------------------------------------ ŻYWNOŚC
            listOfBuildings[1].AddBonusCount(0, 0, 1, food, 3, "Ilość produkowanej żywności");


            //-------------------------------------------------------------- DODANIE ALGORYTMÓW WYMAGAŃ
            //------------------------------------------- MAGAZYN

            Requirement woodWH = new Requirement(0, wood);
            woodWH.SetAlgorithm((level, rV) => { return level == 1 ? 200 : rV + 100; });
            Requirement goldWH = new Requirement(0, gold);
            goldWH.SetAlgorithm((level, rV) => { return level == 1 ? 100 : (level % 3 == 0 ? rV + 50 : rV); });
            Requirement foodWH = new Requirement(0, food);
            foodWH.SetAlgorithm((level, rV) => { return level == 5 ? 100 : (level > 5 ? rV + 30 : rV); });
            Requirement townhallWH = new Requirement(0, listOfBuildings[2]);
            townhallWH.SetAlgorithm((level, rV) => { return level == 10 ? 5 : (level > 10 && level % 5 == 0 ? rV + 1 : rV); });

            listOfBuildings[3].Requirements.Add(woodWH);
            listOfBuildings[3].Requirements.Add(goldWH);
            listOfBuildings[3].Requirements.Add(foodWH);
            listOfBuildings[3].Requirements.Add(townhallWH);

            BuildingAction increaseCapacity = new BuildingAction(1, 0, 1);
            increaseCapacity.Actions += () => {
                wood.MaxAmountMaterial += 1000;
                food.MaxAmountMaterial += 1000;
                gold.MaxAmountMaterial += 1000;
            };
            listOfBuildings[3].AddBonus(increaseCapacity);

            //------------------------------------------- LEŚNICZÓWKA

            Requirement woodWC = new Requirement(0, wood);
            woodWC.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 20; });
            Requirement goldWC = new Requirement(0, gold);
            goldWC.SetAlgorithm((level, rV) => { return level == 1 ? 15 : rV + 10; });
            Requirement foodWC = new Requirement(0, food);
            foodWC.SetAlgorithm((level, rV) => { return level == 5 ? 40 : rV + 30; });

            listOfBuildings[0].Requirements.Add(woodWC);
            listOfBuildings[0].Requirements.Add(goldWC);
            listOfBuildings[0].Requirements.Add(foodWC);

            //-------------------------------------------- KOPALNIA

            Requirement woodMN = new Requirement(100, wood);
            woodMN.SetAlgorithm((level, rV) => { return rV + 30; });
            Requirement goldMN = new Requirement(0, gold);
            goldMN.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 50; });
            Requirement foodMN = new Requirement(0, food);
            foodMN.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 50; });

            listOfBuildings[4].Requirements.Add(woodMN);
            listOfBuildings[4].Requirements.Add(goldMN);
            listOfBuildings[4].Requirements.Add(foodMN);

            //-------------------------------------------- FARMA

            Requirement woodFR = new Requirement(250, wood);
            woodFR.SetAlgorithm((level, rV) => { return rV + 50; });
            Requirement goldFR = new Requirement(150, gold);
            goldFR.SetAlgorithm((level, rV) => { return rV + 25; });
            Requirement foodFR = new Requirement(0, food);
            foodFR.SetAlgorithm((level, rV) => { return level == 1 ? 45 : rV + 10; });

            listOfBuildings[1].Requirements.Add(woodFR);
            listOfBuildings[1].Requirements.Add(goldFR);
            listOfBuildings[1].Requirements.Add(foodFR);

            //-------------------------------------------- RATUSZ

            Requirement woodTH = new Requirement(700, wood);
            woodTH.SetAlgorithm((level, rV) => { return rV + 200; });
            Requirement goldTH = new Requirement(500, gold);
            goldTH.SetAlgorithm((level, rV) => { return rV + 150; });
            Requirement foodTH = new Requirement(350, food);
            foodTH.SetAlgorithm((level, rV) => { return rV + 100; });
            Requirement woodcutterTH = new Requirement(5, listOfBuildings[0]);
            woodcutterTH.SetAlgorithm((level, rV) => { return level % 5 == 0 ? rV + 2 : rV; });
            Requirement mineTH = new Requirement(4, listOfBuildings[4]);
            mineTH.SetAlgorithm((level, rV) => { return level % 3 == 0 ? rV + 1 : rV; });
            Requirement farmTH = new Requirement(3, listOfBuildings[1]);
            farmTH.SetAlgorithm((level, rV) => { return level % 5 == 0 ? rV + 1 : rV; });


            listOfBuildings[2].Requirements.Add(woodTH);
            listOfBuildings[2].Requirements.Add(goldTH);
            listOfBuildings[2].Requirements.Add(foodTH);
            listOfBuildings[2].Requirements.Add(woodcutterTH);
            listOfBuildings[2].Requirements.Add(mineTH);
            listOfBuildings[2].Requirements.Add(farmTH);

            buildPanel.ImportBuildings(listOfBuildings);
            buildingsLayer.UpdateBuildingsOnLayer(listOfBuildings);


            AudioPlayer.StopMusic();
            AudioPlayer.RemoveAllMusic();
            AudioPlayer.AddMusic("Resources/Music/wyspa_soundtrack.mp3");
            AudioPlayer.AddMusic("Resources/Music/ptaki.mp3");
            AudioPlayer.PlayMusic();
        }

        public override void Close()
        {
            base.Close();
        }

    }
}
