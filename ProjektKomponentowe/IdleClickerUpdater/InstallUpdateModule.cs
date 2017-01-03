using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IdleClickerUpdater
{
    public delegate void OnProgressDelegate(double progress);

    class InstallUpdateModule
    {
        private int allFiles = 0;
        private int filesCopied = 0;

        public bool Install()
        { 
            try
            {
                allFiles = CountFiles();
                CreateDirs();
                CopyFiles();
                return true;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
        }

        public event OnProgressDelegate Progress;
        public event OnProgressDelegate FileProgress;

        private void CreateDirs()
        {
            CreateDirs(Config.GamePath + @"\Update", Config.GamePath);
        }

        private void CreateDirs(string updateDirs, string gameDirs)
        {
            DirectoryInfo updateDir = new DirectoryInfo(updateDirs);

            foreach (DirectoryInfo item in updateDir.GetDirectories())
            {
                string dir = gameDirs + @"\" + item;
                Directory.CreateDirectory(dir);
                CreateDirs(updateDirs + @"\" + item,dir);
            }
        }

        private int CountFiles()
        {
            return CountFiles(Config.GamePath + @"\Update");
        }

        private int CountFiles(string dir)
        {
            DirectoryInfo updateDir = new DirectoryInfo(dir);

            int allFiles = updateDir.GetFiles().Length;

            foreach (DirectoryInfo item in updateDir.GetDirectories())
            {
                allFiles += CountFiles(dir + @"\" + item);
            }
            return allFiles;
        }

        private void CopyFiles()
        {
            CopyFiles(Config.GamePath + @"\Update", Config.GamePath);
        }

        private void CopyFiles(string updateDirs, string gameDirs)
        {
            DirectoryInfo updateDir = new DirectoryInfo(updateDirs);

            foreach (FileInfo item in updateDir.GetFiles())
            {
                CustomFileCopier cfc = new CustomFileCopier(item.FullName, gameDirs + @"\" + item);
                cfc.OnProgressChanged += (c) => 
                {
                    if (FileProgress != null)
                        FileProgress(c);
                };
                cfc.OnCompleted += () =>
                {
                    if (FileProgress != null)
                        FileProgress(100.0);
                } ;
                cfc.Copy();

                filesCopied++;
                Progress((double)filesCopied / (double)allFiles * 100.0);
            }
            
            foreach (DirectoryInfo item in updateDir.GetDirectories())
            {
                string dir = gameDirs + @"\" + item;
                Directory.CreateDirectory(dir);
                CreateDirs(updateDirs + @"\" + item,dir);
            }
        }
    }
}
