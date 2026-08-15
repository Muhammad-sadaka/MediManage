using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsMedicinesDataAccess
    {
        public static bool? GetMedicineInfoByID(int? MedicineID, ref string MedicineName, ref string Duration, ref string Repetition, ref string Dose, ref int? MedicalPrescriptionID, ref string Notes)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetMedicineByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicineID", (object)MedicineID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                MedicineName = reader["MedicineName"] == DBNull.Value ? null : (string)reader["MedicineName"];
                                Duration = reader["Duration"] == DBNull.Value ? null : (string)reader["Duration"];
                                Repetition = reader["Repetition"] == DBNull.Value ? null : (string)reader["Repetition"];
                                Dose = reader["Dose"] == DBNull.Value ? null : (string)reader["Dose"];
                                MedicalPrescriptionID = reader["MedicalPrescriptionID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MedicalPrescriptionID"]);
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

        public static int? AddNewMedicine(string MedicineName, string Duration, string Repetition, string Dose, int? MedicalPrescriptionID, string Notes)
        {
            int? MedicineID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewMedicine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MedicineName", (object)MedicineName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Duration", (object)Duration ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Repetition", (object)Repetition ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Dose", (object)Dose ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MedicalPrescriptionID", (object)MedicalPrescriptionID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewMedicineID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            MedicineID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return MedicineID;
        }

        public static bool? UpdateMedicine(int? MedicineID, string MedicineName, string Duration, string Repetition, string Dose, int? MedicalPrescriptionID, string Notes)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateMedicine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MedicineID", (object)MedicineID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MedicineName", (object)MedicineName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Duration", (object)Duration ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Repetition", (object)Repetition ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Dose", (object)Dose ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MedicalPrescriptionID", (object)MedicalPrescriptionID ?? DBNull.Value);
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

        public static DataTable GetAllMedicines()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMedicines", connection))
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

        public static bool DeleteMedicine(int? MedicineID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteMedicine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicineID", (object)MedicineID ?? DBNull.Value);

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

        public static bool? IsMedicineExist(int? MedicineID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckMedicineExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicineID", (object)MedicineID ?? DBNull.Value);

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




