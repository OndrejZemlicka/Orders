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
    public partial class MainMenu : Form
    {
        SqlConnection conn;
        
        public MainMenu(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        /// <summary>
        ///     Takes the user back to the welcome page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 welcomePage = new Form1(conn);
            welcomePage.FormClosed += (s, args) => this.Close(); 
            welcomePage.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {



        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// opens the products window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void products_Click(object sender, EventArgs e)
        {
            this.Hide();

            ProductsWindow nextWindow = new ProductsWindow(conn);
            nextWindow.FormClosed += (s, args) => this.Close(); 
            nextWindow.Show();
        }

        /// <summary>
        /// opens the orders window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orders_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new OrdersWindow(conn);
            window.FormClosed += (s, args) => this.Close();
            window.Show();
        }
    }
}
