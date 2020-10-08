using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Nourriture.Common;
using Nourriture.Inventory.ViewModel;
using Nourriture.Recipes.ViewModel;
using Nourriture.ShoppingList.ViewModel;

namespace Nourriture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db;
        public Database Db
        {
            get
            {
                return this.db;
            }
            set
            {
                this.db = value;
            }
        }

        public MainWindow()
        {
            this.DeserializeData();
            InventoryViewModel invVM = new InventoryViewModel(this.Db);
            RecipesViewModel recVM = new RecipesViewModel(this.Db);
            ShoppingListViewModel slVM = new ShoppingListViewModel(this.Db);
            InitializeComponent();
            this.invView.DataContext = invVM;
            this.recView.DataContext = recVM;
            this.slView.DataContext = slVM;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SerializeData();
        }

        public void SerializeData()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Database));
            TextWriter writer = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\database.xml");
            ser.Serialize(writer, this.Db);
            writer.Close();
        }

        public void DeserializeData()
        {
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\database.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Database));
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "\\database.xml", FileMode.Open);
                this.Db = (Database)ser.Deserialize(fs);
            }
            else this.Db = new Database();
        }
    }
}
