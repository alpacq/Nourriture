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
using System.Windows.Shapes;

namespace Nourriture.NewProductWindow.View
{
    /// <summary>
    /// Interaction logic for NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        public NewProductWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            close.Style = (Style)FindResource(typeof(Button));
            close.Template = (ControlTemplate)FindResource("btnTmpltFile");
            add.Style = (Style)FindResource(typeof(Button));
            add.Template = (ControlTemplate)FindResource("btnTmplt");
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
