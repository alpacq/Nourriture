using Nourriture.Common;
using Nourriture.IngredientsWindow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nourriture.IngredientsWindow.ViewModel
{
    public class IngredientsViewModel
    {
        private IngredientsModel model;

        public IngredientsModel Model
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

        public Meal Recipe
        {
            get
            {
                return this.Model.Meal;
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

        public List<Product> Ingredients
        {
            get
            {
                return this.Model.Ingredients;
            }
            set
            {
                this.Model.Ingredients = value;
            }
        }

        public IngredientsViewModel(Meal meal)
        {
            this.Model = new IngredientsModel(meal);
        }
    }
}
