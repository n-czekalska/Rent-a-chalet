//Natalia Czekalska 40286736
//This is a class used to create a Extras object associated with Booking
//8/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class BookingExtras
    {
        //Variables used to describe extra options for booking
        public bool Breakfast { get; set; }
        public bool EveningMeal { get; set; }
        public bool CarHire { get; set; }
        public DateTime? CarHireStartDate { get; set; }
        public DateTime? CarHireEndDate { get; set; }
        public Guest Driver { get; set; }
        public int DriverId { get; set; }
    }
}
