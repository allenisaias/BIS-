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
    public partial class frmMaintenance : Form
    {

       

        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";

        public frmMaintenance()
        {
            
            InitializeComponent();
        }

           

        public void LoadData()
        {
            string query = "SELECT * FROM official";  // Replace with your actual table name

            //string query = "SELECT ID, Name, etc. FROM official";  // pwd mo e declare yung column name dito. pra hindi ka mahirap sa tracing ng column

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {

                    dataGridView1.Rows.Clear();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.AutoGenerateColumns = false;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        dataGridView1.Rows.Add(dr.ItemArray);
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmOfficials f = new frmOfficials(this);
            f.TopLevel = false;
            

            f.BringToFront();
            f.Show();
            
        }

        private void frmMaintenance_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOfficials frm = new frmOfficials(this);
            frm.btnUpdate.Hide();
            frm.btnDelete.Hide();
            frm.btnClear.Location = frm.btnUpdate.Location;
            frm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                
                if (e.ColumnIndex == 7)
                {
                    frmOfficials frmoff = new frmOfficials(this);
                    frmoff.tbID.Hide();
                    frmoff.tbID.Text = selectedrow.Cells[0].Value?.ToString();
                    frmoff.tbName.Text = selectedrow.Cells[1].Value?.ToString();
                    frmoff.tbChairmanship.Text = selectedrow.Cells[2].Value?.ToString();
                    frmoff.tbPosition.Text = selectedrow.Cells[3].Value?.ToString();
                    frmoff.dtpTs.Text = selectedrow.Cells[4].Value?.ToString();
                    frmoff.dtpTe.Text = selectedrow.Cells[5].Value?.ToString();
                    frmoff.tbStatus.Text = selectedrow.Cells[6].Value?.ToString();
                    frmoff.btnSave.Hide();
                    frmoff.btnSave.Enabled = false;
                    frmoff.btnClear.Location = frmoff.btnDelete.Location;
                    frmoff.btnDelete.Location = frmoff.btnUpdate.Location;
                    frmoff.btnUpdate.Location = frmoff.btnSave.Location;  
                    frmoff.ShowDialog();

                }
            }
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];

                if (e.ColumnIndex == 8)
                {
                    DialogResult result  = MessageBox.Show("Are you sure to delete official?\nCannot Undo.", "Delete Official", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        
                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();
                            string query = "DELETE FROM official WHERE ID = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("?", selectedrow.Cells[0].Value?.ToString());
                                command.ExecuteNonQuery();
                                

                            }
                        }
                        LoadData();
                    }
                }
            }


        }
    }
}
