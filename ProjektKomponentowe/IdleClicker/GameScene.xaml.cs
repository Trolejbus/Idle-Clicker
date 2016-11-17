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
        public GameScene()
        {
            InitializeComponent();

            GameEngine gameEngine = new GameEngine();
            gameEngine.Enabled = true;

            Action action = new Action(5);
            action.Actions += Action_Actions;
            Action action1 = new Action(2);
            action1.Actions += Action_Actions;

            gameEngine.ActionList.AddAction(action);
            gameEngine.ActionList.AddAction(action1);
        }

        private void Action_Actions()
        {
            MessageBox.Show("Działa!");
        }
    }
}
