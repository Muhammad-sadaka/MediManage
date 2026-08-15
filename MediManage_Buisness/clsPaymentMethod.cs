using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPaymentMethod
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PaymentMethodID { set; get; }
        public string PaymentMethodName { set; get; }

        public clsPaymentMethod()
        {
            this.PaymentMethodID = null;
            this.PaymentMethodName = "";

            Mode = enMode.AddNew;
        }

        private clsPaymentMethod(int? PaymentMethodID, string PaymentMethodName)
        {
            this.PaymentMethodID = PaymentMethodID;
            this.PaymentMethodName = PaymentMethodName;

            Mode = enMode.Update;
        }

        private bool _AddNewPaymentMethod()
        {
            this.PaymentMethodID = clsPaymentMethodsDataAccess.AddNewPaymentMethod(this.PaymentMethodName);
            return (this.PaymentMethodID != null);
        }

        private bool _UpdatePaymentMethod()
        {
            return clsPaymentMethodsDataAccess.UpdatePaymentMethod(this.PaymentMethodID, this.PaymentMethodName) ?? false;
        }

        public static clsPaymentMethod FindByID(int? PaymentMethodID)
        {
            if (PaymentMethodID == null) return null;

            string PaymentMethodName = "";

            bool? IsFound = clsPaymentMethodsDataAccess.GetPaymentMethodInfoByID(PaymentMethodID, ref PaymentMethodName);

            if (IsFound == true)
                return new clsPaymentMethod(PaymentMethodID, PaymentMethodName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPaymentMethod())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePaymentMethod();
            }
            return false;
        }

        public static DataTable GetAllPaymentMethods()
        {
            return clsPaymentMethodsDataAccess.GetAllPaymentMethods();
        }

        public static bool DeletePaymentMethod(int? PaymentMethodID)
        {
            return clsPaymentMethodsDataAccess.DeletePaymentMethod(PaymentMethodID);
        }

        public static bool IsPaymentMethodExist(int? PaymentMethodID)
        {
            return clsPaymentMethodsDataAccess.IsPaymentMethodExist(PaymentMethodID) ?? false;
        }
    }
}

