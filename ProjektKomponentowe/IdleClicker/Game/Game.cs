using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;

namespace IdleClicker
{
    public class Game
    {
        public int Points { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime GameDate { get; set; }
        public List<Building> ListOfBuildings { get; set; }
        public ListOfMaterials ListOfMaterials = new ListOfMaterials();
        double NightState { get; set; }

        public Game()
        {
            GameDate = new DateTime(1, 1, 1, 8, 0, 0);
            CreatedDate = DateTime.Now;
            ListOfBuildings = new List<Building>();
            NightState = 0;
            MB();
        }

        public string VillageType
        {
            get
            {
                for (int i = VillageRanks.Ranks.Count - 1; i >= 0 ; i--)
                {
                    if (VillageRanks.Ranks[i].Score <= Points) return VillageRanks.Ranks[i].Name;
                }
                return VillageRanks.Ranks[0].Name;
            }
        }

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="FileName">File path of the new xml file</param>
        public void Load(string FileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);

            XmlNode node = doc.FirstChild;

            FromXml(node);
        }

        public void FromXml(XmlNode root)
        {
            this.CreatedDate = Convert.ToDateTime(root.Attributes["CreatedDate"].Value);
            this.Points = Convert.ToInt32(root.Attributes["Points"].Value);
            this.GameDate = Convert.ToDateTime(root.Attributes["GameDate"].Value);
            this.NightState = Convert.ToInt32(root.Attributes["NightState"].Value);

            XmlNode Buildings = root.ChildNodes[0];
            foreach (XmlNode item in Buildings.ChildNodes)
            {
                ListOfBuildings.Find(a => a.Key == item.Attributes["Key"].Value).FromXml(item);
            }

            XmlNode Materials = root.ChildNodes[1];
            foreach (XmlNode item in Materials.ChildNodes)
            {
                ListOfMaterials.Materials.Find(a => a.Key == item.Attributes["Key"].Value).FromXml(item);
            }
        }

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="FileName">File path of the new xml file</param>
        public void Save()
        {
            this.GameDate = GameEngine.GameTimer.GameDate;
            this.NightState = GameEngine.GameTimer.NightState;

            string FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\SavedGames\" + CreatedDate.ToString("yyyy-MM-dd hh_mm_ss") + ".sav";

            using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fileStream))
            using (XmlTextWriter xmlWriter = new XmlTextWriter(sw))
            {
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 4;

                ToXml(xmlWriter);
            }
        }

        public void ToXml(XmlWriter w)
        {
            w.WriteStartElement("Game");
            w.WriteAttributeString("CreatedDate", CreatedDate.ToString());
            w.WriteAttributeString("Points", Points.ToString());
            w.WriteAttributeString("GameDate", GameDate.ToString());
            w.WriteAttributeString("NightState", NightState.ToString());
            w.WriteStartElement("Buildings");
            foreach (Building item in ListOfBuildings)
            {
                item.ToXml(w);
            }
            w.WriteEndElement();
            w.WriteStartElement("Materials");
            foreach (Material item in ListOfMaterials.Materials)
            {
                item.ToXml(w);
            }
            w.WriteEndElement();
            w.WriteEndElement();
        }

        public void MB()
        {
            GameEngine.SetListOfMaterials(ListOfMaterials);

            //-------------------------------------------------------------- UTWORZENIE DREWNA
            Material wood = new Material();
            wood.Key = "WOOD";
            wood.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/wood.png", UriKind.Relative));
            wood.CurrentAmount = 0;
            wood.BeginningIncreaseQuantity = 0;
            wood.CurrentIncreaseQuantity = 0;       
            ListOfMaterials.AddNewMaterial(wood);

            //-------------------------------------------------------------- UTWORZENIE ZŁOTA
            Material gold = new Material();
            gold.Key = "GOLD";
            gold.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/gold.png", UriKind.Relative));
            gold.CurrentAmount = 0;
            gold.BeginningIncreaseQuantity = 0;
            gold.CurrentIncreaseQuantity = 0;
            ListOfMaterials.AddNewMaterial(gold);

            //-------------------------------------------------------------- UTWORZENIE ŻYWNOŚCI
            Material food = new Material();
            food.Key = "FOOD";
            food.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/food.png", UriKind.Relative));
            food.CurrentAmount = 0;
            food.BeginningIncreaseQuantity = 0;
            food.CurrentIncreaseQuantity = 0;
            ListOfMaterials.AddNewMaterial(food);

            //-------------------------------------------------------------- UTWORZENIE BUDYNKÓW
            ListOfBuildings.Add(new Building("WOODCUTTER", "/IdleClicker;component/Resources/Buildings/Woodshed.png", 0, -400, -400, 20, BuildingType.Productive));
            ListOfBuildings.Add(new Building("FARM", "/IdleClicker;component/Resources/Buildings/Farm.png", 0, -100, 500, 999, BuildingType.Productive));
            ListOfBuildings.Add(new Building("TOWNHALL", "/IdleClicker;component/Resources/Buildings/TownHall.png", 0, 0, 0, 999, BuildingType.TownHall));
            ListOfBuildings.Add(new Building("WARHOUSE", "/IdleClicker;component/Resources/Buildings/Warehouse.png", 0, 400, -400, 999, BuildingType.Warehouse));
            ListOfBuildings.Add(new Building("MINE", "/IdleClicker;component/Resources/Buildings/Mine.png", 0, 900, -400, 999, BuildingType.Productive));

            //-------------------------------------------------------------- DODANIE PRODUKCJI SUROWCÓW
            //------------------------------------------ DREWNO
            ListOfBuildings[0].AddBonusCount(0, 0, 1, wood, 5, "Ilość produkowanego drewna");
            //------------------------------------------ ZŁOTO
            ListOfBuildings[4].AddBonusCount(0, 0, 1, gold, 2, "Ilość produkowanego złota");
            //------------------------------------------ ŻYWNOŚC
            ListOfBuildings[1].AddBonusCount(0, 0, 1, food, 3, "Ilość produkowanej żywności");


            //-------------------------------------------------------------- DODANIE ALGORYTMÓW WYMAGAŃ
            //------------------------------------------- MAGAZYN

            Requirement woodWH = new Requirement(0, wood);
            woodWH.SetAlgorithm((level, rV) => { return level == 1 ? 200 : rV + 100; });
            Requirement goldWH = new Requirement(0, gold);
            goldWH.SetAlgorithm((level, rV) => { return level == 1 ? 100 : (level % 3 == 0 ? rV + 50 : rV); });
            Requirement foodWH = new Requirement(0, food);
            foodWH.SetAlgorithm((level, rV) => { return level == 5 ? 100 : (level > 5 ? rV + 30 : rV); });
            Requirement townhallWH = new Requirement(0, ListOfBuildings[2]);
            townhallWH.SetAlgorithm((level, rV) => { return level == 10 ? 5 : (level > 10 && level % 5 == 0 ? rV + 1 : rV); });

            ListOfBuildings[3].Requirements.Add(woodWH);
            ListOfBuildings[3].Requirements.Add(goldWH);
            ListOfBuildings[3].Requirements.Add(foodWH);
            ListOfBuildings[3].Requirements.Add(townhallWH);

            BuildingAction increaseCapacity = new BuildingAction(1, 0, 1);
            increaseCapacity.Actions += () => {
                wood.MaxAmountMaterial += 1000;
                food.MaxAmountMaterial += 1000;
                gold.MaxAmountMaterial += 1000;
            };
            ListOfBuildings[3].AddBonus(increaseCapacity);

            //------------------------------------------- LEŚNICZÓWKA

            Requirement woodWC = new Requirement(0, wood);
            woodWC.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 20; });
            Requirement goldWC = new Requirement(0, gold);
            goldWC.SetAlgorithm((level, rV) => { return level == 1 ? 15 : rV + 10; });
            Requirement foodWC = new Requirement(0, food);
            foodWC.SetAlgorithm((level, rV) => { return level == 5 ? 40 : rV + 30; });

            ListOfBuildings[0].Requirements.Add(woodWC);
            ListOfBuildings[0].Requirements.Add(goldWC);
            ListOfBuildings[0].Requirements.Add(foodWC);

            //-------------------------------------------- KOPALNIA

            Requirement woodMN = new Requirement(100, wood);
            woodMN.SetAlgorithm((level, rV) => { return rV + 30; });
            Requirement goldMN = new Requirement(0, gold);
            goldMN.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 50; });
            Requirement foodMN = new Requirement(0, food);
            foodMN.SetAlgorithm((level, rV) => { return level == 1 ? 50 : rV + 50; });

            ListOfBuildings[4].Requirements.Add(woodMN);
            ListOfBuildings[4].Requirements.Add(goldMN);
            ListOfBuildings[4].Requirements.Add(foodMN);

            //-------------------------------------------- FARMA

            Requirement woodFR = new Requirement(250, wood);
            woodFR.SetAlgorithm((level, rV) => { return rV + 50; });
            Requirement goldFR = new Requirement(150, gold);
            goldFR.SetAlgorithm((level, rV) => { return rV + 25; });
            Requirement foodFR = new Requirement(0, food);
            foodFR.SetAlgorithm((level, rV) => { return level == 1 ? 45 : rV + 10; });

            ListOfBuildings[1].Requirements.Add(woodFR);
            ListOfBuildings[1].Requirements.Add(goldFR);
            ListOfBuildings[1].Requirements.Add(foodFR);

            //-------------------------------------------- RATUSZ

            Requirement woodTH = new Requirement(700, wood);
            woodTH.SetAlgorithm((level, rV) => { return rV + 200; });
            Requirement goldTH = new Requirement(500, gold);
            goldTH.SetAlgorithm((level, rV) => { return rV + 150; });
            Requirement foodTH = new Requirement(350, food);
            foodTH.SetAlgorithm((level, rV) => { return rV + 100; });
            Requirement woodcutterTH = new Requirement(5, ListOfBuildings[0]);
            woodcutterTH.SetAlgorithm((level, rV) => { return level % 5 == 0 ? rV + 2 : rV; });
            Requirement mineTH = new Requirement(4, ListOfBuildings[4]);
            mineTH.SetAlgorithm((level, rV) => { return level % 3 == 0 ? rV + 1 : rV; });
            Requirement farmTH = new Requirement(3, ListOfBuildings[1]);
            farmTH.SetAlgorithm((level, rV) => { return level % 5 == 0 ? rV + 1 : rV; });


            ListOfBuildings[2].Requirements.Add(woodTH);
            ListOfBuildings[2].Requirements.Add(goldTH);
            ListOfBuildings[2].Requirements.Add(foodTH);
            ListOfBuildings[2].Requirements.Add(woodcutterTH);
            ListOfBuildings[2].Requirements.Add(mineTH);
            ListOfBuildings[2].Requirements.Add(farmTH);
        }
    }
}
