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
using Lesson07.Data;
using Lesson07.Models;
using MaterialDesignThemes.Wpf;
using MvvmHelpers;
using Prism.Commands;
namespace Lesson07.Views
{
    /// <summary>
    /// Interaction logic for CustomerDialog.xaml
    /// </summary>
    public partial class CustomerDialog : UserControl
    {
        private readonly InventoryDbContext database;

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; } = "+998";
        public string Address { get; set; }
        public ICommand SaveCommand { get; private set; }

        public CustomerDialog()
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
                if (string.IsNullOrEmpty(Address))
                {
                    Firstname = "Address can't be empty!";
                    return;
                }
                if  (PhoneNumber.Length != 13)
                {
                    Firstname = "Error format!";
                    return;
                }

                Customer customer = new Customer()
                {
                    FirstName = Firstname,
                    LastName = Lastname,
                    PhoneNumber = PhoneNumber,
                    Address = Address
                };

                var messageBoxResult = MessageBox.Show(
                   "Are you sure!", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult is MessageBoxResult.No) { return; }

                database.Customers.Add(customer);
                database.SaveChanges();

                MessageBox.Show("Customer Successfully created");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
