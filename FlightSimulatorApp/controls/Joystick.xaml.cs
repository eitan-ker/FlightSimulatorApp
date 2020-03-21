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
        public Joystick()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton== System.Windows.Input.MouseButtonState.Pressed)
            {
                Point mousePoint = e.GetPosition(sender as Ellipse);
                double posY = mousePoint.Y;
                double actualHeight = Base.Height - 50;
                double marginBottom = actualHeight - (posY + KnobBase.Height);
                double posX = mousePoint.X ;
                double actualWidth = Base.Width - 20;
                double marginRight = actualWidth - (posX + KnobBase.Width);
                KnobBase.Margin = new Thickness(posX, posY, marginRight, marginBottom); //changing margin gives the ability for control to move
                /*knobPosition.X = e.MouseDevice.GetPosition(sender as Ellipse).X;
                knobPosition.Y = e.MouseDevice.GetPosition(sender as Ellipse).Y;*/
            }
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobLocation.X = 0;
            knobLocation.Y = 0;
        }
    }
}
