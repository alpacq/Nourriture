using Nourriture.Common;
using Nourriture.IngredientsWindow.ViewModel;
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
    public class RecipesViewModel : INotifyPropertyChanged
    {
        private ICommand addCommand;
        private ICommand removeCommand;
        private ICommand addBasket;
        private ICommand cookCommand;
        private ICommand resetCommand;
        private RecipesModel model;
        private Meal selectedRecipe;
        private string search;
        private string status;

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void FirePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(propertyName);
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
            set
            {
                this.Model.Recipes = value;
                OnPropertyChanged("Recipes");
                OnPropertyChanged("RecipesDisplayed");
            }
        }

        public List<Meal> RecipesDisplayed
        {
            get
            {
                return (this.Search != null && this.Search != String.Empty) ? (from recipe in this.Recipes where recipe.Name.Contains(this.Search) select recipe).ToList() : this.Recipes;
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

        public ICommand RemoveCommand
        {
            get
            {
                return this.removeCommand;
            }
            set
            {
                this.removeCommand = value;
                OnPropertyChanged("RemoveCommand");
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

        public ICommand CookCommand
        {
            get
            {
                return this.cookCommand;
            }
            set
            {
                this.cookCommand = value;
                OnPropertyChanged("CookCommand");
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return this.resetCommand;
            }
            set
            {
                this.resetCommand = value;
                OnPropertyChanged("ResetCommand");
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

        public string Search
        {
            get { return this.search; }
            set { this.search = value; OnPropertyChanged("Search"); OnPropertyChanged("RecipesDisplayed"); }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; OnPropertyChanged("Status"); }
        }

        public RecipesViewModel(Database db)
        {
            this.Model = new RecipesModel(db);
            foreach(Meal meal in this.Recipes)
            {
                foreach (Product ing in meal.Products)
                {
                    if (db.Available.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit && i.Amount >= ing.Amount)))
                    {
                        meal.CanDo = true;
                    }
                    else
                    {
                        meal.CanDo = false;
                        break;
                    }
                }
            }
            AddCommand = new RelayCommand(new Action<object>(this.AddMeal));
            RemoveCommand = new RelayCommand(new Action<object>(this.RemoveMeal));
            AddBasket = new RelayCommand(new Action<object>(this.AddToShoppingList));
            CookCommand = new RelayCommand(new Action<object>(this.CookRecipe));
            ResetCommand = new RelayCommand(new Action<object>(this.ResetSearch));
            this.SortRecipes();
        }

        public void AddMeal(object obj)
        {
            NewMealWindow.View.NewMealWindow window = new NewMealWindow.View.NewMealWindow();
            window.DataContext = new NewMealViewModel(this.Model.Db, window.Close);
            bool? result = window.ShowDialog();
            this.SortRecipes();
            if(result.HasValue && result.Value)
                this.Status = DateTime.Now.ToLongTimeString() + " Przepis dodany do bazy.";
        }

        public void RemoveMeal(object obj)
        {
            if (this.SelectedRecipe != null)
            {
                this.Model.Db.Meals = this.Model.Db.Meals.Where(p => (p.Name != this.SelectedRecipe.Name)).ToList<Meal>();
                OnPropertyChanged("Recipes");
                this.SelectedRecipe = null;
                this.SortRecipes();
                this.Status = DateTime.Now.ToLongTimeString() + " Przepis usunięty z bazy.";
            }
        }

        public void AddToShoppingList(object obj)
        {
            if (this.SelectedRecipe != null)
            {
                foreach (Product ing in this.SelectedRecipe.Products)
                {
                    if (this.Model.Db.Basket.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit)))
                    {
                        this.Model.Db.Basket[this.Model.Db.Basket.FindIndex(i => (i.Name == ing.Name && i.Unit == ing.Unit))].Amount += ing.Amount;
                    }
                    else if (this.Model.Db.Available.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit)))
                    {
                        if (this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).Amount < ing.Amount)
                        {
                            this.Model.Db.Basket.Add(new Product(ing.Name,
                                                                 ing.Amount - this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).Amount,
                                                                 ing.Unit,
                                                                 ing.Comment,
                                                                 ing.ShortBBD));
                            this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).OnList = true;
                        }
                        else
                        {
                            List<Meal> withIng = this.Model.Db.MealsToDo.Where(i => i.Products.Any(p => (p.Name == ing.Name && p.Unit == ing.Unit))).ToList<Meal>();
                            float sum = ing.Amount;
                            foreach (Meal m in withIng)
                            {
                                sum += m.Products.First(p => p.Name == ing.Name && p.Unit == ing.Unit).Amount;
                            }
                            if (this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).Amount < sum)
                            {
                                this.Model.Db.Basket.Add(new Product(ing.Name,
                                                                     sum - this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).Amount,
                                                                     ing.Unit,
                                                                     ing.Comment,
                                                                     ing.ShortBBD));
                                this.Model.Db.Available.First(i => (i.Name == ing.Name && i.Unit == ing.Unit)).OnList = true;
                            }
                        }
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
                this.Model.Db.MealsToDo.Add(this.SelectedRecipe);
                this.Status = DateTime.Now.ToLongTimeString() + " Danie dodane do listy zakupów.";
            }
        }

        public void ShowRecipe(Meal meal)
        {
            IngredientsWindow.View.IngredientsWindow window = new IngredientsWindow.View.IngredientsWindow();
            window.DataContext = new IngredientsViewModel(meal, this.Model.Db);
            window.ShowDialog();
        }

        public void SortRecipes()
        {
            if (!this.Model.Db.SortedRecipes)
            {
                foreach (Meal recipe in this.Recipes)
                {
                    foreach (Product ing in recipe.Products)
                    {
                        ing.Is = this.Model.Db.Available.Any(i => i.Name == ing.Name && i.Unit == ing.Unit && i.Amount >= ing.Amount);
                    }
                    recipe.LackingIngredients = recipe.Products.Where(p => !p.Is).Count();
                }
                this.Recipes.Sort((x, y) => x.LackingIngredients.CompareTo(y.LackingIngredients));
                this.Model.Db.SortedRecipes = true;
            }
            OnPropertyChanged("Recipes");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Recipes);
            view.Refresh();
        }

        public void CookRecipe(object obj)
        {
            if (this.SelectedRecipe != null)
            {
                if (this.SelectedRecipe.CanDo)
                {
                    foreach (Product ing in this.SelectedRecipe.Products)
                    {
                        this.Model.Db.Available.First(i => i.Name == ing.Name && i.Unit == ing.Unit).Amount -= ing.Amount;
                    }
                    foreach (Meal meal in this.Recipes)
                    {
                        foreach (Product ing in meal.Products)
                        {
                            if (this.Model.Db.Available.Any(i => (i.Name == ing.Name && i.Unit == ing.Unit && i.Amount >= ing.Amount)))
                            {
                                meal.CanDo = true;
                            }
                            else
                            {
                                meal.CanDo = false;
                                break;
                            }
                        }
                    }
                    this.Model.Db.SortedRecipes = false;
                    this.SortRecipes();
                    this.Status = DateTime.Now.ToLongTimeString() + " Danie przyrządzone - produkty usunięte z inwentarza.";
                }
            }
        }

        public void ResetSearch(object obj)
        {
            this.Search = string.Empty;
        }
    }
}
