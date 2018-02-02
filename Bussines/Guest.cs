//Natalia Czekalska 40286736
//This is a Guest class used to create a Guest object that is used when creating a Booking.
//8/12/2017


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
   public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PassportNumber { get; set; }
        public int Age { get; set; }
    }
}
