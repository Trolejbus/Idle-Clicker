using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IdleClicker
{
    public static class ExtensionsForLineForPanel
    {
        public static readonly DependencyProperty Text = DependencyProperty.RegisterAttached("Text",
           typeof(string), typeof(LineForPanel), new FrameworkPropertyMetadata(null));

        public static string GetText(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            return (string)element.GetValue(Text);
        }
        public static void SetText(UIElement element, string value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            ((LineForPanel)element).TextLineTB.Text = value;
            element.SetValue(Text, value);
        }

        public static readonly DependencyProperty Number = DependencyProperty.RegisterAttached("Number",
           typeof(string), typeof(LineForPanel), new FrameworkPropertyMetadata(null));

        public static string GetNumber(UIElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            return (string)element.GetValue(Text);
        }
        public static void SetNumber(UIElement element, string value)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            ((LineForPanel)element).TextNumberTB.Text = value;
            element.SetValue(Text, value);
        }
    }
}
