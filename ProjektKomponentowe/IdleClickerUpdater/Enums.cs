using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerUpdater
{
    public enum UpdaterMode
    {
        Instalation = 1,
        Update = 2,
    };

    public enum InstalationState
    {
        Welcome = 1,
        SelectPath = 2,
        Options = 3,
        DownloadInstaller = 4,
        EndInstalation = 5
    };

    public enum UpdateState
    {
        CheckIfUpToDate = 1,
        DownloadUpdate = 2,
        CheckIfRunning = 3,
        UpdateNotAvailable = 4,
        UpdateEnd = 5
    };
}
