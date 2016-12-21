using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
        public int MaxLevel { get; private set; }
        public List<Requirement> Requirements { get; }

        public event BonusAction OnChangeLevel;

        public Building()
        {
            Requirements = new List<Requirement>();
            BonusList = new ActionList();
        }

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
