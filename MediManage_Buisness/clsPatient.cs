using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPatient
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PatientID { set; get; }
        public int? PersonID { set; get; }
        public string Sensitivity { set; get; }
        public string ChronicDiseases { set; get; }
        public DateTime? JoinDate { set; get; }
        public int? PatientCaseID { set; get; }

        public clsPatient()
        {
            this.PatientID = null;
            this.PersonID = null;
            this.Sensitivity = null;
            this.ChronicDiseases = null;
            this.JoinDate = null;
            this.PatientCaseID = null;

            Mode = enMode.AddNew;
        }

        private clsPatient(int? PatientID, int? PersonID, string Sensitivity, string ChronicDiseases, DateTime? JoinDate, int? PatientCaseID)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;
            this.Sensitivity = Sensitivity;
            this.ChronicDiseases = ChronicDiseases;
            this.JoinDate = JoinDate;
            this.PatientCaseID = PatientCaseID;

            Mode = enMode.Update;
        }

        private bool _AddNewPatient()
        {
            this.PatientID = clsPatientsDataAccess.AddNewPatient(this.PersonID, this.Sensitivity, this.ChronicDiseases, this.JoinDate, this.PatientCaseID);
            return (this.PatientID != null);
        }

        private bool _UpdatePatient()
        {
            return clsPatientsDataAccess.UpdatePatient(this.PatientID, this.PersonID, this.Sensitivity, this.ChronicDiseases, this.JoinDate, this.PatientCaseID) ?? false;
        }

        public static clsPatient FindByID(int? PatientID)
        {
            if (PatientID == null) return null;

            int? PersonID = null;
            string Sensitivity = null;
            string ChronicDiseases = null;
            DateTime? JoinDate = null;
            int? PatientCaseID = null;

            bool? IsFound = clsPatientsDataAccess.GetPatientInfoByID(PatientID, ref PersonID, ref Sensitivity, ref ChronicDiseases, ref JoinDate, ref PatientCaseID);

            if (IsFound == true)
                return new clsPatient(PatientID, PersonID, Sensitivity, ChronicDiseases, JoinDate, PatientCaseID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePatient();
            }
            return false;
        }

        public static DataTable GetAllPatients()
        {
            return clsPatientsDataAccess.GetAllPatients();
        }

        public static bool DeletePatient(int? PatientID)
        {
            return clsPatientsDataAccess.DeletePatient(PatientID);
        }

        public static bool IsPatientExist(int? PatientID)
        {
            return clsPatientsDataAccess.IsPatientExist(PatientID) ?? false;
        }

        public static int? GetTotalPatientsNumber()
        {
            return clsPatientsDataAccess.GetTotalPatientsNumber();
        }
    }
}

