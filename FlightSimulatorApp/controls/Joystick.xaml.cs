using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace FlightSimulatorApp.controls
{
    public partial class Joystick : UserControl
    {
        private Point knobLocation;
        private UIElement _lastClickedUIElement;
        public Joystick()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton== System.Windows.Input.MouseButtonState.Pressed)
            {
                double posY = e.MouseDevice.GetPosition(sender as Ellipse).Y; ;
                double actualHeight = Base.Height;
                double marginBottom = actualHeight - (posY + KnobBase.Height);
                double posX = e.MouseDevice.GetPosition(sender as Ellipse).X;
                double actualWidth = Base.Width;
                double marginRight = actualWidth - (posX + KnobBase.Width);
                KnobBase.Margin = new Thickness(posX, posY, marginRight, marginBottom);
                /*knobPosition.X = e.MouseDevice.GetPosition(sender as Ellipse).X;
                knobPosition.Y = e.MouseDevice.GetPosition(sender as Ellipse).Y;*/
            }
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }
    }
}
