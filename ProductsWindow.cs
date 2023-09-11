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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Objednavky
{
    public partial class ProductsWindow : Form
    {

        SqlConnection conn;
        DatabaseManager manager;

        public ProductsWindow(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            manager = new DatabaseManager(conn);
        }

        

        /// <summary>
        ///  populates the listView by every product in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsWindow_Load(object sender, EventArgs e)
        {

            listViewProducts.View = View.Details;

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
                


                listViewProducts.Columns.Clear();
                listViewProducts.Items.Clear();

                listViewProducts.Columns.Add("");

                ListViewItem messageItem = new ListViewItem("There are no products");
                messageItem.ForeColor = Color.Red;
                listViewProducts.Items.Add(messageItem);


                listViewProducts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


            }
            else
            {
                List <Product> list = manager.ProductList();

                listViewProducts.Columns.Clear();
                listViewProducts.Items.Clear();
                listViewProducts.Columns.Add("ID");
                listViewProducts.Columns.Add("Name");
                listViewProducts.Columns.Add("Color");

                foreach (var product in list)
                {
                    string id = ""+product.id;
                    ListViewItem item = new ListViewItem(id);
                    item.SubItems.Add(product.name);
                    item.SubItems.Add(product.color);
                    listViewProducts.Items.Add(item);
                }
                listViewProducts.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        /// <summary>
        /// takes the user back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var previousWindow = new MainMenu(conn);
            previousWindow.FormClosed += (s, args) => this.Close(); 
            previousWindow.Show();
        }

        /// <summary>
        /// takes the user to the createProduct window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProduct_Click(object sender, EventArgs e)
        {
            this.Hide();

            var nextWindow = new CreateProduct(conn);
            nextWindow.FormClosed += (s, args) => this.Close(); 
            nextWindow.Show();
        }
    }
}
