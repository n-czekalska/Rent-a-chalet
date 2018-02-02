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
    /// Interaction logic for DisplayInvoice.xaml
    /// </summary>
    public partial class DisplayInvoice : Window
    {
        public DisplayInvoice(Booking booking)
        {
            InitializeComponent();
            var total = 0;
            int nights = (booking.DepartureDate - booking.ArrivalDate).Days;
            this.BookingRef.Content += " " + booking.ReferenceNumber + "";
            this.BookingPeriod.Content = $"{booking.ArrivalDate:dd-MM-yyyy} - {booking.DepartureDate:dd-MM-yyyy}";
            this.Nights.Content = nights + " x " + "£60";
            this.NightsCost.Content = "£" + nights * 60;
            this.Guests.Content = booking.Guests.Count + " guest(s) x " + nights + " nights x " + "£25";
            this.GuestsCost.Content = "£" + booking.Guests.Count * nights * 25;

            total += nights * 60;
            total += nights * booking.Guests.Count * 25;

            if (booking.BookingExtras.Breakfast)
            {
                this.Brekfasts.Content = booking.Guests.Count + " guest(s) x " + nights + " nights x " + "£5";
                this.BrekfastsCost.Content = "£" + booking.Guests.Count * nights * 5;
                total += nights * booking.Guests.Count * 5;

            }
            else
            {
                CostGrid.RowDefinitions[4].Height = new GridLength(0);
            }
            if (booking.BookingExtras.EveningMeal)
            {
                this.EveningMeals.Content = booking.Guests.Count + " guest(s) x " + nights + " nights x " + "£10";
                this.EveningMealsCost.Content = "£" + booking.Guests.Count * nights * 10;
                total += nights * booking.Guests.Count * 10;

            }
            else
            {
                CostGrid.RowDefinitions[5].Height = new GridLength(0);
            }
            if (booking.BookingExtras.CarHire)
            {
                this.CarHire.Content = nights + " nights x " + "£50";
                this.CarHireCost.Content = "£" + booking.Guests.Count * nights * 50;
                total += nights * 50;

            }
            else
            {
                CostGrid.RowDefinitions[6].Height = new GridLength(0);
            }

            Total.Text = "£" + total;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}