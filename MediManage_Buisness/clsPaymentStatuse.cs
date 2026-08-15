using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPaymentStatus
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PaymentStatusID { set; get; }
        public string PaymentStatusName { set; get; }

        public clsPaymentStatus()
        {
            this.PaymentStatusID = null;
            this.PaymentStatusName = "";

            Mode = enMode.AddNew;
        }

        private clsPaymentStatus(int? PaymentStatusID, string PaymentStatusName)
        {
            this.PaymentStatusID = PaymentStatusID;
            this.PaymentStatusName = PaymentStatusName;

            Mode = enMode.Update;
        }

        private bool _AddNewPaymentStatus()
        {
            this.PaymentStatusID = clsPaymentStatusesDataAccess.AddNewPaymentStatus(this.PaymentStatusName);
            return (this.PaymentStatusID != null);
        }

        private bool _UpdatePaymentStatus()
        {
            return clsPaymentStatusesDataAccess.UpdatePaymentStatus(this.PaymentStatusID, this.PaymentStatusName) ?? false;
        }

        public static clsPaymentStatus FindByID(int? PaymentStatusID)
        {
            if (PaymentStatusID == null) return null;

            string PaymentStatusName = "";

            bool? IsFound = clsPaymentStatusesDataAccess.GetPaymentStatusInfoByID(PaymentStatusID, ref PaymentStatusName);

            if (IsFound == true)
                return new clsPaymentStatus(PaymentStatusID, PaymentStatusName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPaymentStatus())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePaymentStatus();
            }
            return false;
        }

        public static DataTable GetAllPaymentStatuses()
        {
            return clsPaymentStatusesDataAccess.GetAllPaymentStatuses();
        }

        public static bool DeletePaymentStatus(int? PaymentStatusID)
        {
            return clsPaymentStatusesDataAccess.DeletePaymentStatus(PaymentStatusID);
        }

        public static bool IsPaymentStatusExist(int? PaymentStatusID)
        {
            return clsPaymentStatusesDataAccess.IsPaymentStatusExist(PaymentStatusID) ?? false;
        }
    }
}

