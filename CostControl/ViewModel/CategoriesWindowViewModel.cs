using CostControl.Model;
using CostControl.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace CostControl.ViewModel
{

    public class CategoriesWindowViewModel : ViewModelBase
    {
        private RelayCommand _addCategoryCommand;
        private RelayCommand _removeCategoryCommand;
        private DataBaseWorker _db;
        public RelayCommand AddCategoryCommand
        {
            get
            {
                return _addCategoryCommand ?? (_addCategoryCommand = new RelayCommand(() =>
                {

                    var vm = new InputNameWindowViewModel();
                    var win = new InputNameWindow() { DataContext = vm, Owner = App.Current.MainWindow };

                    if (win.ShowDialog() == true)
                    {
                        if (_db.AddCategory(vm.Name))
                        {
                            Categories.Add(vm.Name);
                        }
                        else { MessageBox.Show("Не удалось добавить категорию"); }
                    }

                }));
            }
        }

        public RelayCommand RemoveCategoryCommand
        {
            get
            {
                return _removeCategoryCommand ?? (_removeCategoryCommand = new RelayCommand(() =>
                {
                    if (_db.RemoveCategory(_db.Categories.First(obj => string.Compare(obj.Value, SelectedCategory) == 0).Key))
                    {
                        Categories.Remove(SelectedCategory);
                    }
                    else { MessageBox.Show("Не удалось удалить категорию, удалите все расходы этой категории"); }
                    
                }, () => 
                {
                    if (SelectedCategory== null)
                    {
                        return false;
                    }
                    return true;
                }
                ));
            }
        }


        public ObservableCollection<string> Categories
        {
            get; set;
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                _removeCategoryCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public CategoriesWindowViewModel(ObservableCollection<string> tags, DataBaseWorker db)
        {
            Categories = tags;
            _db = db;
        }
    }
}