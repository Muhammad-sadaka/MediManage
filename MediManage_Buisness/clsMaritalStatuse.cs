using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsMaritalStatus
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? MaritalStatusID { set; get; }
        public string MaritalStatusName { set; get; }

        public clsMaritalStatus()
        {
            this.MaritalStatusID = null;
            this.MaritalStatusName = "";

            Mode = enMode.AddNew;
        }

        private clsMaritalStatus(int? MaritalStatusID, string MaritalStatusName)
        {
            this.MaritalStatusID = MaritalStatusID;
            this.MaritalStatusName = MaritalStatusName;

            Mode = enMode.Update;
        }

        private bool _AddNewMaritalStatus()
        {
            this.MaritalStatusID = clsMaritalStatusesDataAccess.AddNewMaritalStatus(this.MaritalStatusName);
            return (this.MaritalStatusID != null);
        }

        private bool _UpdateMaritalStatus()
        {
            return clsMaritalStatusesDataAccess.UpdateMaritalStatus(this.MaritalStatusID, this.MaritalStatusName) ?? false;
        }

        public static clsMaritalStatus FindByID(int? MaritalStatusID)
        {
            if (MaritalStatusID == null) return null;

            string MaritalStatusName = "";

            bool? IsFound = clsMaritalStatusesDataAccess.GetMaritalStatusInfoByID(MaritalStatusID, ref MaritalStatusName);

            if (IsFound == true)
                return new clsMaritalStatus(MaritalStatusID, MaritalStatusName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMaritalStatus())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMaritalStatus();
            }
            return false;
        }

        public static DataTable GetAllMaritalStatuses()
        {
            return clsMaritalStatusesDataAccess.GetAllMaritalStatuses();
        }

        public static bool DeleteMaritalStatus(int? MaritalStatusID)
        {
            return clsMaritalStatusesDataAccess.DeleteMaritalStatus(MaritalStatusID);
        }

        public static bool IsMaritalStatusExist(int? MaritalStatusID)
        {
            return clsMaritalStatusesDataAccess.IsMaritalStatusExist(MaritalStatusID) ?? false;
        }
    }
}

