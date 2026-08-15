using MediManage_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediManage
{
    public partial class frmAddUpdatePerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        enMode _Mode = enMode.AddNew;

        clsPerson person = new clsPerson();

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            person.PersonID = PersonID;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResestDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void pbPersonImage_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            person.FirstName = tbFirstName.Text.Trim();
            person.SecondName = tbSecondName.Text.Trim();
            person.ThirdName = tbThirdName.Text.Trim();
            person.LastName = tbLastName.Text.Trim();
            person.NationalNo = tbNationalNo.Text.Trim();
            person.DateOfBirth = dateTimePicker1.Value;
            if (rbMale.Checked)
                person.Gender = "0";
            else
                person.Gender = "1";
            person.Phone = tbPhone.Text.Trim();
            person.Email = tbEmail.Text.Trim();
            person.Address = tbAddress.Text.Trim();
            person.BloodTypeID = cbBloodType.SelectedIndex + 1;
            person.MaritalStatusID = cbMaritalStatus.SelectedIndex + 1;
            person.CountryID = cbCountries.SelectedIndex + 1;

            //person.Image

            if (person.Save())
            {
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data Saved Successfully.");

               // PersonIDDataBack?.Invoke(this, _PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }
        }

        public void _FillComboBox(string s,DataTable dt,ComboBox comboBox)
        {
            foreach (DataRow row in dt.Rows)
            {
                comboBox.Items.Add(row[s]);
            }
        }

        private void _ResestDefualtValues()
        {
            //_FillCountriesInComboBox();
            DataTable dt = clsBloodType.GetAllBloodTypes();
            _FillComboBox("BloodTypeSymbol",dt,cbBloodType);
            dt = clsMaritalStatus.GetAllMaritalStatuses();
            _FillComboBox("MaritalStatusName", dt, cbMaritalStatus);
            dt = clsCountry.GetAllCountries(); 
            _FillComboBox("CountryName", dt, cbCountries);

            if (_Mode == enMode.AddNew)
            {
                person = new clsPerson();
                lblTitle.Text = "Add New Person                       ";
            }
            else
                lblTitle.Text = "Update Person                        ";


            //if (rBMale.Checked)
            //    pBPersonalImage.Image = Properties.Resources.male_v2;
            //else
            //    pBPersonalImage.Image = Properties.Resources.female_v2;

            //lbllinkRemove.Visible = (pBPersonalImage.ImageLocation != null);

            //_SelectDateTime();

            //cBCountries.SelectedIndex = cBCountries.FindString("Syria");

            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbThirdName.Text = "";
            tbLastName.Text = "";
            tbNationalNo.Text = "";
            tbPhone.Text = "";
            tbEmail.Text = "";
            tbAddress.Text = "";
            cbBloodType.SelectedIndex = 0;
            cbMaritalStatus.SelectedIndex = 0;
            cbCountries.SelectedIndex = 0;

            rbMale.Checked = true;
        }

        private void _LoadData()
        {
            person = clsPerson.FindByID(person.PersonID);

            if (person == null)
            {
                MessageBox.Show("This form will be closed because No _Person with ID = " + person.PersonID);
                this.Close();
                return;
            }

               tbFirstName.Text = person.FirstName;
               tbSecondName.Text = person.SecondName;
               tbThirdName.Text = person.ThirdName;
               tbLastName.Text = person.LastName;
               tbNationalNo.Text = person.NationalNo;
               dateTimePicker1.Value = person.DateOfBirth.Value;
               if (person.Gender == "0")
                   rbMale.Checked = true;
               else
                  rbFemale.Checked = true;
              tbPhone.Text =person.Phone;
              tbEmail.Text=person.Address;
              tbAddress.Text = person.Address;
              cbBloodType.SelectedIndex = person.BloodTypeID.Value -1;
              cbMaritalStatus.SelectedIndex = person.MaritalStatusID.Value - 1;
              cbCountries.SelectedIndex = person.CountryID.Value - 1;

            //person.Image

            //if (_Person.ImagePath != "" && _Person.ImagePath != null)
            //{
            //    pBPersonalImage.ImageLocation = _Person.ImagePath;
            //}
            //lbllinkRemove.Visible = (_Person.ImagePath != "");
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

    }
}
