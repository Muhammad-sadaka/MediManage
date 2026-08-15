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
    public partial class frmPeopleList : Form
    {
        public frmPeopleList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPeopleList_Load(object sender, EventArgs e)
        {
            DGVPeopleList.DataSource = clsPerson.GetAllPeople();
            lblTotalRecords.Text = "Total: " + DGVPeopleList.RowCount + " records";
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();  
            frm.ShowDialog();
        }

        private void tbNationalNo_Enter(object sender, EventArgs e)
        {
            if (tbNationalNo.Text == "National No")
                tbNationalNo.Clear();
            tbNationalNo.ForeColor = Color.Black;
        }

        private void tbNationalNo_Leave(object sender, EventArgs e)
        {
            if (tbNationalNo.Text == "" || tbNationalNo.Text == null)
            {
                tbNationalNo.ForeColor = Color.Gray;
                tbNationalNo.Text = "National No";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(tbNationalNo.Text != null)
            {
                //clsPerson.FindByNationNo(tbNationalNo.Text);
            }
        }
    }
}
