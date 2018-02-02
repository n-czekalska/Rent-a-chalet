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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data;
using Business;
using System.Collections.ObjectModel;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Customer> Customers
        {
            get { return CustomerMethods.Instance.list; }
        }
        public ObservableCollection<Booking> Bookings
        {
            get { return BookingMethods.Instance.list; }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void SetAllCustomers()
        {
           
        }
        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddNewCustomer addNewCustomerWindow = new AddNewCustomer();
            addNewCustomerWindow.ShowDialog();
        }

        void AddNewCustomerWindow_Closed(object sender, EventArgs e)
        {
          
        }
        private void AddNewBooking_Click(object sender, RoutedEventArgs e)
        {
            AddNewBooking addNewBookingWindow = new AddNewBooking();
            addNewBookingWindow.ShowDialog();
        }
        //--------------------------------------------------BOOKING TAB---------------------------------------------------------------------------------------------------------------------------

        private void bookingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isEnabled = bookingListView.SelectedIndex >= 0;
            EditBookingButton.IsEnabled = isEnabled;
            RemoveBookingButton.IsEnabled = isEnabled;
            DisplayInvoiceButton.IsEnabled = isEnabled;

        }
        private void RemoveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            var bookingId = bookingListView.SelectedIndex;
            if (bookingId >= 0)
            {
                bookingId = Bookings[bookingId].ReferenceNumber;
                var errors = BookingMethods.Instance.Remove(bookingId);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join(System.Environment.NewLine, errors), "Validation Error", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }
        private void EditBookingButton_Click(object sender, RoutedEventArgs e)
        {
            var id = bookingListView.SelectedIndex;
            if (id > -1)
            {
                var booking = BookingMethods.Instance.list[id];
                AddNewBooking addNewBooking = new AddNewBooking(booking);
                addNewBooking.ShowDialog();
            }
        }
        private void DisplayInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            var id = bookingListView.SelectedIndex;
            if (id > -1)
            {
                var booking = BookingMethods.Instance.list[id];
                DisplayInvoice displayInvoice = new DisplayInvoice(booking);
                displayInvoice.ShowDialog();
            }
        }

        //--------------------------------------------------CUSTOMER TAB---------------------------------------------------------------------------------------------------------------------------
        private void RemoveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customerId = customersListView.SelectedIndex;
            if (customerId >= 0)
            {
                customerId = Customers[customerId].ReferenceNumber;
                var errors = CustomerMethods.Instance.Remove(customerId);
                if (errors.Any())
                {
                    MessageBox.Show(string.Join(System.Environment.NewLine, errors), "Validation Error", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }
        private void EditCustomergButton_Click(object sender, RoutedEventArgs e)
        {
            var id = customersListView.SelectedIndex;
            if (id > -1)
            {
                var customer = CustomerMethods.Instance.list[id];
                AddNewCustomer addNewCustomer = new AddNewCustomer(customer);
                addNewCustomer.ShowDialog();
            }
        }
        private void customersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isEnabled = customersListView.SelectedIndex >= 0;
            EditCustomerButton.IsEnabled = isEnabled;
            RemoveCustomerButton.IsEnabled = isEnabled;
        }

      
    }

}

    

