using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace IdleClicker
{
    public delegate void OnStatusTextChangedDelegate(string newStatusText, bool isError);
    public delegate void OnStartUpdateActionDelegate();
    public delegate void OnEndUpdateActionDelegate();

    public static class UpdateModule
    {
        private static string statusText;

        public static event OnStatusTextChangedDelegate OnStatusTextChanged;
        public static event OnStartUpdateActionDelegate OnStartUpdateAction;
        public static event OnEndUpdateActionDelegate OnEndUpdateAction;

        public static string StatusText
        {
            get
            {
                return statusText;
            }
            private set
            {
                OnStatusTextChanged(value, isError);
                statusText = StatusText;
            }
        }

        public static bool isError { get; private set; }

        public async static void UpToDate()
        {

        }

        public async static void CheckIfUpToDate()
        {
            try
            {
                if(OnStartUpdateAction != null)
                    OnStartUpdateAction();
                isError = false;
                StatusText = "Sprawdzanie wersji";

                Task<Version> newestVersion = GetLastVersion();
                await newestVersion;

                Program.NewestVersion = newestVersion.Result;

                if (newestVersion.Result.CompareTo(Program.Version) > 0)
                {
                    Task<string> t1 = GetChangeLogs();
                    await t1;

                    string result = t1.Result;
                    isError = false;
                    StatusText = "Dostępna nowa wersja!";
                }
                else
                {
                    isError = false;
                    StatusText = "Program aktualny!";
                }
            }
            catch(Exception e)
            {
                isError = true;
                StatusText = "Błąd połączenia";
            }
            finally
            {
                if (OnEndUpdateAction != null)
                    OnEndUpdateAction();
            }
        }

        private static async Task<List<Version>> GetVersions()
        {
            HttpClient webClient;
            webClient = new HttpClient();
            Task<string> getStringTask = webClient.GetStringAsync("http://www.IdleClicker.hexcore.pl/NewestVersion.php");
            //Task<string> getStringTask = webClient.GetStringAsync("http://localhost/IdleClicker/NewestVersion.php");
            await getStringTask;

            List<Version> versions = new List<Version>();
            string[] stringVersions = getStringTask.Result.Split('|');
            foreach (string item in stringVersions)
            {
                Version newVersion = new Version(0,0,0);
                newVersion.FromString(item);
                versions.Add(newVersion);
            }
        
            return versions;
        }

        private static async Task<string> GetChangeLogs()
        {
            HttpClient webClient;
            webClient = new HttpClient();
            Task<string> getStringTask = webClient.GetStringAsync("http://www.IdleClicker.hexcore.pl/ChangeLogs.php");
            //Task<string> getStringTask = webClient.GetStringAsync("http://localhost/IdleClicker/ChangeLogs.php");
            await getStringTask;

            return getStringTask.Result;
        }

        private static async Task<Version> GetLastVersion()
        {
            Task<List<Version>> Versions = GetVersions();
            await Versions;
            return Versions.Result.First<Version>();
        }
    }
}
