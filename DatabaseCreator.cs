using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objednavky
{
    internal class DatabaseCreator
    {


        public DatabaseCreator()
        {

        }

        /// <summary>
        ///     Executes sql commands to create everything that needs to be in the database for this application to work.
        /// </summary>
        /// <param name="connection"></param>
        public void CreateDatabase(SqlConnection connection)
        {
            CreateTable("Products", connection);
            CreateTable("Orders", connection);
            
            CreateTable("OrdersProducts", connection);

            CreateTable("Customers", connection);

            
            
            
            CreateConstraints(connection);
        }

        /// <summary>
        ///     Creates a table based on the name of the table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        public void CreateTable(string tableName, SqlConnection connection)
        {
            if (tableName == "Orders")
            {
                
                    
                    string sqlCommandText = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Orders') " +
                                                "BEGIN " +
                                                    "CREATE TABLE Orders(" +
                                                        "id_or INT IDENTITY(100,1) PRIMARY KEY," +
                                                        "price MONEY," +
                                                        "currency NVARCHAR(15)," +
                                                        "cis_obj NVARCHAR(15)," +
                                                        "cis_zas NVARCHAR(15)," +
                                                        "date DATE, " +
                                                        "id_cu INT ) " +
                                                "END;";

                    using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
                    {
                        // Execute the SQL command
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table Orders created");
                    }
                    
                
            }
            if (tableName == "Customers")
            {
                
                    
                    string sqlCommandText = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers') " +
                                                "BEGIN " +
                                                    "CREATE TABLE Customers(" +
                                                        "id_cu INT IDENTITY(100,1) PRIMARY KEY," +
                                                        "name NVARCHAR(31) ," +
                                                        "surname NVARCHAR(31) ," +
                                                        "address NVARCHAR(31) ," +
                                                        "zipcode NVARCHAR(15) ," +
                                                        "city NVARCHAR(31) ," +
                                                        "country NVARCHAR(31) ) " +
                                                "END;";

                    using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
                    {
                        // Execute the SQL command
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table Customers created");
                    }
                    
                
            }
            if (tableName == "Products")
            {
                
                   
                    string sqlCommandText = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products') " +
                                                "BEGIN " +
                                                    "CREATE TABLE Products(" +
                                                        "id_pr INT IDENTITY(100,1) PRIMARY KEY," +
                                                        "name NVARCHAR(31) ," +
                                                        "color NVARCHAR(15) )" +
                                                "END;";

                    using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
                    {
                        // Execute the SQL command
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table Products created");
                    }
                    
                
            }
            if (tableName == "OrdersProducts")
            {
                
                    string sqlCommandText = "IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'OrdersProducts') " +
                                                "BEGIN " +
                                                    "CREATE TABLE OrdersProducts(" +
                                                        "id_orpr INT IDENTITY(1,1) PRIMARY KEY," +
                                                        "id_or INT ," +
                                                        "id_pr INT )" +
                                                "END;";

                    using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
                    {
                        // Execute the SQL command
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table OrdersProducts created");
                    }
                    
                
            }

        }


        /// <summary>
        ///     Creates every constraint between the tables if it doesn't already exist
        /// </summary>
        /// <param name="connection"></param>
        public void CreateConstraints(SqlConnection connection)
        {
            
                
             string sqlCommandText = "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.fk_or_cu') AND parent_object_id = OBJECT_ID(N'dbo.Orders')) " +
                                         "BEGIN " +
                                             "ALTER TABLE Orders " +
                                                    "ADD CONSTRAINT fk_or_cu " +
                                                    "FOREIGN KEY (id_cu) " +
                                                    "REFERENCES Customers (id_cu);" +
                                         "END";

            using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
            {
                // Execute the SQL command
                command.ExecuteNonQuery();
                Console.WriteLine("Orders constraints added");
            }
                
            
            
                
                string sql = "IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.fk_orpr_or') AND parent_object_id = OBJECT_ID(N'dbo.OrdersProducts')) " +
                                            "BEGIN " +
                                                "ALTER TABLE OrdersProducts " +
                                                    "ADD CONSTRAINT fk_orpr_or " +
                                                    "FOREIGN KEY (id_or) " +
                                                    "REFERENCES Orders(id_or);" +
                                                "ALTER TABLE OrdersProducts " +
                                                    "ADD CONSTRAINT fk_orpr_pr " +
                                                    "FOREIGN KEY (id_pr) " +
                                                    "REFERENCES Products(id_pr);" +
                                            "END";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Execute the SQL command
                    command.ExecuteNonQuery();
                    Console.WriteLine("OrdersProducts constraints added");
                }
                
            
        }


    }
}
