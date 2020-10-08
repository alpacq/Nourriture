using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.Common
{
    public class Product : INameable, INotifyPropertyChanged
    {
        private string name;
        private string comment;
        private bool shortBBD;
        private float amount;
        private string unit;
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

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public bool ShortBBD
        {
            get
            {
                return this.shortBBD;
            }
            set
            {
                this.shortBBD = value;
                OnPropertyChanged("ShortBBD");
            }
        }

        public float Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                this.amount = value;
                if (amount == 0.0f) this.ShortBBD = false;
                OnPropertyChanged("Amount");
            }
        }

        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
                OnPropertyChanged("Unit");
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

        public Product()
        {
            this.Name = "";
            this.Amount = 0.0f;
            this.Unit = "g";
            this.Comment = "";
            this.ShortBBD = false;
            this.OnList = false;
        }

        public Product(string name, float amount, string unit = "g", string comment = "", bool shortBBD = false, bool onList = false)
        {
            this.Name = name;
            this.Amount = amount;
            this.Unit = unit;
            this.Comment = comment;
            this.ShortBBD = shortBBD;
            this.OnList = onList;
        }
    }
}
