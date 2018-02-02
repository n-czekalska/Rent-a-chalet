//Natalia Czekalska 40286736
//This is a Customer class used to create a Customer object that extednds a Factory class used to produce a unique reference numbers
//8/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class Customer : Factory<Customer>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public override int ReferenceNumber { get; set; }

        //method overriding abstract method SetRefNumber
        public override int SetRefNumber(int id)
        {
           int lastid = id;
            ReferenceNumber = base.GetReferenceNumber(lastid);
            return ReferenceNumber;
        }
}
}
