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

          //  villageBackground.Source = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/VillageBackground.png", UriKind.Relative));

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

            TownHallPanel ratusz = new TownHallPanel();
            ratusz.image.Source = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/wood.png", UriKind.Relative));
            ListOfLines surowce = new ListOfLines();
            surowce.TitleTextBlock.Text = "Aktualny przyrost surowców:";
            ListOfLines levele = new ListOfLines();
            levele.TitleTextBlock.Text = "Aktualne poziomy budynków:";
            LineForPanel line1 = new LineForPanel();
            LineForPanel line2 = new LineForPanel();
            LineForPanel line3 = new LineForPanel();
            line1.TextLineTB.Text = "Złoto";
            line1.TextNumberTB.Text = "200";
            line2.TextLineTB.Text = "Leśniczówka";
            line2.TextNumberTB.Text = "3";
            line3.TextLineTB.Text = "Farma";
            line3.TextNumberTB.Text = "999";

            surowce.StackOfLineSP.Children.Add(line1);
            surowce.StackOfLineSP.Children.Add(line3);
            levele.StackOfLineSP.Children.Add(line2);

            ratusz.StackOfListsSP.Children.Add(surowce);
            ratusz.StackOfListsSP.Children.Add(levele);

            canvas.Children.Add(ratusz);
        }
    }
}
