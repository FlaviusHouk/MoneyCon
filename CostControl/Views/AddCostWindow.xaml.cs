using CostControl.ViewModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace CostControl.Views
{
    /// <summary>
    /// Description for AddCostWindow.
    /// </summary>
    public partial class AddCostWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the AddCostWindow class.
        /// </summary>
        public AddCostWindow()
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
                    (DataContext as AddCostViewModel)?.Cost.InsertIntoDB();
                }));
            }
        }
    }
}