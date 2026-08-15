using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsServiceType
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? ServiceTypeID { set; get; }
        public string ServicTypeName { set; get; }

        public clsServiceType()
        {
            this.ServiceTypeID = null;
            this.ServicTypeName = "";

            Mode = enMode.AddNew;
        }

        private clsServiceType(int? ServiceTypeID, string ServicTypeName)
        {
            this.ServiceTypeID = ServiceTypeID;
            this.ServicTypeName = ServicTypeName;

            Mode = enMode.Update;
        }

        private bool _AddNewServiceType()
        {
            this.ServiceTypeID = clsServiceTypesDataAccess.AddNewServiceType(this.ServicTypeName);
            return (this.ServiceTypeID != null);
        }

        private bool _UpdateServiceType()
        {
            return clsServiceTypesDataAccess.UpdateServiceType(this.ServiceTypeID, this.ServicTypeName) ?? false;
        }

        public static clsServiceType FindByID(int? ServiceTypeID)
        {
            if (ServiceTypeID == null) return null;

            string ServicTypeName = "";

            bool? IsFound = clsServiceTypesDataAccess.GetServiceTypeInfoByID(ServiceTypeID, ref ServicTypeName);

            if (IsFound == true)
                return new clsServiceType(ServiceTypeID, ServicTypeName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewServiceType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateServiceType();
            }
            return false;
        }

        public static DataTable GetAllServiceTypes()
        {
            return clsServiceTypesDataAccess.GetAllServiceTypes();
        }

        public static bool DeleteServiceType(int? ServiceTypeID)
        {
            return clsServiceTypesDataAccess.DeleteServiceType(ServiceTypeID);
        }

        public static bool IsServiceTypeExist(int? ServiceTypeID)
        {
            return clsServiceTypesDataAccess.IsServiceTypeExist(ServiceTypeID) ?? false;
        }
    }
}

