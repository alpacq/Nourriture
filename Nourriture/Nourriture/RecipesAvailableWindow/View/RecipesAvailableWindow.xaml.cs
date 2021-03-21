using Nourriture.IngredientsWindow.ViewModel;
using Nourriture.RecipesAvailableWindow.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nourriture.RecipesAvailableWindow.View
{
    /// <summary>
    /// Interaction logic for RecipesAvailableWindow.xaml
    /// </summary>
    public partial class RecipesAvailableWindow : Window
    {
        public RecipesAvailableWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            close.Style = (Style)FindResource(typeof(Button));
            close.Template = (ControlTemplate)FindResource("btnTmpltFile");
        }

        private void productsList_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IngredientsWindow.View.IngredientsWindow window = new IngredientsWindow.View.IngredientsWindow();
            window.DataContext = new IngredientsViewModel(((RecipesAvailableViewModel)this.DataContext).Model.Db.Meals.FirstOrDefault(m => m.Name == ((RecipesAvailableViewModel)this.DataContext).SelectedMeal), ((RecipesAvailableViewModel)this.DataContext).Model.Db);
            window.ShowDialog();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
