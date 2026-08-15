using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsBillsDataAccess
    {
        public static bool? GetBillInfoByID(int? Bill_ID, ref int? PatientID, ref int? CreatedByUserID, ref DateTime? BillDate, ref decimal? AmountOfPaid, ref decimal? AmountOfRemaining, ref decimal? TotalAmount, ref int? PaymentStatusID, ref int? PaymentMethodID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBillByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                PatientID = reader["PatientID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PatientID"]);
                                CreatedByUserID = reader["CreatedByUserID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CreatedByUserID"]);
                                BillDate = reader["BillDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["BillDate"]);
                                AmountOfPaid = reader["AmountOfPaid"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["AmountOfPaid"]);
                                AmountOfRemaining = reader["AmountOfRemaining"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["AmountOfRemaining"]);
                                TotalAmount = reader["TotalAmount"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["TotalAmount"]);
                                PaymentStatusID = reader["PaymentStatusID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PaymentStatusID"]);
                                PaymentMethodID = reader["PaymentMethodID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PaymentMethodID"]);
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

        public static int? AddNewBill(int? PatientID, int? CreatedByUserID, DateTime? BillDate, decimal? AmountOfPaid, decimal? AmountOfRemaining, decimal? TotalAmount, int? PaymentStatusID, int? PaymentMethodID)
        {
            int? Bill_ID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PatientID", (object)PatientID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BillDate", (object)BillDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AmountOfPaid", (object)AmountOfPaid ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AmountOfRemaining", (object)AmountOfRemaining ?? DBNull.Value);
                        command.Parameters.AddWithValue("@TotalAmount", (object)TotalAmount ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentStatusID", (object)PaymentStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentMethodID", (object)PaymentMethodID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewBillID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            Bill_ID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return Bill_ID;
        }

        public static bool? UpdateBill(int? Bill_ID, int? PatientID, int? CreatedByUserID, DateTime? BillDate, decimal? AmountOfPaid, decimal? AmountOfRemaining, decimal? TotalAmount, int? PaymentStatusID, int? PaymentMethodID)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PatientID", (object)PatientID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedByUserID", (object)CreatedByUserID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BillDate", (object)BillDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AmountOfPaid", (object)AmountOfPaid ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AmountOfRemaining", (object)AmountOfRemaining ?? DBNull.Value);
                        command.Parameters.AddWithValue("@TotalAmount", (object)TotalAmount ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentStatusID", (object)PaymentStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentMethodID", (object)PaymentMethodID ?? DBNull.Value);

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

        public static DataTable GetAllBills()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllBills", connection))
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

        public static bool DeleteBill(int? Bill_ID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBill", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);

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

        public static bool? IsBillExist(int? Bill_ID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckBillExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);

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


