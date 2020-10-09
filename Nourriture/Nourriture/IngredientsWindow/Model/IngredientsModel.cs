using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public IngredientsModel(Meal meal)
        {
            this.Meal = meal;
        }
    }
}
