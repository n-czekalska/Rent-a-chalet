//Natalia Czekalska 40286736
//This is a  class used to add, remove, change customers and validate the input. This class is created using Singleton pattern to 
//have only 1 list of customers.
//8/12/2017

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Business;

namespace Data
{
    public class CustomerMethods

    {
        private ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();//creates a private list of customers
        public ObservableCollection<Customer> list // public access to the list Customers list.
        {
            get { return Customers; }
        }

        //using Singleton pattern to create Customer instance
        private static CustomerMethods instance;
     
        public static CustomerMethods Instance
         {
             get
             {
                if (instance == null)
                {
                    instance = new CustomerMethods(); 
                  
               }
               return instance;
             }
   
      }
        //Singleton end

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

        //method used to add a new customer and create a unique reference number for them
        public void AddNew(Customer item)
        {
            int id = GetLastId();
            item.ReferenceNumber = item.SetRefNumber( id);
            Instance.list.Add(item);
         
        }
      
        //method used to remove customer from a list only if they do not have any future bookings
        public List<string> Remove(int id)
        {
            
               if (BookingMethods.Instance.list.Any(x => x.CustomerId == id))
               {
                   return new List<string> { "Cannot delete the customer who has future bookings - First delete the booking" };
               }
            
            Customers.Remove(Customers.First(x => x.ReferenceNumber == id));
            return new List<string>();
      }

        //method to insert changed customer details in the place of the old one based on the reference number
            public List<string> Edit(Customer item)
        {
            var it = Customers.First(x => x.ReferenceNumber == item.ReferenceNumber);
            var index = Customers.IndexOf(it);
            Customers[index] = item;
            return new List<string>();
        }
    }
}
