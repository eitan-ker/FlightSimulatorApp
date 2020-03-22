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

        private void centerKnob_Completed(object sender, EventArgs e)
        {

        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                knobLocation = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - knobLocation.X;
                double y = e.GetPosition(this).Y - knobLocation.Y;
                //knobPosition.X = x;
                //knobPosition.Y = y;
                if(Math.Sqrt(x*x + y*y) <= Base.Width/2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                } 
                else
                {
                    knobPosition.X = ((Base.Width / 2) / Math.Sqrt(x * x + y * y)) * x;
                    knobPosition.Y = ((Base.Width / 2) / Math.Sqrt(x * x + y * y)) * y;
                }
              /*if(Math.Abs(x) < (blackBase.Width / 2))
                {
                    knobPosition.X = x;
                }
                if (Math.Abs(y) < (blackBase.Height / 2))
                {
                    knobPosition.Y = y;
                }*/
            }
        }
       
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }
    }
}