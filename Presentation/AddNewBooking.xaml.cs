using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Collections.ObjectModel;

namespace Presentation
{
   
    public partial class AddNewBooking : Window
    {
        private bool isEditMode = false;
        private int refNumber = -1;
       
        public ObservableCollection<string> Chalets
        {
            get { return ChaletMethods.Instance.Chalets; }
        }
        public ObservableCollection<Customer> Customers
        {
          get { return CustomerMethods.Instance.list; }
        }
        private ObservableCollection<Guest> guests;

        public ObservableCollection<Guest> Guests
        {
            get { return this.guests; }

        }
        public AddNewBooking(Booking booking = null)
        {
            
            DataContext = this;
            this.guests = new ObservableCollection<Guest>();
            InitializeComponent();
            

            if (booking != null)
            {
                isEditMode = true;
                refNumber = booking.ReferenceNumber;
                EditBooking(booking);
                this.Title = "Edit Booking";
            }

        }

        private void EditBooking(Booking booking)
        {
            this.guests = new ObservableCollection<Guest>(booking.Guests);
            GuestsListView.ItemsSource = guests;
            this.ArrivalDate.SelectedDate = booking.ArrivalDate;
            this.DepratureDate.SelectedDate = booking.DepartureDate;
            this.Chalet.SelectedIndex = booking.ChaletId;
            var customer = this.Customers.First(x => x.ReferenceNumber == booking.CustomerId);
            this.Customer.SelectedItem = customer;
            this.HireCarCheckbox.IsChecked = booking.BookingExtras.CarHire;
            CarDriverName.ItemsSource = guests;
            if (booking.BookingExtras.CarHire)
            {
                CarFrom.IsEnabled = true;
                CarTo.IsEnabled = true;
                CarDriverName.IsEnabled = true;
                CarFrom.SelectedDate = booking.BookingExtras.CarHireStartDate;
                CarTo.SelectedDate = booking.BookingExtras.CarHireEndDate;
                var driver = this.guests.First(x => x.Id == booking.BookingExtras.DriverId);
                CarDriverName.SelectedItem = driver;
            }
            this.AddBreakfasts.IsChecked = booking.BookingExtras.Breakfast;
            this.AddEveningMeals.IsChecked = booking.BookingExtras.EveningMeal;

        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddNewCustomer addNewCustomer = new AddNewCustomer();
            addNewCustomer.ShowDialog();
        }

        private void AddNewBooking_Click(object sender, RoutedEventArgs e)
        {

            var extras = new BookingExtras()
            {
                Breakfast = AddBreakfasts.IsChecked.HasValue && AddBreakfasts.IsChecked.Value,
                EveningMeal = AddEveningMeals.IsChecked.HasValue && AddEveningMeals.IsChecked.Value,
                CarHire = HireCarCheckbox.IsChecked.HasValue && HireCarCheckbox.IsChecked.Value,
                DriverId = CarDriverName.SelectedIndex+1,
                CarHireStartDate = CarFrom.SelectedDate,
                CarHireEndDate = CarTo.SelectedDate,
            };

         

            var booking = new Booking()
            {
                CustomerId =Customer.SelectedIndex+1,
                ChaletId = Chalet.SelectedIndex,
                ArrivalDate = ArrivalDate.SelectedDate.Value,
                DepartureDate = DepratureDate.SelectedDate.Value,
                Guests = guests.ToList(),
                BookingExtras = extras
                
            };
            
            List<string> errors;
            if (isEditMode)
            {
                booking.ReferenceNumber = refNumber;
                errors = BookingMethods.Instance.Edit(booking);
            }
            else
            {
                errors = BookingMethods.Instance.AddNew(booking);
            }
            if (errors.Any())
            {
                MessageBox.Show(string.Join(System.Environment.NewLine, errors), "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                this.Close();
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit? Unsaved changes will be lost", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void AddNewGuest_Click(object sender, RoutedEventArgs e)
        {
            var name = AddNewGuestName.Text;
            var passPortNumber = AddNewGuestPassportNumber.Text;
            var age = int.Parse(AddNewGuestAge.Text);
            this.guests.Add(new Guest()
            {
                Id = this.guests.Count + 1,
                PassportNumber = passPortNumber,
                Name = name,
                Age = age
            });

            this.CleanAddNewGuest();
            if (this.guests.Count == 6)
            {
                this.AddNewGuestButton.IsEnabled = false;
            }

        }

        private void CleanAddNewGuest()
        {
            AddNewGuestName.Text = string.Empty;
            AddNewGuestPassportNumber.Text = string.Empty;
            AddNewGuestAge.Text = string.Empty;
        }

        private void guestsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = GuestsListView.SelectedIndex;
            this.RemoveSelectedGuestButton.IsEnabled = index >= 0;
        }

        private void RemoveSelectedGuest_Click(object sender, RoutedEventArgs e)
        {
            int index = GuestsListView.SelectedIndex;
            if (index >= 0)
            {
                this.guests.RemoveAt(index);
            }
            if (this.guests.Count == 0)
            {
                this.RemoveSelectedGuestButton.IsEnabled = false;
            }
            if (this.guests.Count < 6)
            {
                this.AddNewGuestButton.IsEnabled = true;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            int age = 0;
            if (int.TryParse(AddNewGuestAge.Text + e.Text, out age))
            {
                if (age < 0)
                    e.Handled = true;
                if (age > 101)
                    e.Handled = true;
            }
            else
            {
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        

        private void HireCarCheckboxChanged(object sender, RoutedEventArgs e)
        {
            if (HireCarCheckbox.IsChecked.HasValue && HireCarCheckbox.IsChecked.Value)
            {
                CarFrom.IsEnabled = true;
                CarTo.IsEnabled = true;
                CarDriverName.IsEnabled = true;
            }
            else
            {
                CarFrom.IsEnabled = false;
                CarTo.IsEnabled = false;
                CarDriverName.IsEnabled = false;
                CarFrom.SelectedDate = null;
                CarTo.SelectedDate = null;
                CarDriverName.SelectedItem = null;
            }
        }
    }
}