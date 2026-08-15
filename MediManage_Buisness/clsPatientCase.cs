using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPatientCase
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PatientCaseID { set; get; }
        public string PatientCaseName { set; get; }

        public clsPatientCase()
        {
            this.PatientCaseID = null;
            this.PatientCaseName = "";

            Mode = enMode.AddNew;
        }

        private clsPatientCase(int? PatientCaseID, string PatientCaseName)
        {
            this.PatientCaseID = PatientCaseID;
            this.PatientCaseName = PatientCaseName;

            Mode = enMode.Update;
        }

        private bool _AddNewPatientCase()
        {
            this.PatientCaseID = clsPatientCasesDataAccess.AddNewPatientCase(this.PatientCaseName);
            return (this.PatientCaseID != null);
        }

        private bool _UpdatePatientCase()
        {
            return clsPatientCasesDataAccess.UpdatePatientCase(this.PatientCaseID, this.PatientCaseName) ?? false;
        }

        public static clsPatientCase FindByID(int? PatientCaseID)
        {
            if (PatientCaseID == null) return null;

            string PatientCaseName = "";

            bool? IsFound = clsPatientCasesDataAccess.GetPatientCaseInfoByID(PatientCaseID, ref PatientCaseName);

            if (IsFound == true)
                return new clsPatientCase(PatientCaseID, PatientCaseName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatientCase())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePatientCase();
            }
            return false;
        }

        public static DataTable GetAllPatientCases()
        {
            return clsPatientCasesDataAccess.GetAllPatientCases();
        }

        public static bool DeletePatientCase(int? PatientCaseID)
        {
            return clsPatientCasesDataAccess.DeletePatientCase(PatientCaseID);
        }

        public static bool IsPatientCaseExist(int? PatientCaseID)
        {
            return clsPatientCasesDataAccess.IsPatientCaseExist(PatientCaseID) ?? false;
        }
    }
}




