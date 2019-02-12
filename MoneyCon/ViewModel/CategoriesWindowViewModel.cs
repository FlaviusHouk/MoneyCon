using MoneyCon.ViewModel.Database;
using MoneyCon.ViewModel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MoneyCon.ViewModel
{
    public class CategoriesWindowViewModel : BindableBase
    {
        private Command _addCategoryCommand;
        private Command _removeCategoryCommand;
        private IDatabaseWorker _db;
        public Command AddCategoryCommand
        {
            get
            {
                return _addCategoryCommand ?? (_addCategoryCommand = new Command(par =>
                {

                    /*var vm = new InputNameWindowViewModel();
                    var win = new InputNameWindow() { DataContext = vm, Owner = App.Current.MainWindow };

                    if (win.ShowDialog() == true)
                    {
                        if (_db.AddCategory(vm.Name))
                        {
                            Categories.Add(vm.Name);
                        }
                        //else { MessageBox.Show("Не удалось добавить категорию"); }
                    }*/

                }));
            }
        }

        public Command RemoveCategoryCommand
        {
            get
            {
                return _removeCategoryCommand ?? (_removeCategoryCommand = new Command(par =>
                {
                    if (_db.RemoveCategory(_db.Categories.First(obj => string.Compare(obj.Value, SelectedCategory) == 0).Key))
                    {
                        Categories.Remove(SelectedCategory);
                    }
                    //else { MessageBox.Show("Не удалось удалить категорию, удалите все расходы этой категории"); }

                }));
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
                _removeCategoryCommand.CanExecuteFlag = value != null;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public CategoriesWindowViewModel(ObservableCollection<string> tags, IDatabaseWorker db)
        {
            Categories = tags;
            _db = db;
        }
    }
}
