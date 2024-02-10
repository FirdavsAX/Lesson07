using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lesson07.Data;
using Lesson07.Models;
using Lesson07.Views;
using MaterialDesignThemes.Wpf;
using MvvmHelpers;
using Prism.Commands;
namespace Lesson07.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        private readonly InventoryDbContext database;
        public ObservableCollection<Customer> Customers { get; set; }
        private List<Customer> AllCustomers;
        private string _search;

        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand {get;set;}
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                FilterCustomers();
            }
        }

        public CustomersViewModel()
        {
            Customers = new();
            AllCustomers = new();
            database = new();

            CreateCommand = new DelegateCommand(OnCreateCustomer);
            SaveCommand = new DelegateCommand(OnSaveCustomer);

            Load();
        }
        public void Load()
        {
            AllCustomers = database.Customers.ToList();
            Customers.AddRange(AllCustomers);
        }
        public void FilterCustomers()
        {
            Customers.Clear();
            var filterList = AllCustomers
                .Where(c => c.FirstName.Contains(_search) || c.LastName.Contains(_search) || c.Address.Contains(_search))
                .ToList();
            Customers.AddRange(filterList);
        }
        private void OnCreateCustomer()
        {
            var view = new CustomerDialog();
            var result = DialogHost.Show(view, "MainDialog");
        }
        public void OnSaveCustomer()
        {

        }

    }
}
