using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IdleClicker
{
    class ArcPanel : Panel
    {
        protected int ItemsSet;

        public double StartAngle { get; set; }
        public double Radius { get; set; }

        public override void EndInit()
        {
            Button bt = new Button();
            this.Children.Add(bt);
            base.EndInit();
        }

        // Make the panel as big as the biggest element
        protected override Size MeasureOverride(Size availableSize)
        {
            Size maxSize = new Size();

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                maxSize.Height = Math.Max(child.DesiredSize.Height, maxSize.Height);
                maxSize.Width = Math.Max(child.DesiredSize.Width, maxSize.Width);
            }

            return base.MeasureOverride(maxSize);
        }

        // Arrange the child elements to their final position
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(finalSize));
            }
            return base.ArrangeOverride(finalSize);
        }

        /*protected override Size MeasureOverride(Size availableSize)
        {
            Debug.WriteLine(Children.Count);

            return base.MeasureOverride(availableSize);
        }*/
    }
}
