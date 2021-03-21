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
    public partial class RecipesView : Page
    {
        public RecipesView()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            resetBtn.Style = (Style)FindResource(typeof(Button));
            resetBtn.Template = (ControlTemplate)FindResource("btnTmpltFile");
            addProduct.Style = (Style)FindResource(typeof(Button));
            addProduct.Template = (ControlTemplate)FindResource("btnTmplt");
            removeProduct.Style = (Style)FindResource(typeof(Button));
            removeProduct.Template = (ControlTemplate)FindResource("btnTmplt");
            addBasket.Style = (Style)FindResource(typeof(Button));
            addBasket.Template = (ControlTemplate)FindResource("btnTmplt");
            cookRecipe.Style = (Style)FindResource(typeof(Button));
            cookRecipe.Template = (ControlTemplate)FindResource("btnTmplt");
        }

        private void productsList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((RecipesViewModel)(this.DataContext)).ShowRecipe((Meal)this.productsList.SelectedItem);
        }

        private void productsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.90;
            var col2 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
        }
    }
}
