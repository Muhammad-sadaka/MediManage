using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsAppointmentsDataAccess
    {
        public static bool? GetAppointmentInfoByID(int? AppointmentID, ref int? PatientID, ref int? DoctorID, ref int? CreatedByUserID, ref DateTime? BookingDate, ref DateTime? AppointmentDate, ref int? AppointmentCaseID, ref byte? Duration, ref string Reason, ref string Notes)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAppointmentByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                PatientID = reader["PatientID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PatientID"]);
                                DoctorID = reader["DoctorID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["DoctorID"]);
                                CreatedByUserID = reader["CreatedByUserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedByUserID"]);
                                BookingDate = reader["BookingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["BookingDate"]);
                                AppointmentDate = reader["AppointmentDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["AppointmentDate"]);
                                AppointmentCaseID = reader["AppointmentCaseID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["AppointmentCaseID"]);
                                Duration = reader["Duration"] == DBNull.Value ? (byte?)null : Convert.ToByte(reader["Duration"]);
                                Reason = reader["Reason"] == DBNull.Value ? null : (string)reader["Reason"];
                                Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                            }
                            else
                            {
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            return isFound;
        }

        public static int? AddNewAppointment(int? PatientID, int? DoctorID, int? CreatedByUserID, DateTime? BookingDate, DateTime? AppointmentDate, int? AppointmentCaseID, byte? Duration, string Reason, string Notes)
        {
            int? AppointmentID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", (object)PatientID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DoctorID", (object)DoctorID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BookingDate", (object)BookingDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AppointmentDate", (object)AppointmentDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AppointmentCaseID", (object)AppointmentCaseID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Duration", (object)Duration ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Reason", (object)Reason ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewAppointmentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            AppointmentID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return AppointmentID;
        }

        public static bool? UpdateAppointment(int? AppointmentID, int? PatientID, int? DoctorID, int? CreatedByUserID, DateTime? BookingDate, DateTime? AppointmentDate, int? AppointmentCaseID, byte? Duration, string Reason, string Notes)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PatientID", (object)PatientID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DoctorID", (object)DoctorID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BookingDate", (object)BookingDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AppointmentDate", (object)AppointmentDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AppointmentCaseID", (object)AppointmentCaseID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Duration", (object)Duration ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Reason", (object)Reason ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
                return false;
            }

            return rowsAffected > 0;
        }

        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllAppointments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return dt;
        }

        public static bool DeleteAppointment(int? AppointmentID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }
            return (rowsAffected > 0);
        }

        public static bool? IsAppointmentExist(int? AppointmentID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckAppointmentExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (returnParameter.Value != null)
                        {
                            isFound = (int)returnParameter.Value == 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
                isFound = false;
            }

            return isFound;
        }

        public static DataTable GetTodayAppointments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(@"SP_GetTodayAppointments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return dt;
        }
    }
}


