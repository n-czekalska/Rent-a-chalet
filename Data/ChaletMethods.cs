//Natalia Czekalska 40286736
//This is a class using a Singleton pattern to create only one instance of itself accessed from any place in the program.

//8/12/2017


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using System.Collections.ObjectModel;

namespace Data
{
    public class ChaletMethods
    {

        //using Singleton pattern to create Chalet instance
        private static ChaletMethods instance;


        public static ChaletMethods Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChaletMethods();
                }
                return instance;
            }

        }
        //Singleton end

        public ObservableCollection<string> Chalets { get; }//list of strings to store chalets that is readonly
        public ChaletMethods()
        {
            Chalets = new ObservableCollection<string>();
            var enums = Enum.GetNames(typeof(Chalet)).ToList(); // creating a list of chaltes from enum type of variable from Bookings
            foreach (var item in enums)
            {
                Chalets.Add(item);
            }
        }
    }
}
