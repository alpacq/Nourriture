using Nourriture.Common;
using Nourriture.Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Nourriture.Inventory.View
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Page
    {
        public InventoryView()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Page));
            addProduct.Style = (Style)FindResource(typeof(Button));
            addProduct.Template = (ControlTemplate)FindResource("btnTmplt");
            removeProduct.Style = (Style)FindResource(typeof(Button));
            removeProduct.Template = (ControlTemplate)FindResource("btnTmplt");
        }

        private void productsList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((InventoryViewModel)(this.DataContext)).ShowRecipes((Product)this.productsList.SelectedItem);
        }

        private void productsList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                ((InventoryViewModel)(this.DataContext)).FireProductsChanged();
                ICollectionView view = CollectionViewSource.GetDefaultView(((InventoryViewModel)(this.DataContext)).Products);
                view.Refresh();
            }
        }

        private void productsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.30;
            var col2 = 0.10;
            var col3 = 0.10;
            var col4 = 0.40;
            var col5 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
            gView.Columns[4].Width = workingWidth * col5;
        }
    }
}
