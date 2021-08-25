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
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Nourriture.Common;
using Nourriture.Inventory.View;
using Nourriture.Recipes.View;
using Nourriture.ShoppingList.View;
using Nourriture.Inventory.ViewModel;
using Nourriture.Recipes.ViewModel;
using Nourriture.ShoppingList.ViewModel;
using System.Windows.Controls.Primitives;
using System.Xml;

namespace Nourriture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db;
        private Uri AzurePath = new Uri("https://nourriture.blob.core.windows.net/nourriture/database.xml");
        private string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\database.xml";
        private InventoryViewModel invVM;
        private RecipesViewModel recVM;
        private ShoppingListViewModel slVM;
        private InventoryView invView;
        private RecipesView recView;
        private ShoppingListView slView;
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
            this.DownloadXml();
            this.DeserializeData();
            this.invVM = new InventoryViewModel(this.Db);
            this.recVM = new RecipesViewModel(this.Db);
            this.slVM = new ShoppingListViewModel(this.Db);
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            invBtn.Style = (Style)FindResource(typeof(Button));
            invBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            recBtn.Style = (Style)FindResource(typeof(Button));
            recBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            slBtn.Style = (Style)FindResource(typeof(Button));
            slBtn.Template = (ControlTemplate)FindResource("btnTmpltMenu");
            save.Style = (Style)FindResource(typeof(Button));
            save.Template = (ControlTemplate)FindResource("btnTmpltFile");
            close.Style = (Style)FindResource(typeof(Button));
            close.Template = (ControlTemplate)FindResource("btnTmpltFile");
            this.invView = new InventoryView();
            this.recView = new RecipesView();
            this.slView = new ShoppingListView();
            this.invView.Style = (Style)FindResource(typeof(Page));
            this.recView.Style = (Style)FindResource(typeof(Page));
            this.slView.Style = (Style)FindResource(typeof(Page));
            this.frm.Content = this.invView;
            this.invView.DataContext = this.invVM;
            this.recView.DataContext = this.recVM;
            this.slView.DataContext = this.slVM;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SerializeData();
            this.UploadXml();
        }

        public void SerializeData()
        {
            XmlSerializer ser = new XmlSerializer(typeof(Database));
            TextWriter writer = new StreamWriter(LocalPath);
            ser.Serialize(writer, this.Db);
            writer.Close();
        }

        public void DeserializeData()
        {
            if (File.Exists(LocalPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Database));
                using (XmlReader reader = XmlReader.Create(LocalPath))
                {
                    this.Db = (Database)ser.Deserialize(reader);
                }
            }
            else this.Db = new Database();
        }

        public void UploadXml()
        {
            BlobContainerClient container = new BlobContainerClient(AzureConnectionString1, "nourriture");
            try
            {
                BlobClient blob = container.GetBlobClient("database.xml");
                blob.Delete();

                blob = container.GetBlobClient("database.xml");
                blob.Upload(File.OpenRead(LocalPath));
                status.Text = DateTime.Now.ToLongTimeString() + " Dane zapisane w chmurze.";
            }
            finally
            {
                //
            }
        }

        public void DownloadXml()
        {
            BlobContainerClient container = new BlobContainerClient(AzureConnectionString1, "nourriture");
            try
            {
                BlobClient blob = container.GetBlobClient("database.xml");
                BlobDownloadInfo download = blob.Download();
                using (FileStream file = File.OpenWrite(LocalPath))
                {
                    download.Content.CopyTo(file);
                }
            }
            finally
            {
                //
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            this.SerializeData();
            this.UploadXml();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void invBtn_Click(object sender, RoutedEventArgs e)
        {
            this.invView = new InventoryView();
            this.recView = new RecipesView();
            this.slView = new ShoppingListView();
            this.invView.Style = (Style)FindResource(typeof(Page));
            this.recView.Style = (Style)FindResource(typeof(Page));
            this.slView.Style = (Style)FindResource(typeof(Page));
            this.invVM = new InventoryViewModel(this.Db);
            this.recVM = new RecipesViewModel(this.Db);
            this.slVM = new ShoppingListViewModel(this.Db);
            this.invView.DataContext = this.invVM;
            this.recView.DataContext = this.recVM;
            this.slView.DataContext = this.slVM;
            this.frm.Content = this.invView;
            this.recVM.SortRecipes();
        }

        private void recBtn_Click(object sender, RoutedEventArgs e)
        {
            this.invView = new InventoryView();
            this.recView = new RecipesView();
            this.slView = new ShoppingListView();
            this.invView.Style = (Style)FindResource(typeof(Page));
            this.recView.Style = (Style)FindResource(typeof(Page));
            this.slView.Style = (Style)FindResource(typeof(Page));
            this.invVM = new InventoryViewModel(this.Db);
            this.recVM = new RecipesViewModel(this.Db);
            this.slVM = new ShoppingListViewModel(this.Db);
            this.invView.DataContext = this.invVM;
            this.recView.DataContext = this.recVM;
            this.slView.DataContext = this.slVM;
            this.frm.Content = this.recView;
            this.recVM.SortRecipes();
        }

        private void slBtn_Click(object sender, RoutedEventArgs e)
        {
            this.invView = new InventoryView();
            this.recView = new RecipesView();
            this.slView = new ShoppingListView();
            this.invView.Style = (Style)FindResource(typeof(Page));
            this.recView.Style = (Style)FindResource(typeof(Page));
            this.slView.Style = (Style)FindResource(typeof(Page));
            this.invVM = new InventoryViewModel(this.Db);
            this.recVM = new RecipesViewModel(this.Db);
            this.slVM = new ShoppingListViewModel(this.Db);
            this.invView.DataContext = this.invVM;
            this.recView.DataContext = this.recVM;
            this.slView.DataContext = this.slVM;
            this.frm.Content = this.slView;
            this.recVM.SortRecipes();
        }

        private void invBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = invBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "Inwentarz";
        }

        private void onMouseLeave(object sender, MouseEventArgs e)
        {
            popupWrapper.Visibility = Visibility.Collapsed;
            popupWrapper.IsOpen = false;
        }

        private void recBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = recBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "Przepisy";
        }

        private void slBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            popupWrapper.PlacementTarget = slBtn;
            popupWrapper.Placement = PlacementMode.Bottom;
            popupWrapper.IsOpen = true;
            header.PopupText.Text = "Lista zakupów";
        }
    }
}
