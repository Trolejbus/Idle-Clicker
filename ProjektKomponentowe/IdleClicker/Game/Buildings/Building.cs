using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace IdleClicker
{
    public delegate void BonusAction(int level);

    public class Building : IRequired
    {
        private ActionList bonusList;
        public String Key { get; private set; }
        public ImageSource IconSource { get; private set; }
        public int Level { get; private set; }
        public Point Location { get; private set; }
        public int MaxLevel{ get; private set; }
        public List<Requirement> Requirements { get; }
        public BuildingType BuildingType { get; private set; }
        public List<BuildingInfo> BuildingInfos;


        //public ImageSource IconSource { get; private set; }

        public event BonusAction OnChangeLevel;

        // AK: Konstruktor budowany na potrzeby testów
        public Building(String key, string iconSource, int level, int xPosition, int yPosition, int maxLevel, BuildingType buildingType)
        {
            Key = key;
            Level = level;
            IconSource = new BitmapImage(new Uri(iconSource, UriKind.RelativeOrAbsolute));
            Requirements = new List<Requirement>();
            bonusList = new ActionList();
            Location = new Point(xPosition, yPosition);
            //IconSource = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            MaxLevel = maxLevel;
            BuildingType = buildingType;
            BuildingInfos = new List<BuildingInfo>();
        }

        public void ToXml(XmlWriter w)
        {
            w.WriteStartElement("Building");
            w.WriteAttributeString("Key", Key);
            w.WriteAttributeString("Level", Level.ToString());
            w.WriteEndElement();
        }

        public void FromXml(XmlNode root)
        {
            for (int i = 0; i < Convert.ToInt32(root.Attributes["Level"].Value); i++)
            {
                Build(true);
            }
        }

        public void AddBuildingInfo(string key, string label, string value)
        {
            BuildingInfos.Add(new BuildingInfo() {
                    Key = key,
                    Label = label,
                    Value = value
            });
        }

        public void AddBonus(BuildingAction bonus)
        {
            bonus.SetBuilding(this);
            bonusList.AddAction(bonus);
        }

        public void AddBonusCount(int triggerValue, int executeTimes, int frequencyTimes, IProductable productableObject, double bonusCount, string displayText, bool display = true)
        {
            
            BuildingAction bonus = new BuildingAction(triggerValue, executeTimes, frequencyTimes);
            BuildingInfo b1 = BuildingInfos.Find(BuildingInfo => BuildingInfo.Key == productableObject.Key);

            if (display)
            {
                //if (b1 != null)
                //{
                //    b1.Value = (Convert.ToDouble(b1.Value) + bonusCount).ToString();
                //}
                if (b1 == null)
                {
                    b1 = new BuildingInfo()
                    {
                        Key = productableObject.Key,
                        Value = 0.ToString(),
                        Label = displayText
                    };
                    BuildingInfos.Add(b1);
                }
                bonus.Actions += () =>
                {
                    productableObject.AddIncreaseCount(bonusCount);
                    b1.Value = (Convert.ToDouble(b1.Value) + bonusCount).ToString();
                };
            }
            else
            {
                bonus.Actions += () =>
                {
                    productableObject.AddIncreaseCount(bonusCount);
                };
            }

            AddBonus(bonus);
        }

        // AK: Dodane na potrzeby testów
        public void AddRequirement(int requireValue, IRequired requiredObject)
        {
            Requirement r = new Requirement(requireValue, requiredObject);
            r.SetAlgorithm((level, requireValueInRequirement) => { return 0; });
            Requirements.Add(r);
        }

        public void AddRequirement(Requirement requirement)
        {
            Requirements.Add(requirement);
        }
        // ------------------------------------

        public RequireType RequireType
        {
            get
            {
                return RequireType.Building;
            }
        }

        public ImageSource GetIcon()
        {
            return IconSource;
        }

        public bool Build(bool forceBuild = false)
        {
            if (MaxLevel != -1 && Level >= MaxLevel) return false;

            if(!forceBuild)
            foreach (Requirement item in Requirements)
            {
                if (!item.CheckIfCompleted()) return false;
            }

            Level++;
            for (int i = 0; i < Requirements.Count; i++)
            {
                if (!forceBuild)
                {
                    if (Requirements[i].requiredObject is IReducible)
                        ((IReducible)Requirements[i].requiredObject).ReduceMaterial(Requirements[i].RequireValue);
                }

                Requirements[i].UpdateToLevel(Level, Requirements[i].RequireValue);
            }
            
            if(OnChangeLevel != null)
                OnChangeLevel(Level);
            bonusList.Execute(Level);
            return true;
        }

        public double GetValue()
        {
            return (double)Level;
        }
    }
}
