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
using Microsoft.VisualBasic.ApplicationServices;

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
                    command.Parameters.AddWithValue("@Bdate", user.Bdate.ToString("MM-dd-yyyy"));  // Ensures correct format
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

        public void PatientReg2(Patient patient, string username, double height, double weight, double bmi, string bloodType, string preCon, string treatment, string prevSurg)
        {
            // SQL query to insert or update the patient's record
            string query = @"
                INSERT INTO tb_patientinfo (P_Height, P_Weight, P_BMI, P_Blood_Type, P_Precondition, P_Treatment, P_PrevSurgery, P_username)
                VALUES (@Height, @Weight, @BMI, @BloodType, @PreCon, @Treatment, @PrevSurg, @Username)
                ON DUPLICATE KEY UPDATE 
                P_Height = IFNULL(@Height, P_Height),
                P_Weight = IFNULL(@Weight, P_Weight),
                P_BMI = IFNULL(@BMI, P_BMI),
                P_Blood_Type = IFNULL(@BloodType, P_Blood_Type),
                P_Precondition = IFNULL(@PreCon, P_Precondition),
                P_Treatment = IFNULL(@Treatment, P_Treatment),
                P_PrevSurgery = IFNULL(@PrevSurg, P_PrevSurgery);";


            using (var connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Height", height > 0 ? height : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Weight", weight > 0 ? weight : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BMI", bmi > 0 ? bmi : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BloodType", string.IsNullOrEmpty(bloodType) ? DBNull.Value : bloodType);
                cmd.Parameters.AddWithValue("@PreCon", string.IsNullOrEmpty(preCon) ? DBNull.Value : preCon);
                cmd.Parameters.AddWithValue("@Treatment", string.IsNullOrEmpty(treatment) ? DBNull.Value : treatment);
                cmd.Parameters.AddWithValue("@PrevSurg", string.IsNullOrEmpty(prevSurg) ? DBNull.Value : prevSurg);
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating or inserting patient data: " + ex.Message);
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

        public string GetPatientName(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT P_Firstname, P_Lastname FROM tb_patientinfo WHERE P_Username = @Username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["P_Firstname"].ToString();
                            string lastName = reader["P_Lastname"].ToString();

                            return $"{lastName}, {firstName}"; // Return the formatted name
                        }
                        else
                        {
                            throw new Exception("No patient found with the given username.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching patient name: " + ex.Message);
                }
            }
        }

        public void DeletePatientByUsername(string username)
        {
            string query = "DELETE FROM tb_patientinfo WHERE P_Username = @Username";

            using (var connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No row found to delete.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting patient record: " + ex.Message);
                }
            }
        }

    }
}
