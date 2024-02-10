using Microsoft.EntityFrameworkCore.Query;
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
using System.Collections.ObjectModel;
using Lesson07.Data;
using Lesson07.Models;
using Prism.Commands;
namespace Lesson07.Views
{
    /// <summary>
    /// Interaction logic for CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : UserControl
    {
        private readonly InventoryDbContext database;
        public string CategoryName { get; set; }
        public ICommand SaveCommand { get; private set; }

        public CategoryDialog()
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
                if (string.IsNullOrEmpty(CategoryName))
                {
                    CategoryName = "CAtegory name can't be empty!";
                    return;
                }
              

                Category category = new()
                {
                    Name = CategoryName,
                };

                var messageBoxResult = MessageBox.Show(
                   "Are you sure!", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult is MessageBoxResult.No) { return; }

                database.Categories.Add(category);
                database.SaveChanges();

                MessageBox.Show("Category successfully created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
