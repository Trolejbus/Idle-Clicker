using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IdleClickerCommon;
using Microsoft.Win32;
using System.IO;

namespace IdleClickerUpdater
{
    /// <summary>
    /// Interaction logic for UpdaterPanel.xaml
    /// </summary>
    public partial class UpdaterPanel : UserControl
    {
        bool instalationMode = false;
        string InstallFileDestination = Program.ApplicationExecutablePath;
        string downloadInitialPath = Program.ApplicationExecutablePath;
        string installProgram = @"Idle Clicker Install.exe";
        string downloadPath;

        public UpdaterPanel()
        {
            InitializeComponent();
            instalationMode = !UpdateModule.CheckIfInstalled();
            UpdateModule.OnStatusTextChanged += UpdateModule_OnStatusTextChanged;
            if (instalationMode)
            {
                SetStartMode(UpdaterMode.Instalation);
            }
            else
            {
                SetStartMode(UpdaterMode.Update);
            }

            downloadPath = downloadInitialPath;
        }

        private void UpdateModule_OnStatusTextChanged(string newStatusText, bool isError)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateModule_OnStatusTextChanged(newStatusText, isError));
                return;
            }

            ProgressStatusTextBox.Text = newStatusText;
        }

        private void SetStartMode(UpdaterMode mode)
        {
            if(mode == UpdaterMode.Instalation)
            {
                StartMode();
            }
            else if(mode == UpdaterMode.Update)
            {
                UpdateMode();
            }
        }

        private void StartMode(InstalationState step = InstalationState.Welcome)
        {
            HideAll();

            switch (step)
            {
                case InstalationState.Welcome:
                    InstalationPanel1.Visibility = Visibility.Visible;
                    break;
                case InstalationState.SelectPath:
                    DownloadPathTextBox.Text = downloadPath + @"\" + installProgram;
                    InstalationPanel2.Visibility = Visibility.Visible;
                    break;
                case InstalationState.Options:
                    InstalationPanel3.Visibility = Visibility.Visible;
                    break;
                case InstalationState.DownloadInstaller:
                    ProgressHeaderTextBox.Text = "Pobieranie instalatora";
                    ProgressStatusTextBox.Text = "";
                    ProgressPanel.Visibility = Visibility.Visible;
                    break;
                case InstalationState.EndInstalation:
                    InstalationPanel4.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void UpdateMode()
        {
            HideAll();
        }


        private void HideAll()
        {
            foreach (FrameworkElement item in grid.Children)
            {
                if(item.Tag is String && ((String)item.Tag) == "ToHide")
                {
                    item.Visibility = Visibility.Hidden;
                }
            }
        }

        private void customButton_Click(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.SelectPath);
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.SelectedPath = downloadInitialPath;
            fbd.Description = "Wybierz folder, do którego zostanie pobrany plik instalatora.";

            System.Windows.Forms.DialogResult result = fbd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);
                downloadPath = fbd.SelectedPath;
                DownloadPathTextBox.Text = downloadPath + @"\" + installProgram;
            }            
        }

        private void customButton2_Copy_Click(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.Welcome);
        }

        private void customButton2_Click(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.SelectPath);
        }

        private void customButton2_Copy1_Click(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.SelectPath);
        }

        private void customButton2_Click_1(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.Options);
        }

        private void customButton1_Click(object sender, RoutedEventArgs e)
        {
            StartMode(InstalationState.DownloadInstaller);
            StartAction(new DownloadInstaller());
        }

        private void SetSettings(UpdaterActionSetting settings)
        {
            progressBar.Visibility = settings.Progress1Visible ? Visibility.Visible : Visibility.Hidden;
            progressBar2.Visibility = settings.Progress2Visible ? Visibility.Visible : Visibility.Hidden;
            progressBar.IsIndeterminate = settings.Progress1IsIndeterminate;
            progressBar2.IsIndeterminate = settings.Progress2IsIndeterminate;
        }

        private void StartAction(IUpdaterAction a)
        {
            UpdaterActionSetting settings = a.GetSettings();
            SetSettings(settings);
            a.OnEndUpdateAction += A_OnEndUpdateAction;

            Task task = new Task(() => a.Start(downloadPath));
            task.Start();
        }

        private void A_OnEndUpdateAction()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => A_OnEndUpdateAction());
                return;
            }

            StartMode(InstalationState.EndInstalation);
        }

        /*InstallUpdateModule ium = new InstallUpdateModule();

            ium.FileProgress += OnFileProgress;
            ium.Progress += Ium_Progress;

            Task<bool> task = new Task<bool>(ium.DownloadInstallation);
            task.Start();*/
        private void ProgressBar1_Progress(double progress)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ProgressBar1_Progress(progress));
                return;
            }

            progressBar.Value = (int)progress;
        }

        void ProgressBar2_Progress(double progress)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => ProgressBar2_Progress(progress));
                return;
            }

            progressBar2.Value = (int)progress;
        }

        private void customButton2_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void customButton3_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(downloadPath + @"\IdleClicker Installer.exe");
            Application.Current.Shutdown();
        }
    }
}
