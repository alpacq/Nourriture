﻿using Nourriture.Common;
using Nourriture.Inventory.Model;
using Nourriture.NewProductWindow.ViewModel;
using Nourriture.NewProductWindow.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using Nourriture.RecipesAvailableWindow.ViewModel;

namespace Nourriture.Inventory.ViewModel
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private ICommand addCommand;
        private ICommand removeCommand;
        private InventoryModel model;
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
        public InventoryModel Model
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

        public List<Product> Products
        {
            get 
            { 
                return this.Model.Products.Where(i => i.Amount > 0).ToList<Product>(); 
            }
            set
            {
                this.Model.Products = value;
                OnPropertyChanged("Products");
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

        public InventoryViewModel(Database db)
        {
            this.Model = new InventoryModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.AddProduct));
            RemoveCommand = new RelayCommand(new Action<object>(this.RemoveProduct));
            this.SortProducts();
        }

        public void FireProductsChanged()
        {
            OnPropertyChanged("Products");
        }

        public void AddProduct(object obj)
        {
            NewProductWindow.View.NewProductWindow window = new NewProductWindow.View.NewProductWindow();
            window.DataContext = new NewProductViewModel(this.Model.Db, window.Close);
            window.ShowDialog();
            this.SortProducts();
        }

        public void SortProducts()
        {
            if(!this.Model.Db.SortedInventory)
            {
                this.Model.Db.Available.Sort((x, y) => string.Compare(x.Name, y.Name));
                this.Model.Db.SortedInventory = true;
            }
            OnPropertyChanged("Products");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Products);
            view.Refresh();
        }

        public void RemoveProduct(object obj)
        {
            if (this.SelectedProduct != null)
            {
                this.Model.Products = this.Model.Products.Where(p => (p.Name != this.SelectedProduct.Name || p.Unit != this.SelectedProduct.Unit)).ToList<Product>();
                OnPropertyChanged("Products");
                ICollectionView view = CollectionViewSource.GetDefaultView(this.Products);
                view.Refresh();
            }
        }

        public void ShowRecipes(Product product)
        {
            if (this.SelectedProduct != null)
            {
                RecipesAvailableWindow.View.RecipesAvailableWindow window = new RecipesAvailableWindow.View.RecipesAvailableWindow();
                window.DataContext = new RecipesAvailableViewModel(this.SelectedProduct, this.Model.Db);
                window.ShowDialog();
            }
        }
    }
}
