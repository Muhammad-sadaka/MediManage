using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsMedicalPrescription
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? MedicalPrescriptionID { set; get; }
        public int? DetectionID { set; get; }
        public string Notes { set; get; }

        public clsMedicalPrescription()
        {
            this.MedicalPrescriptionID = null;
            this.DetectionID = null;
            this.Notes = "";

            Mode = enMode.AddNew;
        }

        private clsMedicalPrescription(int? MedicalPrescriptionID, int? DetectionID, string Notes)
        {
            this.MedicalPrescriptionID = MedicalPrescriptionID;
            this.DetectionID = DetectionID;
            this.Notes = Notes;

            Mode = enMode.Update;
        }

        private bool _AddNewMedicalPrescription()
        {
            this.MedicalPrescriptionID = clsMedicalPrescriptionsDataAccess.AddNewMedicalPrescription(this.DetectionID, this.Notes);
            return (this.MedicalPrescriptionID != null);
        }

        private bool _UpdateMedicalPrescription()
        {
            return clsMedicalPrescriptionsDataAccess.UpdateMedicalPrescription(this.MedicalPrescriptionID, this.DetectionID, this.Notes) ?? false;
        }

        public static clsMedicalPrescription FindByID(int? MedicalPrescriptionID)
        {
            if (MedicalPrescriptionID == null) return null;

            int? DetectionID = null;
            string Notes = "";

            bool? IsFound = clsMedicalPrescriptionsDataAccess.GetMedicalPrescriptionInfoByID(MedicalPrescriptionID, ref DetectionID, ref Notes);

            if (IsFound == true)
                return new clsMedicalPrescription(MedicalPrescriptionID, DetectionID, Notes);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMedicalPrescription())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMedicalPrescription();
            }
            return false;
        }

        public static DataTable GetAllMedicalPrescriptions()
        {
            return clsMedicalPrescriptionsDataAccess.GetAllMedicalPrescriptions();
        }

        public static bool DeleteMedicalPrescription(int? MedicalPrescriptionID)
        {
            return clsMedicalPrescriptionsDataAccess.DeleteMedicalPrescription(MedicalPrescriptionID);
        }

        public static bool IsMedicalPrescriptionExist(int? MedicalPrescriptionID)
        {
            return clsMedicalPrescriptionsDataAccess.IsMedicalPrescriptionExist(MedicalPrescriptionID) ?? false;
        }
    }
}

