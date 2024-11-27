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
        private string connectionString = "Server=127.0.0.1; Database=db_infocare_project;User ID=root; Password=;"; // Your MySQL connection string

        // Method to connect to the database
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Method to insert a User (common method for all User types)
        public void PatientReg1(User user)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    // SQL query to insert data into the patient info table
                    string query = "INSERT INTO tb_patientinfo (p_FirstName, p_LastName, p_middlename, p_Suffix, p_Username, P_Password, P_ContactNumber, P_Bdate, P_Sex) " +
                                   "VALUES (@FirstName, @LastName, @MiddleName, @Suffix, @Username, @Password, @ContactNumber, @Bdate, @Sex)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                    command.Parameters.AddWithValue("@Suffix", user.Suffix);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password); // You can change this to hashed password if needed
                    command.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);

                    // Convert DateTime to MySQL date format (YYYY-MM-DD) if needed
                    command.Parameters.AddWithValue("@Bdate", user.Bdate.ToString("yyyy-MM-dd"));  // Ensures correct format
                    command.Parameters.AddWithValue("@Sex", user.Sex);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle any database errors
                    throw new Exception("Error inserting patient data: " + ex.Message);
                }
            }
        }

       
        public bool PatientLogin(string username, string password)
        {
            using (var connection = GetConnection()) 
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM tb_patientinfo WHERE p_Username = @Username AND P_Password = @Password";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();                   
                    int result = Convert.ToInt32(command.ExecuteScalar());
                    
                    return result == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error validating login: " + ex.Message);
                }
            }
        }
    }
}
