using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsDoctor
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? DoctorID { set; get; }
        public int? PersonID { set; get; }
        public byte? YearsOfExperience { set; get; }
        public string Qualification { set; get; }
        public bool? IsActive { set; get; }
        public int? SpecialtyID { set; get; }

        public clsDoctor()
        {
            this.DoctorID = null;
            this.PersonID = null;
            this.YearsOfExperience = null;
            this.Qualification = "";
            this.IsActive = null;
            this.SpecialtyID = null;

            Mode = enMode.AddNew;
        }

        private clsDoctor(int? DoctorID, int? PersonID, byte? YearsOfExperience, string Qualification, bool? IsActive, int? SpecialtyID)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.YearsOfExperience = YearsOfExperience;
            this.Qualification = Qualification;
            this.IsActive = IsActive;
            this.SpecialtyID = SpecialtyID;

            Mode = enMode.Update;
        }

        private bool _AddNewDoctor()
        {
            this.DoctorID = clsDoctorsDataAccess.AddNewDoctor(this.PersonID, this.YearsOfExperience, this.Qualification, this.IsActive, this.SpecialtyID);
            return (this.DoctorID != null);
        }

        private bool _UpdateDoctor()
        {
            return clsDoctorsDataAccess.UpdateDoctor(this.DoctorID, this.PersonID, this.YearsOfExperience, this.Qualification, this.IsActive, this.SpecialtyID) ?? false;
        }

        public static clsDoctor FindByID(int? DoctorID)
        {
            if (DoctorID == null) return null;

            int? PersonID = null;
            byte? YearsOfExperience = null;
            string Qualification = "";
            bool? IsActive = null;
            int? SpecialtyID = null;

            bool? IsFound = clsDoctorsDataAccess.GetDoctorInfoByID(DoctorID, ref PersonID, ref YearsOfExperience, ref Qualification, ref IsActive, ref SpecialtyID);

            if (IsFound == true)
                return new clsDoctor(DoctorID, PersonID, YearsOfExperience, Qualification, IsActive, SpecialtyID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDoctor())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDoctor();
            }
            return false;
        }

        public static DataTable GetAllDoctors()
        {
            return clsDoctorsDataAccess.GetAllDoctors();
        }

        public static bool DeleteDoctor(int? DoctorID)
        {
            return clsDoctorsDataAccess.DeleteDoctor(DoctorID);
        }

        public static bool IsDoctorExist(int? DoctorID)
        {
            return clsDoctorsDataAccess.IsDoctorExist(DoctorID) ?? false;
        }
    }
}

