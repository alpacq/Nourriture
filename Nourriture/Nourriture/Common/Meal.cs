using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.Common
{
    public class Meal : INameable, INotifyPropertyChanged
    {
        private string name;
        private List<Product> products;
        private bool canDo;
        private bool onList;

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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Product> Products
        {
            get
            {
                return this.products != null ? this.products : new List<Product>();
            }
            set
            {
                this.products = value;
                OnPropertyChanged("Products");
            }
        }

        public bool CanDo
        {
            get
            {
                return this.canDo;
            }
            set
            {
                this.canDo = value;
                OnPropertyChanged("CanDo");
            }
        }

        public bool OnList
        {
            get
            {
                return this.onList;
            }
            set
            {
                this.onList = value;
                OnPropertyChanged("OnList");
            }
        }

        public Meal()
        {
            this.Name = "";
            this.Products = new List<Product>();
            this.CanDo = false;
            this.OnList = false;
        }

        public Meal(string name, List<Product> products)
        {
            this.Name = name;
            this.Products = products;
            this.CanDo = false;
            this.OnList = false;
        }
    }
}
