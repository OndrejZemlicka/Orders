using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objednavky
{
    public class Customer
    {

        public string name { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }


        public Customer(string name, string surname, string address, string zipcode, string city, string country)
        {
            this.name = name;
            this.surname = surname;
            this.address = address;
            this.zipcode = zipcode;
            this.city = city;
            this.country = country;
        }


    }
}
