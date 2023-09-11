using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Objednavky
{
    internal class DatabaseConnection
    {

        private static DatabaseConnection instance;
        private SqlConnection connection;


        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.InitialCatalog = "Objednavky";
            connectionStringBuilder.DataSource = "LAPTOP-AV3G0QJC\\SQLEXPRESS";
            connectionStringBuilder.IntegratedSecurity = true;
            connectionStringBuilder.Encrypt = false;

            return connectionStringBuilder.ConnectionString;
        }







        private DatabaseConnection()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.InitialCatalog = "Objednavky";
            connectionStringBuilder.DataSource = "LAPTOP-AV3G0QJC\\SQLEXPRESS";
            connectionStringBuilder.IntegratedSecurity = true;
            connectionStringBuilder.Encrypt = false;

            connection = new SqlConnection(connectionStringBuilder.ConnectionString);
        }



        public static DatabaseConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }



    }
}
