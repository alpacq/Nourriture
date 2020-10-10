using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Nourriture.RecipesAvailableWindow.Model
{
    public class RecipesAvailableModel : INotifyPropertyChanged
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

        private Database db;
        private Product ingredient;

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

        public Product Ingredient
        {
            get
            {
                return this.ingredient;
            }
            set
            {
                this.ingredient = value;
                OnPropertyChanged("Ingredient");
            }
        }

        public string Name
        {
            get
            {
                return this.Ingredient.Name;
            }
        }

        public List<string> Meals
        {
            get
            {
                return this.Db.Meals.Where(i => i.Products.Any(p => p.Name == this.Ingredient.Name)).Select(k => k.Name).ToList<string>();
            }
        }

        public RecipesAvailableModel(Product product, Database db)
        {
            this.Ingredient = product;
            this.Db = db;
        }
    }
}
