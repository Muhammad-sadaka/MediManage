using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MediManage_DataAccess
{
    public class clsPeopleDataAccess
    {
        public static bool? GetPersonInfoByID(int? PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string NationalNo, ref string Phone, ref DateTime? DateOfBirth, ref string Gender, ref string Image, ref string Address, ref string Email, ref int? BloodTypeID, ref int? MaritalStatusID ,ref int? CountryId)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPersonByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                FirstName = reader["FirstName"] == DBNull.Value ? null : (string)reader["FirstName"];
                                SecondName = reader["SecondName"] == DBNull.Value ? null : (string)reader["SecondName"];
                                ThirdName = reader["ThirdName"] == DBNull.Value ? null : (string)reader["ThirdName"];
                                LastName = reader["LastName"] == DBNull.Value ? null : (string)reader["LastName"];
                                NationalNo = reader["NationalNo"] == DBNull.Value ? null : (string)reader["NationalNo"];
                                Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"];
                                DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfBirth"]);
                                Gender = reader["Gender"] == DBNull.Value ? null : (string)reader["Gender"];
                                Image = reader["Image"] == DBNull.Value ? null : (string)reader["Image"];
                                Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"];
                                Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
                                BloodTypeID = reader["BloodTypeID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["BloodTypeID"]);
                                MaritalStatusID = reader["MaritalStatusID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaritalStatusID"]);
                                CountryId = reader["CountryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CountryId"]);
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

        public static int? AddNewPerson(string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, string Phone, DateTime? DateOfBirth, string Gender, string Image, string Address, string Email, int? BloodTypeID, int? MaritalStatusID,int? CountryId)
        {
            int? PersonID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", (object)FirstName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SecondName", (object)SecondName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NationalNo", (object)NationalNo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Gender", (object)Gender ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Image", (object)Image ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BloodTypeID", (object)BloodTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MaritalStatusID", (object)MaritalStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CountryId", (object)CountryId ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (outputIdParam.Value != DBNull.Value)
                            PersonID = (int)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.EventLogCreate();
                EventLog.WriteEntry(clsDataAccessSettings.sourceName, "Error: " + ex.Message, EventLogEntryType.Error);
            }

            return PersonID;
        }

        public static bool? UpdatePerson(int? PersonID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, string Phone, DateTime? DateOfBirth, string Gender, string Image, string Address, string Email, int? BloodTypeID, int? MaritalStatusID, int? CountryId)
        {
            int? rowsAffected = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@FirstName", (object)FirstName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SecondName", (object)SecondName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ThirdName", (object)ThirdName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", (object)LastName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NationalNo", (object)NationalNo ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DateOfBirth", (object)DateOfBirth ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Gender", (object)Gender ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Image", (object)Image ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address", (object)Address ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BloodTypeID", (object)BloodTypeID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MaritalStatusID", (object)MaritalStatusID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CountryId", (object)CountryId ?? DBNull.Value);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
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

        public static bool DeletePerson(int? PersonID)
        {
            int? rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

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

        public static bool? IsPersonExist(int? PersonID)
        {
            bool? isFound = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_CheckPersonExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", (object)PersonID ?? DBNull.Value);

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
