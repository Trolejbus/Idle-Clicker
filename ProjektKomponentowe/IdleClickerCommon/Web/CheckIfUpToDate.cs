using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerCommon
{
    class CheckIfUpToDate : IUpdaterAction
    {
        public event OnEndUpdateActionDelegate OnEndUpdateAction;
        public event OnProgressDelegate OnProgress1;
        public event OnProgressDelegate OnProgress2;
        public event OnStartUpdateActionDelegate OnStartUpdateAction;

        public UpdaterActionSetting GetSettings()
        {
            return new UpdaterActionSetting()
            {
                Progress1IsIndeterminate = true,
                Progress1Visible = false,
                Progress2IsIndeterminate = false,
                Progress2Visible = false
            };
        }

        public void Start(string destination)
        {
            throw new NotImplementedException();
        }
    }
}
