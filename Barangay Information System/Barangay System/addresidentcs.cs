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
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace Barangay_System
{
    public partial class addresidentcs : Form
    {
        private string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\SYSTEM\Barangay System\Barangay System\bin\Debug\BIS.accdb;";
        public int ID, resID;
        

        private resident _resident;
        public addresidentcs(resident residentdash)
        {
            _resident = residentdash;
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addresidentcs_Load(object sender, EventArgs e)
        {

        }

        public void LoadResidentIndividual()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT [Lastname], [Name], [Fullname],[Middlename], [Gender], [BirthDate],[Placeofbirth], [Age],[CpNumber], [CivilStatus], " +
                    "[Occupation], [Religion],[HouseNumber], [Street], [ResidentStatus], [VoterStatus], [ResidentIMG] FROM residents WHERE [ResidentID] = @resiID ";
                
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
                                    
                                        tbLN.Text = dr["Lastname"].ToString();
                                        tbN.Text = dr["Name"].ToString();
                                        tbFN.Text = dr["Fullname"].ToString();
                                        tbMn.Text = dr["Middlename"].ToString();
                                        tbGender.Text = dr["Gender"].ToString();
                                        dtpBD.Text = Convert.ToDateTime(dr["BirthDate"]).ToString("dd/MM/yyyy");
                                        tbPOB.Text = dr["Placeofbirth"].ToString();
                                        tbAge.Text = Convert.ToInt32(dr["Age"]).ToString();
                                        tbCP.Text = dr["CpNumber"].ToString();
                                        tbCS.Text = dr["CivilStatus"].ToString();
                                        tbOCC.Text = dr["Occupation"].ToString();
                                        tbREL.Text = dr["Religion"].ToString();
                                        tbHN.Text = dr["HouseNumber"].ToString();
                                        tbST.Text = dr["Street"].ToString();
                                        tbRS.Text = dr["ResidentStatus"].ToString();
                                        tbVS.Text = dr["VoterStatus"].ToString();

                                        if (dr["ResidentIMG"] != DBNull.Value)
                                        {
                                            byte[] imageBytes = (byte[])dr["ResidentIMG"];
                                            using (MemoryStream ms = new MemoryStream(imageBytes))
                                            {
                                                pictureBox1.Image = Image.FromStream(ms); // Show image in PictureBox
                                            }
                                        }
                                        else
                                        {
                                            pictureBox1.Image = null; // Clear PictureBox if no image
                                        }


                                }
                            }

                        }
                    }
                    catch { }

                }

            }
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
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
                    string query = "UPDATE residents SET [Lastname] = ?, [NAME] = ?, [Fullname] = ?, [Middlename] = ?, [Gender] = ?, [BirthDate] = ?, [Placeofbirth] = ?, [Age] = ?, " +
                           "[CpNumber] = ?, [CivilStatus] = ?, [Occupation] = ?, [Religion] = ?, [HouseNumber] = ?, [Street] = ?, " +
                           "[ResidentStatus] = ?, [VoterStatus] = ? WHERE [ResidentID] = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbLN.Text; // Lastname
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbN.Text;  // Name
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbFN.Text; // Fullname
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbMn.Text; // Middlename
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbGender.Text; // Gender
                        command.Parameters.Add("?", OleDbType.Date).Value = Convert.ToDateTime(dtpBD.Text); // BirthDate
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbPOB.Text; // Placeofbirth
                        command.Parameters.Add("?", OleDbType.Integer).Value = Convert.ToInt32(tbAge.Text); // Age
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbCP.Text; // CpNumber (stored as string if needed)
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbCS.Text; // CivilStatus
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbOCC.Text; // Occupation
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbREL.Text; // Religion
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbHN.Text; // HouseNumber
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbST.Text; // Street
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbRS.Text; // ResidentStatus
                        command.Parameters.Add("?", OleDbType.VarChar).Value = tbVS.Text; // VoterStatus
                        command.Parameters.Add("?", OleDbType.Integer).Value = resID;     // ID (Primary Key)

                        command.ExecuteNonQuery();
                        

                    }
                }
                _resident.LoadResidents();
                MessageBox.Show("Resident have been updated. Form will close");
                this.Close();
            }
            
        }

        private void tbN_TextChanged(object sender, EventArgs e)
        {
            if (tbN.Text.Length > 0)
            {
                tbFN.Text = tbN.Text;
            }
        }

        private void tbMn_TextChanged(object sender, EventArgs e)
        {
            if (tbMn.Text.Length > 0)
            {
                tbFN.Text = tbN.Text + " " + tbMn.Text;
            }
        }

        private void tbLN_TextChanged(object sender, EventArgs e)
        {
            if (tbLN.Text.Length > 0)
            {
                tbFN.Text = tbN.Text + " " + tbMn.Text + " " + tbLN.Text;
            }
        }

        private void tbFN_TextChanged(object sender, EventArgs e)
        {
            if (tbLN.Text.Length >= 0)
            {
                tbFN.Text = tbN.Text + " " + tbMn.Text + " " + tbLN.Text;
            }
        }

        private void dtpBD_ValueChanged(object sender, EventArgs e)
        {
           int AgeX = DateTime.Today.Year - dtpBD.Value.Year;
            if (dtpBD.Value > DateTime.Today.AddYears(-AgeX)) AgeX--;
            tbAge.Text = AgeX.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete Resident?\nCannot Undo.", "Delete Resident", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM residents WHERE ResidentID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", resID);
                        command.ExecuteNonQuery();
                        _resident.LoadResidents();
                        this.Close();
                       


                    }
                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbN.Text = "";
            tbLN.Text = "";
            tbMn.Text = "";
            tbFN.Text = "";
            tbGender.Text = "";
            tbPOB.Text = "";
            tbCP.Text = "";
            tbCS.Text = "";
            tbOCC.Text = "";
            tbREL.Text = "";
            tbHN.Text = "";
            tbST.Text = "";
            tbRS.Text = "";
            tbVS.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbLN.Text != "" && tbN.Text != "" && tbFN.Text != "" && tbMn.Text != "" && tbMn.Text != "" && tbGender.Text != "" && tbPOB.Text != "" && tbCP.Text != "" && tbCS.Text != "" && tbRS.Text != "" && tbVS.Text != "")
            {
                if (pictureBox1.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Save image to memory as PNG
                        byte[] imageBytes = ms.ToArray();

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            string query = "UPDATE residents SET [ResidentIMG] = @image WHERE [ResidentID] = @id";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                command.Parameters.Add("@image", OleDbType.Binary).Value = imageBytes;
                                command.Parameters.Add("@id", OleDbType.Integer).Value = resID; // Replace with actual resident ID

                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    if (fileInfo.Length <= 1 * 1024 * 1024) // Check if file size is <= 1MB
                    {
                        pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("File size must not exceed 1MB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to remove the resident's image?",
                                          "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    string query = "UPDATE residents SET [ResidentIMG] = NULL WHERE [ResidentID] = @id";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("@id", OleDbType.Integer).Value = resID; // Replace with actual resident ID

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                pictureBox1.Image = null; // Clear the PictureBox
                MessageBox.Show("Image removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1,"Double Click to browse image");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string NAME = tbLN.Text;

            if (tbLN.Text != "" && tbN.Text != "" && tbFN.Text != "" && tbMn.Text != "" && tbMn.Text != "" && tbGender.Text != "" && tbPOB.Text != "" && tbCP.Text != "" && tbCS.Text != "" && tbRS.Text != "" && tbVS.Text != "")
            {
                if (ID == 0) 
                {
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        string query = "INSERT INTO residents ([Lastname], [Name], [Fullname] ,[Middlename], [Gender], [Birthdate], [Placeofbirth], [Age], [CpNumber], [CivilStatus], [Occupation], [Religion], [HouseNumber], [Street], [ResidentStatus], [VoterStatus] ) VALUES (@ln, @n, @fn, @mn, @g, @bd, @pb, @a, @cn, @cs, @o, @r, @hn, @s, @rs, @vs)";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        cmd.Parameters.Add("@ln", OleDbType.VarChar).Value = tbLN.Text;
                        cmd.Parameters.Add("@n", OleDbType.VarChar).Value = tbN.Text;
                        cmd.Parameters.Add("@fn", OleDbType.VarChar).Value = tbFN.Text;
                        cmd.Parameters.Add("@mn", OleDbType.VarChar).Value = tbMn.Text;
                        cmd.Parameters.Add("@g", OleDbType.VarChar).Value = tbGender.Text;
                        cmd.Parameters.Add("@bd", OleDbType.Date).Value = Convert.ToDateTime(dtpBD.Value);
                        cmd.Parameters.Add("@pb", OleDbType.VarChar).Value = tbPOB.Text;
                        cmd.Parameters.Add("@a", OleDbType.Integer).Value = Convert.ToInt32(tbAge.Text);
                        cmd.Parameters.Add("@cn", OleDbType.VarChar).Value = tbCP.Text;
                        cmd.Parameters.Add("@cs", OleDbType.VarChar).Value = tbCS.Text;
                        cmd.Parameters.Add("@o", OleDbType.VarChar).Value = tbOCC.Text;
                        cmd.Parameters.Add("@r", OleDbType.VarChar).Value = tbREL.Text;
                        cmd.Parameters.Add("@hn", OleDbType.VarChar).Value = tbHN.Text;
                        cmd.Parameters.Add("@s", OleDbType.VarChar).Value = tbST.Text;
                        cmd.Parameters.Add("@rs", OleDbType.VarChar).Value = tbRS.Text;
                        cmd.Parameters.Add("@vs", OleDbType.VarChar).Value = tbVS.Text;


                        conn.Open();
                        cmd.ExecuteNonQuery();
                        
                         
                    }

                    _resident.LoadResidents();
                    MessageBox.Show("Resident added successfully.");
                    this.Close();
                }
                else 
                {
                    DialogResult result = MessageBox.Show("Are you sure to Update official?.", "Update Official", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        using (OleDbConnection connection = new OleDbConnection(connectionString))
                        {
                            connection.Open();
                            string query = "UPDATE residents SET [Lastname] = '" + tbLN.Text + "', [Name] = '" + tbN.Text + "', [Fullname] = '" + tbFN.Text + "', [Middlename] = '" + tbMn.Text + "', [Gender] = '" + tbGender.Text + "', [Birthdate] = '"   + dtpBD.Text + "', [Placeofbirth] = '" + tbPOB.Text + "', [Age] = '" + tbAge.Text + "', [CpNumber] = '" + tbCP.Text + "', [CivilStatus] = '" + tbCS.Text + "', [Occupation] = '" + tbOCC.Text + "', [Religion] = '" + tbREL.Text + "', [HouseNumber] = '" + tbHN.Text + "', [Street] = '" + tbST.Text + "', [ResidentStatus] = '" + tbRS.Text + "', [VoterStatus] = '" + tbVS + "'  WHERE ID = ?";
                            using (OleDbCommand command = new OleDbCommand(query, connection))
                            {
                                

                                command.ExecuteNonQuery();
                                MessageBox.Show("Official have been updated. Form will close");
                                this.Close();


                            }
                        }
                        _resident.LoadResidents();
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
