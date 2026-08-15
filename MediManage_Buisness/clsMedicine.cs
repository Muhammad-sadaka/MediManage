using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsMedicine
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? MedicineID { set; get; }
        public string MedicineName { set; get; }
        public string Duration { set; get; }
        public string Repetition { set; get; }
        public string Dose { set; get; }
        public int? MedicalPrescriptionID { set; get; }
        public string Notes { set; get; }

        public clsMedicine()
        {
            this.MedicineID = null;
            this.MedicineName = "";
            this.Duration = "";
            this.Repetition = "";
            this.Dose = "";
            this.MedicalPrescriptionID = null;
            this.Notes = null;

            Mode = enMode.AddNew;
        }

        private clsMedicine(int? MedicineID, string MedicineName, string Duration, string Repetition, string Dose, int? MedicalPrescriptionID, string Notes)
        {
            this.MedicineID = MedicineID;
            this.MedicineName = MedicineName;
            this.Duration = Duration;
            this.Repetition = Repetition;
            this.Dose = Dose;
            this.MedicalPrescriptionID = MedicalPrescriptionID;
            this.Notes = Notes;

            Mode = enMode.Update;
        }

        private bool _AddNewMedicine()
        {
            this.MedicineID = clsMedicinesDataAccess.AddNewMedicine(this.MedicineName, this.Duration, this.Repetition, this.Dose, this.MedicalPrescriptionID, this.Notes);
            return (this.MedicineID != null);
        }

        private bool _UpdateMedicine()
        {
            return clsMedicinesDataAccess.UpdateMedicine(this.MedicineID, this.MedicineName, this.Duration, this.Repetition, this.Dose, this.MedicalPrescriptionID, this.Notes) ?? false;
        }

        public static clsMedicine FindByID(int? MedicineID)
        {
            if (MedicineID == null) return null;

            string MedicineName = "";
            string Duration = "";
            string Repetition = "";
            string Dose = "";
            int? MedicalPrescriptionID = null;
            string Notes = null;

            bool? IsFound = clsMedicinesDataAccess.GetMedicineInfoByID(MedicineID, ref MedicineName, ref Duration, ref Repetition, ref Dose, ref MedicalPrescriptionID, ref Notes);

            if (IsFound == true)
                return new clsMedicine(MedicineID, MedicineName, Duration, Repetition, Dose, MedicalPrescriptionID, Notes);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMedicine())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMedicine();
            }
            return false;
        }

        public static DataTable GetAllMedicines()
        {
            return clsMedicinesDataAccess.GetAllMedicines();
        }

        public static bool DeleteMedicine(int? MedicineID)
        {
            return clsMedicinesDataAccess.DeleteMedicine(MedicineID);
        }

        public static bool IsMedicineExist(int? MedicineID)
        {
            return clsMedicinesDataAccess.IsMedicineExist(MedicineID) ?? false;
        }
    }
}

