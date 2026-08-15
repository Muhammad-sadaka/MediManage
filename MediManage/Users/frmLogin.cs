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
    public partial class frmLogin : Form
    {

        bool GetFromRegister = false;
        public frmLogin()
        {
            InitializeComponent();
            Login_Load();
            //EventArgs e = new EventArgs();
            //airForm1.ParentForm.AcceptButton =
            //       btnLogin;
            //airForm1.ParentForm.CancelButton = airForm1.();

            // It is temp code
            clsGlobal.RememberUsernameAndPasswordInRegistry("", "");
        }

        private void Login_Load()
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentialFromRegistry(ref UserName, ref Password))
            {
                tbUserName.TextButton = UserName;
                tbPassword.TextButton = Password;
                chkRememberMe.Checked = true;
                tbUserName.ForeColor = Color.Black;
                tbPassword.ForeColor = Color.Black;
                tbPassword.Password = true;
                GetFromRegister = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {      
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsUser user = new clsUser();

            if (!GetFromRegister)
            {
                user = clsUser.FindByUsernameAndPassword(tbUserName.TextButton.Trim(), clsGlobal.ComputeHash(tbPassword.TextButton.Trim()));
            }
            else
                user = clsUser.FindByUsernameAndPassword(tbUserName.TextButton.Trim(), tbPassword.TextButton.Trim());



            // It is temp code
            user = clsUser.FindByUsernameAndPassword(tbUserName.TextButton.Trim(), clsGlobal.ComputeHash(tbPassword.TextButton.Trim()));


            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPasswordInRegistry(tbUserName.TextButton.Trim(), tbPassword.TextButton.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPasswordInRegistry("", "");
                }

                //incase the user is not active
                if (!user.IsActive.Value)
                {

                    tbUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                Form frm = new frmMainScreen(this);
                frm.ShowDialog();

            }
            else
            {
                tbUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbUserName_Enter(object sender, EventArgs e)
        {
            if (tbUserName.TextButton == "User name")
                tbUserName.textBox.Clear();
            tbUserName.ForeColor = Color.Black;
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassword.TextButton == "Password")
            {
                tbPassword.textBox.Clear();
                tbPassword.Password = true;
            }
            tbPassword.ForeColor = Color.Black;

            //GetFromRegister = true;
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassword.TextButton == "" || tbPassword.TextButton == null)
            {
                tbPassword.ForeColor = Color.Gray;
                tbPassword.TextButton = "Password";
                tbPassword.Password = false;
            }
        }

        private void tbUserName_Leave(object sender, EventArgs e)
        {
            if (tbUserName.TextButton == "" || tbPassword.TextButton == null)
            {
                tbUserName.ForeColor = Color.Gray;
                tbUserName.TextButton = "User name";
            }
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUserName.TextButton))
            {
                e.Cancel = true;
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "UserName should have a value");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.TextButton))
            {
                e.Cancel = true;
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "Password should have a value");
            }
        }

    }
}
