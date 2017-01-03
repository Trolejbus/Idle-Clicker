using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Drawing.Drawing2D;
using System.Windows.Media;
using IdleClickerCommon;

namespace IdleClicker
{
    class HiderBehaviour : Behavior<CustomButton>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += HideElement;
        }

        private void HideElement(object sender, RoutedEventArgs e)
        {
            FrameworkElement panel = GetMoveablePanel();
            if (panel != null)
                panel.Visibility = Visibility.Hidden;
        }

        private FrameworkElement GetMoveablePanel()
        {
            FrameworkElement c1 = (FrameworkElement)((FrameworkElement)AssociatedObject.Parent).Parent;
            if (c1.Parent != null)
            {
                if (((FrameworkElement)c1.Parent).Parent != null)
                {
                    return (FrameworkElement)((FrameworkElement)c1.Parent).Parent;
                }
            }
            return null;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Click -= HideElement;
        }
    }
}
