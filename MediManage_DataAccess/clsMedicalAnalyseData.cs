using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsMedicalAnalysesDataAccess
    {
        public static bool? GetMedicalAnalysisInfoByID(int? MedicalAnalysisID, ref string Result, ref DateTime? OrderDate, ref DateTime? ResultDate, ref int? AnalysisStatusID, ref int? AnalysisTypeID, ref int? DetectionID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetMedicalAnalysisByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalAnalysisID", (object)MedicalAnalysisID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                Result = reader["Result"] == DBNull.Value ? null : (string)reader["Result"];
                                OrderDate = reader["OrderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["OrderDate"]);
                                ResultDate = reader["ResultDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ResultDate"]);
                                AnalysisStatusID = reader["AnalysisStatusID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["AnalysisStatusID"]);
                                AnalysisTypeID = reader["AnalysisTypeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["AnalysisTypeID"]);
                                DetectionID = reader["DetectionID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["DetectionID"]);
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

        public static int? AddNewMedicalAnalysis(string Result, DateTime? OrderDate, DateTime? ResultDate, int? AnalysisStatusID, int? AnalysisTypeID, int? DetectionID)
        {
            int? MedicalAnalysisID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewMedicalAnalysis", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Result", (object)Result ?? DBNull.Value);
                        command.Parameters.AddWithValue("@OrderDate", (object)OrderDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ResultDate", (object)ResultDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AnalysisStatusID", (object)AnalysisStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AnalysisTypeID", (object)AnalysisTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DetectionID", (object)DetectionID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewMedicalAnalysisID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            MedicalAnalysisID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return MedicalAnalysisID;
        }

        public static bool? UpdateMedicalAnalysis(int? MedicalAnalysisID, string Result, DateTime? OrderDate, DateTime? ResultDate, int? AnalysisStatusID, int? AnalysisTypeID, int? DetectionID)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateMedicalAnalysis", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MedicalAnalysisID", (object)MedicalAnalysisID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Result", (object)Result ?? DBNull.Value);
                        command.Parameters.AddWithValue("@OrderDate", (object)OrderDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ResultDate", (object)ResultDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AnalysisStatusID", (object)AnalysisStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AnalysisTypeID", (object)AnalysisTypeID ?? DBNull.Value);
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
                return false;
            }

            return rowsAffected > 0;
        }

        public static DataTable GetAllMedicalAnalyses()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMedicalAnalyses", connection))
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

        public static bool DeleteMedicalAnalysis(int? MedicalAnalysisID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteMedicalAnalysis", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalAnalysisID", (object)MedicalAnalysisID ?? DBNull.Value);

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

        public static bool? IsMedicalAnalysisExist(int? MedicalAnalysisID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckMedicalAnalysisExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicalAnalysisID", (object)MedicalAnalysisID ?? DBNull.Value);

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





