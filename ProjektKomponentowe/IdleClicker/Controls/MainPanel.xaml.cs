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
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda dodająca kontrolkę nowego surowca, jeśli taki zostanie dodany
        /// </summary>
        /// <param name="M"></param>
        public void UpdateKindOfMaterials(Material M)
        {
            ResourceInfo newMaterial = new ResourceInfo();
            newMaterial.Name = M.Key;
            newMaterial.ResourceIconME.Source = M.IconSource;
            newMaterial.ResourceCountTB.Text = M.CurrentAmount.ToString();

            resourcesSP.Children.Add(newMaterial);
        }

        public void UpdateCountOfMaterials(Material M)
        {
            for (int i = 0; i < this.resourcesSP.Children.Count; i++)
            {
                if (((ResourceInfo)this.resourcesSP.Children[i]).Name == M.Key)
                {
                    ((ResourceInfo)this.resourcesSP.Children[i]).ResourceCountTB.Text = M.CurrentAmount.ToString();
                }
            }
            
        }
    }

}
