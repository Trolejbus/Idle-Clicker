using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker
{
    public delegate void OnVersionChangeDelegate(Version newVersion);
    public delegate void OnChangeLogsChangeDelegate(string changeLogs);

    public static class Program
    {
        private static Version version;
        private static Version newestVersion;
        private static string changeLogs;

        public static string ChangeLogs
        {
            get
            {
                return changeLogs;
            }
            set
            {
                changeLogs = value;
                if (OnChangeLogsChange != null)
                    OnChangeLogsChange(changeLogs);
            }
        }
        public static event OnChangeLogsChangeDelegate OnChangeLogsChange;

        public static Version Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                if (OnVersionChange != null)
                    OnVersionChange(version);
            }
        }
        public static event OnVersionChangeDelegate OnVersionChange;

        public static Version NewestVersion
        {
            get
            {
                return newestVersion;
            }
            set
            {
                newestVersion = value;
                if (OnNewestVersionChange != null)
                    OnNewestVersionChange(newestVersion);
            }
        }
        public static event OnVersionChangeDelegate OnNewestVersionChange;

        static Program()
        {
            version = new Version(1, 0, 0);
        }
    }
}
