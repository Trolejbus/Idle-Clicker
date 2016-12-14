using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdleClicker.Web
{
    delegate void OnCheckChangeLogStartDelegate();
    delegate void OnCheckChangeLogStopDelegate(string ChangeLogs);

    class UpdateModule
    {
        HttpClient webClient;
        public event OnCheckChangeLogStartDelegate OnCheckChangeLogStart;
        public event OnCheckChangeLogStopDelegate OnCheckChangeLogStop;

        async Task<string> GetChangeLogs()
        {
            OnCheckChangeLogStart();

            if(webClient == null)
                webClient = new HttpClient();
            Task<string> getStringTask = webClient.GetStringAsync("localhost/IdleClicker/ChangeLogs.html");
            await getStringTask;
            OnCheckChangeLogStop(getStringTask.Result);

            return getStringTask.Result;
        }
    }
}
