using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
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
        ViewModelClass _vm;
        private Point knobLocation;
        public Joystick()
        {
            InitializeComponent();
            //_vm = ((MainWindow)Application.Current.MainWindow).getVM();
            _vm = (ViewModelClass)this.DataContext;
            //DataContext = _vm;
            
        }
        public ViewModelClass Status { get; set; }
        

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


                //Console.WriteLine(knobPosition.X/(Base.Width/2));
                //Console.WriteLine(knobPosition.Y/(Base.Height/-2));
                _vm = ((MainWindow)Application.Current.MainWindow).getVM();
                /// the values to send to simulator, the joystick range is between -1 to 1 when the horizontal to the right take value 1 and most vertical up takes vlaue 1
                _vm.FlyPlane(knobPosition.X / (Base.Width / 2), knobPosition.Y / (Base.Height / -2));
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
            Window parentWin = Window.GetWindow(this);
            _vm = ((MainWindow)Application.Current.MainWindow).getVM();
            _vm.FlyPlane(0, 0);
        }

        private void Aileron_value_MouseLeave(object sender, MouseEventArgs e)
        {
            var aileron = sender as Slider;
            MainWindow mainWind = Application.Current.MainWindow as MainWindow;
            if ( mainWind.connectionIndication.Text != "Connected")
            {
                Console.WriteLine("you cant change aileron while not connected to simulator");
                aileron.Value = 0;
            }
            
        }

        private void Throttle_value_MouseLeave(object sender, MouseEventArgs e)
        {
            var throttle = sender as Slider;
            MainWindow mainWind = Application.Current.MainWindow as MainWindow;
            if (mainWind.connectionIndication.Text != "Connected")
            {
                Console.WriteLine("you cant change throttle while not connected to simulator");
                throttle.Value = 0;
            }
        }
    }
}