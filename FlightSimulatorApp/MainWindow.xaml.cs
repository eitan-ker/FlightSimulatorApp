using FlightSimulatorApp.controls;
using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelClass vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ViewModelClass(new MySimApp(new MytelnetClient()));
            this.DataContext = vm;
            
            //this.JoystickVM = new VM_Joystick(new MySimApp(new MytelnetClient()));
        }
        public ViewModelClass getVM() 
        {
            return this.vm;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            StackPanelFixed_SP.Children.Clear();
            UserControl1 UC1 = new UserControl1();
            StackPanelFixed_SP.Children.Add(UC1);
            UC1.Visibility = System.Windows.Visibility.Visible;
            UC1.Status = vm;
            
        }

        private void disconnect_Click(object sender, RoutedEventArgs e)
        {
            vm.disconnect();
            disconnect.IsChecked = false;
            disconnect.IsEnabled = false;
        }

        private void dashboard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /*private void disconnect_Checked(object sender, RoutedEventArgs e) //toggle button - if there wansnt attempt to connect yet, this button will be grey and not clickable
        {
            if((sender as ToggleButton).IsChecked == false)
            {
                
                if(ConnectionStatus.Text == "Connected")
                {
                    disconnect.IsChecked = true;
                    disconnect.IsEnabled = true;
                }
            } else
            {
                disconnect.IsChecked = false;
                disconnect.IsEnabled = false;
            }
        }*/
    }
}
