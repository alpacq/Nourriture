using Nourriture.Common;
using Nourriture.NewMealWindow.ViewModel;
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
using System.Windows.Shapes;

namespace Nourriture.NewMealWindow.View
{
    /// <summary>
    /// Interaction logic for NewMealWindow.xaml
    /// </summary>
    public partial class NewMealWindow : Window
    {
        public NewMealWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            close.Style = (Style)FindResource(typeof(Button));
            close.Template = (ControlTemplate)FindResource("btnTmpltFile");
            add.Style = (Style)FindResource(typeof(Button));
            add.Template = (ControlTemplate)FindResource("btnTmplt");
            addIng.Style = (Style)FindResource(typeof(Button));
            addIng.Template = (ControlTemplate)FindResource("btnTmpltFile");
            removeIng.Style = (Style)FindResource(typeof(Button));
            removeIng.Template = (ControlTemplate)FindResource("btnTmpltFile");
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
