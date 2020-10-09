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
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
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
    }
}
