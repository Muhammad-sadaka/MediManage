using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? AppointmentID { set; get; }
        public int? PatientID { set; get; }
        public int? DoctorID { set; get; }
        public int? CreatedByUserID { set; get; }
        public DateTime? BookingDate { set; get; }
        public DateTime? AppointmentDate { set; get; }
        public int? AppointmentCaseID { set; get; }
        public byte? Duration { set; get; }
        public string Reason { set; get; }
        public string Notes { set; get; }

        public clsAppointment()
        {
            this.AppointmentID = null;
            this.PatientID = null;
            this.DoctorID = null;
            this.CreatedByUserID = null;
            this.BookingDate = null;
            this.AppointmentDate = null;
            this.AppointmentCaseID = null;
            this.Duration = null;
            this.Reason = "";
            this.Notes = null;

            Mode = enMode.AddNew;
        }

        private clsAppointment(int? AppointmentID, int? PatientID, int? DoctorID, int? CreatedByUserID, DateTime? BookingDate, DateTime? AppointmentDate, int? AppointmentCaseID, byte? Duration, string Reason, string Notes)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.CreatedByUserID = CreatedByUserID;
            this.BookingDate = BookingDate;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentCaseID = AppointmentCaseID;
            this.Duration = Duration;
            this.Reason = Reason;
            this.Notes = Notes;

            Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            this.AppointmentID = clsAppointmentsDataAccess.AddNewAppointment(this.PatientID, this.DoctorID, this.CreatedByUserID, this.BookingDate, this.AppointmentDate, this.AppointmentCaseID, this.Duration, this.Reason, this.Notes);
            return (this.AppointmentID != null);
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentsDataAccess.UpdateAppointment(this.AppointmentID, this.PatientID, this.DoctorID, this.CreatedByUserID, this.BookingDate, this.AppointmentDate, this.AppointmentCaseID, this.Duration, this.Reason, this.Notes) ?? false;
        }

        public static clsAppointment FindByID(int? AppointmentID)
        {
            if (AppointmentID == null) return null;

            int? PatientID = null;
            int? DoctorID = null;
            int? CreatedByUserID = null;
            DateTime? BookingDate = null;
            DateTime? AppointmentDate = null;
            int? AppointmentCaseID = null;
            byte? Duration = null;
            string Reason = "";
            string Notes = null;

            bool? IsFound = clsAppointmentsDataAccess.GetAppointmentInfoByID(AppointmentID, ref PatientID, ref DoctorID, ref CreatedByUserID, ref BookingDate, ref AppointmentDate, ref AppointmentCaseID, ref Duration, ref Reason, ref Notes);

            if (IsFound == true)
                return new clsAppointment(AppointmentID, PatientID, DoctorID, CreatedByUserID, BookingDate, AppointmentDate, AppointmentCaseID, Duration, Reason, Notes);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAppointment();
            }
            return false;
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentsDataAccess.GetAllAppointments();
        }

        public static bool DeleteAppointment(int? AppointmentID)
        {
            return clsAppointmentsDataAccess.DeleteAppointment(AppointmentID);
        }

        public static bool IsAppointmentExist(int? AppointmentID)
        {
            return clsAppointmentsDataAccess.IsAppointmentExist(AppointmentID) ?? false;
        }

        public static DataTable GetTodayAppointments()
        {
            return clsAppointmentsDataAccess.GetTodayAppointments();
        }
    }
}

