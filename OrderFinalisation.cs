using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objednavky
{
    public partial class OrderFinalisation : Form
    {

        SqlConnection conn;
        Order order;
        Customer cus;
        List<int> productIDs;


        public OrderFinalisation(SqlConnection conn, Order order, Customer cus, List<int> productIDs)
        {
            InitializeComponent();
            this.conn = conn;
            this.order = order;
            this.cus = cus;
            this.productIDs = productIDs;
        }

        /// <summary>
        /// takes the user back to the AddProducts window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new AddProducts(this.conn, this.order, this.cus);
            window.FormClosed += (s, args) => this.Close();
            window.Show();
        }

        /// <summary>
        ///     First deletes the time from the date and leaves only the day, month and year. Then it changes the labels so it desribes
        ///     the whole order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderFinalisation_Load(object sender, EventArgs e)
        {
            string dateString = this.order.date;
            DateTime dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateOnly = dateTime.Date;
            this.order.date = dateOnly.ToString("dd/MM/yyyy");

            label1.Text = cus.name + " " + cus.surname;
            label2.Text = cus.address + ", " + cus.zipcode;
            label3.Text = cus.city + ", " + cus.country;
            int index = 0;
            label4.Text = "Products: ";
            foreach(int id in this.productIDs)
            {
                if (index == 0)
                {
                    label4.Text += id;
                    index++;
                }
                else
                {
                    label4.Text += ", " + id;
                    index++;
                } 
            }

            label5.Text = order.price + " " + order.currency;
            label6.Text = order.date;
            label7.Text = "Order number: " + order.cis_obj;
            label8.Text = "Package number: " + order.cis_zas;



        }

        /// <summary>
        ///     Inserts all data into their respective database tables and takes the user back to the Orders window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishButton_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Customers VALUES('"+cus.name+"', '"+cus.surname+"', '"+cus.address+"', '"+cus.zipcode+"', '"+cus.city+"', '"+cus.country+"');";

            using(SqlCommand cmd = new SqlCommand(sql, this.conn))
            {
                cmd.ExecuteNonQuery();
            }

            sql = "SELECT c.id_cu " +
                    "FROM Customers c " +
                    "ORDER BY c.id_cu DESC;";

            int id_cu = 0;

            using(SqlCommand cmd = new SqlCommand(sql, this.conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_cu = (int)reader[0];
                }
                reader.Close();
            }

            sql = "INSERT INTO Orders VALUES("+order.price+", '"+order.currency+"', '"+order.cis_obj+"', '"+order.cis_zas+"', '"+order.date+"', "+id_cu+");";

            using (SqlCommand cmd = new SqlCommand(sql, this.conn))
            {
                cmd.ExecuteNonQuery();
            }

            sql = "SELECT o.id_or " +
                    "FROM Orders o " +
                    "ORDER BY o.id_or DESC;";

            int id_or = 0;

            using (SqlCommand cmd = new SqlCommand(sql, this.conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_or = (int)reader[0];
                }
                reader.Close();
            }

            foreach (int id in this.productIDs)
            {
                sql = "INSERT INTO OrdersProducts VALUES(" + id_or + ", " + id + ");";
                using (SqlCommand cmd = new SqlCommand(sql, this.conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }


            this.Hide();

            var window = new OrdersWindow(this.conn);
            window.FormClosed += (s, args) => this.Close(); 
            window.Show();


        }
    }
}
