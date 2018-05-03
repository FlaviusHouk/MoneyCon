﻿using CostControl.Model;
using CostControl.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
                        Categories.Add(vm.Name);
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
                    Categories.Remove(SelectedCategory);
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

        public CategoriesWindowViewModel(List<string> tags, DataBaseWorker db)
        {
            Categories = new ObservableCollection<string>(tags);
            _db = db;
        }
    }
}