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
using Infocare_Project.NewFolder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Collections;
using Infocare_Project_1;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Configuration;
using Infocare_Project_1.Classes;
using Infocare_Project_1.Object_Models;

namespace Infocare_Project
{
    static class Database
    {

        private static string dbms = "Workbench";
        public static string connectionString = ConfigurationManager.ConnectionStrings[dbms].ConnectionString;

        public static void ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        //Other Functions are seperated here by region

        #region Get Functions

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool IsEmailExisted(Role role, UserModel user)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string tablename = ProcessMethods.GetTablenameByRole(role);

                string query = $"SELECT COUNT(*) FROM {tablename} WHERE email = @Email AND username = @Username";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Username", user.UserName);

                int result = Convert.ToInt32(command.ExecuteScalar());

                return result != 0;

            }
        }

        public static bool RoleLogin(string username, string password, Role role)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string tableName = ProcessMethods.GetTablenameByRole(role);

                    string tableColumn =
                        
                         role == Role.Admin ? "A_" : role == Role.Staff ? "S_" : "";
                          


                    string query = $"SELECT COUNT(*) FROM {tableName} WHERE Username = @Username AND {tableColumn}Password = @Password";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    string hashhPassword = ProcessMethods.HashCharacter(password);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashhPassword);

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


        public static string GetPatientName(PatientModel patient)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT P_Firstname, P_Lastname FROM tb_patientinfo WHERE P_Username = @Username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", patient.UserName);

                try
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["P_Firstname"].ToString();
                            string lastName = reader["P_Lastname"].ToString();

                            return $"{lastName}, {firstName}";
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

        public static List<string> GetDoctorNames(string specialization)
        {
            List<string> doctorNames = new List<string>();

            string query = @"
                    SELECT DISTINCT CONCAT('Dr. ', Lastname, ', ', Firstname) AS doctor_name
                    FROM tb_doctor_specializations ds
                    JOIN tb_doctorinfo di ON ds.doctor_id = di.id
                    WHERE ds.specialization = @Specialization;
                    ";

            using (var connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Specialization", specialization);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctorNames.Add(reader["doctor_name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting doctor names: " + ex.Message);
                }
            }

            return doctorNames;
        }

        public static List<string> GetDoctorAvailableTimes(string doctorName, string specialization)
        {
            List<string> availableTimes = new List<string>();

            string query = @"
                SELECT DISTINCT di.start_time, di.end_time, di.day_availability 
                FROM tb_doctorinfo di
                JOIN tb_doctor_specializations ds ON di.id = ds.doctor_id
                WHERE CONCAT('Dr. ', di.lastname, ', ', di.firstname) = @DoctorName
                  AND ds.specialization = @Specialization;
            ";

            using (var connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DoctorName", doctorName);
                cmd.Parameters.AddWithValue("@Specialization", specialization);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TimeSpan startTime = (TimeSpan)reader["start_time"];
                            TimeSpan endTime = (TimeSpan)reader["end_time"];

                            TimeSpan totalDuration = endTime - startTime;
                            TimeSpan slotDuration = TimeSpan.FromTicks(totalDuration.Ticks / 4);

                            TimeSpan currentTime = startTime;

                            for (int i = 0; i < 4; i++)
                            {
                                availableTimes.Add($"{currentTime:hh\\:mm}");
                                currentTime = currentTime.Add(slotDuration);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error fetching available times: {ex.Message}");
                }
            }

            return availableTimes;
        }

        public static string GetDoctorAvailability(string doctorName)
        {
            string query = @"
SELECT day_availability 
FROM tb_doctorinfo 
WHERE CONCAT('Dr. ', Lastname, ', ', Firstname) = @DoctorName";

            using (var connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@DoctorName", doctorName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string dayAvailability = result.ToString().Replace("-", ",");
                        return dayAvailability;
                    }
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching doctor availability: " + ex.Message);
                }
            }
        }


        public static List<string> GetSpecialization()
        {
            HashSet<string> specializationSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string query = @"SELECT Specialization FROM tb_doctorinfo";

            using (var connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string specializations = reader["Specialization"]?.ToString();
                            if (!string.IsNullOrEmpty(specializations))
                            {
                                // Split and trim each specialization
                                foreach (string spec in specializations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    specializationSet.Add(spec.Trim());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting specializations: " + ex.Message);
                }
            }
            return specializationSet.ToList();
        }


        public static (string firstName, string lastName) GetStaffNameDetails(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT s_Firstname, s_Lastname FROM tb_staffinfo WHERE username = @Username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["s_Firstname"].ToString();
                            string lastName = reader["s_Lastname"].ToString();

                            return (firstName, lastName);
                        }
                        else
                        {
                            throw new Exception("No Staff found with the given username.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching patient name details: " + ex.Message);
                }
            }
        }


        public static DoctorModel GetDoctorNameDetails(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT * FROM tb_doctorinfo WHERE Username = @Username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["firstname"].ToString();
                            string lastName = reader["lastname"].ToString();
                            string middleName = reader["middlename"].ToString();
                            string contactNumber = reader["contactNumber"].ToString();
                            string password = reader["password"].ToString();
                            string specialties = reader["specialization"].ToString();

                            List<string> specialty = new List<string>(specialties.Split(','));

                            decimal consultationFee = decimal.Parse(reader["consultationfee"].ToString());

                            TimeSpan startTime;
                            TimeSpan.TryParse(reader["start_time"].ToString(), out startTime);

                            TimeSpan endTime;
                            TimeSpan.TryParse(reader["end_time"].ToString(), out endTime);

                            string DayAvailability = reader["day_availability"].ToString();

                            return new DoctorModel
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                MiddleName = middleName,
                                Password = password,
                                Specialty = specialty,
                                ConsultationFee = consultationFee,
                                StartTime = startTime,
                                EndTime = endTime,
                                DayAvailability = DayAvailability
                            };
                        }
                        else
                        {
                            throw new Exception("No Doctor found with the given username.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching patient name details: " + ex.Message);
                }
            }

        }
        public static List<string> GetPatientNames()
        {
            List<string> patientNames = new List<string>();

            string query = @"SELECT CONCAT(P_Lastname, ', ', P_Firstname) AS patient_name
                     FROM tb_patientinfo";

            using (var connection = GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientNames.Add(reader["patient_name"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting patient data: " + ex.Message);
                }
            }
            return patientNames;
        }


        public static DataTable StaffList()
        {
            string query = @"SELECT id as 'Staff ID', s_Firstname AS 'First Name', s_middleName AS 'Middle Name', s_lastname AS 'Last Name', s_suffix AS 'Suffix', s_contactnumber AS 'Contact Number', email AS 'Email' FROM tb_staffinfo";

            DataTable staffTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(staffTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving staff list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return staffTable;
        }

        public static DataTable DoctorList()
        {
            string query = @"SELECT id as 'Doctor ID', Firstname AS 'First Name', middleName AS 'Middle Name', lastname AS 'Last Name', specialization AS 'Specialization', day_availability AS 'Day Available' FROM tb_doctorinfo";

            DataTable DoctorTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(DoctorTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving staff list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DoctorTable;
        }

        public static DataTable PatientList()
        {
            string query = @"SELECT id as 'Patient ID', p_Firstname AS 'First Name', p_middleName AS 'Middle Name', P_lastname AS 'Last Name', p_suffix AS 'Suffix', p_sex AS 'Sex', P_bdate AS 'Birth Date', p_address AS 'Full Address' FROM tb_patientinfo";

            DataTable PatientTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(PatientTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving patient list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return PatientTable;
        }

        public static decimal? GetConsultationFee(string doctorName)
        {
            using (var connection = GetConnection())
            {
                string query = @"SELECT consultationfee 
                 FROM tb_doctorinfo 
                 WHERE CONCAT('Dr. ', Lastname, ', ', Firstname) = @doctorName";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@doctorName", doctorName);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.IsDBNull(reader.GetOrdinal("consultationfee"))
                                ? (decimal?)null
                                : reader.GetDecimal("consultationfee");
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching consultation fee: " + ex.Message);
                }
            }
        }



        public static bool IsPatientAppointmentPendingOrAccepted(string patientName)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT COUNT(*) 
                FROM tb_appointmenthistory 
                WHERE ah_Patient_Name = @PatientName 
                  AND ah_status IN ('Pending', 'Accepted')";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", patientName);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking appointment status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool IsDoctorOccupied(string doctorName, DateTime appointmentDate)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT COUNT(*) 
                FROM tb_appointmenthistory 
                WHERE ah_Doctor_Name = @DoctorName 
                  AND ah_date = @AppointmentDate 
                  AND ah_status IN ('Pending', 'Accepted')";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorName", doctorName);
                        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.Date);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking doctor's availability: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public static DataTable AppointmentList()
        {
            string query = @"select id as 'Transaction ID', ah_patient_name AS 'Patient Name', ah_doctor_name AS 'Doctor Name',ah_Specialization AS 'Specialization', ah_time AS 'Time Slot', ah_date AS 'Date', ah_consfee AS 'Consultation Fee' From tb_appointmenthistory";

            DataTable AppointmentTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(AppointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AppointmentTable;
        }

        public static DataTable PendingAppointmentList(string doctorFullName)
        {
            string query = @"SELECT ah_Patient_Name, id, ah_Specialization, ah_doctor_name, ah_time, ah_date, ah_consfee  FROM tb_appointmenthistory 
                     WHERE ah_status = 'Pending' AND ah_Doctor_Name = @DoctorFullName";

            DataTable appointmentTable = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DoctorFullName", doctorFullName);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(appointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return appointmentTable;
        }


        public static DataTable CheckOutAppointmentList(string doctorFullName)
        {
            string query = @"SELECT ah_Patient_Name, id, ah_Specialization, ah_doctor_name, ah_time, ah_date, ah_consfee  FROM tb_appointmenthistory 
                     WHERE ah_status = 'CheckOut' AND ah_Doctor_Name = @DoctorFullName";

            DataTable appointmentTable = new DataTable();
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DoctorFullName", doctorFullName);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(appointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return appointmentTable;
        }



        public static DataTable ViewAppointments(string doctorFullName)
        {
            string query = @"SELECT ah_Patient_Name, id, ah_Specialization, ah_doctor_name, ah_time, ah_date, ah_consfee FROM tb_appointmenthistory 
                     WHERE ah_status = 'Accepted' AND ah_Doctor_Name = @DoctorFullName";

            DataTable AppointmentTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DoctorFullName", doctorFullName);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(AppointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AppointmentTable;
        }

        public static DataTable ViewCompletedAppointments(string doctorFullName)
        {
            string query = @"SELECT id, ah_Patient_Name as 'Patient Name', ah_doctor_name as 'Doctor Name', ah_specialization as 'Specialization', ah_time as 'Appointment Time', ah_date as 'Appointment Date', ah_consfee as 'Consultation Fee' FROM tb_appointmenthistory 
             WHERE ah_status = 'Completed'";
            //aayusin pa yung sa doctor for now completed muna
            DataTable AppointmentTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DoctorFullName", doctorFullName);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(AppointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AppointmentTable;
        }

        public static DataTable DeclinedAppointments(string doctorFullName)
        {
            string query = @"SELECT ah_Patient_Name, id, ah_Specialization, ah_doctor_name, ah_time, ah_date, ah_consfee  FROM tb_appointmenthistory 
                     WHERE ah_status = 'Declined' AND ah_Doctor_Name = @DoctorFullName";

            DataTable AppointmentTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DoctorFullName", doctorFullName);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(AppointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AppointmentTable;
        }


        public static DataTable ViewCompletedppointments()
        {
            string query = @"SELECT id as 'Transaction ID', ah_Patient_Name as 'Patient Name', ah_doctor_name as 'Doctor Name', ah_specialization as 'Specialization', ah_time as 'Appointment Time', ah_date as 'Appointment Date', ah_consfee as 'Consultation Fee' FROM tb_appointmenthistory 
             WHERE ah_status = 'Completed'";
            DataTable AppointmentTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(AppointmentTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return AppointmentTable;
        }

        public static bool IsUsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM tb_patientinfo WHERE P_Username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }



        public static bool UsernameExistsStaff(string username)
        {
            string query = "SELECT COUNT(*) FROM tb_staffinfo WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool UsernameExistsDoctor(string username)
        {
            string query = "SELECT COUNT(*) FROM tb_doctorinfo WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }




        public static void viewDocument(int appointmentId, Action<Dictionary<string, string>> onSuccess, Action<string> onFailure)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string getPatientNameQuery = "SELECT * FROM tb_appointmenthistory WHERE id = @appointmentId";
                    using (MySqlCommand command = new MySqlCommand(getPatientNameQuery, connection))
                    {
                        command.Parameters.AddWithValue("@appointmentId", appointmentId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string patientName = reader["ah_Patient_Name"].ToString();
                                string doctorName = reader["ah_Doctor_Name"].ToString();
                                var nameParts = patientName.Split(',');
                                var doctorParts = doctorName.Split(',');

                                if (nameParts.Length < 2 || doctorParts.Length < 2)
                                {
                                    onFailure?.Invoke("Invalid patient or doctor name format in the database.");
                                    return;
                                }

                                string patientFirstName = nameParts[1].Trim();
                                string patientLastName = nameParts[0].Trim();
                                string doctorFirstName = doctorParts[1].Trim();
                                string doctorLastName = doctorParts[0].Trim();

                                var patientDetails = new Dictionary<string, string>
                        {
                            { "P_Firstname", patientFirstName },
                            { "P_Lastname", patientLastName },
                            { "P_Bdate", reader["P_bdate"].ToString() },
                            { "P_Height", reader["P_height"].ToString() },
                            { "P_Weight", reader["P_weight"].ToString() },
                            { "P_BMI", reader["P_bmi"].ToString() },
                            { "P_Blood_Type", reader["P_Blood_type"].ToString() },
                            { "P_Alergy", reader["P_alergy"].ToString() },
                            { "P_Medication", reader["P_medication"].ToString() },
                            { "P_PrevSurgery", reader["P_prevsurgery"].ToString() },
                            { "P_Precondition", reader["P_precondition"].ToString() },
                            { "P_Treatment", reader["P_treatment"].ToString() },
                            { "ah_DoctorFirstName", doctorFirstName },
                            { "ah_DoctorLastName", doctorLastName },
                            { "ah_Time", reader["ah_time"].ToString() },
                            { "ah_Date", reader["ah_date"].ToString() },
                            { "ah_Consfee", reader["ah_Consfee"].ToString() },
                            { "d_diagnosis", reader["d_diagnosis"].ToString() },
                            { "d_additionalnotes", reader["d_additionalnotes"].ToString() },
                            { "d_doctoroder", reader["d_doctoroder"].ToString() },
                            { "d_prescription", reader["d_prescription"].ToString() }
                        };

                                onSuccess?.Invoke(patientDetails);
                            }
                            else
                            {
                                onFailure?.Invoke("Appointment details not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                onFailure?.Invoke($"An error occurred: {ex.Message}");
            }
        }




        public static string GetDoctorSpecialization(string doctorFullName)
        {
            string specialization = string.Empty;
            string query = "SELECT specialization FROM tb_doctorinfo WHERE CONCAT('Dr. ', Lastname, ', ', Firstname) = @doctorName";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@doctorName", doctorFullName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        specialization = result.ToString();
                    }
                }
            }
            return specialization;
        }

        public static DataTable ChecOutList()
        {
            string query = @"SELECT ah_patient_name, ah_Consfee, ah_time, ah_date from tb_appointmenthistory where ah_status = 'CheckOut'";

            DataTable CheckoutTable = new DataTable();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(CheckoutTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return CheckoutTable;
        }


        #endregion

        #region Create Functions

        public static void PatientRegFunc(PatientModel patient)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO tb_patientinfo (p_FirstName, p_LastName, p_MiddleName, p_Suffix, p_Username, P_Password, P_ContactNumber, P_Bdate, P_Sex, P_Address) " +
                                   "VALUES (@FirstName, @LastName, @MiddleName, @Suffix, @Username, @Password, @ContactNumber, @Bdate, @Sex, @Address)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@LastName", patient.LastName);
                    command.Parameters.AddWithValue("@MiddleName", patient.MiddleName);
                    command.Parameters.AddWithValue("@Suffix", patient.Suffix);
                    command.Parameters.AddWithValue("@Username", patient.UserName);
                    command.Parameters.AddWithValue("@Password", patient.Password);
                    command.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);

                    command.Parameters.AddWithValue("@Bdate", patient.BirthDate.ToString("dd-MM-yyyy"));
                    command.Parameters.AddWithValue("@Sex", patient.sex.ToString());

                    string fullAddress = $"{patient.Address.HouseNo},{patient.Address.ZipCode}, {patient.Address.Zone}, {patient.Address.Street} street, Brgy. {patient.Address.Barangay}, {patient.Address.City}";
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

        public static void PatientRegFunc(PatientModel patient, string username, double height, double weight, double bmi, string bloodType, string preCon, string treatment, string prevSurg, string allergy, string medication)
        {
            string query =
                            @"INSERT INTO tb_patientinfo 
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

        public static void PatientRegFunc(EmergencyContactModel Emergency, string username, string firstName, string lastName, string middleName, string suffix, int houseNo, string street, string barangay, string city, int zipCode, int zone)
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

                    command.Parameters.AddWithValue("@P_username", username);
                    command.Parameters.AddWithValue("@Eme_Firstname", string.IsNullOrEmpty(firstName) ? DBNull.Value : firstName);
                    command.Parameters.AddWithValue("@Eme_Middlename", string.IsNullOrEmpty(middleName) ? DBNull.Value : middleName);
                    command.Parameters.AddWithValue("@Eme_Lastname", string.IsNullOrEmpty(lastName) ? DBNull.Value : lastName);
                    command.Parameters.AddWithValue("@Eme_Suffix", string.IsNullOrEmpty(suffix) ? DBNull.Value : suffix);

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

        public static void AddStaff(StaffModel staff)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO tb_staffinfo (s_FirstName, s_LastName, s_MiddleName, username, s_Password, s_suffix, s_contactNumber, email) " +
                                   "VALUES (@FirstName, @LastName, @MiddleName, @Username, @Password, @suffix, @ContactNumber, @email)";

                    string hashPassword = ProcessMethods.HashCharacter(staff.Password);

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", staff.FirstName);
                    command.Parameters.AddWithValue("@LastName", staff.LastName);
                    command.Parameters.AddWithValue("@MiddleName", staff.MiddleName);
                    command.Parameters.AddWithValue("@Username", staff.UserName);

                    command.Parameters.AddWithValue("@Password", hashPassword);
                    command.Parameters.AddWithValue("@suffix", staff.Suffix);
                    command.Parameters.AddWithValue("@ContactNumber", staff.ContactNumber);
                    command.Parameters.AddWithValue("@email", staff.Email);



                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting staff data: " + ex.Message);
                }
            }
        }

        public static void AddSpecialization(int doctorId, string specialization)
        {
            string query = "INSERT INTO tb_doctor_specializations (doctor_id, specialization) VALUES (@DoctorId, @Specialization)";

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                cmd.Parameters.AddWithValue("@Specialization", specialization);
                cmd.ExecuteNonQuery();
            }
        }



        public static bool SaveAppointment(string patientName, string specialization, string doctorName, string timeSlot, DateTime appointmentDate, decimal consFee)
        {

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    string query = @"INSERT INTO tb_appointmenthistory (ah_Patient_Name, ah_Specialization, ah_Doctor_Name, ah_time, ah_date, ah_consfee, ah_status) 
                             VALUES (@PatientName, @Specialization, @DoctorName, @TimeSlot, @AppointmentDate, @ConsFee, @Pending)";


                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", patientName);
                        command.Parameters.AddWithValue("@Specialization", specialization);
                        command.Parameters.AddWithValue("@DoctorName", doctorName);
                        command.Parameters.AddWithValue("@TimeSlot", timeSlot);
                        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                        command.Parameters.AddWithValue("@ConsFee", consFee);
                        command.Parameters.AddWithValue("@Pending", "Pending");


                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CreateDiagnosis(int appointmentId, Action<Dictionary<string, string>> onSuccess, Action<string> onFailure)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string getPatientNameQuery = "SELECT ah_Patient_Name FROM tb_appointmenthistory WHERE id = @appointmentId and ah_status = 'Accepted'";
                    string patientName;

                    using (MySqlCommand command = new MySqlCommand(getPatientNameQuery, connection))
                    {
                        command.Parameters.AddWithValue("@appointmentId", appointmentId);

                        object result = command.ExecuteScalar();
                        if (result == null)
                        {
                            onFailure?.Invoke("No patient associated with this appointment.");
                            return;
                        }

                        patientName = result.ToString();
                    }

                    var nameParts = patientName.Split(',');
                    if (nameParts.Length < 2)
                    {
                        onFailure?.Invoke("Invalid patient name format in the database.");
                        return;
                    }

                    string lastName = nameParts[0].Trim();
                    string firstName = nameParts[1].Trim();

                    string getPatientDetailsQuery = "SELECT P_Firstname, P_Lastname, P_Bdate, P_Height, P_Weight, P_BMI, " +
                                                    "P_Blood_Type, P_Alergy, P_Medication, P_PrevSurgery, P_Precondition, P_Treatment " +
                                                    "FROM tb_patientinfo WHERE P_Firstname = @firstName AND P_Lastname = @lastName";

                    using (MySqlCommand command = new MySqlCommand(getPatientDetailsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", firstName);
                        command.Parameters.AddWithValue("@lastName", lastName);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var patientDetails = new Dictionary<string, string>
                            {
                                { "P_Firstname", reader["P_Firstname"].ToString() },
                                { "P_Lastname", reader["P_Lastname"].ToString() },
                                { "P_Bdate", reader["P_Bdate"].ToString() },
                                { "P_Height", reader["P_Height"].ToString() },
                                { "P_Weight", reader["P_Weight"].ToString() },
                                { "P_BMI", reader["P_BMI"].ToString() },
                                { "P_Blood_Type", reader["P_Blood_Type"].ToString() },
                                { "P_Alergy", reader["P_Alergy"].ToString() },
                                { "P_Medication", reader["P_Medication"].ToString() },
                                { "P_PrevSurgery", reader["P_PrevSurgery"].ToString() },
                                { "P_Precondition", reader["P_Precondition"].ToString() },
                                { "P_Treatment", reader["P_Treatment"].ToString() }
                            };

                                onSuccess?.Invoke(patientDetails);
                            }
                            else
                            {
                                onFailure?.Invoke("Patient details not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                onFailure?.Invoke($"An error occurred: {ex.Message}");
            }
        }


        public static int AddDoctor1(DoctorModel doctor)
        {
            using (var connection = GetConnection())
            {
                MySqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    string query = @"INSERT INTO tb_doctorinfo 
                     (firstname, middlename, lastname, username, password, consultationfee, start_time, end_time, day_availability, contactnumber, email) 
                     VALUES (@FirstName, @MiddleName, @LastName, @Username, @Password, @ConsultationFee, @StartTime, @EndTime, @DayAvailability, @ContactNumber, @Email)";
                    MySqlCommand command = new MySqlCommand(query, connection, transaction);

                    string hashPassword = ProcessMethods.HashCharacter(doctor.Password);
                    command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", doctor.MiddleName);
                    command.Parameters.AddWithValue("@LastName", doctor.LastName);
                    command.Parameters.AddWithValue("@Username", doctor.UserName);
                    command.Parameters.AddWithValue("@Password", hashPassword);
                    command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
                    command.Parameters.AddWithValue("@StartTime", doctor.StartTime);
                    command.Parameters.AddWithValue("@EndTime", doctor.EndTime);
                    command.Parameters.AddWithValue("@DayAvailability", doctor.DayAvailability);
                    command.Parameters.AddWithValue("@ContactNumber", doctor.ContactNumber);
                    command.Parameters.AddWithValue("@Email", doctor.Email);

                    command.ExecuteNonQuery();
                    int doctorId = (int)command.LastInsertedId;

                    List<string> specializationsList = new List<string>();

                    Console.WriteLine("Number of specializations to insert: " + doctor.Specialty.Count);

                    foreach (var specialization in doctor.Specialty)
                    {
                        Console.WriteLine("Inserting specialization: " + specialization);

                        string specializationQuery = @"INSERT INTO tb_doctor_specializations (doctor_id, specialization) 
                                       VALUES (@DoctorId, @Specialization)";
                        MySqlCommand specializationCommand = new MySqlCommand(specializationQuery, connection, transaction);

                        specializationCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                        specializationCommand.Parameters.AddWithValue("@Specialization", specialization);

                        specializationCommand.ExecuteNonQuery();

                        specializationsList.Add(specialization);
                    }

                    Console.WriteLine("Specializations inserted: " + string.Join(", ", specializationsList));

                    string joinedSpecializations = string.Join(", ", specializationsList);
                    string updateQuery = @"UPDATE tb_doctorinfo 
                                   SET specialization = @Specialization 
                                   WHERE id = @DoctorId";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction);
                    updateCommand.Parameters.AddWithValue("@Specialization", joinedSpecializations);
                    updateCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                    updateCommand.ExecuteNonQuery();

                    transaction.Commit();
                    return doctorId;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw new Exception("Error inserting doctor data: " + ex.Message);
                }
            }
        }


        #endregion

        #region Update Functions


        public static void UpdateUserPassword(Role role, UserModel user) { 
        
           using (var connection = GetConnection())
            {
                connection.Open();
                string tblName = ProcessMethods.GetTablenameByRole(role);
                

                try
                {
                    string query = $"UPDATE {tblName} SET {(role == Role.Staff ? "s_" : "")}password = @Password WHERE email = @Email AND username = @UserName";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);

                    cmd.ExecuteScalar();
                    return;
                }
               
                catch(Exception ex)
                {
                    throw ex;
                }

            }

        }

        public static void NullPatientReg2Data(string username)
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


        public static void AcceptAppointment(int appointmentId)
        {
            string updateQuery = "update tb_appointmenthistory set ah_status = 'Accepted' where id = @id and ah_status = 'Pending'";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", appointmentId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void DeclineAppointment(int appointmentId)
        {
            string query = "update tb_appointmenthistory set ah_status = 'Declined' where id = @id and ah_status = 'Pending'";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", appointmentId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error declining appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CheckOutAppointment(int appointmentID)
        {
            string updateQuery = "update tb_appointmenthistory set ah_status = 'CheckOut' where id = @id and ah_status = 'Completed'";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", appointmentID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error CheckOut appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void ReconsiderAppointment(int appointmentId)
        {
            string updateQuery = "update tb_appointmenthistory set ah_status = 'Pending' where id = @id and ah_status = 'Declined'";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", appointmentId);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void UpdateStatus(string doctorName)
        {
            string query = @"UPDATE tb_appointmenthistory
                     SET ah_status = @status
                     WHERE ah_Doctor_Name = @doctorName and ah_status = 'Checkout'";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string status = "InvoiceChecked";
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.Parameters.AddWithValue("@doctorName", doctorName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        #endregion

        #region Delete Functions

        public static void DeletePatientByUsername(string username)
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


        public static void DeletePatientReg1Data(PatientModel patient)
        {
            string query = @"
                Delete from tb_patientinfo
                WHERE P_Username = @Username";

            using (var connection = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", patient.UserName);

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


        #endregion


    }
}
