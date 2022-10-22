using NahpiWebsite.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NahpiWebsite.Forms
{
    public partial class StudentForm : TemplateForm
    {
        public StudentForm()
        {
            InitializeComponent();

        }
        


       //prorities for update process
        public int studentID { get; set; }
        public bool isFormUpdate { get; set; }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (isFormValid())
            {
                
                if (this.isFormUpdate)
                {
                    // do update operation
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_UpdateUsersInfoss", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("studentID", this.studentID);
                            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@Matricule", txtMatricule.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@Tel", txtTel.Text.Trim());
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker.Value);
                            cmd.Parameters.AddWithValue("@DepartmentID", txtMatricule.Text.Trim());
                            //   cmd.Parameters.AddWithValue("@", txtDepartment.Text.Trim());
                            


                            //checking for validity
                            DialogResult dialogR = MessageBox.Show("Are you sure you want to Update", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (dialogR == DialogResult.Yes)
                            {
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }


                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Update Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                btnsaveInfo.Text = "Update Information";
                            }

                        }
                    }
                }
            }


            else
            {
                //btnDelete.Enabled = false;                  //disable 
                // do insert operation
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_storeStudentInfoInDBsss", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Matricule", txtMatricule.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Tel", txtTel.Text.Trim());
                        cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker.Value);
                        cmd.Parameters.AddWithValue("@DepartmentID", txtMatricule.Text.Trim());

                        
                        //checking for validity
                        DialogResult dialogR = MessageBox.Show("Check information to make sure everything is correct if correct press Yes", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogR == DialogResult.Yes)
                        {
                            if (con.State != ConnectionState.Open)
                            {
                                con.Open();
                            }
                           
                           // [department]

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successful Insertion", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //method to clear all the text
                   //         clearAll();


                        }

                    }
                }
            }
        }


        private void clearAll()
        {
            txtName.Clear();
            txtMatricule.Clear();
            txtPassword.Clear();
            txtTel.Clear();
            txtEmail.Clear();
            //dateTimePicker.CloseUp();


            //checking update operations
            if (this.isFormUpdate)
            {
                this.studentID =studentID ;
                this.isFormUpdate = true;

                btnsaveInfo.Text = "Update Information";
                btnDeletess.Enabled = true;
            }
        }
    
        private bool isFormValid()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            if (txtMatricule.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Matricule", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            if (txtMatricule.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Department", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            if (txtTel.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Tel", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            if (txtMatricule.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Matricle", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            if (txtEmail.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Email", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }


            else
            {
                return true;
            }

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            //loaddataIntoComboBox();

            if (this.isFormUpdate)
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_studentUpdateDBs", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@studentID", studentID);


                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        DataTable dta = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();

                        dta.Load(sdr);

                        //loading data for one user

                        DataRow dtrow = dta.Rows[0];

                        // passing values to another form
                        txtName.Text = dtrow["Name"].ToString();
                        txtMatricule.Text = dtrow["Matricule"].ToString();
                       // comboBox = comboBox.SelectedText;
                        txtTel.Text = dtrow["Tel"].ToString();
                        txtEmail.Text = dtrow["Email"].ToString();
                        //dateTimePicker.Value = dtrow["DateOfBirth"].ToString();
                        txtPassword.Text = dtrow["Password"].ToString();
                    }

                }
            }

            else
            {
                //do insert operation

            }

        }

      /*  private void loaddataIntoComboBox()
        {
            using (SqlConnection con=new SqlConnection(AppConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("uusp_LoadDepartmentIntoComboBoxss", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    DataTable dta = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dta.Load(sdr);

                    //comboboxDepartment = dta;

                    comboBoxDep.DataSource = dta;

                    comboBoxDep.DisplayMember = "department";
                    comboBoxDep.ValueMember = "DepartmentID";

                    
                }
            }
        }
        */
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.isFormUpdate)
            {
                btnDeletess.Enabled = true;

                using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_studentDelete", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("studentID", this.studentID);

                        DialogResult dialogre = MessageBox.Show("Are you sure you want to Delete", "Validity", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialogre == DialogResult.Yes)
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successful");

                            clearAll();
                        }

                    }
                }

            }
        }

       private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.isFormUpdate)
            {
                
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteStudentInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("studentID", this.studentID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("successful");

                        btnDeletess.Enabled = true;
                    }
                }
            }
        }
        
        private void allStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentFormInDGV stdgv = new StudentFormInDGV();
            stdgv.ShowDialog();
        }
    }
}