using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? UserID { set; get; }
        public int? PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public byte? Permissions { set; get; }
        public bool? IsActive { set; get; }
        public clsPerson Person { set; get; }


        public clsUser()
        {
            this.UserID = null;
            this.PersonID = null;
            this.UserName = "";
            this.Password = null; // Allows Null
            this.Permissions = null;
            this.IsActive = null;
            this.Person = null;

            Mode = enMode.AddNew;
        }

        private clsUser(int? UserID, int? PersonID, string UserName, string Password, byte? Permissions, bool? IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Permissions = Permissions;
            this.IsActive = IsActive;
            this.Person = clsPerson.FindByID(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.Permissions, this.IsActive);
            return (this.UserID != null);
        }

        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.Permissions, this.IsActive) ?? false;
        }

        public static clsUser FindByID(int? UserID)
        {
            if (UserID == null) return null;

            int? PersonID = null;
            string UserName = "";
            string Password = null;
            byte? Permissions = null;
            bool? IsActive = null;

            bool? IsFound = clsUsersDataAccess.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref Permissions, ref IsActive);

            if (IsFound == true)
                return new clsUser(UserID, PersonID, UserName, Password, Permissions, IsActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }

        public static bool DeleteUser(int? UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID);
        }

        public static bool IsUserExist(int? UserID)
        {
            return clsUsersDataAccess.IsUserExist(UserID) ?? false;
        }

        public static clsUser FindByUsernameAndPassword(string UserName,string Password)
        {
            int? UserID = null;
            int? PersonID = null;
            byte? Permissions = null;
            bool? IsActive = null;

            bool? IsFound = clsUsersDataAccess.FindByUsernameAndPassword(ref UserID, ref PersonID,  UserName,  Password, ref Permissions, ref IsActive);

            if (IsFound == true)
                return new clsUser(UserID, PersonID, UserName, Password, Permissions, IsActive);
            else
                return null;
        }
    }
}

