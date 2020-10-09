using Nourriture.Common;
using Nourriture.NewMealWindow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Nourriture.NewMealWindow.ViewModel
{
    public class NewMealViewModel : INotifyPropertyChanged
    {
        private ICommand addCommand;
        private ICommand addIngCommand;
        private NewMealModel model;
        private Database db;

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

        public Action CloseAction { get; set; }

        public Database Db
        {
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
                OnPropertyChanged("Db");
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

        public ICommand AddIngCommand
        {
            get
            {
                return this.addIngCommand;
            }
            set
            {
                this.addIngCommand = value;
                OnPropertyChanged("AddIngCommand");
            }
        }

        public NewMealModel Model
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

        public Meal Recipe
        {
            get
            {
                return this.Model.NewMeal;
            }
        }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
            set
            {
                this.Model.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Product> Ingredients
        {
            get
            {
                return this.Model.Ingredients;
            }
            set
            {
                this.Model.Ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public NewMealViewModel(Database db, Action close)
        {
            this.Db = db;
            this.Model = new NewMealModel();
            this.AddCommand = new RelayCommand(new Action<object>(this.AddRecipe));
            this.AddIngCommand = new RelayCommand(new Action<object>(this.AddIngredient));
            this.CloseAction = close;
        }

        public void AddRecipe(object obj)
        {
            try
            {
                foreach(Product ing in this.Model.Ingredients)
                {
                    if(this.Db.Available.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit && i.Amount >= ing.Amount)))
                    {
                        this.Model.CanDo = true;
                    }
                    else
                    {
                        this.Model.CanDo = false;
                        break;
                    }
                }
                this.Db.Meals.Add(this.Recipe);
            }
            catch
            {
                //TODO
            }
            this.CloseAction();
        }

        public void AddIngredient(object obj)
        {
            this.Model.Ingredients.Add(new Product());
            OnPropertyChanged("Ingredients");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Ingredients);
            view.Refresh();
        }
    }
}
