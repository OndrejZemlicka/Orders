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
    public partial class Form1 : Form
    {

        DatabaseCreator creator = new DatabaseCreator();
        DatabaseConnection databaseConnection = DatabaseConnection.Instance;

        SqlConnection conn;


        public Form1(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Creates the database and takes the user to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            creator.CreateDatabase(conn);
            this.Hide();
            
            MainMenu mainMenuForm = new MainMenu(conn);
            mainMenuForm.FormClosed += (s, args) => this.Close(); 
            mainMenuForm.Show();
        }

        /// <summary>
        ///     Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
