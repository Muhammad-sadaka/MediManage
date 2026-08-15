using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsDetectionsDataAccess
    {
        public static bool? GetDetectionInfoByID(int? DetectionID, ref int? AppointmentID, ref int? CreatedByUserID, ref string Symproms, ref string Diagnosis, ref byte? Temperature, ref byte? Wight, ref byte? BloodPressure, ref byte? HeartRate, ref string Notes)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetDetectionByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetectionID", (object)DetectionID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                AppointmentID = reader["AppointmentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["AppointmentID"]);
                                CreatedByUserID = reader["CreatedByUserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedByUserID"]);
                                Symproms = reader["Symproms"] == DBNull.Value ? null : (string)reader["Symproms"];
                                Diagnosis = reader["Diagnosis"] == DBNull.Value ? null : (string)reader["Diagnosis"];
                                Temperature = reader["Temperature"] == DBNull.Value ? (byte?)null : Convert.ToByte(reader["Temperature"]);
                                Wight = reader["Wight"] == DBNull.Value ? (byte?)null : Convert.ToByte(reader["Wight"]);
                                BloodPressure = reader["BloodPressure"] == DBNull.Value ? (byte?)null : Convert.ToByte(reader["BloodPressure"]);
                                HeartRate = reader["HeartRate"] == DBNull.Value ? (byte?)null : Convert.ToByte(reader["HeartRate"]);
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

        public static int? AddNewDetection(int? AppointmentID, int? CreatedByUserID, string Symproms, string Diagnosis, byte? Temperature, byte? Wight, byte? BloodPressure, byte? HeartRate, string Notes)
        {
            int? DetectionID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewDetection", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Symproms", (object)Symproms ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Diagnosis", (object)Diagnosis ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Temperature", (object)Temperature ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Wight", (object)Wight ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BloodPressure", (object)BloodPressure ?? DBNull.Value);
                        command.Parameters.AddWithValue("@HeartRate", (object)HeartRate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewDetectionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            DetectionID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return DetectionID;
        }

        public static bool? UpdateDetection(int? DetectionID, int? AppointmentID, int? CreatedByUserID, string Symproms, string Diagnosis, byte? Temperature, byte? Wight, byte? BloodPressure, byte? HeartRate, string Notes)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateDetection", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DetectionID", (object)DetectionID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AppointmentID", (object)AppointmentID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Symproms", (object)Symproms ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Diagnosis", (object)Diagnosis ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Temperature", (object)Temperature ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Wight", (object)Wight ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BloodPressure", (object)BloodPressure ?? DBNull.Value);
                        command.Parameters.AddWithValue("@HeartRate", (object)HeartRate ?? DBNull.Value);
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

        public static DataTable GetAllDetections()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllDetections", connection))
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

        public static bool DeleteDetection(int? DetectionID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteDetection", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetectionID", (object)DetectionID ?? DBNull.Value);

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

        public static bool? IsDetectionExist(int? DetectionID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckDetectionExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DetectionID", (object)DetectionID ?? DBNull.Value);

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
    }
}


