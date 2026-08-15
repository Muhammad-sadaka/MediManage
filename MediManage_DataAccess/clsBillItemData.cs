using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsBillItemsDataAccess
    {
        public static bool? GetBillItemInfoByID(int? BillItemID, ref int? Bill_ID, ref int? ServiceTypeID, ref string Description, ref decimal? Price, ref int? Amount, ref int? Total)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBillItemByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillItemID", (object)BillItemID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                Bill_ID = reader["Bill_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Bill_ID"]);
                                ServiceTypeID = reader["ServiceTypeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ServiceTypeID"]);
                                Description = reader["Description"] == DBNull.Value ? null : (string)reader["Description"];
                                Price = reader["Price"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["Price"]);
                                Amount = reader["Amount"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Amount"]);
                                Total = reader["Total"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Total"]);
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

        public static int? AddNewBillItem(int? Bill_ID, int? ServiceTypeID, string Description, decimal? Price, int? Amount, int? Total)
        {
            int? BillItemID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBillItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ServiceTypeID", (object)ServiceTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Price", (object)Price ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Amount", (object)Amount ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Total", (object)Total ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewBillItemID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            BillItemID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return BillItemID;
        }

        public static bool? UpdateBillItem(int? BillItemID, int? Bill_ID, int? ServiceTypeID, string Description, decimal? Price, int? Amount, int? Total)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBillItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BillItemID", (object)BillItemID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Bill_ID", (object)Bill_ID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ServiceTypeID", (object)ServiceTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Price", (object)Price ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Amount", (object)Amount ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Total", (object)Total ?? DBNull.Value);

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

        public static DataTable GetAllBillItems()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllBillItems", connection))
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

        public static bool DeleteBillItem(int? BillItemID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBillItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillItemID", (object)BillItemID ?? DBNull.Value);

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

        public static bool? IsBillItemExist(int? BillItemID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckBillItemExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BillItemID", (object)BillItemID ?? DBNull.Value);

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


