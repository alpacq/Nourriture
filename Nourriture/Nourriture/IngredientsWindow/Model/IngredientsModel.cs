using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Nourriture.IngredientsWindow.Model
{
    public class IngredientsModel : INotifyPropertyChanged
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
        private Meal meal;

        public Meal Meal
        {
            get
            {
                return this.meal;
            }
            set
            {
                this.meal = value;
                OnPropertyChanged("NewMeal");
            }
        }

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

        public string Name
        {
            get
            {
                return this.Meal.Name;
            }
            set
            {
                this.Meal.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Product> Ingredients
        {
            get
            {
                return this.Meal.Products;
            }
            set
            {
                this.Meal.Products = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public bool CanDo
        {
            get
            {
                return this.Meal.CanDo;
            }
            set
            {
                this.Meal.CanDo = value;
                OnPropertyChanged("CanDo");
            }
        }

        public IngredientsModel(Meal meal, Database db)
        {
            this.Meal = meal;
            this.Db = db;
            foreach(Product ing in this.Ingredients)
            {
                ing.Is = this.Db.Available.Any(i => i.Name == ing.Name && i.Unit == ing.Unit && i.Amount >= ing.Amount);
            }
            this.Ingredients.Sort((x, y) => x.Is.CompareTo(y.Is));
        }
    }
}
