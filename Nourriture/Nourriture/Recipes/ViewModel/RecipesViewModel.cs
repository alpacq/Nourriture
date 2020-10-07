using Nourriture.Common;
using Nourriture.NewMealWindow.ViewModel;
using Nourriture.Recipes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Nourriture.Recipes.ViewModel
{
    public class RecipesViewModel
    {
        private ICommand addCommand;
        private RecipesModel model;

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        public RecipesModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                OnPropertyChanged("Model");
            }
        }

        public List<Meal> Recipes
        {
            get
            {
                return this.Model.Recipes;
            }
        }
        public ICommand AddCommand
        {
            get
            {
                return this.addCommand;
            }
            set
            {
                this.addCommand = value;
                OnPropertyChanged("AddCommand");
            }
        }

        public RecipesViewModel(Database db)
        {
            this.Model = new RecipesModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.AddMeal));
        }

        public void AddMeal(object obj)
        {
            NewMealWindow.View.NewMealWindow window = new NewMealWindow.View.NewMealWindow();
            window.DataContext = new NewMealViewModel(this.Model.Db, window.Close);
            window.ShowDialog();
            OnPropertyChanged("Recipes");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Recipes);
            view.Refresh();
        }
    }
}
