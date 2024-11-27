using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.Logging;
using System.Drawing;

namespace Infocare_Project
{
    internal class Database
    {


        MySqlConnection connection = new MySqlConnection("Server=127.0.0.1; Database=patient;User ID=root;Password=");

        public int getType(string un, string pw)
        {

            string query = "select Type from UserTable where Username ='" + un + "' AND Password ='" + pw + "'";
            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {

                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                connection.Close();

                return Convert.ToInt32(dt.Rows[0].ItemArray[0]);


            }
            catch
            {
                //Login.l.Error();
                throw;
            }



        }

    }
}
