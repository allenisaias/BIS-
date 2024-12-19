using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;



namespace Barangay_System
{
    public partial class Form2 : Form
    {
        

        public Form2()
        {
            InitializeComponent();
           

        }
        

        private void Form2_Load(object sender, EventArgs e)
        {
            


        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            int y = Screen.PrimaryScreen.Bounds.Height;
            int x = Screen.PrimaryScreen.Bounds.Width;
            this.Height = y - 40;
            this.Width = x;
            this.Left = 0;
            this.Top = 0;
        }

        private void r(object sender, EventArgs e)
        {

        }

        private void panel1_Resize(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
          
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            frmMaintenance f = new frmMaintenance();
            f.TopLevel = false;
            panel2.Controls.Add(f);
            
            f.BringToFront();
            f.Show();
            f.LoadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            dashboard f = new dashboard();
            f.TopLevel = false;
            panel2.Controls.Add(f);

            f.BringToFront();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboard f = new dashboard();
            f.TopLevel = false;
            panel2.Controls.Add(f);

            f.BringToFront();
            f.Show();
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            resident f = new resident();
            f.TopLevel = false;
            panel2.Controls.Add(f);

            f.BringToFront();
            f.Show();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide current form
            LoginForm loginForm = new LoginForm(); // Create new instance of LoginForm
            loginForm.Show(); // Show login form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            report f = new report();
            f.TopLevel = false;
            panel2.Controls.Add(f);

            f.BringToFront();
            f.Show();
            
        }
    }
}
