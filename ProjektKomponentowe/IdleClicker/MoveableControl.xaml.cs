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
    /// Interaction logic for MoveableControl.xaml
    /// </summary>
    public partial class MoveableControl : UserControl
    {
        TranslateTransform RenderTransform = new TranslateTransform();

        // True if a drag is in progress.
        private bool DragInProgress = false;

        // The drag's last point.
        private Point LastPoint;

        public MoveableControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid.Background = new SolidColorBrush(Color.FromRgb(255, 0, 255));
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            //e.LeftButton.HasFlag.DragInProgress()
           /* if (DragInProgress)
            {
                // See how much the mouse has moved.
                Point point = Mouse.GetPosition(canvas);
                double offset_x = point.X - LastPoint.X;
                double offset_y = point.Y - LastPoint.Y;

                // Get the rectangle's current position.
                double new_x = Canvas.GetLeft(rectangle1);
                double new_y = Canvas.GetTop(rectangle1);
                double new_width = rectangle1.Width;
                double new_height = rectangle1.Height;


                new_x += offset_x;
                new_y += offset_y;


                // Update the rectangle.
                Canvas.SetLeft(rectangle1, new_x);
                Canvas.SetTop(rectangle1, new_y);
                rectangle1.Width = new_width;
                rectangle1.Height = new_height;

                // Save the mouse's new location.
                LastPoint = point;
            }*/
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            grid.Background = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        }
    }
}
