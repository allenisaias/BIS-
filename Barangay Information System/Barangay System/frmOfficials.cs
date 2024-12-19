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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Collections;

namespace Barangay_System
{
    public partial class frmOfficials : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        private int ID;
        // frmMaintenance f;

        private frmMaintenance _frmMaintenance;


        public frmOfficials(frmMaintenance frmMaintenancedash)
        {
            InitializeComponent();
            _frmMaintenance = frmMaintenancedash;
        }
        public frmOfficials(int id, string name)
        {
            InitializeComponent();
            ID = id;
            tbName.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmOfficials_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string NAME = tbName.Text;
           
            if (tbName.Text != "" && tbChairmanship.Text != "" && tbPosition.Text != "" && tbStatus.Text != "")
            {
                if (ID == 0) 
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        string query = "INSERT INTO Official ([Name], [Chairmanship], [Position], [TermStart], [TermEnd], [Status]) VALUES (@n, @c, @p, @ts, @te, @s)";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        cmd.Parameters.Add("@n", OleDbType.VarChar).Value = tbName.Text;
                        cmd.Parameters.Add("@c", OleDbType.VarChar).Value = tbChairmanship.Text;
                        cmd.Parameters.Add("@p", OleDbType.VarChar).Value = tbPosition.Text;
                        cmd.Parameters.Add("@ts", OleDbType.Date).Value = dtpTs.Value;
                        cmd.Parameters.Add("@te", OleDbType.Date).Value = dtpTe.Value;
                        cmd.Parameters.Add("@s", OleDbType.VarChar).Value = tbStatus.Text;

                        conn.Open();
                        cmd.ExecuteNonQuery();  
                        MessageBox.Show("Official added successfully.");
                        _frmMaintenance.LoadData();
                        this.Close(); // Close the form after saving
                    }
                }
                else // Update existing product
                {
                    DialogResult result = MessageBox.Show("Are you sure to Update official?.", "Update Official", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();
                            string query = "UPDATE official SET [NAME] = '" + tbName.Text + "', [Chairmanship] = '" + tbChairmanship.Text + "', [Position] = '" + tbPosition.Text + "', [TermStart] = '" + dtpTs.Text + "', [TermEnd] = '" + dtpTe.Text + "', [Status] = '" + tbStatus.Text + "' WHERE ID = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("?", tbID.Text);

                                command.ExecuteNonQuery();
                                MessageBox.Show("Official have been updated. Form will close");
                                this.Close();


                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("User Cancelled");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Please Complete Information.");
            }

            
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
        

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dgvOfficial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete official?\nCannot Undo.", "Delete Official", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM official WHERE ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", tbID.Text);
                        command.ExecuteNonQuery();
                        _frmMaintenance.LoadData();
                        this.Close();

                    }
                }
            }

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbChairmanship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvUser_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID, Name, Chairmanship,Position,TermStart,TermEnd,Status FROM official";
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    //dgvUser.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void tbID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure to Update official?.", "Update Official", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE official SET [NAME] = '" + tbName.Text + "', [Chairmanship] = '" + tbChairmanship.Text + "', [Position] = '" + tbPosition.Text + "', [TermStart] = '" + dtpTs.Text + "', [TermEnd] = '" + dtpTe.Text + "', [Status] = '" + tbStatus.Text + "' WHERE ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", tbID.Text);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Official have been updated. Form will close");
                        _frmMaintenance.LoadData();
                        this.Close();


                    }
                }

            }
            else
            {
                MessageBox.Show("User Cancelled");
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
