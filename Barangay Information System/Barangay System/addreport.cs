using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_System
{
    public partial class addreport : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        public int ID, resID;
        public addreport()
        {
            InitializeComponent();
        }

        private void addreport_Load(object sender, EventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT [NameofComplainant], [TypeofIncident], [ReportStatus],[DateandTimeReported], [DateandtimeofIncident], [PlaceofIncident],[NameofIncharge], [NameofReported], " +
                    " FROM report WHERE [BlotterNumber] = @resiID ";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.Add("@resiID", OleDbType.Integer).Value = resID;


                    try
                    {
                        conn.Open();
                        using (OleDbDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    tbNOC.Text = dr["NameofComplainant"].ToString();
                                    tbTOI.Text = dr["TypeofIncident"].ToString();
                                    tbNOR.Text = dr["NameofReported"].ToString();
                                    cbRS.Text = dr["ReportStatus"].ToString();
                                    dtpDATE.Text = Convert.ToDateTime(dr["DateandTimeReported"]).ToString("dd/MM/yyyy");
                                    tbPOI.Text = dr["PlaceofIncident"].ToString();
                                    dtpDOI.Text = Convert.ToDateTime(dr["DateandtimeofIncident"]).ToString("dd/MM/yyyy");
                                    tbNOI.Text = Convert.ToInt32(dr["NameofIncharge"]).ToString();
                                   

                                   


                                }
                            }

                        }
                    }
                    catch { }

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to Update resident?.", "Update Resident", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE report SET [NameofComplainant] = ?, [TypeofIncident] = ?, [ReportStatus] = ?, [DateandTimeReported] = ?, [DateandtimeofIncident] = ?, [PlaceofIncident] = ?, [NameofIncharge] = ?, [NameofReported] = ? WHERE [ResidentID] = ?";
                           
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbNOC.Text; // Lastname
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbTOI.Text;  // Name
                        command.Parameters.Add("?", OleDbType.VarChar).Value = cbRS.Text; // Fulln
                        command.Parameters.Add("?", OleDbType.Date).Value = Convert.ToDateTime(dtpDATE.Text); // Gender
                        command.Parameters.Add("?", OleDbType.Date).Value = Convert.ToDateTime(dtpDOI.Text); // BirthDate
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbPOI.Text; // Placeofbirth
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbNOI.Text; // Age
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbNOR.Text; // CpNumber (stored as string if needed)
                        
                        command.Parameters.Add("?", OleDbType.Integer).Value = resID;     // ID (Primary Key)

                        command.ExecuteNonQuery();


                    }
                }
               
                MessageBox.Show("Resident have been updated. Form will close");
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string NAME = tbNOC.Text;

            if (tbNOC.Text != "" && tbTOI.Text != "" && tbNOR.Text != "" && cbRS.Text != "" && dtpDATE.Text != "" && tbPOI.Text != "" && dtpDOI.Text != "" && tbNOI.Text !=  "")
            {
                if (ID == 0)
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        string query = "INSERT INTO report ([NameofComplainant], [TypeofIncident], [ReportStatus] ,[DateandTimeReported], [DateandtimeofIncident], [PlaceofIncident], [NameofIncharge], [NameofReported] ) VALUES (@n, @t, @r, @d, @dt, @p, @ni, @nr)";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        cmd.Parameters.Add("@n", OleDbType.VarChar).Value = tbNOC.Text;
                        cmd.Parameters.Add("@t", OleDbType.VarChar).Value = tbTOI.Text;
                        cmd.Parameters.Add("@r", OleDbType.VarChar).Value = cbRS.Text;
                        cmd.Parameters.Add("@d", OleDbType.Date).Value = Convert.ToDateTime(dtpDATE.Value);
                        cmd.Parameters.Add("@dt", OleDbType.Date).Value = Convert.ToDateTime(dtpDOI.Value);
                        cmd.Parameters.Add("@p", OleDbType.VarChar).Value = tbPOI.Text;
                        cmd.Parameters.Add("@ni", OleDbType.VarChar).Value = tbNOI.Text;
                        cmd.Parameters.Add("@nr", OleDbType.VarChar).Value = tbNOR.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();


                    }

                    
                    MessageBox.Show("report added successfully.");
                    this.Close();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure to Update report?.", "Update Report", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();
                            string query = "UPDATE report SET [NameofComplainant] = '" + tbNOC.Text + "', [TypeofIncident] = '" + tbTOI.Text + "', [ReportStatus] = '" + cbRS.Text + "', [DateandTimeReported] = '" + dtpDATE.Text + "', [DateandtimeofIncident] = '" + dtpDOI.Text + "', [PlaceofIncident] = '" + tbPOI.Text + "', [NameofIncharge] = '" + tbNOI.Text + "', [NameofReported] = '" + tbNOR.Text +"'  WHERE ID = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {


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
    }
}
