using Lesson07.Data;
using Lesson07.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
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
namespace Lesson07.Views
{
    /// <summary>
    /// Interaction logic for SupplierDialog.xaml
    /// </summary>
    public partial class SupplierDialog : UserControl
    {
        private readonly InventoryDbContext database;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; } = "+998";
        public string Company { get; set; }
        public ICommand SaveCommand { get; private set; }

        public SupplierDialog()
        {
            InitializeComponent();
            DataContext = this;

            database = new();
            SaveCommand = new DelegateCommand(OnSave);
        }
        private void OnSave()
        {
            try
            {
                if (string.IsNullOrEmpty(Firstname))
                {
                    Firstname = "Firstname can't be empty!";
                    return;
                }
                if (string.IsNullOrEmpty(Company))
                {
                    Firstname = "Company can't be empty!";
                    return;
                }
                if (PhoneNumber.Length != 13)
                {
                    Firstname = "Error format!";
                    return;
                }

                Supplier supplier= new ()
                {
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNumber = PhoneNumber,
                    Company = Company,
                };

                var messageBoxResult = MessageBox.Show(
                   "Are you sure!", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult is MessageBoxResult.No) { return; }

                database.Suppliers.Add(supplier);
                database.SaveChanges();

                MessageBox.Show("Supplier successfully created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
