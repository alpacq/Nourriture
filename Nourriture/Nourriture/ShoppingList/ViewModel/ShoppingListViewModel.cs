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
        private ShoppingListModel model;

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
                return this.Model.Basket;
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

        public ShoppingListViewModel(Database db)
        {
            this.Model = new ShoppingListModel(db);
            AddCommand = new RelayCommand(new Action<object>(this.DoShopping));
        }

        public void DoShopping(object obj)
        {
            foreach (Product product in this.Basket)
            {
                if (this.Model.Db.Available.Any(i => (i.Name == product.Name && i.Unit == product.Unit)))
                {
                    this.Model.Db.Available[this.Model.Db.Available.FindIndex(i => (i.Name == product.Name && i.Unit == product.Unit))].Amount += product.Amount;
                    this.Model.Db.Available[this.Model.Db.Available.FindIndex(i => (i.Name == product.Name && i.Unit == product.Unit))].OnList = false;
                }
                else
                {
                    product.OnList = false;
                    this.Model.Db.Available.Add(product);
                }
            }
            this.Basket.Clear();
            OnPropertyChanged("Basket");
            ICollectionView view = CollectionViewSource.GetDefaultView(this.Basket);
            view.Refresh();
        }
    }
}
