using Nourriture.Common;
using Nourriture.RecipesAvailableWindow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nourriture.RecipesAvailableWindow.ViewModel
{
    public class RecipesAvailableViewModel
    {
        private RecipesAvailableModel model;

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

        public RecipesAvailableViewModel(Product product, Database db)
        {
            this.Model = new RecipesAvailableModel(product, db);
        }
    }
}
