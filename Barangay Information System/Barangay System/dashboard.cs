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
    public partial class dashboard : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        public dashboard()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
          

        }

        private void dashboard_Load(object sender, EventArgs e)
        {
            UpdateRecordCount();
            string query = "SELECT * FROM official";  // Replace with your actual table name

            

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {

                    dataGridView2.Rows.Clear();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    

                    dataGridView2.AutoGenerateColumns = false;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        dataGridView2.Rows.Add(dr.ItemArray);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }
        private void UpdateRecordCount()
        {

            string query6 = "SELECT Count(ReportStatus) AS ReportStatusXY FROM report WHERE ReportStatus like 'Settled%'";



            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {


                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query6, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);



                    SRcount.Text = Convert.ToInt32(dataTable.Rows[0]["ReportStatusXY"]).ToString();


                }
                catch { }
            }

            string query5 = "SELECT Count(ReportStatus) AS ReportStatusXY FROM report WHERE ReportStatus like 'Active%'";



            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {


                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query5, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);



                    URcount.Text = Convert.ToInt32(dataTable.Rows[0]["ReportStatusXY"]).ToString();


                }
                catch { }
            }

            string query4 = "SELECT Count(VoterStatus) AS VoterStatusXY FROM residents WHERE VoterStatus like 'Registered%'";



            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {


                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query4, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);



                    Vcount.Text = Convert.ToInt32(dataTable.Rows[0]["VoterStatusXY"]).ToString();


                }
                catch { }
            }
            string query = "SELECT Count(gender) AS genderXY FROM residents WHERE gender like 'MALE%'";
            


            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {

                    
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                   

                    Mcount.Text = Convert.ToInt32(dataTable.Rows[0]["genderXY"]).ToString();


                }
                catch { }
            }

            string query1 = "SELECT Count(gender) AS genderXY FROM residents WHERE gender like 'FEMALE%'";


            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {


                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query1, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);



                    Fcount.Text = Convert.ToInt32(dataTable.Rows[0]["genderXY"]).ToString();


                }
                catch { }
            }


            string query2 = "SELECT Count(gender) AS genderXY FROM residents";


            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {


                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query2, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);



                    TotRes.Text = Convert.ToInt32(dataTable.Rows[0]["genderXY"]).ToString();


                }
                catch { }

            }


        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Vcount_Click(object sender, EventArgs e)
        {

        }
    }
}
