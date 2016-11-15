using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace IdleClicker
{
    class Config
    {
        public static string ApplicationExecutablePath { get { return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); } }
    }
}
