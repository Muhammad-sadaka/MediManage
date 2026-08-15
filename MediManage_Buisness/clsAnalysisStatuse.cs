using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsAnalysisStatus
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? AnalysisStatusID { set; get; }
        public string AnalysisStatusName { set; get; }

        public clsAnalysisStatus()
        {
            this.AnalysisStatusID = null;
            this.AnalysisStatusName = "";

            Mode = enMode.AddNew;
        }

        private clsAnalysisStatus(int? AnalysisStatusID, string AnalysisStatusName)
        {
            this.AnalysisStatusID = AnalysisStatusID;
            this.AnalysisStatusName = AnalysisStatusName;

            Mode = enMode.Update;
        }

        private bool _AddNewAnalysisStatus()
        {
            this.AnalysisStatusID = clsAnalysisStatuseData.AddNewAnalysisStatus(this.AnalysisStatusName);
            return (this.AnalysisStatusID != null);
        }

        private bool _UpdateAnalysisStatus()
        {
            return clsAnalysisStatuseData.UpdateAnalysisStatus(this.AnalysisStatusID, this.AnalysisStatusName) ?? false;
        }

        public static clsAnalysisStatus FindByID(int? AnalysisStatusID)
        {
            if (AnalysisStatusID == null) return null;

            string AnalysisStatusName = "";

            bool? IsFound = clsAnalysisStatuseData.GetAnalysisStatusInfoByID(AnalysisStatusID, ref AnalysisStatusName);

            if (IsFound == true)
                return new clsAnalysisStatus(AnalysisStatusID, AnalysisStatusName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAnalysisStatus())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAnalysisStatus();
            }
            return false;
        }

        public static DataTable GetAllAnalysisStatuses()
        {
            return clsAnalysisStatuseData.GetAllAnalysisStatuses();
        }

        public static bool DeleteAnalysisStatus(int? AnalysisStatusID)
        {
            return clsAnalysisStatuseData.DeleteAnalysisStatus(AnalysisStatusID);
        }

        public static bool IsAnalysisStatusExist(int? AnalysisStatusID)
        {
            return clsAnalysisStatuseData.IsAnalysisStatusExist(AnalysisStatusID) ?? false;
        }
    }
}
