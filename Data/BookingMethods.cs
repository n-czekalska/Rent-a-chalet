//Natalia Czekalska 40286736
//This is a  class used to add, remove, change bookings and validate the input. This class is created using Singleton pattern to 
//have only 1 list of bookings.
//8/12/2017


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Business;

namespace Data
{
    public class BookingMethods
    {
        private ObservableCollection<Booking> Bookings = new ObservableCollection<Booking>();//creates a private list of bookings
        public ObservableCollection<Booking> list // public access to the list Bookings.
        {
            get { return Bookings; }
        }
        //Sinleton pattern used to create 1 instance of the class
        private static BookingMethods instance; 
        public static BookingMethods Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookingMethods();
                }
                return instance;
            }

        }
        //end of Singleton

            //metod to find the last used reference number
        public int GetLastId()
        {
            int lastid;
            if (list.Count == 0)
            {
                lastid = 0;
            }
            else
            {
                lastid = list.Last().ReferenceNumber;
            }
            return lastid;
        }
        //method that validates the input,  adds new booking to the list  and creates a unique reference number
        public List<string> AddNew(Booking item)
        {
            int id = GetLastId();
            var result = new List<string>();
            var errors = RunBookingValidation(item);
            if (errors.Any())
                return errors;
            item.ReferenceNumber = item.SetRefNumber(id);
            Instance.Bookings.Add(item);
            return result;
        }

        //method to remove booking based on it's reference number
        public List<string> Remove(int id)
        {
            Bookings.Remove(Bookings.First(x => x.ReferenceNumber == id));
            return new List<string>();

        }
            //method for inserting a changed booking tointo a place of the old one based on its reference number
        public List<string> Edit(Booking item)
        {
            var errors = RunBookingValidation(item);
            if (errors.Any())
                return errors;
            var it = Bookings.First(x => x.ReferenceNumber == item.ReferenceNumber);
            var index = Bookings.IndexOf(it);

            Bookings[index] = item;
            return errors;
        }
        //method used for the input check and error message display
        internal List<string> RunBookingValidation(Booking item)
        {
            var result = new List<string>();

            if (item.ArrivalDate == null) 
            {
                result.Add("Arrival Date must be set");
                return result;
            }

            if (item.ArrivalDate == null)
            {
                result.Add("Departure Date must be set");
                return result;
            }

            var arrivalDate = item.ArrivalDate;
            var departureDate = item.DepartureDate;

            if (departureDate <= arrivalDate)
            {
                result.Add("Deprature Date must be later than Arrival Date");
            }

            if (arrivalDate.Date < DateTime.Now.Date || departureDate < DateTime.Now.Date)
            {
                result.Add("Cannot book in the past");
            }

            var customerId = item.CustomerId;
            var chaletId = item.ChaletId;

            if (customerId <= 0)
            {
                result.Add("Please select a customer");
            }
            if (chaletId < 0)
            {
                result.Add("Please select a chalet");
            }
            if (item.Guests.Count == 0)
            {
                result.Add("Please add a minimum of 1 guest");
            }

            if (item.BookingExtras != null && item.BookingExtras.CarHire)
            {
                if (!item.BookingExtras.CarHireStartDate.HasValue)
                {
                    result.Add("Car hire start date must be set");
                    return result;
                }
                if (!item.BookingExtras.CarHireEndDate.HasValue)
                {
                    result.Add("Car hire end date  must be set");
                    return result;
                }

                var carStartDate = item.BookingExtras.CarHireStartDate.Value;
                var carEndDate = item.BookingExtras.CarHireEndDate.Value;
                if (carEndDate <= carStartDate)
                {
                    result.Add("Please change the end of the hire day");
                }
                var guestId = item.BookingExtras.DriverId;
                if (guestId < 0)
                {
                    result.Add("Please select a driver from guests");
                }
            }
            if (result.Any())// if any errors are present , message about it is returned
            {
                return result;
            }
            // if selected chalet has a booking at the selected day a message about existing reservation is displayed and prompt to change the chalet or days
            item.ChaletID = ((Chalet)item.ChaletId + 1);
            var bookingOfThisChalet = Bookings.Where(x => x.ChaletId == item.ChaletId && x.ReferenceNumber != item.ReferenceNumber).ToList();

            foreach (var b in bookingOfThisChalet)
            {
                if (b.ArrivalDate.Date < item.DepartureDate.Date && item.ArrivalDate.Date < b.DepartureDate.Date)
                {
                    result.Add("There is another booking between these days at this chalet! Please select another Chalet or different days");
                    break;
                }
            }
            //car can only be rented during the booking period
            if (item.BookingExtras != null && item.BookingExtras.CarHire)
            {
                if (item.BookingExtras.CarHireStartDate.Value.Date < item.ArrivalDate.Date || item.BookingExtras.CarHireEndDate.Value.Date > item.DepartureDate.Date)
                {
                    result.Add("Car can be hired only between booking days!");
                }
            }
            return result;
        }

      
    }
}