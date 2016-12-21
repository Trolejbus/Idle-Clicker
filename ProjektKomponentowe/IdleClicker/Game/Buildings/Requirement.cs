using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate int RequireAlgorithm(int level);

    public class Requirement
    {
        public int RequireValue { get; private set; }
        public IRequired requiredObject;
        public bool CheckIfCompleted()
        {
            return requiredObject.GetValue() >= RequireValue;
        }
        public RequireAlgorithm requireAlgorithm;
        public void UpdateToLevel(int level)
        {
            RequireValue = requireAlgorithm(level);
        }
       
    }
}
