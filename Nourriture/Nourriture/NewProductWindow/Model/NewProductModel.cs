using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.NewProductWindow.Model
{
    public class NewProductModel : INotifyPropertyChanged
    {
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

        private Product newProduct;

        public Product NewProduct
        {
            get
            {
                return this.newProduct;
            }
            set
            {
                this.newProduct = value;
                OnPropertyChanged("NewProduct");
            }
        }

        public string Name
        {
            get
            {
                return this.NewProduct.Name;
            }
            set
            {
                this.NewProduct.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Comment
        {
            get
            {
                return this.NewProduct.Comment;
            }
            set
            {
                this.NewProduct.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public bool ShortBBD
        {
            get
            {
                return this.NewProduct.ShortBBD;
            }
            set
            {
                this.NewProduct.ShortBBD = value;
                OnPropertyChanged("ShortBBD");
            }
        }

        public string Amount
        {
            get
            {
                return this.NewProduct.Amount.ToString();
            }
            set
            {
                this.NewProduct.Amount = float.Parse(value);
                OnPropertyChanged("Amount");
            }
        }

        public string Unit
        {
            get
            {
                return this.NewProduct.Unit;
            }
            set
            {
                this.NewProduct.Unit = value;
                OnPropertyChanged("Unit");
            }
        }

        public NewProductModel()
        {
            this.NewProduct = new Product();
        }
    }
}
