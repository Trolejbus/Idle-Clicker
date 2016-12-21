using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IdleClicker
{
    public delegate void BonusAction(int level);

    public class Building : IRequired
    {
        public ActionList BonusList { get; private set; }
        public String Key { get; private set; }
        public ImageSource IconSource { get; private set; }
        public int Level { get; private set; }
        public Point Location { get; private set; }
        public int MaxLevel{ get; private set; }
        public List<Requirement> Requirements { get; }
        //public ImageSource IconSource { get; private set; }

        public event BonusAction OnChangeLevel;

        // AK: Konstruktor budowany na potrzeby testów
        public Building(String key, string iconSource, int level, int xPosition, int yPosition, int maxLevel)
        {
            Key = key;
            Level = level;
            IconSource = new BitmapImage(new Uri(iconSource, UriKind.RelativeOrAbsolute));
            Requirements = new List<Requirement>();
            BonusList = new ActionList();
            Location = new Point(xPosition, yPosition);
            //IconSource = new BitmapImage(new Uri(imageSource, UriKind.Relative));
            MaxLevel = maxLevel;
        }

        // AK: Dodane na potrzeby testów
        public void AddRequirement(int requireValue, IRequired requiredObject)
        {
            Requirements.Add(new Requirement(requireValue, requiredObject));
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

        public bool Build()
        {
            if (MaxLevel != -1 && Level >= MaxLevel) return false;

            foreach (Requirement item in Requirements)
            {
                if (!item.CheckIfCompleted()) return false;
            }

            Level++;
            foreach (Requirement item in Requirements)
            {
                if (item is IReducible)
                    ((IReducible)item).ReduceMaterial(item.RequireValue);

                item.UpdateToLevel(Level);

            }
            
            OnChangeLevel(Level);
            BonusList.Execute(Level);
            return true;
        }

        public double GetValue()
        {
            return (double)Level;
        }
    }
}
