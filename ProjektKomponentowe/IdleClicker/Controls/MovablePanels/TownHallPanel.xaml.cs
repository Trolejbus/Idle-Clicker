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
    /// Interaction logic for TownHallPanel.xaml
    /// </summary>
    public partial class TownHallPanel : UserControl
    {
        public TownHallPanel()
        {
            InitializeComponent();
        }

        public TownHallPanel(ImageSource imageURL, string title)
        {
            InitializeComponent();
            image.Source = imageURL;
            TitleWindowTB.Text = title;
        }

        public void AddNewParagraph(string title, params string[] lines)
        {
            ListOfLines newParagraph = new ListOfLines();
            newParagraph.TitleTextBlock.Text = title;

            for (int i = 0; i < lines.Length; i++)
            {
                if (i < lines.Length-1 )
                {
                    newParagraph.StackOfLineSP.Children.Add(new LineForPanel(lines[i], lines[i + 1]));
                    i++;
                }
            }

            this.StackOfListsSP.Children.Add(newParagraph);
        }
    }
}
