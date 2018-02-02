using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Business;
using Data;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AddNewCustomer.xaml
    /// </summary>
    public partial class AddNewCustomer : Window
    {
        private bool isEditMode = false;
        private int refNumber = -1;
        public AddNewCustomer(Customer customer = null)
        {
            InitializeComponent();
            DataContext = this;
            if (customer != null)
            {
                this.Title = "Edit Customer";
                this.refNumber = customer.ReferenceNumber;
                isEditMode = true;
                EditCustomer(customer);
            }

        }

        private void EditCustomer(Customer customer)
        {
            customerNameTextBox.Text = customer.Name;
            customerAddressTextBox.Text = customer.Address;
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            var name = customerNameTextBox.Text;
            var address = customerAddressTextBox.Text;
            if (isEditMode)
            {
                CustomerMethods.Instance.Edit(new Customer() { Address = address, Name = name, ReferenceNumber = refNumber });
            }
            else
            {
                Customer customer = new Customer() { Address = address, Name = name };
                // CustomerMethods.Instance.AddNew(new Customer() { Address = address, Name = name });
                CustomerMethods.Instance.AddNew(customer);
               
            }
            this.Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit? Unsaved changes will be lost", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();

            }
        }
    }
}
