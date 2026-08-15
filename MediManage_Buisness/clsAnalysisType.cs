using System;
using System.Data;
using MediManage_DataAccess;

namespace MediManage_Buisness
{
    public class clsAnalysisType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? AnalysisTypeID { set; get; }
        public string AnalysisTypeName { set; get; }
        public decimal? Price { set; get; }

        public clsAnalysisType()
        {
            this.AnalysisTypeID = null;
            this.AnalysisTypeName = "";
            this.Price = null;

            Mode = enMode.AddNew;
        }

        private clsAnalysisType(int? AnalysisTypeID, string AnalysisTypeName, decimal? Price)
        {
            this.AnalysisTypeID = AnalysisTypeID;
            this.AnalysisTypeName = AnalysisTypeName;
            this.Price = Price;

            Mode = enMode.Update;
        }

        private bool _AddNewAnalysisType()
        {
            this.AnalysisTypeID = clsAnalysisTypeData.AddNewAnalysisType(this.AnalysisTypeName, this.Price);
            return (this.AnalysisTypeID != null);
        }

        private bool _UpdateAnalysisType()
        {
            return clsAnalysisTypeData.UpdateAnalysisType(this.AnalysisTypeID, this.AnalysisTypeName, this.Price) ?? false;
        }

        public static clsAnalysisType FindByID(int? AnalysisTypeID)
        {
            if (AnalysisTypeID == null) return null;

            string AnalysisTypeName = "";
            decimal? Price = null;

            bool? IsFound = clsAnalysisTypeData.GetAnalysisTypeInfoByID(AnalysisTypeID, ref AnalysisTypeName, ref Price);

            if (IsFound == true)
                return new clsAnalysisType(AnalysisTypeID, AnalysisTypeName, Price);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAnalysisType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAnalysisType();
            }
            return false;
        }

        public static DataTable GetAllAnalysisTypes()
        {
            return clsAnalysisTypeData.GetAllAnalysisTypes();
        }

        public static bool DeleteAnalysisType(int? AnalysisTypeID)
        {
            return clsAnalysisTypeData.DeleteAnalysisType(AnalysisTypeID);
        }

        public static bool IsAnalysisTypeExist(int? AnalysisTypeID)
        {
            return clsAnalysisTypeData.IsAnalysisTypeExist(AnalysisTypeID) ?? false;
        }
    }
}

