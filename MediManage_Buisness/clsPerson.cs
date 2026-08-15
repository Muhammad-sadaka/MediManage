using System;
using System.Data;
using MediManage_DataAccess;


namespace MediManage_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int? PersonID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string NationalNo { set; get; }
        public string Phone { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string Gender { set; get; }
        public string Image { set; get; }
        public string Address { set; get; }
        public string Email { set; get; }
        public int? BloodTypeID { set; get; }
        public int? MaritalStatusID { set; get; }
        public int? CountryID { set; get; }

        public clsPerson()
        {
            this.PersonID = null;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.Phone = "";
            this.DateOfBirth = null;
            this.Gender = "";
            this.Image = null; // Nullable
            this.Address = "";
            this.Email = null; // Nullable
            this.BloodTypeID = null;
            this.MaritalStatusID = null;
            this.CountryID = null;

            Mode = enMode.AddNew;
        }

        private clsPerson(int? PersonID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, string Phone, DateTime? DateOfBirth, string Gender, string Image, string Address, string Email, int? BloodTypeID, int? MaritalStatusID,int? CountryID)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.Phone = Phone;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Image = Image;
            this.Address = Address;
            this.Email = Email;
            this.BloodTypeID = BloodTypeID;
            this.MaritalStatusID = MaritalStatusID;
            this.CountryID = CountryID;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleDataAccess.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo, this.Phone, this.DateOfBirth, this.Gender, this.Image, this.Address, this.Email, this.BloodTypeID, this.MaritalStatusID,this.CountryID);
            return (this.PersonID != null);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo, this.Phone, this.DateOfBirth, this.Gender, this.Image, this.Address, this.Email, this.BloodTypeID, this.MaritalStatusID,this.CountryID) ?? false;
        }

        public static clsPerson FindByID(int? PersonID)
        {
            if (PersonID == null) return null;

            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            string NationalNo = "";
            string Phone = "";
            DateTime? DateOfBirth = null;
            string Gender = "";
            string Image = null;
            string Address = "";
            string Email = null;
            int? BloodTypeID = null;
            int? MaritalStatusID = null;
            int? CountryID = null;

            bool? IsFound = clsPeopleDataAccess.GetPersonInfoByID(PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref NationalNo, ref Phone, ref DateOfBirth, ref Gender, ref Image, ref Address, ref Email, ref BloodTypeID, ref MaritalStatusID,ref CountryID);

            if (IsFound == true)
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, Phone, DateOfBirth, Gender, Image, Address, Email, BloodTypeID, MaritalStatusID, CountryID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.GetAllPeople();
        }

        public static bool DeletePerson(int? PersonID)
        {
            return clsPeopleDataAccess.DeletePerson(PersonID);
        }

        public static bool IsPersonExist(int? PersonID)
        {
            return clsPeopleDataAccess.IsPersonExist(PersonID) ?? false;
        }
    }
}

