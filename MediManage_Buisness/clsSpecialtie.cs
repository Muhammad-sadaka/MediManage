using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsSpecialty
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? SpecialtyID { set; get; }
        public string SpecialtyName { set; get; }
        public string Description { set; get; }
        public decimal? Fees { set; get; }

        public clsSpecialty()
        {
            this.SpecialtyID = null;
            this.SpecialtyName = "";
            this.Description = "";
            this.Fees = 0;

            Mode = enMode.AddNew;
        }

        private clsSpecialty(int? SpecialtyID, string SpecialtyName, string Description, decimal? Fees)
        {
            this.SpecialtyID = SpecialtyID;
            this.SpecialtyName = SpecialtyName;
            this.Description = Description;
            this.Fees = Fees;

            Mode = enMode.Update;
        }

        private bool _AddNewSpecialty()
        {
            this.SpecialtyID = clsSpecialtiesDataAccess.AddNewSpecialty(this.SpecialtyName, this.Description, this.Fees);
            return (this.SpecialtyID != null);
        }

        private bool _UpdateSpecialty()
        {
            return clsSpecialtiesDataAccess.UpdateSpecialty(this.SpecialtyID, this.SpecialtyName, this.Description, this.Fees) ?? false;
        }

        public static clsSpecialty FindByID(int? SpecialtyID)
        {
            if (SpecialtyID == null) return null;

            string SpecialtyName = "";
            string Description = "";
            decimal? Fees = 0;

            bool? IsFound = clsSpecialtiesDataAccess.GetSpecialtyInfoByID(SpecialtyID, ref SpecialtyName, ref Description, ref Fees);

            if (IsFound == true)
                return new clsSpecialty(SpecialtyID, SpecialtyName, Description, Fees);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSpecialty())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateSpecialty();
            }
            return false;
        }

        public static DataTable GetAllSpecialties()
        {
            return clsSpecialtiesDataAccess.GetAllSpecialties();
        }

        public static bool DeleteSpecialty(int? SpecialtyID)
        {
            return clsSpecialtiesDataAccess.DeleteSpecialty(SpecialtyID);
        }

        public static bool IsSpecialtyExist(int? SpecialtyID)
        {
            return clsSpecialtiesDataAccess.IsSpecialtyExist(SpecialtyID) ?? false;
        }
    }
}

