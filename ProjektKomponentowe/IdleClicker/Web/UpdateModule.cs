using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IdleClicker
{
    delegate void OnCheckChangeLogStartDelegate();
    delegate void OnCheckChangeLogStopDelegate(string ChangeLogs);
    delegate void ErrorhandlerDelegate(string Error);

    class UpdateModule
    {
        HttpClient webClient;
        public event OnCheckChangeLogStartDelegate OnCheckChangeLogStart;
        public event OnCheckChangeLogStopDelegate OnCheckChangeLogStop;
        public event ErrorhandlerDelegate OnError;

        public async Task<string> GetChangeLogs()
        {
            try
            {
                if (OnCheckChangeLogStart != null)
                    OnCheckChangeLogStart();
                if (webClient == null)
                    webClient = new HttpClient();
                Task<string> getStringTask = webClient.GetStringAsync("http://localhost/IdleClicker/ChangeLogs.html");
                await getStringTask;
                if (OnCheckChangeLogStop != null)
                    OnCheckChangeLogStop(getStringTask.Result);

                return getStringTask.Result;
            }
            catch (Exception e)
            {
                OnError(e.Message);
            }
            return null;
        }

        public async Task<List<string>> GetFilesToDownload()
        {
            try
            {
                if (OnCheckChangeLogStart != null)
                    OnCheckChangeLogStart();
                if (webClient == null)
                    webClient = new HttpClient();
                Task<string> getStringTask = webClient.GetStringAsync("http://localhost/IdleClicker/ChangeLogs.html");
                await getStringTask;
                if (OnCheckChangeLogStop != null)
                    OnCheckChangeLogStop(getStringTask.Result);

                //return getStringTask.Result;
            }
            catch (Exception e)
            {
                OnError(e.Message);
            }
            return null;
        }
    }
}
