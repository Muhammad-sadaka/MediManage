using System;
using System.Data;
using MediManage_DataAccess;

namespace MediManage_Buisness
{
    public class clsBill
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? Bill_ID { set; get; }
        public int? PatientID { set; get; }
        public int? CreatedByUserID { set; get; }
        public DateTime? BillDate { set; get; }
        public decimal? AmountOfPaid { set; get; }
        public decimal? AmountOfRemaining { set; get; }
        public decimal? TotalAmount { set; get; }
        public int? PaymentStatusID { set; get; }
        public int? PaymentMethodID { set; get; }

        public clsBill()
        {
            this.Bill_ID = null;
            this.PatientID = null;
            this.CreatedByUserID = null;
            this.BillDate = null;
            this.AmountOfPaid = null;
            this.AmountOfRemaining = null;
            this.TotalAmount = null;
            this.PaymentStatusID = null;
            this.PaymentMethodID = null;

            Mode = enMode.AddNew;
        }

        private clsBill(int? Bill_ID, int? PatientID, int? CreatedByUserID, DateTime? BillDate, decimal? AmountOfPaid, decimal? AmountOfRemaining, decimal? TotalAmount, int? PaymentStatusID, int? PaymentMethodID)
        {
            this.Bill_ID = Bill_ID;
            this.PatientID = PatientID;
            this.CreatedByUserID = CreatedByUserID;
            this.BillDate = BillDate;
            this.AmountOfPaid = AmountOfPaid;
            this.AmountOfRemaining = AmountOfRemaining;
            this.TotalAmount = TotalAmount;
            this.PaymentStatusID = PaymentStatusID;
            this.PaymentMethodID = PaymentMethodID;

            Mode = enMode.Update;
        }

        private bool _AddNewBill()
        {
            this.Bill_ID = clsBillsDataAccess.AddNewBill(this.PatientID, this.CreatedByUserID, this.BillDate, this.AmountOfPaid, this.AmountOfRemaining, this.TotalAmount, this.PaymentStatusID, this.PaymentMethodID);
            return (this.Bill_ID != null);
        }

        private bool _UpdateBill()
        {
            return clsBillsDataAccess.UpdateBill(this.Bill_ID, this.PatientID, this.CreatedByUserID, this.BillDate, this.AmountOfPaid, this.AmountOfRemaining, this.TotalAmount, this.PaymentStatusID, this.PaymentMethodID) ?? false;
        }

        public static clsBill FindByID(int? Bill_ID)
        {
            if (Bill_ID == null) return null;

            int? PatientID = null;
            int? CreatedByUserID = null;
            DateTime? BillDate = null;
            decimal? AmountOfPaid = null;
            decimal? AmountOfRemaining = null;
            decimal? TotalAmount = null;
            int? PaymentStatusID = null;
            int? PaymentMethodID = null;

            bool? IsFound = clsBillsDataAccess.GetBillInfoByID(Bill_ID, ref PatientID, ref CreatedByUserID, ref BillDate, ref AmountOfPaid, ref AmountOfRemaining, ref TotalAmount, ref PaymentStatusID, ref PaymentMethodID);

            if (IsFound == true)
                return new clsBill(Bill_ID, PatientID, CreatedByUserID, BillDate, AmountOfPaid, AmountOfRemaining, TotalAmount, PaymentStatusID, PaymentMethodID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBill())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBill();
            }
            return false;
        }

        public static DataTable GetAllBills()
        {
            return clsBillsDataAccess.GetAllBills();
        }

        public static bool DeleteBill(int? Bill_ID)
        {
            return clsBillsDataAccess.DeleteBill(Bill_ID);
        }

        public static bool IsBillExist(int? Bill_ID)
        {
            return clsBillsDataAccess.IsBillExist(Bill_ID) ?? false;
        }
    }
}
