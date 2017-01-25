using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// 

    public partial class GameScene : Scene
    {
        public GameScene()
        {
            InitializeComponent();
            MainPanel.MenuButton.Click += (o, i) => { menuPanel.Visibility = menuPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            buildButton.Click += (o, i) => { buildPanel.Visibility = buildPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed; };
            /*menuPanel.exitButton.Click += (o, i) => {
                AudioPlayer.StopMusic();
                AudioPlayer.RemoveAllMusic();
                AudioPlayer.AddMusic("Resources/Music/main_menu_slaby_end.mp3");
                AudioPlayer.PlayMusic();
                sceneController.LoadScene(new MainMenuScene());
            };
            menuPanel.exitButton.Click += (o, i) => { sceneController.LoadScene(new MainMenuScene()); };
            menuPanel.SoundButton.Click += (o, i) => { canvas.Children.Add(new SoundPanel()); };
            menuPanel.loadGameButton.Click += (o, i) => { canvas.Children.Add(new LoadGamePanel()); };

            //  villageBackground.Source = new BitmapImage(new Uri("/IdleClicker;component/Resources/Images/VillageBackground.png", UriKind.Relative));
            
            GameEngine.Enabled = true;

            foreach (Material item in GameEngine.Game.ListOfMaterials.Materials)
            {
                item.onChangeMaterial += MainPanel.UpdateCountOfMaterials;
            }
            GameEngine.Game.ListOfMaterials.NewMaterial += this.MainPanel.UpdateKindOfMaterials;

            foreach (Material item in GameEngine.Game.ListOfMaterials.Materials)
            {
                MainPanel.UpdateKindOfMaterials(item);
            }

            buildPanel.ImportBuildings(GameEngine.Game.ListOfBuildings);
            buildingsLayer.UpdateBuildingsOnLayer(GameEngine.Game.ListOfBuildings);


            AudioPlayer.StopMusic();
            AudioPlayer.RemoveAllMusic();
            AudioPlayer.AddMusic("Resources/Music/wyspa_soundtrack.mp3");
            AudioPlayer.AddMusic("Resources/Music/ptaki.mp3");
            AudioPlayer.PlayMusic();
        }

        public override void Close()
        {
            base.Close();
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            this.console.listBox.Items.Add(this.console.textBox.Text);
            this.console.textBox.Text = "";

        }
    }
}
