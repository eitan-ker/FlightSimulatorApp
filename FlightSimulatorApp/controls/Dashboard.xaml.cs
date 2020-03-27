﻿using FlightSimulatorApp.Model;
using FlightSimulatorApp.ViewModel;
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

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        VM_Dashboard vm;
        public Dashboard()
        {
            InitializeComponent();
            vm = (VM_Dashboard)this.DataContext;
            //DataContext = new VM_Dashboard(new MySimApp(new MytelnetClient()));
        }
    }
}
