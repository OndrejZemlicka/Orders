using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objednavky
{
    public class DatabaseManager
    {

        SqlConnection conn;
        public DatabaseManager(SqlConnection conn)
        {
            this.conn = conn;
        }

        /// <summary>
        ///     Copies all products from the database to a list
        /// </summary>
        /// <returns>A list full of Product instances</returns>
        public List<Product> ProductList()
        {
            List<Product> list = new List<Product>();

            string sql = "SELECT p.id_pr, p.name, p.color " +
                            "FROM Products p";

            using (SqlCommand cmd = new SqlCommand(sql, this.conn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product prod = new Product((int)reader[0], (string)reader[1], (string)reader[2]);
                    list.Add(prod);
                }
                reader.Close();
            }

            return list;
        }




    }
}
