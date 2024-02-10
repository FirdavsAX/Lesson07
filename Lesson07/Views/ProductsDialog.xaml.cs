using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
using Lesson07.Data;
using Lesson07.Models;
using Microsoft.EntityFrameworkCore.Query;
using MvvmHelpers;
using Prism.Commands;

namespace Lesson07.Views
{
    /// <summary>
    /// Interaction logic for ProductsDialog.xaml
    /// </summary>
    public partial class ProductsDialog : UserControl
    {
        private readonly InventoryDbContext database;
        public List<Category> Categories { get;private set; }        
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public DateTime ProductExpireDate { get; set; }
        public ICommand SaveCommand { get; set; }

        public ProductsDialog()
        {
            InitializeComponent();
            DataContext = this;

            database = new InventoryDbContext();
            Categories = new();

            
            SaveCommand = new DelegateCommand(OnSave);
            Load();

        }
        public void Load()
        {
            Categories = database.Categories.ToList();
        }
        private void OnSave()
        {
            try
            {
                var messageBoxResult = MessageBox.Show(
                    "Are you sure!", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult is MessageBoxResult.No) { return; }

                Product product = new Product()
                {
                    Name = ProductName,
                    Description = ProductDescription,
                    Price = decimal.Parse(ProductPrice),
                    ExpireDate = ProductExpireDate,
                    CategoryId = (CategoryComboBox.SelectedItem as Category).Id
                };

                database.Products.Add(product);
                database.SaveChanges();
                MessageBox.Show("Product succesfully created!");
            
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
            
        }

    }
}
