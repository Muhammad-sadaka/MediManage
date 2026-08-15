using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsDetection
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? DetectionID { set; get; }
        public int? AppointmentID { set; get; }
        public int? CreatedByUserID { set; get; }
        public string Symproms { set; get; }
        public string Diagnosis { set; get; }
        public byte? Temperature { set; get; }
        public byte? Wight { set; get; }
        public byte? BloodPressure { set; get; }
        public byte? HeartRate { set; get; }
        public string Notes { set; get; }

        public clsDetection()
        {
            this.DetectionID = null;
            this.AppointmentID = null;
            this.CreatedByUserID = null;
            this.Symproms = "";
            this.Diagnosis = "";
            this.Temperature = null;
            this.Wight = null;
            this.BloodPressure = null;
            this.HeartRate = null;
            this.Notes = "";

            Mode = enMode.AddNew;
        }

        private clsDetection(int? DetectionID, int? AppointmentID, int? CreatedByUserID, string Symproms, string Diagnosis, byte? Temperature, byte? Wight, byte? BloodPressure, byte? HeartRate, string Notes)
        {
            this.DetectionID = DetectionID;
            this.AppointmentID = AppointmentID;
            this.CreatedByUserID = CreatedByUserID;
            this.Symproms = Symproms;
            this.Diagnosis = Diagnosis;
            this.Temperature = Temperature;
            this.Wight = Wight;
            this.BloodPressure = BloodPressure;
            this.HeartRate = HeartRate;
            this.Notes = Notes;

            Mode = enMode.Update;
        }

        private bool _AddNewDetection()
        {
            this.DetectionID = clsDetectionsDataAccess.AddNewDetection(this.AppointmentID, this.CreatedByUserID, this.Symproms, this.Diagnosis, this.Temperature, this.Wight, this.BloodPressure, this.HeartRate, this.Notes);
            return (this.DetectionID != null);
        }

        private bool _UpdateDetection()
        {
            return clsDetectionsDataAccess.UpdateDetection(this.DetectionID, this.AppointmentID, this.CreatedByUserID, this.Symproms, this.Diagnosis, this.Temperature, this.Wight, this.BloodPressure, this.HeartRate, this.Notes) ?? false;
        }

        public static clsDetection FindByID(int? DetectionID)
        {
            if (DetectionID == null) return null;

            int? AppointmentID = null;
            int? CreatedByUserID = null;
            string Symproms = "";
            string Diagnosis = "";
            byte? Temperature = null;
            byte? Wight = null;
            byte? BloodPressure = null;
            byte? HeartRate = null;
            string Notes = "";

            bool? IsFound = clsDetectionsDataAccess.GetDetectionInfoByID(DetectionID, ref AppointmentID, ref CreatedByUserID, ref Symproms, ref Diagnosis, ref Temperature, ref Wight, ref BloodPressure, ref HeartRate, ref Notes);

            if (IsFound == true)
                return new clsDetection(DetectionID, AppointmentID, CreatedByUserID, Symproms, Diagnosis, Temperature, Wight, BloodPressure, HeartRate, Notes);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetection())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetection();
            }
            return false;
        }

        public static DataTable GetAllDetections()
        {
            return clsDetectionsDataAccess.GetAllDetections();
        }

        public static bool DeleteDetection(int? DetectionID)
        {
            return clsDetectionsDataAccess.DeleteDetection(DetectionID);
        }

        public static bool IsDetectionExist(int? DetectionID)
        {
            return clsDetectionsDataAccess.IsDetectionExist(DetectionID) ?? false;
        }
    }
}

