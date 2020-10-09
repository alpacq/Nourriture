﻿using Nourriture.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.NewMealWindow.Model
{
    public class NewMealModel : INotifyPropertyChanged
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

        private Meal newMeal;

        public Meal NewMeal
        {
            get
            {
                return this.newMeal;
            }
            set
            {
                this.newMeal = value;
                OnPropertyChanged("NewMeal");
            }
        }

        public string Name
        {
            get
            {
                return this.NewMeal.Name;
            }
            set
            {
                this.NewMeal.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Product> Ingredients
        {
            get
            {
                return this.NewMeal.Products;
            }
            set
            {
                this.NewMeal.Products = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public bool CanDo
        {
            get
            {
                return this.NewMeal.CanDo;
            }
            set
            {
                this.NewMeal.CanDo = value;
                OnPropertyChanged("CanDo");
            }
        }

        public NewMealModel()
        {
            this.NewMeal = new Meal();
            this.Ingredients.Add(new Product());
        }
    }
}
