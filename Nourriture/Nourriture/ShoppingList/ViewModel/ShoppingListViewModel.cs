using Nourriture.Common;
using Nourriture.ShoppingList.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Nourriture.ShoppingList.ViewModel
{
    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        private ICommand addCommand;
        private ICommand removeCommand;
        private ShoppingListModel model;
        private Product selectedProduct;

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
        public ShoppingListModel Model
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

        public List<Product> Basket
        {
            get
            {
                return this.Model.Basket.Where(i => i.Amount > 0).ToList<Product>();
            }
            set
            {
                this.Model.Basket = value;
                OnPropertyChanged("Basket");
            }
        }

        public Product SelectedProduct
        {
            get
            {
                return this.selectedProduct;
            }
            set
            {
                this.selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
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

        public ShoppingListViewModel(Database db)
        {
            this.Model = new ShoppingListModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.DoShopping));
            RemoveCommand = new RelayCommand(new Action<object>(this.RemoveItem));
        }

        public void DoShopping(object obj)
        {
            foreach (Product product in this.Basket)
            {
                if (this.Model.Db.Available.Any(i => (i.Name == product.Name && i.Unit == product.Unit)))
                {
                    this.Model.Db.Available.First(i => (i.Name == product.Name && i.Unit == product.Unit)).Amount += (float)Math.Ceiling(product.Amount);
                    this.Model.Db.Available.First(i => (i.Name == product.Name && i.Unit == product.Unit)).OnList = false;
                }
                else
                {
                    product.OnList = false;
                    product.Amount = (float)Math.Ceiling(product.Amount);
                    this.Model.Db.Available.Add(product);
                    OnPropertyChanged("Model");
                }
            }
            this.Model.Db.Basket.Clear();
            this.Model.Db.MealsToDo.Clear();
            this.Model.Db.SortedInventory = false;
            this.Model.Db.SortedRecipes = false;
            OnPropertyChanged("Basket");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Basket);
            view.Refresh();
        }

        public void RemoveItem(object obj)
        {
            if (this.SelectedProduct != null)
            {
                this.Basket = this.Basket.Where(p => (p.Name != this.SelectedProduct.Name || p.Unit != this.SelectedProduct.Unit)).ToList<Product>();
                OnPropertyChanged("Basket");
                ICollectionView view = CollectionViewSource.GetDefaultView(this.Basket);
                view.Refresh();
            }
        }
    }
}
