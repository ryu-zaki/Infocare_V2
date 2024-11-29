using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Infocare_Project.Classes
{
    internal class Database_DataGridView
    {
        public class PatientList
        {
            // Connection string for connecting to the database
            private string connectionString = "Server=127.0.0.1; Database=db_infocare_Project; User ID=root; Password=";

            // Private method to create and return a MySqlConnection
            private MySqlConnection GetConnection()
            {
                return new MySqlConnection(connectionString);
            }

            public DataTable GetPatientList()
            {
                using (MySqlConnection connection = GetConnection())
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT P_Firstname as Firstname, P_Lastname as Lastname , P_Middlename as Middlename, P_Suffix as Suffix, P_Username as Username, P_ContactNumber as 'Contact No.', P_Sex as Sex FROM PatientInfo";

                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                        DataTable patientTable = new DataTable();
                        adapter.Fill(patientTable);

                        return patientTable;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"An error occurred while fetching patient data: {ex.Message}");
                    }
                }
            }

            public DataTable GetDoctorList()
            {
                using (MySqlConnection connection = GetConnection())
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT D_Firstname as Firstname, D_Lastname as Lastname , D_Middlename as Middlename, D_Suffix as Suffix, D_Username as Username, D_ContactNumber as 'Contact No.', D_ConsultationFee as 'Constultation Fee', D_Sex as Sex FROM DoctorInfo";

                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                        DataTable doctorTable = new DataTable();
                        adapter.Fill(doctorTable);

                        return doctorTable;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"An error occurred while fetching doctor data: {ex.Message}");
                    }
                }
            }


            public DataTable GetAllAppointmentList()
            {
                using (MySqlConnection connection = GetConnection())
                {
                    try
                    {
                        connection.Open();

                        string query = "SELECT  * FROM Appointment";

                        MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                        DataTable appointmentTable = new DataTable();
                        adapter.Fill(appointmentTable);

                        return appointmentTable;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"An error occurred while fetching patient data: {ex.Message}");
                    }
                }
            }
        }
    }
}
