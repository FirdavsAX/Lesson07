using Lesson07.Data;
using Lesson07.Models;
using MvvmHelpers;
using Prism.Commands;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.IdentityModel.Tokens;
using Lesson07.Views;
using MaterialDesignThemes.Wpf;

namespace Lesson07.ViewModels
{
    public class SuppliersViewModel : BaseViewModel
    {
        private readonly InventoryDbContext database;
        public ObservableCollection<Supplier> Suppliers{ get; set; }
        private List<Supplier> AllSuppliers;

        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                FilterSuppliers();
            }
        }
        public SuppliersViewModel()
        {
            database = new();
            Suppliers= new();
            AllSuppliers = new();

            Task.Run(Load);

            CreateCommand = new DelegateCommand(OnCreate);
            SaveCommand = new DelegateCommand(OnSave);
        }
        public void OnCreate()
        {
            var view = new SupplierDialog();
            var result = DialogHost.Show(view, "MainDialog");
        }
        public void OnSave()
        {

        }
        public async Task Load()
        {
            AllSuppliers = await database.Suppliers.ToListAsync();
            Suppliers.AddRange(AllSuppliers);
        }

        public void FilterSuppliers()
        {
            if (_search.IsNullOrEmpty())
            {
                return;
            }
            Suppliers.Clear();
            var filterList = AllSuppliers
                .Where(s => (s.FirstName + s.LastName + s.Company).Contains(_search))
                .ToList();
            Suppliers.AddRange(filterList);
        }
    }
}
