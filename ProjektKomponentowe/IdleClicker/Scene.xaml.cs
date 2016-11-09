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
    /// Interaction logic for Scene.xaml
    /// </summary>
    public abstract partial class Scene : UserControl
    {
        public Scene()
        {
            InitializeComponent();
        }

        public abstract void Load();
        public abstract void Close();
    }
}
