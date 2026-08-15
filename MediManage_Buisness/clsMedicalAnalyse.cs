using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsMedicalAnalysis
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? MedicalAnalysisID { set; get; }
        public string Result { set; get; }
        public DateTime? OrderDate { set; get; }
        public DateTime? ResultDate { set; get; }
        public int? AnalysisStatusID { set; get; }
        public int? AnalysisTypeID { set; get; }
        public int? DetectionID { set; get; }

        public clsMedicalAnalysis()
        {
            this.MedicalAnalysisID = null;
            this.Result = "";
            this.OrderDate = null;
            this.ResultDate = null;
            this.AnalysisStatusID = null;
            this.AnalysisTypeID = null;
            this.DetectionID = null;

            Mode = enMode.AddNew;
        }

        private clsMedicalAnalysis(int? MedicalAnalysisID, string Result, DateTime? OrderDate, DateTime? ResultDate, int? AnalysisStatusID, int? AnalysisTypeID, int? DetectionID)
        {
            this.MedicalAnalysisID = MedicalAnalysisID;
            this.Result = Result;
            this.OrderDate = OrderDate;
            this.ResultDate = ResultDate;
            this.AnalysisStatusID = AnalysisStatusID;
            this.AnalysisTypeID = AnalysisTypeID;
            this.DetectionID = DetectionID;

            Mode = enMode.Update;
        }

        private bool _AddNewMedicalAnalysis()
        {
            this.MedicalAnalysisID = clsMedicalAnalysesDataAccess.AddNewMedicalAnalysis(this.Result, this.OrderDate, this.ResultDate, this.AnalysisStatusID, this.AnalysisTypeID, this.DetectionID);
            return (this.MedicalAnalysisID != null);
        }

        private bool _UpdateMedicalAnalysis()
        {
            return clsMedicalAnalysesDataAccess.UpdateMedicalAnalysis(this.MedicalAnalysisID, this.Result, this.OrderDate, this.ResultDate, this.AnalysisStatusID, this.AnalysisTypeID, this.DetectionID) ?? false;
        }

        public static clsMedicalAnalysis FindByID(int? MedicalAnalysisID)
        {
            if (MedicalAnalysisID == null) return null;

            string Result = "";
            DateTime? OrderDate = null;
            DateTime? ResultDate = null;
            int? AnalysisStatusID = null;
            int? AnalysisTypeID = null;
            int? DetectionID = null;

            bool? IsFound = clsMedicalAnalysesDataAccess.GetMedicalAnalysisInfoByID(MedicalAnalysisID, ref Result, ref OrderDate, ref ResultDate, ref AnalysisStatusID, ref AnalysisTypeID, ref DetectionID);

            if (IsFound == true)
                return new clsMedicalAnalysis(MedicalAnalysisID, Result, OrderDate, ResultDate, AnalysisStatusID, AnalysisTypeID, DetectionID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMedicalAnalysis())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateMedicalAnalysis();
            }
            return false;
        }

        public static DataTable GetAllMedicalAnalyses()
        {
            return clsMedicalAnalysesDataAccess.GetAllMedicalAnalyses();
        }

        public static bool DeleteMedicalAnalysis(int? MedicalAnalysisID)
        {
            return clsMedicalAnalysesDataAccess.DeleteMedicalAnalysis(MedicalAnalysisID);
        }

        public static bool IsMedicalAnalysisExist(int? MedicalAnalysisID)
        {
            return clsMedicalAnalysesDataAccess.IsMedicalAnalysisExist(MedicalAnalysisID) ?? false;
        }
    }
}



