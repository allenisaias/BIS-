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
    public partial class report : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            string query = "SELECT [BlotterNumber], [NameofComplainant], [TypeofIncident], [ReportStatus], [DateandTimeReported], [DateandtimeofIncident], [PlaceofIncident], [NameofIncharge], [NameofReported] FROM report";


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
                        

                       

                        // Add the formatted row to the DataGridView
                        dataGridView1.Rows.Add(
                        dr["BlotterNumber"],
                        dr["NameofComplainant"],
                        dr["TypeofIncident"],
                        dr["ReportStatus"],
                        dr["DateandTimeReported"],
                        dr["DateandtimeofIncident"],
                        
                        dr["PlaceofIncident"]
                       
                    );
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            addreport f = new addreport();
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];

                if (e.ColumnIndex == 7)
                {
                    addreport frmoff = new addreport();

                    frmoff.resID = Convert.ToInt32(selectedrow.Cells[0].Value?.ToString());
                    
                    frmoff.btnClear.Location = frmoff.btnDelete.Location;
                    frmoff.btnDelete.Location = frmoff.btnUpdate.Location;
                    frmoff.btnUpdate.Location = frmoff.btnSave.Location;
                    frmoff.btnSave.Hide();
                    frmoff.ShowDialog();

                }
            }
        }
    }
}
