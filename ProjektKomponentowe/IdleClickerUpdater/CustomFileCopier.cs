using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleClickerUpdater
{
    public delegate void ProgressChangeDelegate(double Percentage);
    public delegate void CompletedDelegate();

    class CustomFileCopier
    {
        public event ProgressChangeDelegate OnProgressChanged;
        public event CompletedDelegate OnCompleted;

        public string SourceFilePath { get; set; }
        public string DestinationFilePath { get; set; }

        public CustomFileCopier(string source, string destination)
        {
            this.SourceFilePath = source;
            this.DestinationFilePath = destination;

            OnProgressChanged += delegate { };
            OnCompleted += delegate { };
        }

        public void Copy()
        {
            byte[] buffer = new byte[1024 * 1024];

            using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
            {
                long fileLength = source.Length;
                using (FileStream dest = new FileStream(DestinationFilePath, FileMode.Create, FileAccess.Write))
                {
                    long totalBytes = 0;
                    int currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        double percentage = (double)totalBytes * 100.0 / fileLength;
                        dest.Write(buffer, 0, currentBlockSize);

                        OnProgressChanged(percentage);
                    }

                    OnCompleted();
                }
            }
        }
    }
}
