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
    /// Interaction logic for SlotScene.xaml
    /// </summary>
    public partial class SlotScene : Scene
    {
        public SlotScene()
        {
            InitializeComponent();
            GameLoader.LoadGames();

            foreach (Game item in GameLoader.games)
            {
                CreateSlotPanel(item);
            }
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            GameLoader.games.Add(game);
            CreateSlotPanel(game);
        }

        private void CreateSlotPanel(Game game)
        {
            Slot newSlot = new IdleClicker.Slot();
            newSlot.Margin = new Thickness(0, 10, 0, 0);
            newSlot.AssignSlot(game);
            newSlot.Tag = game;
            newSlot.MouseLeftButtonUp += NewSlot_MouseLeftButtonUp;
            stackPanel.Children.Insert(stackPanel.Children.Count - 1, newSlot);
            scrollviewer.ScrollToEnd();
        }

        private void NewSlot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GameEngine.Game = (Game)((Control)sender).Tag;
            GameEngine.GameTimer.GameDate = GameEngine.Game.GameDate;
            GameScene gameScene = new GameScene();      
            sceneController.LoadScene(gameScene);
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            sceneController.LoadScene(new MainMenuScene());
        }
    }
}
