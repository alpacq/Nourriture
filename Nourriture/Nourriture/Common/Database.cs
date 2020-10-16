using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nourriture.Common
{
    public class Database
    {
        private List<Product> available;
        private List<Product> basket;
        private List<Meal> mealsToDo;
        private List<Meal> meals;
        private bool sortedRecipes;
        private bool sortedInventory;

        public List<Product> Available
        {
            get
            {
                return this.available != null ? this.available : new List<Product>();
            }
            set
            {
                this.available = value;
            }
        }

        public List<Product> Basket
        {
            get
            {
                return this.basket != null ? this.basket : new List<Product>();
            }
            set
            {
                this.basket = value;
            }
        }

        public List<Meal> MealsToDo
        {
            get
            {
                return this.mealsToDo != null ? this.mealsToDo : new List<Meal>();
            }
            set
            {
                this.mealsToDo = value;
            }
        }

        public List<Meal> Meals
        {
            get
            {
                return this.meals != null ? this.meals : new List<Meal>();
            }
            set
            {
                this.meals = value;
            }
        }

        public Database()
        {
            this.Available = new List<Product>();
            this.Basket = new List<Product>();
            this.MealsToDo = new List<Meal>();
            this.Meals = new List<Meal>();
            this.SortedRecipes = false;
            this.SortedInventory = false;
        }

        public Database(List<Product> available, List<Product> basket, List<Meal> mealsToDo, List<Meal> meals)
        {
            this.Available = available;
            this.Basket = basket;
            this.MealsToDo = mealsToDo;
            this.Meals = meals;
            this.SortedRecipes = false;
            this.SortedInventory = false;
        }

        public Database(List<Product> available, List<Meal> meals)
        {
            this.Available = available;
            this.Meals = meals;
            this.Basket = new List<Product>();
            this.MealsToDo = new List<Meal>();
            this.SortedRecipes = false;
            this.SortedInventory = false;
        }

        [XmlIgnore]
        public bool SortedRecipes
        {
            get 
            { 
                return this.sortedRecipes; 
            }
            set
            {
                this.sortedRecipes = value;
            }
        }

        [XmlIgnore]
        public bool SortedInventory
        {
            get
            {
                return this.sortedInventory;
            }
            set
            {
                this.sortedInventory = value;
            }
        }

        public bool CanDo(List<Product> recipe)
        {
            if (this.Available == null || recipe == null)
                return false;
            return recipe.Intersect(this.Available).Count() == recipe.Count();
        }
    }
}
