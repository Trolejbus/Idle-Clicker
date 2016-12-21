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

namespace IdleClicker
{
    class DragBehaviour : Behavior<FrameworkElement>
    {
        Point StartMousePosition = new Point();
        FrameworkElement MoveablePanel;
        Thickness PanelPosition;

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += MouseLeftButtonUp;
            AssociatedObject.MouseMove += MouseMove;

            //   MoveablePanel = (UserControl)((Grid)((UserControl)((Grid)AssociatedObject).Parent).Parent).Parent;
            //  ()((UserControl)(.Parent)).Parent).Parent;
            
            SetMoveablePanel();
        }

        private void SetMoveablePanel()
        {
            FrameworkElement c1 = (FrameworkElement)AssociatedObject.Parent;
            if(c1.Parent != null)
            {
                if(((FrameworkElement)c1.Parent).Parent != null)
                {
                    MoveablePanel = (FrameworkElement)((FrameworkElement)c1.Parent).Parent;
                    PanelPosition = MoveablePanel.Margin;
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= MouseLeftButtonUp;
            AssociatedObject.MouseMove -= MouseMove;

            MoveablePanel = null;

            base.OnDetaching();
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetMoveablePanel();

            StartMousePosition = e.GetPosition(MoveablePanel);
            AssociatedObject.CaptureMouse();

            SetMoveablePanel();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured && e.LeftButton == MouseButtonState.Pressed)
            {
                SetMousePosition(e.GetPosition(MoveablePanel));
            }
        }

        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.IsMouseCaptured)
            {
                MoveablePanel.Margin = PanelPosition;
                SetMousePosition(e.GetPosition(MoveablePanel));
                AssociatedObject.ReleaseMouseCapture();
            }
        }

        private void SetMousePosition(Point mousePosition)
        {
            Point position = new Point();

            position.X = PanelPosition.Left + mousePosition.X - StartMousePosition.X; 
            position.Y = PanelPosition.Top + mousePosition.Y - StartMousePosition.Y;

            double Width = ((FrameworkElement)((UserControl)MoveablePanel).Parent).ActualWidth;
            double Height = ((FrameworkElement)((UserControl)MoveablePanel).Parent).ActualHeight;
            double pWidth = MoveablePanel.ActualWidth;
            double pHeight = MoveablePanel.ActualHeight;

            if (position.X <= Width - pWidth)
                if (position.X >= 0)
                    PanelPosition.Left = position.X;
                else
                    PanelPosition.Left = 0;
            else
                PanelPosition.Left = Width - pWidth;

            if (position.Y <= Height - pHeight)
                if (position.Y >= 0)
                    PanelPosition.Top = position.Y;
                else
                    PanelPosition.Top = 0;
            else
                PanelPosition.Top = Height - pHeight;  
            MoveablePanel.Margin = PanelPosition;
        }
    }
}
