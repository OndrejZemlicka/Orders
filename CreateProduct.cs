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
    public partial class CreateProduct : Form
    {
        SqlConnection conn;

        public CreateProduct(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        /// <summary>
        ///     If the textBoxes are filled out, the product is inserted into the database table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateProductButton_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string color = textBoxColor.Text;


            if (name != "" && color != "")
            {

                string sql = "INSERT INTO Products VALUES('" + name + "', '" + color + "');";

                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Product added");
                }

                this.Hide();

                var previousWindow = new ProductsWindow(conn);
                previousWindow.FormClosed += (s, args) => this.Close(); 
                previousWindow.Show();

            }
            else
            {
                if (name == "")
                {
                    nameMessage.ForeColor = Color.Red;
                }
                if (color == "")
                {
                    colorMessage.ForeColor = Color.Red;
                }
            }

            


        }

        /// <summary>
        ///     Takes the user back to the ProductsWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var previousWindow = new ProductsWindow(conn);
            previousWindow.FormClosed += (s, args) => this.Close(); 
            previousWindow.Show();
        }

 

    }
}
