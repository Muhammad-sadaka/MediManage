using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediManage_Buisness;

namespace MediManage
{
    public partial class frmMainScreen : Form
    {
        frmLogin _frmLogin;

        public frmMainScreen(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
            LoadHomePage();
        }

        void LoadHomePage()
        {
            lblWelcome.Text = "Welcome, Dr. " + clsGlobal.CurrentUser.Person.FirstName;
            lblTodayDate.Text = "Today's date: " + DateTime.Today.ToLongDateString();
            lblTotalPatients.Text = "Total Patients = " + clsPatient.GetTotalPatientsNumber();
            lblTodayRevenue.Text = "Today Revenue = $";
            lblTodayAppointments.Text = "Today Appointments = " + DGVTodayAppointments.Rows.Count.ToString();
            DGVTodayAppointments.DataSource = clsAppointment.GetTodayAppointments();

            if (DGVTodayAppointments.Rows.Count > 0)
            {
                DGVTodayAppointments.Columns[0].Width = 140;

                DGVTodayAppointments.Columns[1].Width = 140;

                DGVTodayAppointments.Columns[2].Width = 140;

                DGVTodayAppointments.Columns[3].Width = 140;
            }

        }

        private void hopeTabPage1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 10)
            {
                e.Cancel = true;
                clsGlobal.CurrentUser = null;
                _frmLogin.Show();
                this.Close();
            }
            else if (e.TabPageIndex == 0)
            {
                LoadHomePage();
            }
        }

        private void btnAddNewPerson_MouseHover(object sender, EventArgs e)
        {
            btnAddNewPerson.ForeColor = Color.Red;
        }

        private void btnAddNewPerson_MouseLeave(object sender, EventArgs e)
        {
            btnAddNewPerson.ForeColor = Color.Black;
        }

        private void btnPeopleList_MouseHover(object sender, EventArgs e)
        {
            btnPeopleList.ForeColor = Color.Red;
        }

        private void btnPeopleList_MouseLeave(object sender, EventArgs e)
        {
            btnPeopleList.ForeColor = Color.Black;
        }

        private void btnPeopleList_Click(object sender, EventArgs e)
        {
            frmPeopleList frm = new frmPeopleList();
            frm.ShowDialog();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }
    }
}
