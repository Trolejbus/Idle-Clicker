using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public struct Version
    {
        public int _MainVersion;// { get; set; }
        public int _SubVersion;// { get; set; }
        public int _DetailVersion;// { get; set; }

        public Version(int MainVersion,int SubVersion, int DetailVersion)
        {
            _MainVersion = MainVersion;
            _SubVersion = SubVersion;
            _DetailVersion = DetailVersion;
        }

        public override string ToString()
        {
            return _MainVersion + "." + _SubVersion + "." + _DetailVersion;
        }
    }
}
