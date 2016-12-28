using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public class Version : IComparable<Version>
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

        public void FromString(string versionString)
        {
            try
            {
                string[] subString = versionString.Split('.');
                if (subString.Length != 3) throw new Exception("Zły format wersji");

                _MainVersion = Convert.ToInt32(subString[0]);
                _SubVersion = Convert.ToInt32(subString[1]);
                _DetailVersion = Convert.ToInt32(subString[2]);
            }
            catch
            {
                throw;
            }
        }

        public override string ToString()
        {
            return _MainVersion + "." + _SubVersion + "." + _DetailVersion;
        }

        public int CompareTo(Version other)
        {
            int Main = _MainVersion - other._MainVersion;
            int Sub = _SubVersion - other._SubVersion;
            int Detail = _DetailVersion - other._DetailVersion;

            if (Main != 0) return Main;
            if (Sub != 0) return Sub;
            return Detail;
        }
    }
}
