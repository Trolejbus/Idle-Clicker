using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerCommon
{
    public delegate void OnProgressDelegate(double progress);
    public delegate void OnStartUpdateActionDelegate();
    public delegate void OnEndUpdateActionDelegate();

    public class UpdaterActionSetting
    {
        public bool Progress1Visible { get; set; }
        public bool Progress2Visible { get; set; }
        public bool Progress1IsIndeterminate { get; set; }
        public bool Progress2IsIndeterminate { get; set; }
    }

    public interface IUpdaterAction
    {
        UpdaterActionSetting GetSettings();
        void Start(string destination);

        event OnProgressDelegate OnProgress1;
        event OnProgressDelegate OnProgress2;

        event OnStartUpdateActionDelegate OnStartUpdateAction;
        event OnEndUpdateActionDelegate OnEndUpdateAction;
    }
}
