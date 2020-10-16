using Nourriture.Common;
using Nourriture.RecipesAvailableWindow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nourriture.RecipesAvailableWindow.ViewModel
{
    public class RecipesAvailableViewModel : INotifyPropertyChanged
    {
        private RecipesAvailableModel model;
        private string selectedMeal;

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

        public RecipesAvailableModel Model
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

        public Product Ingredient
        {
            get
            {
                return this.Model.Ingredient;
            }
            set
            {
                this.Model.Ingredient = value;
            }
        }

        public string Name
        {
            get
            {
                return this.Model.Name;
            }
        }

        public List<string> Meals
        {
            get
            {
                return this.Model.Meals;
            }
        }

        public string SelectedMeal
        {
            get
            {
                return this.selectedMeal;
            }
            set
            {
                this.selectedMeal = value;
                OnPropertyChanged("SelectedMeal");
            }
        }

        public RecipesAvailableViewModel(Product product, Database db)
        {
            this.Model = new RecipesAvailableModel(product, db);
        }
    }
}
