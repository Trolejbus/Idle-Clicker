using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerCommon
{
    public delegate void OnVersionChangeDelegate(ProgramVersion newVersion);
    public delegate void OnChangeLogsChangeDelegate(string changeLogs);

    public static class Program
    {
        private static ProgramVersion version;
        private static ProgramVersion updateToVersion;
        private static ProgramVersion newestVersion;
        private static string changeLogs;

        private static string webSite = @"http://www.IdleClicker.hexcore.pl";
        //private static string webSite = @"http://localhost/IdleClicker";

        public static string WebSite { get { return webSite; } }

        public static string ApplicationExecutablePath { get; set; }


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

        public static ProgramVersion Version
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

        public static ProgramVersion NewestVersion
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

        public static event OnVersionChangeDelegate OnUpdateToVersionChange;

        public static ProgramVersion UpdateToVersion
        {
            get
            {
                return updateToVersion;
            }
            set
            {
                if (UpdateToVersion == null || UpdateToVersion.CompareTo(Version) > 0)
                {
                    updateToVersion = value;
                    if (OnUpdateToVersionChange != null)
                        OnUpdateToVersionChange(updateToVersion);
                }
            }
        }

        public static event OnVersionChangeDelegate OnNewestVersionChange;

        static Program()
        {
            version = new ProgramVersion(1, 0, 0);
        }
    }
}
