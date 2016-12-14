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
        IGameEngine gameEngine;

        public GameScene()
        {
            InitializeComponent();

            gameEngine = new GameEngine();
            gameEngine.SetActionList(new ActionList());
            gameEngine.Enabled = true;

            ListOfMaterials lista = new ListOfMaterials();
            lista.NewMaterial += this.MainPanel.UpdateKindOfMaterials;

            Material m = new Material(gameEngine);
            m.Key = "WOOD";
            m.IconSource = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/wood.png", UriKind.Relative));
            m.CurrentAmount = 200;
            m.BeginningIncreaseQuantity = 2;
            m.CurrentIncreaseQuantity = 2;
            m.onChangeMaterial += this.MainPanel.UpdateCountOfMaterials;

            lista.AddNewMaterial(m);


            // akcja która wykona się za 5 tików zegara, 3 razy, w odstępach 2 sekundowych
            Action naszaAkcja = new Action(0,10,2);
            //Action naszaAkcja = new Action(0,10);
            naszaAkcja.Actions += delegate() 
            {
                m.BoostMaterial();
            };

            gameEngine.GetActionList().AddAction(naszaAkcja);

            Action bonus = new Action(10, 1, 0);

            bonus.Actions += delegate ()
            {
                m.AddBonusQuantity(1, 1);
            };

            gameEngine.GetActionList().AddAction(bonus);
            
        }
    }
}
