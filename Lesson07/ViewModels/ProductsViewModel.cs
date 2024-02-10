using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Lesson07.Models;
using System.Threading.Tasks;
using Lesson07.Data;
using System.Windows.Input;
using Prism.Commands;
using Lesson07.TestDataCreator;
using Microsoft.EntityFrameworkCore;
using Lesson07.Views;
using MaterialDesignThemes.Wpf;

namespace Lesson07.ViewModels
{
    public class ProductsViewModel:BaseViewModel
    {
        private readonly InventoryDbContext database;
        public ObservableCollection<Product> Products { get; set; }
        public List<Product> AllProducts { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                FilterProduct();
            }
        }

        public ProductsViewModel()
        {
            Products = new();
            AllProducts = new();
            database = new();


            CreateCommand = new DelegateCommand(OnCreateProduct);
            SaveCommand = new DelegateCommand(OnSaveProduct);

            Task.Run(async () =>
            {
                await Load();
            });
        }
        public async Task Load()
        {
            AllProducts = await database.Products.ToListAsync();
         
            Products.AddRange(AllProducts);
        }
        public async void OnCreateProduct()
        {
                var view = new ProductsDialog();
                var result = await DialogHost.Show(view, "MainDialog");
        }
        public void OnSaveProduct()
        {

        }
        public void FilterProduct()
        {
            Products.Clear();
            var filterList = AllProducts.Where(p => p.Name.Contains(_search)).ToList();
            Products.AddRange(filterList);
        }

    }
}
