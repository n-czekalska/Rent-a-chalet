//Natalia Czekalska 40286736
//This is a Booking class used to create a Booking object that extednds a Factory class  used to produce a unique reference numbers
//8/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{

    public class Booking : Factory<Booking>
    {
        //Booking variables
        public override int ReferenceNumber { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public List<Guest> Guests { get; set; }
        public Customer Customer { get; set; }
        public Chalet ChaletID { get; set; }
        public int ChaletId { get; set; }
        public int CustomerId { get; set; }

        //variable used to store number of guests associated with this booking
        public int NumberOfGuests { get { return this.Guests.Count; } }

        //variable of type BookingExtras to store added supplements
        public BookingExtras BookingExtras { get; set; }

        //method overriding abstract method SetRefNumber
        public override int SetRefNumber(int id)
        {
            int lastid = id;
            ReferenceNumber = base.GetReferenceNumber(lastid);
            return ReferenceNumber;
        }
    }
    //enum Chalet used e.g. to create combobox Chalets in AddNewBooking window
    public enum Chalet
    {
        Chalet1 = 1,
        Chalet2 = 2,
        Chalet3 = 3,
        Chalet4 = 4,
        Chalet5 = 5,
        Chalet6 = 6,
        Chalet7 = 7,
        Chalet8 = 8,
        Chalet9 = 9,
        Chalet10 = 10,

    }
}

