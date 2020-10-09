using Nourriture.Common;
using Nourriture.Recipes.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nourriture.Recipes.View
{
    /// <summary>
    /// Interaction logic for RecipesView.xaml
    /// </summary>
    public partial class RecipesView : UserControl
    {
        public RecipesView()
        {
            InitializeComponent();
        }

        private void productsList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((RecipesViewModel)(this.DataContext)).ShowRecipe((Meal)this.productsList.SelectedItem);
        }
    }
}
