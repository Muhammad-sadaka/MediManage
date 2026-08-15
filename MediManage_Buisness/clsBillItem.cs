using System;
using System.Data;
using MediManage_DataAccess;

namespace MediManage_Buisness
{
    public class clsBillItem
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? BillItemID { set; get; }
        public int? Bill_ID { set; get; }
        public int? ServiceTypeID { set; get; }
        public string Description { set; get; }
        public decimal? Price { set; get; }
        public int? Amount { set; get; }
        public int? Total { set; get; }

        public clsBillItem()
        {
            this.BillItemID = null;
            this.Bill_ID = null;
            this.ServiceTypeID = null;
            this.Description = "";
            this.Price = null;
            this.Amount = null;
            this.Total = null;

            Mode = enMode.AddNew;
        }

        private clsBillItem(int? BillItemID, int? Bill_ID, int? ServiceTypeID, string Description, decimal? Price, int? Amount, int? Total)
        {
            this.BillItemID = BillItemID;
            this.Bill_ID = Bill_ID;
            this.ServiceTypeID = ServiceTypeID;
            this.Description = Description;
            this.Price = Price;
            this.Amount = Amount;
            this.Total = Total;

            Mode = enMode.Update;
        }

        private bool _AddNewBillItem()
        {
            this.BillItemID = clsBillItemsDataAccess.AddNewBillItem(this.Bill_ID, this.ServiceTypeID, this.Description, this.Price, this.Amount, this.Total);
            return (this.BillItemID != null);
        }

        private bool _UpdateBillItem()
        {
            return clsBillItemsDataAccess.UpdateBillItem(this.BillItemID, this.Bill_ID, this.ServiceTypeID, this.Description, this.Price, this.Amount, this.Total) ?? false;
        }

        public static clsBillItem FindByID(int? BillItemID)
        {
            if (BillItemID == null) return null;

            int? Bill_ID = null;
            int? ServiceTypeID = null;
            string Description = "";
            decimal? Price = null;
            int? Amount = null;
            int? Total = null;

            bool? IsFound = clsBillItemsDataAccess.GetBillItemInfoByID(BillItemID, ref Bill_ID, ref ServiceTypeID, ref Description, ref Price, ref Amount, ref Total);

            if (IsFound == true)
                return new clsBillItem(BillItemID, Bill_ID, ServiceTypeID, Description, Price, Amount, Total);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBillItem())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateBillItem();
            }
            return false;
        }

        public static DataTable GetAllBillItems()
        {
            return clsBillItemsDataAccess.GetAllBillItems();
        }

        public static bool DeleteBillItem(int? BillItemID)
        {
            return clsBillItemsDataAccess.DeleteBillItem(BillItemID);
        }

        public static bool IsBillItemExist(int? BillItemID)
        {
            return clsBillItemsDataAccess.IsBillItemExist(BillItemID) ?? false;
        }
    }
}

