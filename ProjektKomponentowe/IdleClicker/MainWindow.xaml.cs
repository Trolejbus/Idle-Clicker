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

namespace IdleClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;   
        }

        /// <summary>
        /// Metoda wywoływana podczas zamykania głównego okna gry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AudioPlayer.StopMusic();
            //AudioPlayer.StopSound();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (sceneController.CurrentScene.GetType() == typeof(GameScene))
                {

                    UIElement ui = null;
                    //((GameScene)sceneController.CurrentScene).canvas.Children.Add(btn);
                    foreach (var item in ((GameScene)sceneController.CurrentScene).canvas.Children)
                    {
                        if (item.GetType() == typeof(TownHallPanel))
                        {
                            ui = (TownHallPanel)item;
                        }
                    }

                    if (ui != null)
                    { 
                        ((GameScene)sceneController.CurrentScene).canvas.Children.Remove(ui);
                    }
                    
                }
            }
        }
    }
}
