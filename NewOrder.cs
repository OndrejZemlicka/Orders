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
    public partial class NewOrder : Form
    {

        SqlConnection conn;

        public NewOrder(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        /// <summary>
        ///     takes the user back to the Orders window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();

            var window = new OrdersWindow(conn);
            window.FormClosed += (s, args) => this.Close();
            window.Show();
        }

        /// <summary>
        ///     Takes the data from the form and creates an Order instance. The user is then taken to the AddCustomer window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, EventArgs e)
        {

            Order order = new Order((int)boxPrice.Value, boxCurrency.Text, boxOrderNum.Text, boxPackageNum.Text, "" + boxDate.Value);

            this.Hide();

            var window = new AddCustomer(conn, order);
            window.FormClosed += (s, args) => this.Close();
            window.Show();
        }
    }
}
