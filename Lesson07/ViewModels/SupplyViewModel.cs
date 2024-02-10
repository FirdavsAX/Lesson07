using Lesson07.Data;
using Lesson07.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;
using MvvmHelpers;
using Prism.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lesson07.ViewModels
{
    public class SupplyViewModel:BaseViewModel
    {
        private readonly InventoryDbContext database;
        public ObservableCollection<Supply> Supplies { get; set; }
        private List<Supply> AllSupplies;

        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        private DateTime _search = DateTime.Now;
        public DateTime Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                FilterSupplies();
            }
        }
        public SupplyViewModel()
        {
            database = new();
            Supplies = new();
            AllSupplies = new();

            Task.Run(Load);

            CreateCommand = new DelegateCommand(OnCreate);
            SaveCommand = new DelegateCommand(OnSave);
        }
        public void OnCreate()
        {

        }
        public void OnSave() 
        {

        }
        public async Task Load()
        {
            AllSupplies = await database.Supplies.ToListAsync();
            Supplies.AddRange(AllSupplies);
        }

        public void FilterSupplies()
        {
            if(_search >= DateTime.Now)
            {
                return;
            }
            Supplies.Clear();
            var filterList = AllSupplies
                .Where(s => s.Date <= _search)
                .ToList();
            Supplies.AddRange(filterList);
        }
    }
}
