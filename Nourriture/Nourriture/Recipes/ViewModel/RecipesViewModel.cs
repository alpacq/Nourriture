using Nourriture.Common;
using Nourriture.NewMealWindow.ViewModel;
using Nourriture.Recipes.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Nourriture.Recipes.ViewModel
{
    public class RecipesViewModel
    {
        private ICommand addCommand;
        private ICommand addBasket;
        private RecipesModel model;
        private Meal selectedRecipe;

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

        public ICommand AddBasket
        {
            get
            {
                return this.addBasket;
            }
            set
            {
                this.addBasket = value;
                OnPropertyChanged("AddBasket");
            }
        }

        public Meal SelectedRecipe
        {
            get
            {
                return this.selectedRecipe;
            }
            set
            {
                this.selectedRecipe = value;
                OnPropertyChanged("SelectedRecipe");
            }
        }

        public RecipesViewModel(Database db)
        {
            this.Model = new RecipesModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.AddMeal));
            AddBasket = new RelayCommand(new Action<object>(this.AddToShoppingList));
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

        public void AddToShoppingList(object obj)
        {
            this.Model.Db.MealsToDo.Add(this.SelectedRecipe);
            foreach(Product ing in this.SelectedRecipe.Products)
            {
                if(this.Model.Db.Basket.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit)))
                {
                    this.Model.Db.Basket[this.Model.Db.Basket.FindIndex(i => (i.Name == ing.Name && i.Unit == ing.Unit))].Amount += ing.Amount;
                }
                else if(this.Model.Db.Available.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit)))
                {
                    this.Model.Db.Basket.Add(new Product(ing.Name, 
                                                         ing.Amount - this.Model.Db.Available[this.Model.Db.Available.FindIndex(i => (i.Name == ing.Name && i.Unit == ing.Unit))].Amount,
                                                         ing.Unit,
                                                         ing.Comment,
                                                         ing.ShortBBD));
                    this.Model.Db.Available[this.Model.Db.Available.FindIndex(i => (i.Name == ing.Name && i.Unit == ing.Unit))].OnList = true;
                }
                else
                {
                    this.Model.Db.Basket.Add(new Product(ing.Name,
                                                         ing.Amount,
                                                         ing.Unit,
                                                         ing.Comment,
                                                         ing.ShortBBD));
                }
            }
        }
    }
}
