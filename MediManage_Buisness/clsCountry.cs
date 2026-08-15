using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediManage_DataAccess;

namespace MediManage_Buisness
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? CountryID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            this.CountryID = null;
            this.CountryName = "";
            Mode = enMode.AddNew;
        }

        private clsCountry(int? CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        private bool _AddNewCountry()
        {
            this.CountryID = clsCountryData.AddNewCountry(this.CountryName);
            return (this.CountryID != null);
        }

        private bool _UpdateCountry()
        {
            return clsCountryData.UpdateCountry(this.CountryID, this.CountryName) ?? false;
        }

        public static clsCountry FindByID(int? CountryID)
        {
            if (CountryID == null) return null;
            string CountryName = "";
            bool? IsFound = clsCountryData.GetCountryInfoByID(CountryID, ref CountryName);

            if (IsFound == true)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static bool DeleteCountry(int? CountryID)
        {
            return clsCountryData.DeleteCountry(CountryID);
        }

        public static bool IsCountryExist(int? CountryID)
        {
            return clsCountryData.IsCountryExist(CountryID) ?? false;
        }
    }
}
