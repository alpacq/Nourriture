using Nourriture.Common;
using Nourriture.NewProductWindow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nourriture.NewProductWindow.ViewModel
{
    public class NewProductViewModel
    {
        private ICommand addCommand;
        private NewProductModel model;
        private Database db;

        public Action CloseAction { get; set; }

        public Database Db
        {
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
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
            }
        }

        public NewProductModel Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }

        public Product Product
        {
            get
            {
                return this.Model.NewProduct;
            }
        }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
            set
            {
                this.Model.Name = value;
            }
        }

        public string Unit
        {
            get
            {
                return this.Model.Unit;
            }
            set
            {
                this.Model.Unit = value;
            }
        }

        public string Comment
        {
            get
            {
                return this.Model.Comment;
            }
            set
            {
                this.Model.Comment = value;
            }
        }

        public string Amount
        {
            get
            {
                return this.Model.Amount;
            }
            set
            {
                this.Model.Amount = value;
            }
        }

        public bool ShortBBD
        {
            get
            {
                return this.Model.ShortBBD;
            }
            set
            {
                this.Model.ShortBBD = value;
            }
        }

        public NewProductViewModel(Database db, Action close)
        {
            this.Db = db;
            this.Model = new NewProductModel();
            this.AddCommand = new RelayCommand(new Action<object>(this.AddProduct));
            this.CloseAction = close;
        }

        public void AddProduct(object obj)
        {
            try
            {
                this.Db.Available.Add(this.Product);
                this.Db.SortedInventory = false;
            }
            catch
            {
                //TODO
            }
            this.CloseAction();
        }
    }
}
