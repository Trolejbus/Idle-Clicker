using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IdleClicker
{
    /// <summary>
    /// Interaction logic for UpdatePanel.xaml
    /// </summary>
    public partial class UpdatePanel : UserControl
    {
        public UpdatePanel()
        {
            /*Program.OnVersionChange += Program_OnVersionChange;
            Program.OnNewestVersionChange += Program_OnNewestVersionChange;
            Program.OnChangeLogsChange += Program_OnChangeLogsChange;
            Program.OnUpdateToVersionChange += Program_OnUpdateToVersionChange;


            UpdateModule.OnStartUpdateAction += UpdateModule_OnStartUpdateAction;
            UpdateModule.OnEndUpdateAction += UpdateModule_OnEndUpdateAction;
            UpdateModule.OnStatusTextChanged += UpdateModule_OnStatusTextChanged;
            

            InitializeComponent();
            Program_OnNewestVersionChange(Program.NewestVersion);
            Program_OnVersionChange(Program.Version);*/
        }

        /*private void Program_OnUpdateToVersionChange(ProgramVersion newVersion)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => Program_OnUpdateToVersionChange(newVersion));
                return;
            }

            updateToButton.Visibility = Visibility.Visible;
            updateToButton.Content = "Zaktualizuj do " + newVersion;
        }*/

        private void Program_OnChangeLogsChange(string changeLogs)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => Program_OnChangeLogsChange(changeLogs));
                return;
            }

            changeLogTextBox.Text = changeLogs;
        }

        private void UpdateModule_OnStatusTextChanged(string newStatusText, bool isError)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateModule_OnStatusTextChanged(newStatusText,isError));
                return;
            }

            updateState.Foreground = isError ? Brushes.Red : new SolidColorBrush(Color.FromArgb(255, 228, 181, 123));
            updateState.Text = newStatusText;
        }

        private void UpdateModule_OnEndUpdateAction()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateModule_OnEndUpdateAction());
                return;
            }

            updateProgressBar.IsIndeterminate = false;
        }

        private void UpdateModule_OnStartUpdateAction()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => UpdateModule_OnStartUpdateAction());
                return;
            }

            updateProgressBar.IsIndeterminate = true;
        }

        /*private void Program_OnNewestVersionChange(ProgramVersion newestVersion)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => Program_OnNewestVersionChange(newestVersion));
                return;
            }

            string newestVersionValue = newestVersion == null ? "Nie sprawdzono" : newestVersion.ToString();

            NewestVersionTextBlock.Text = "Aktualna wersja: " + newestVersionValue;
        }

        private void Program_OnVersionChange(ProgramVersion version)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(() => Program_OnVersionChange(version));
                return;
            }

            VersionTextBlock.Text = "Wersja programu: " + version;
        }*/

        private async void mainButton_Click(object sender, RoutedEventArgs e)
        {
            /*Task checkIfUpToDateTask = new Task( () => UpdateModule.CheckIfUpToDate());
            checkIfUpToDateTask.Start();
            await checkIfUpToDateTask;*/
        }

        private async void mainButton_Click_1(object sender, RoutedEventArgs e)
        {
            /*Task upToDateTask = new Task(() => UpdateModule.UpToDate());
            upToDateTask.Start();
            await upToDateTask;*/
        }
    }
}
