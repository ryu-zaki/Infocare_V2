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

namespace Infocare_Project
{
    internal class Database
    {
        private string connectionString = "Server=127.0.0.1; Database=db_infocare_project;User ID=root; Password=;";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

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


        public void AddStaff(Staff staff)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = @"INSERT INTO tb_staffinfo (s_FirstName, s_LastName, s_MiddleName, s_Username, s_Password, s_suffix, s_contactNumber, s_email) " +
                                   "VALUES (@FirstName, @LastName, @MiddleName, @Username, @Password, @suffix, @ContactNumber, @email)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", staff.FirstName);
                    command.Parameters.AddWithValue("@LastName", staff.LastName);
                    command.Parameters.AddWithValue("@MiddleName", staff.MiddleName);
                    command.Parameters.AddWithValue("@Username", staff.Username);
                    command.Parameters.AddWithValue("@Password", staff.Password);
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

        public bool StaffLogin(string username, string password)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM tb_staffinfo WHERE s_Username = @Username AND s_Password = @Password";

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


        public bool Doctorlogin(string username, string password)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM tb_doctorinfo WHERE Username = @Username AND Password = @Password";

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
                             (firstname, middlename, lastname, username, password, consultationfee, specialization, start_time, end_time, day_availability) 
                             VALUES (@FirstName, @MiddleName, @LastName, @Username, @Password, @ConsultationFee, @Specialization, @StartTime, @EndTime, @DayAvailability)";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", doctor.MiddleName);
                    command.Parameters.AddWithValue("@LastName", doctor.LastName);
                    command.Parameters.AddWithValue("@Username", doctor.Username);
                    command.Parameters.AddWithValue("@Password", doctor.Password);
                    command.Parameters.AddWithValue("@ConsultationFee", doctor.ConsultationFee);
                    command.Parameters.AddWithValue("@Specialization", doctor.Specialty);
                    command.Parameters.AddWithValue("@StartTime", doctor.StartTime);
                    command.Parameters.AddWithValue("@EndTime", doctor.EndTime);
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

        public (string firstName, string lastName) GetStaffNameDetails(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT s_Firstname, s_Lastname FROM tb_staffinfo WHERE s_Username = @Username";

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

             public (string firstName, string lastName) GetDoctorNameDetails(string username)
        {
            using (var connection = GetConnection())
            {
                string query = "SELECT Firstname, Lastname FROM tb_doctorinfo WHERE Username = @Username";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["Firstname"].ToString();
                            string lastName = reader["Lastname"].ToString();

                            return (firstName, lastName);
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
        public List<string> GetPatientNames()
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


        public List<string> GetDoctorNames(string specialization)
        {
            List<string> doctorNames = new List<string>();

            string query = @"SELECT CONCAT('Dr. ', Lastname, ', ', Firstname) AS doctor_name
                     FROM tb_doctorinfo
                    WHERE specialization = @Specialization";

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
                    throw new Exception("Error getting doctor data: " + ex.Message);
                }
            }
            return doctorNames;
        }

        public List<string> GetDoctorAvailableTimes(string doctorName)
        {
            List<string> timeSlots = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT Start_Time, End_Time FROM tb_doctorinfo WHERE CONCAT('Dr. ', Lastname, ', ', Firstname) = @doctorName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@doctorName", doctorName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string startTime = reader["Start_Time"].ToString();
                            string endTime = reader["End_Time"].ToString();

                            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                            {
                                TimeSpan start = TimeSpan.Parse(startTime);
                                TimeSpan end = TimeSpan.Parse(endTime);

                                for (TimeSpan currentTime = start; currentTime < end; currentTime = currentTime.Add(TimeSpan.FromHours(1)))
                                {
                                    timeSlots.Add(currentTime.ToString(@"hh\:mm"));
                                }

                            }
                        }
                    }
                }
            }

            return timeSlots;
        }

        public string GetDoctorAvailability(string doctorName)
        {
            string query = @"SELECT day_availability 
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
                    return result != null ? result.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching doctor availability: " + ex.Message);
                }
            }
        }

        public DataTable StaffList()
        {
            string query = @"SELECT s_Firstname AS 'First Name', s_middleName AS 'Middle Name', s_lastname AS 'Last Name', s_suffix AS 'Suffix', s_contactnumber AS 'Contact Number', s_email AS 'Email' FROM tb_staffinfo";

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

        public DataTable DoctorList()
        {
            string query = @"SELECT Firstname AS 'First Name', middleName AS 'Middle Name', lastname AS 'Last Name', specialization AS 'Specialization', day_availability AS 'Day Available' FROM tb_doctorinfo";

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

        public DataTable PatientList()
        {
            string query = @"SELECT p_Firstname AS 'First Name', p_middleName AS 'Middle Name', P_lastname AS 'Last Name', p_suffix AS 'Suffix', p_sex AS 'Sex', P_bdate AS 'Birth Date', p_address AS 'Full Address' FROM tb_patientinfo";

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


        public List<string> GetSpecialization()
        {
            List<string> specialization = new List<string>();

            string query = @"select Specialization from tb_doctorinfo group by Specialization";

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
                            string spec = reader["Specialization"].ToString();
                            if (!string.IsNullOrEmpty(spec))
                            {
                                specialization.Add(spec);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting specialization: " + ex.Message);
                }
            }
            return specialization;
        }

        public decimal? GetConsultationFee(string specialization)
        {
            using (var connection = GetConnection())
            {
                // Corrected SQL query
                string query = @"SELECT consultationfee FROM tb_doctorinfo WHERE Specialization = @specialization";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@specialization", specialization);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Return the consultation fee from the database
                            return reader.IsDBNull(reader.GetOrdinal("consultationfee")) ? (decimal?)null : reader.GetDecimal("consultationfee");
                        }
                        else
                        {
                            return null; // No data found
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching consultation fee: " + ex.Message);
                }
            }
        }

        public bool IsAppointmentBooked(string doctorName, DateTime appointmentDate)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM tb_appointmenthistory WHERE ah_Doctor_Name = @DoctorName AND ah_date = @AppointmentDate";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorName", doctorName);
                        command.Parameters.AddWithValue("@AppointmentDate", appointmentDate.Date);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0; // Return true if an appointment already exists
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool IsPatientAppointmentPendingOrAccepted(string patientName)
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

        public bool SaveAppointment(string patientName, string specialization, string doctorName, string timeSlot, DateTime appointmentDate, decimal consFee)
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

        public DataTable AppointmentList()
        {
            string query = @"select ah_patient_name AS 'Patient Name', ah_doctor_name AS 'Doctor Name',ah_Specialization AS 'Specialization', ah_time AS 'Time Slot', ah_date AS 'Date', ah_consfee AS 'Consultation Fee', ah_status AS 'Appointment Status' From tb_appointmenthistory";

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

        public DataTable PendingAppointmentList(string doctorFullName)
{
    string query = @"SELECT * FROM tb_appointmenthistory 
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




        public void AcceptAppointment(int appointmentId)
        {
            string updateQuery = "update tb_appointmenthistory set ah_status = 'Accepted' where id = @id";

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

        public void DeclineAppointment(int appointmentId)
        {
            string query = "update tb_appointmenthistory set ah_status = 'Declined' where id = @id";

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

        public DataTable ViewAppointments(string doctorFullName)
        {
            string query = @"SELECT * FROM tb_appointmenthistory 
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

        public DataTable DeclinedAppointments(string doctorFullName)
        {
            string query = @"SELECT * FROM tb_appointmenthistory 
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

        public void CreateDiagnosis(int appointmentId, Action<Dictionary<string, string>> onSuccess, Action<string> onFailure)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string getPatientNameQuery = "SELECT ah_Patient_Name FROM tb_appointmenthistory WHERE id = @appointmentId";
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
    }
}
