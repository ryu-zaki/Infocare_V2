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

        public string getFname(string un)
        {
            string query = "select P_FirstName from tb_patientinfo where Username='" + un + "'";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                connection.Close();
                return dt.Rows[0].ItemArray[0].ToString();

            }

            catch { throw; }
        }

        public string getLname(string un)
        {
            string query = "select P_Lastname from tb_patientinfo where Username='" + un + "'";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                connection.Close();
                return dt.Rows[0].ItemArray[0].ToString();

            }

            catch { throw; }
        }

        public string getMname(string un)
        {
            string query = "select P_Middlename from tb_patientinfo where Username='" + un + "'";

            connection.Open();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                connection.Close();
                return dt.Rows[0].ItemArray[0].ToString();

            }

            catch { throw; }
        }


    }
}
