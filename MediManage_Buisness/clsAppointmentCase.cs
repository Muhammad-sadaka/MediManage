using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsAppointmentCase
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? AppointmentCaseID { set; get; }
        public string AppointmentCaseName { set; get; }

        public clsAppointmentCase()
        {
            this.AppointmentCaseID = null;
            this.AppointmentCaseName = "";

            Mode = enMode.AddNew;
        }

        private clsAppointmentCase(int? AppointmentCaseID, string AppointmentCaseName)
        {
            this.AppointmentCaseID = AppointmentCaseID;
            this.AppointmentCaseName = AppointmentCaseName;

            Mode = enMode.Update;
        }

        private bool _AddNewAppointmentCase()
        {
            this.AppointmentCaseID = clsAppointmentCaseData.AddNewAppointmentCase(this.AppointmentCaseName);
            return (this.AppointmentCaseID != null);
        }

        private bool _UpdateAppointmentCase()
        {
            return clsAppointmentCaseData.UpdateAppointmentCase(this.AppointmentCaseID, this.AppointmentCaseName) ?? false;
        }

        public static clsAppointmentCase FindByID(int? AppointmentCaseID)
        {
            if (AppointmentCaseID == null) return null;

            string AppointmentCaseName = "";

            bool? IsFound = clsAppointmentCaseData.GetAppointmentCaseInfoByID(AppointmentCaseID, ref AppointmentCaseName);

            if (IsFound == true)
                return new clsAppointmentCase(AppointmentCaseID, AppointmentCaseName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAppointmentCase())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAppointmentCase();
            }
            return false;
        }

        public static DataTable GetAllAppointmentCases()
        {
            return clsAppointmentCaseData.GetAllAppointmentCases();
        }

        public static bool DeleteAppointmentCase(int? AppointmentCaseID)
        {
            return clsAppointmentCaseData.DeleteAppointmentCase(AppointmentCaseID);
        }

        public static bool IsAppointmentCaseExist(int? AppointmentCaseID)
        {
            return clsAppointmentCaseData.IsAppointmentCaseExist(AppointmentCaseID) ?? false;
        }
    }
}


