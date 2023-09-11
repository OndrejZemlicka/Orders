using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objednavky
{
    public partial class OrdersWindow : Form
    {

        SqlConnection conn;

        public OrdersWindow(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        
        /// <summary>
        /// takes the user back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new MainMenu(conn);
            window.FormClosed += (s, args) => this.Close(); 
            window.Show();
        }

        /// <summary>
        /// when the page loads, the listView is populated by all the orders in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersWindow_Load(object sender, EventArgs e)
        {
            string sql = "SELECT COUNT(*) " +
                            "FROM Orders";
            int count = 0;

            using (SqlCommand cmd = new SqlCommand(sql, this.conn))
            {

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = (int)reader[0];
                }
                reader.Close();
            }

            listViewOrders.View = View.Details;

            Console.WriteLine("Count is : " + count);
            if (count == 0)
            {


                // Clear any existing columns and items
                listViewOrders.Columns.Clear();
                listViewOrders.Items.Clear();
                // Add a single column to the ListView
                listViewOrders.Columns.Add("");

                // Add the "there are no products" message as a single row
                ListViewItem messageItem = new ListViewItem("There are no orders");
                messageItem.ForeColor = Color.Red;
                listViewOrders.Items.Add(messageItem);

                // Auto-resize the column to fit the content
                listViewOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


            }
            else
            {

                listViewOrders.Columns.Clear();
                listViewOrders.Items.Clear();
                listViewOrders.Columns.Add("ID");
                listViewOrders.Columns.Add("Name");
                listViewOrders.Columns.Add("Surname");
                listViewOrders.Columns.Add("Price");
                listViewOrders.Columns.Add("Date");
                listViewOrders.Columns.Add("Order number");
                listViewOrders.Columns.Add("Package number");
                

                string sql2 = "SELECT o.id_or, c.name, c.surname, o.price, o.date, o.cis_obj, o.cis_zas " +
                                "FROM Orders o " +
                                "INNER JOIN Customers c " +
                                "   ON c.id_cu = o.id_cu;";

                using(SqlCommand cmd = new SqlCommand(sql2, this.conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem("" + reader[0]);
                        item.SubItems.Add("" + reader[1]);
                        item.SubItems.Add("" + reader[2]);
                        item.SubItems.Add("" + reader[3]);
                        item.SubItems.Add("" + reader[4]);
                        item.SubItems.Add("" + reader[5]);
                        item.SubItems.Add("" + reader[6]);
                        listViewOrders.Items.Add(item);
                    }
                }




                

                
                listViewOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }




        }

        /// <summary>
        /// takes the user to the NewOrder window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newOrderButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new NewOrder(conn);
            window.FormClosed += (s, args) => this.Close(); 
            window.Show();
        }
    }
}
