using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate int RequireAlgorithm(int level);

    class Requirement
    {
        public int RequireValue { get; private set; }
        protected IRequired requiredObject;
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
