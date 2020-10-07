using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.Inventory.Model
{
    public class InventoryModel : INotifyPropertyChanged
    {
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

        public InventoryModel(Database db)
        {
            this.Db = db;
        }

        public List<Product> Products
        {
            get
            {
                return this.Db.Available;
            }
        }
    }
}
