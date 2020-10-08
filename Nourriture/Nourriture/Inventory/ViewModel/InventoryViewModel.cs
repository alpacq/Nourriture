using Nourriture.Common;
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
        private InventoryModel model;

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

        public List<Product> Products
        {
            get 
            { 
                return this.Model.Products.Where(i => i.Amount > 0.0f).ToList<Product>(); 
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

        public InventoryViewModel(Database db)
        {
            this.Model = new InventoryModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.AddProduct));
        }

        public void AddProduct(object obj)
        {
            NewProductWindow.View.NewProductWindow window = new NewProductWindow.View.NewProductWindow();
            window.DataContext = new NewProductViewModel(this.Model.Db, window.Close);
            window.ShowDialog();
            OnPropertyChanged("Products");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Products);
            view.Refresh();
        }

        public void ShowRecipes(Product product)
        {
            RecipesAvailableWindow.View.RecipesAvailableWindow window = new RecipesAvailableWindow.View.RecipesAvailableWindow();
            window.DataContext = new RecipesAvailableViewModel();
            window.ShowDialog();
        }
    }
}
