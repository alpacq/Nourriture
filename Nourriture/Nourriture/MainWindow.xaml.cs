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
        private Uri AzurePath = new Uri("https://nourriture.blob.core.windows.net/nourriture/database.xml");
        private string LocalPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\database.xml";
        private const string AzureConnectionString1 = "DefaultEndpointsProtocol=https;AccountName=nourriture;AccountKey=fHfAH9oez8kA9H7YxQFe42LWMFaRISaZSPtcSd5EwbYIAYY27Y0VbaQrQ/8mEmOQ3fOGNB+3nmgGzHWr0TSAPQ==;EndpointSuffix=core.windows.net";
        private const string AzureConnectionString2 = "DefaultEndpointsProtocol=https;AccountName=nourriture;AccountKey=+ZMK6rMq4Hf2nFytgz8iP9fYx3E5fn1/87MBce5SejgI3JCC9m+UFi5mHlJyvO5vFTHLvuBipPLURGI/aOyeSg==;EndpointSuffix=core.windows.net";
        private InventoryViewModel invVM;
        private RecipesViewModel recVM;
        private ShoppingListViewModel slVM;
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
                FileStream fs = new FileStream(LocalPath, FileMode.Open);
                this.Db = (Database)ser.Deserialize(fs);
            }
            else this.Db = new Database();
        }

        public void UploadXml()
        {
            BlobContainerClient container = new BlobContainerClient(AzureConnectionString1, "nourriture");
            try
            {
                BlobClient blob = container.GetBlobClient("database.xml");
                blob.Upload(File.OpenRead(LocalPath));
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
                blob.Delete();
            }
            finally
            {
                //
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is System.Windows.Controls.TabItem)
                {
                    this.invVM = new InventoryViewModel(this.Db);
                    this.recVM = new RecipesViewModel(this.Db);
                    this.slVM = new ShoppingListViewModel(this.Db);
                    this.invView.DataContext = this.invVM;
                    this.recView.DataContext = this.recVM;
                    this.slView.DataContext = this.slVM;
                }
            }
            this.recVM.SortRecipes();
        }
    }
}
