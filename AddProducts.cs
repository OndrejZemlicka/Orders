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

    public partial class AddProducts : Form
    {

        SqlConnection conn;
        DatabaseManager manager;
        Order order;
        Customer cus;

        List<int> productIDs;


        public AddProducts(SqlConnection conn, Order order, Customer cus)
        {
            InitializeComponent();
            this.conn = conn;
            this.order = order;
            this.cus = cus;
            manager = new DatabaseManager(conn);
            productIDs = new List<int>();
        }

        /// <summary>
        ///         Takes the user back to the AddCustomer window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new AddCustomer(this.conn, this.order);
            window.FormClosed += (s, args) => this.Close();
            window.Show();
        }

        /// <summary>
        ///     Method is executed when the window loads. It populates the listView with products.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProducts_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;

            string sql = "SELECT COUNT(*) " +
                            "FROM Products";
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
            Console.WriteLine("Count is : " + count);
            if (count == 0)
            {


                listView1.Columns.Clear();
                listView1.Items.Clear();

                listView1.Columns.Add("");


                ListViewItem messageItem = new ListViewItem("There are no products");
                messageItem.ForeColor = Color.Red;
                listView1.Items.Add(messageItem);

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


            }
            else
            {
                List<Product> list = manager.ProductList();

                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Columns.Add("ID");
                listView1.Columns.Add("Name");
                listView1.Columns.Add("Color");

                foreach (var product in list)
                {
                    string id = "" + product.id;
                    ListViewItem item = new ListViewItem(id);
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.color);
                    listView1.Items.Add(item);
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        /// <summary>
        ///     Executes when the user clicks on an item inside the listView. The clicked item is then in a "shopping cart"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listView1.HitTest(e.X, e.Y);
            if (hit.Item != null)
            {
                int id = int.Parse(hit.Item.Text);

                if (!productIDs.Contains(id))
                {

                    this.productIDs.Add(id);

                    listView2.Items.Add("" + id);

                }

            }
        }


        /// <summary>
        ///     If the user chose a wrong product, they can delete it from the "shopping cart" by clicking on it in the second listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView2_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = listView2.HitTest(e.X, e.Y);
            if (hit.Item != null)
            {
                int id = int.Parse(hit.Item.Text);

                this.productIDs.Remove(id);

                listView2.Items.Remove(hit.Item);

            }
        }

        /// <summary>
        ///     If the user chose at least one product, they are taken to the OrderFinalisation window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(productIDs.Count == 0)
            {
                label1.ForeColor = Color.Red;
            }
            else
            {
                this.Hide();

                var window = new OrderFinalisation(this.conn, this.order, this.cus, this.productIDs);
                window.FormClosed += (s, args) => this.Close();
                window.Show();
            }
        }
    }
}
