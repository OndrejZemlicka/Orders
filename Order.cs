using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objednavky
{
    public class Order
    {

        public int id { get; set; }
        public float price { get; set; }     
        public string currency { get; set; }
        public string cis_obj { get; set; }
        public string cis_zas { get; set; }
        public string date { get; set; }
        public int id_cu { get; set; }


        public Order(float price, string currency, string cis_obj, string cis_zas, string date)
        {
            this.price = price;
            this.currency = currency;
            this.cis_obj = cis_obj;
            this.cis_zas = cis_zas;
            this.date = date;
        }





    }
}
