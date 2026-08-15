using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsBloodType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? BloodTypeID { set; get; }
        public string BloodTypeSymbol { set; get; }

        public clsBloodType()
        {
            this.BloodTypeID = null;
            this.BloodTypeSymbol = "";

            Mode = enMode.AddNew;
        }

        private clsBloodType(int? BloodTypeID, string BloodTypeSymbol)
        {
            this.BloodTypeID = BloodTypeID;
            this.BloodTypeSymbol = BloodTypeSymbol;

            Mode = enMode.Update;
        }

        private bool _AddNewBloodType()
        {
            this.BloodTypeID = clsBloodTypesDataAccess.AddNewBloodType(this.BloodTypeSymbol);
            return (this.BloodTypeID != null);
        }

        private bool _UpdateBloodType()
        {
            return clsBloodTypesDataAccess.UpdateBloodType(this.BloodTypeID, this.BloodTypeSymbol) ?? false;
        }

        public static clsBloodType FindByID(int? BloodTypeID)
        {
            if (BloodTypeID == null) return null;

            string BloodTypeSymbol = "";

            bool? IsFound = clsBloodTypesDataAccess.GetBloodTypeInfoByID(BloodTypeID, ref BloodTypeSymbol);

            if (IsFound == true)
                return new clsBloodType(BloodTypeID, BloodTypeSymbol);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBloodType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBloodType();
            }
            return false;
        }

        public static DataTable GetAllBloodTypes()
        {
            return clsBloodTypesDataAccess.GetAllBloodTypes();
        }

        public static bool DeleteBloodType(int? BloodTypeID)
        {
            return clsBloodTypesDataAccess.DeleteBloodType(BloodTypeID);
        }

        public static bool IsBloodTypeExist(int? BloodTypeID)
        {
            return clsBloodTypesDataAccess.IsBloodTypeExist(BloodTypeID) ?? false;
        }
    }
}

