using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objednavky
{
    public partial class AddCustomer : Form
    {

        SqlConnection conn;
        Order order;

        public AddCustomer(SqlConnection conn, Order order)
        {
            InitializeComponent();
            this.conn = conn;
            this.order = order;
        }

        /// <summary>
        ///         takes the user to the NewOrder window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new NewOrder(conn);
            window.FormClosed += (s, args) => this.Close(); 
            window.Show();
        }

        /// <summary>
        ///         Checks if the user filled out everything, and if not, changes the color if the messages to red so the
        ///         user knows what still needs to be filled out. If everything is filled out, the user is taken to the AddProducts window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, EventArgs e)
        {

            if(boxAddress.Text != "" && boxCity.Text != "" && boxCountry.Text != "" && boxName.Text != "" && boxSurname.Text != "" && boxZipcode.Text != "")
            {


                Customer cus = new Customer(boxName.Text, boxSurname.Text, boxAddress.Text, boxZipcode.Text, boxCity.Text, boxCountry.Text);


                this.Hide();

                var window = new AddProducts(this.conn, this.order, cus);
                window.FormClosed += (s, args) => this.Close(); 
                window.Show();


            }
            else
            {
                if(boxName.Text == "")
                {
                    label7.ForeColor = Color.Red;
                }
                if(boxSurname.Text == "")
                {
                    label8.ForeColor = Color.Red;
                }
                if(boxAddress.Text == "")
                {
                    label9.ForeColor = Color.Red;
                }
                if (boxZipcode.Text == "")
                {
                    label10.ForeColor = Color.Red;
                }
                if(boxCity.Text == "")
                {
                    label11.ForeColor = Color.Red;
                }
                if(boxCountry.Text == "")
                {
                    label12.ForeColor = Color.Red;
                }
                
              
                
            }


        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
