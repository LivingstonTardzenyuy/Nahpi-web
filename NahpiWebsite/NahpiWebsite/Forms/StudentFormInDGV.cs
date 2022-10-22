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
    public partial class StudentFormInDGV : TemplateForm
    {
        public StudentFormInDGV()
        {
            InitializeComponent();

            LoadDataIntoDVG();
        }

        private void LoadDataIntoDVG()
        {
            using (SqlConnection con= new SqlConnection(AppConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_loadDataIntoDGV", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    DataTable dta = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    if(sdr.HasRows)
                    {
                        dta.Load(sdr);
                        dataGridView.DataSource = dta;
                    }
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(dataGridView.Rows.Count>0)
            {
                using (SqlConnection con=new SqlConnection(AppConnection.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_SearchByMatricule", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Matricule", txtSearch.Text.Trim());

                        if(con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }

                        DataTable dta = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();

                        if(sdr.HasRows)
                        {
                            dta.Load(sdr);
                            dataGridView.DataSource = dta;
                            
                        }

                        else
                        {
                            MessageBox.Show("Not availible");
                        }
                    }
                }
            }
        }

        private void refreshDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadDataIntoDVG();
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView.Rows.Count>0)
            {
                int studentID = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
               // MessageBox.Show(studentID.ToString());

                StudentForm stf = new StudentForm();
                stf.studentID = studentID;
                stf.isFormUpdate = true;
                
                

                stf.ShowDialog();
                LoadDataIntoDVG();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentForm stf = new StudentForm();
            stf.ShowDialog();
            LoadDataIntoDVG();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //if(this.)
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
