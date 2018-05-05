﻿using GalaSoft.MvvmLight.Command;
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
using System.Windows.Shapes;

namespace CostControl.Views
{
    /// <summary>
    /// Interaction logic for InputNameWindow.xaml
    /// </summary>
    public partial class InputNameWindow : Window
    {
        public InputNameWindow()
        {
            InitializeComponent();
        }

        private RelayCommand _okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand(() =>
                {
                    DialogResult = true;
                }));
            }
        }
    }
}