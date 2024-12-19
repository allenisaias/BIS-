using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Barangay_System
{
    public partial class resident : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        public resident()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void resident_Load(object sender, EventArgs e)
        {
            LoadResidents();
        }

        public void LoadResidents()
        {
            string query = "SELECT [ResidentID], [Fullname], [BirthDate], [Age], [VoterStatus] FROM residents";  

            
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
                        string birthDateFormatted = "";

                        // Check if BirthDate is not null or DBNull, then format it
                        if (dr["BirthDate"] != DBNull.Value)
                        {
                            DateTime birthDate = Convert.ToDateTime(dr["BirthDate"]);
                            birthDateFormatted = birthDate.ToString("dd/MM/yyyy");
                        }

                        // Add the formatted row to the DataGridView
                            dataGridView1.Rows.Add(
                            dr["ResidentID"],
                            dr["Fullname"],
                            birthDateFormatted,
                            dr["Age"],
                            dr["VoterStatus"]
                        );
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        public void tbSearchLoadResidents()
        {
            string query = "SELECT [ResidentID], [Fullname], [BirthDate], [Age], [VoterStatus] FROM residents WHERE ([Fullname] like '%"+ tbSearch.Text.Trim() + "%' or [HouseNumber] like '"+ tbSearch.Text.Trim() + "%') ";  

            

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
                        string birthDateFormatted = "";

                        // Check if BirthDate is not null or DBNull, then format it
                        if (dr["BirthDate"] != DBNull.Value)
                        {
                            DateTime birthDate = Convert.ToDateTime(dr["BirthDate"]);
                            birthDateFormatted = birthDate.ToString("dd/MM/yyyy");
                        }

                        // Add the formatted row to the DataGridView
                             dataGridView1.Rows.Add(
                            dr["ResidentID"],
                            dr["Fullname"],
                            birthDateFormatted,
                            dr["Age"],
                            dr["VoterStatus"]
                        );
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];

                if (e.ColumnIndex == 5)
                {
                    addresidentcs frmoff = new addresidentcs(this);
                    
                    frmoff.resID = Convert.ToInt32(selectedrow.Cells[0].Value?.ToString());
                    frmoff.LoadResidentIndividual();
                    frmoff.btnClear.Location = frmoff.btnDelete.Location;
                    frmoff.btnDelete.Location = frmoff.btnUpdate.Location;
                    frmoff.btnUpdate.Location = frmoff.btnSave.Location;
                    frmoff.btnSave.Hide();
                    frmoff.ShowDialog();

                }
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            addresidentcs frmresi = new addresidentcs(this);
            frmresi.btnClear.Location = frmresi.btnDelete.Location;
            frmresi.btnDelete.Location= frmresi.btnUpdate.Location;
            frmresi.btnClear.Location = frmresi.btnDelete.Location;
            frmresi.btnDelete.Hide();
            frmresi.btnUpdate.Hide();
            frmresi.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbSearch_MouseClick(object sender, MouseEventArgs e)
        {
            tbSearch.Text = "";
        }

        private void tbSearch_MouseLeave(object sender, EventArgs e)
        {
            if (tbSearch.Text == "")
            {
                tbSearch.Text = "Type to search";
            }
           
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbSearch.Text == "Type to search")
            {
                tbSearch.Text = "";
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbSearchLoadResidents();
            }
            
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text.Length == 0)
            {
                LoadResidents();
            }
        }
    }
}
