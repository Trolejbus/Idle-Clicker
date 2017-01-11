using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate int RequireAlgorithm(int level, int requireValue);

    public class Requirement
    {
        public int RequireValue { get; private set; }
        public IRequired requiredObject;

        public bool CheckIfCompleted()
        {
            return requiredObject.GetValue() >= RequireValue;
        }
        private RequireAlgorithm requireAlgorithm = null;

        public void UpdateToLevel(int level, int requireValue)
        {
            if (requireAlgorithm(level, requireValue) != 0)
            {
                RequireValue = requireAlgorithm(level, requireValue);
            }
        }
       
        public void SetAlgorithm(RequireAlgorithm algorithm)
        {
            requireAlgorithm = algorithm;
        }

        // AK: Dodane na potrzeby testów ------------
        public Requirement(int requireValue, IRequired requiredObject)
        {
            RequireValue = requireValue;
            this.requiredObject = requiredObject;
        }
        // ------------------------------------------
    }
}
