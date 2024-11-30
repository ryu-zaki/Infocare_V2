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

        /*PATIENT
         * DATABASE  
         */


        public void PatientReg1(User user)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO tb_patientinfo (p_FirstName, p_LastName, p_MiddleName, p_Suffix, p_Username, P_Password, P_ContactNumber, P_Bdate, P_Sex, P_Address) " +
                                   "VALUES (@FirstName, @LastName, @MiddleName, @Suffix, @Username, @Password, @ContactNumber, @Bdate, @Sex, @Address)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@MiddleName", user.MiddleName);
                    command.Parameters.AddWithValue("@Suffix", user.Suffix);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);

                    command.Parameters.AddWithValue("@Bdate", user.Bdate.ToString("dd-MM-yyyy"));
                    command.Parameters.AddWithValue("@Sex", user.Sex);

                    string fullAddress = $"{user.HouseNo},{user.ZipCode}, {user.Zone}, {user.Street} street, Brgy. {user.Barangay}, {user.City}";
                    command.Parameters.AddWithValue("@Address", fullAddress);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting patient data: " + ex.Message);
                }
            }
        }


        public void PatientReg2(Patient patient, string username, double height, double weight, double bmi, string bloodType, string preCon, string treatment, string prevSurg, string allergy, string medication)
        {
            // SQL query to insert or update the patient's record
            string query = @"
        INSERT INTO tb_patientinfo 
        (P_Height, P_Weight, P_BMI, P_Blood_Type, P_Precondition, P_Treatment, P_PrevSurgery, P_Username, P_Alergy, P_Medication)
        VALUES 
        (@Height, @Weight, @BMI, @BloodType, @PreCon, @Treatment, @PrevSurg, @Username, @Allergy, @Medication)
        ON DUPLICATE KEY UPDATE 
        P_Height = IFNULL(@Height, P_Height),
        P_Weight = IFNULL(@Weight, P_Weight),
        P_BMI = IFNULL(@BMI, P_BMI),
        P_Blood_Type = IFNULL(@BloodType, P_Blood_Type),
        P_Precondition = IFNULL(@PreCon, P_Precondition),
        P_Treatment = IFNULL(@Treatment, P_Treatment),
        P_PrevSurgery = IFNULL(@PrevSurg, P_PrevSurgery),
        P_Alergy = IFNULL(@Allergy, P_Alergy),
        P_Medication = IFNULL(@Medication, P_Medication);";

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
                cmd.Parameters.AddWithValue("@Allergy", string.IsNullOrEmpty(allergy) ? DBNull.Value : allergy);
                cmd.Parameters.AddWithValue("@Medication", string.IsNullOrEmpty(medication) ? DBNull.Value : medication);
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

        public void PatientReg3(EmergencyContact Emergency, string username, string firstName, string lastName, string middleName, string suffix, int houseNo, string street, string barangay, string city, int zipCode, int zone)
        {

            string query = @"
                INSERT INTO tb_patientinfo 
                (P_username, Eme_Firstname, Eme_Middlename, Eme_Lastname, Eme_Suffix, Eme_Address)
                VALUES 
                (@P_username, @Eme_Firstname, @Eme_Middlename, @Eme_Lastname, @Eme_Suffix, @Eme_Address)
                ON DUPLICATE KEY UPDATE 
                Eme_Firstname = @Eme_Firstname,
                Eme_Middlename = @Eme_Middlename,
                Eme_Lastname = @Eme_Lastname,
                Eme_Suffix = @Eme_Suffix,
                Eme_Address = @Eme_Address;";


            using (var connection = GetConnection())
            {
                try
                {

                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Personal details
                    command.Parameters.AddWithValue("@P_username", username);
                    command.Parameters.AddWithValue("@Eme_Firstname", string.IsNullOrEmpty(firstName) ? DBNull.Value : firstName);
                    command.Parameters.AddWithValue("@Eme_Middlename", string.IsNullOrEmpty(middleName) ? DBNull.Value : middleName);
                    command.Parameters.AddWithValue("@Eme_Lastname", string.IsNullOrEmpty(lastName) ? DBNull.Value : lastName);
                    command.Parameters.AddWithValue("@Eme_Suffix", string.IsNullOrEmpty(suffix) ? DBNull.Value : suffix);

                    // Address concatenation
                    string fullAddress = $"{houseNo},{zipCode}, {zone}, {street} street, Brgy. {barangay}, {city}";
                    command.Parameters.AddWithValue("@Eme_Address", string.IsNullOrEmpty(fullAddress) ? DBNull.Value : fullAddress);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating patient data: " + ex.Message);
                }
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM tb_patientinfo WHERE p_Username = @Username";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                FirstName = reader["P_FirstName"].ToString(),
                                LastName = reader["P_LastName"].ToString(),
                                Username = reader["P_Username"].ToString(),
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching user data: " + ex.Message);
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

        //ADMIN LOGIN
        public bool AdminLogin(string username, string password)
        {

            using (var connection = GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM tb_adminlogin WHERE a_Username = @Username AND a_Password = @Password";

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

        public void AddDoctor(Doctor doctor)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO tb_doctorinfo 
                             (firstname, middlename, lastname, username, password, consultationfee, specialization, time_availability, day_availability) 
                             VALUES (@FirstName, @MiddleName, @LastName, @Username, @Password, @ConsultationFee, @Specialization, @TimeAvailability, @DayAvailability)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", doctor.MiddleName);
                    command.Parameters.AddWithValue("@LastName", doctor.LastName);
                    command.Parameters.AddWithValue("@Username", doctor.Username);
                    command.Parameters.AddWithValue("@Password", doctor.Password);
                    command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
                    command.Parameters.AddWithValue("@Specialization", doctor.Specialty);
                    command.Parameters.AddWithValue("@TimeAvailability", doctor.TimeAvailability);
                    command.Parameters.AddWithValue("@DayAvailability", doctor.DayAvailability);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting doctor data: " + ex.Message);
                }
            }
        }

        public void NullPatientReg2Data(string username)
        {
            string query = @"
        UPDATE tb_patientinfo
        SET 
            P_Height = NULL,
            P_Weight = NULL,
            P_BMI = NULL,
            P_Blood_Type = NULL,
            P_Precondition = NULL,
            P_Treatment = NULL,
            P_PrevSurgery = NULL,
            P_Alergy = NULL,
            P_Medication = NULL
        WHERE P_Username = @Username";

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
                        throw new Exception("No records were found to update.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting patient data: " + ex.Message);
                }
            }
        }

        public void DeletePatientReg1Data(string username)
        {
            string query = @"
                Delete from tb_patientinfo
                WHERE P_Username = @Username";

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
                        throw new Exception("No records were found to update.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting patient data: " + ex.Message);
                }
            }
        }
    }
}
