//Natalia Czekalska 40286736
//This is an abstract class working as a factory interface that standarizes creation of unique reference numbers
//8/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public abstract class ReferenceFactory
    {

        public virtual int ReferenceNumber { get; set; }//variable reference number 
        public abstract int SetRefNumber(int id); //abstract method that will be ovverided during the creation of objects

    }
    public abstract class Factory<T> : ReferenceFactory //creating a factory that extends a ReferenceFactory class
    {

        public abstract override int SetRefNumber(int id);

        protected int GetReferenceNumber(int lastid)//adds 1 to the last known reference number
        {
            int id = lastid + 1;
            return id;
        }


        
    }
}