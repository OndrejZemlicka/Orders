using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Objednavky
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseConnection databaseConnection = DatabaseConnection.Instance;
            SqlConnection connection = databaseConnection.GetConnection();
            AllocConsole();
            Console.WriteLine("Hello world");
            
            using (connection)
            {
                connection.Open();
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(connection));
            }
            
        }


        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();



    }
}
