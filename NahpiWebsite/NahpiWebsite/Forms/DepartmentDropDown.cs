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
    public partial class DepartmentDropDown : TemplateForm
    {
        public DepartmentDropDown()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DepartmentDropDowns", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@department", txtDepartment.Text.Trim());

                    if(con.State!=ConnectionState.Open)
                    {
                        con.Open();

                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfull");

                }
            }
        }
    }
}
