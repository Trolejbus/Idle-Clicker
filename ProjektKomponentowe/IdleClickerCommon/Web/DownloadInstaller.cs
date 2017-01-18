using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerCommon
{
    public class DownloadInstaller : IUpdaterAction
    {
        public event OnEndUpdateActionDelegate OnEndUpdateAction;
        public event OnStartUpdateActionDelegate OnStartUpdateAction;

        public event OnProgressDelegate OnProgress1;
        public event OnProgressDelegate OnProgress2;
        

        public UpdaterActionSetting GetSettings()
        {
            return new UpdaterActionSetting()
            {
                Progress1IsIndeterminate = true,
                Progress1Visible = true,
                Progress2Visible = false
            };
        }

        public void Start(string destination)
        {
            try
            {
                if (OnStartUpdateAction != null)
                    OnStartUpdateAction();
                UpdateModule.DownloadInstallFile(destination);
            }
            catch(Exception e)
            {

            }
            finally
            {
                if (OnEndUpdateAction != null)
                    OnEndUpdateAction();
            }
        }
    }
}
