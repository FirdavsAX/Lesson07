using Lesson07.Data;
using Lesson07.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MvvmHelpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lesson07.ViewModels
{
    public class SalesViewModel:BaseViewModel
    {
        private readonly InventoryDbContext database;
        public ObservableCollection<Sale> Sales { get; set; }
        private List<Sale> AllSales;
        
        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        private DateTime _search = DateTime.Now;
        public DateTime Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                FilterSales();
            }
        }
        public SalesViewModel()
        {
            database = new();
            Sales = new();
            AllSales = new();

            Load();

            CreateCommand = new DelegateCommand(OnCreate);
            SaveCommand = new DelegateCommand(OnSave);
        }
        public void OnCreate()
        {

        }
        public void OnSave()
        {

        }
        public void Load()
        {
            AllSales = database.Sales.ToList();
            Sales.AddRange(AllSales);
        }

        public void FilterSales()
        {
            if (_search >= DateTime.Now) return;

            Sales.Clear();
            var filterList = AllSales
                .Where(s => s.SaleDate <= _search)
                .ToList();
            Sales.AddRange(filterList);
        }
    }

}
