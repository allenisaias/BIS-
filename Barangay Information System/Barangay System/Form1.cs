using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_System
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        int count, attempt;

        void disable()
        {
            if (attempt == 3)
            {
                MessageBox.Show("You've have reached the maximum 3 attempts! \nPlease try again.", "Attempts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                attempt = 0;
                count = 30;

                timerLogin.Enabled = true;
                btnLogin.Enabled = false;
                tbUsername.Enabled = false;
                tbPassword.Enabled = false;
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            attempt = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text ==  "admin" && tbPassword.Text ==  "admin")
            {
                MessageBox.Show("Welcome!", "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form2 frm = new Form2();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect. \nPlease try again!","Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbUsername.Clear();
                tbPassword.Clear();
                attempt = attempt + 1;
                disable();
            }
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                timerLogin.Enabled = false;
                btnLogin.Enabled = true;
                tbUsername.Enabled=true;
                tbPassword.Enabled=true;
                btnLogin.Text = "Login";
                tbUsername.Focus();
            }
            else
            {
                btnLogin.Text = "Log in" + count;
                count = count - 1;
            }
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tbUsername_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
