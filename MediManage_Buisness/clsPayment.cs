using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPayment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PaymentID { set; get; }
        public int? Bill_ID { set; get; }
        public DateTime? PaymentDate { set; get; }
        public int? CreatedByUserID { set; get; }
        public decimal? Amount { set; get; }

        public clsPayment()
        {
            this.PaymentID = null;
            this.Bill_ID = null;
            this.PaymentDate = null;
            this.CreatedByUserID = null;
            this.Amount = null;

            Mode = enMode.AddNew;
        }

        private clsPayment(int? PaymentID, int? Bill_ID, DateTime? PaymentDate, int? CreatedByUserID, decimal? Amount)
        {
            this.PaymentID = PaymentID;
            this.Bill_ID = Bill_ID;
            this.PaymentDate = PaymentDate;
            this.CreatedByUserID = CreatedByUserID;
            this.Amount = Amount;

            Mode = enMode.Update;
        }

        private bool _AddNewPayment()
        {
            this.PaymentID = clsPaymentsDataAccess.AddNewPayment(this.Bill_ID, this.PaymentDate, this.CreatedByUserID, this.Amount);
            return (this.PaymentID != null);
        }

        private bool _UpdatePayment()
        {
            return clsPaymentsDataAccess.UpdatePayment(this.PaymentID, this.Bill_ID, this.PaymentDate, this.CreatedByUserID, this.Amount) ?? false;
        }

        public static clsPayment FindByID(int? PaymentID)
        {
            if (PaymentID == null) return null;

            int? Bill_ID = null;
            DateTime? PaymentDate = null;
            int? CreatedByUserID = null;
            decimal? Amount = null;

            bool? IsFound = clsPaymentsDataAccess.GetPaymentInfoByID(PaymentID, ref Bill_ID, ref PaymentDate, ref CreatedByUserID, ref Amount);

            if (IsFound == true)
                return new clsPayment(PaymentID, Bill_ID, PaymentDate, CreatedByUserID, Amount);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPayment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePayment();
            }
            return false;
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentsDataAccess.GetAllPayments();
        }

        public static bool DeletePayment(int? PaymentID)
        {
            return clsPaymentsDataAccess.DeletePayment(PaymentID);
        }

        public static bool IsPaymentExist(int? PaymentID)
        {
            return clsPaymentsDataAccess.IsPaymentExist(PaymentID) ?? false;
        }
    }
}

