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

namespace Nourriture.ShoppingList.View
{
    /// <summary>
    /// Interaction logic for ShoppingListView.xaml
    /// </summary>
    public partial class ShoppingListView : Page
    {
        public ShoppingListView()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            addProduct.Style = (Style)FindResource(typeof(Button));
            addProduct.Template = (ControlTemplate)FindResource("btnTmplt");
            removeProduct.Style = (Style)FindResource(typeof(Button));
            removeProduct.Template = (ControlTemplate)FindResource("btnTmplt");
        }

        private void productsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.90;
            var col2 = 0.05;
            var col3 = 0.05;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }

        private void mealsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar

            gView.Columns[0].Width = workingWidth;
        }
    }
}
