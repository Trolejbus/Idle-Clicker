using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for GameScene.xaml
    /// </summary>
    public partial class GameScene : Scene
    {
        GameEngine gameEngine;

        public GameScene()
        {
            InitializeComponent();

            gameEngine = new GameEngine();
            gameEngine.Enabled = true;

            MainPanel.resourcesSP.Children.Add(new Controls.ResourceInfo());

            // akcja która wykona się za 5 tików zegara, 3 razy, w odstępach 2 sekundowych
            //Action naszaAkcja = new Action(5,3,2);
            /*Action naszaAkcja = new Action(0,10);
            naszaAkcja.Actions += NaszaAkcja;

            gameEngine.ActionList.AddAction(naszaAkcja);*/
        }
    }
}
