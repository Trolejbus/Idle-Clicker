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
        Point StartMousePosition = new Point();
        public UserControl UserControlParent;

        public MoveableControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartMousePosition = e.GetPosition(canvas);
            TopPanel.CaptureMouse();
            canvas.Background = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (TopPanel.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                SetMousePosition(e.GetPosition(canvas));
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TopPanel.IsMouseCaptured)
            {
                //MessageBox.Show());
                canvas.Background = new SolidColorBrush(Color.FromRgb(255, 0, 255));
                SetMousePosition(e.GetPosition(canvas));
                TopPanel.ReleaseMouseCapture();
            }
        }

        private void SetMousePosition(Point mousePosition)
        {
            Point position = new Point();

            position.X = Canvas.GetLeft(this) + mousePosition.X - StartMousePosition.X; 
            position.Y = Canvas.GetTop(this) + mousePosition.Y - StartMousePosition.Y;

            if (position.X <= ((Canvas)UserControlParent.Parent).ActualWidth - this.Width)
                if (position.X >= 0)
                    Canvas.SetLeft(this, position.X);
                else
                    Canvas.SetLeft(this, 0);
            else
                Canvas.SetLeft(this, ((Canvas)UserControlParent.Parent).ActualWidth - this.Width);

            if (position.Y <= ((Canvas)UserControlParent.Parent).ActualHeight - this.Height)
                if (position.Y >= 0)
                    Canvas.SetTop(this, position.Y);
                else
                    Canvas.SetTop(this, 0);
            else
                Canvas.SetTop(this, ((Canvas)UserControlParent.Parent).ActualHeight - this.Height);      
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TopPanel.Width = canvas.ActualWidth;
        }
    }
}
