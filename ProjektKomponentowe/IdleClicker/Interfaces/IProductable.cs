using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public interface IProductable
    {
        string Key { get; set; }
        void AddIncreaseCount(double value);
    }
}
