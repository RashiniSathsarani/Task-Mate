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

namespace TaskMate
{
    public partial class frmReportViewer : Form
    {
        private string email;

        public frmReportViewer(string userEmail)
        {
            InitializeComponent();
            email = userEmail;

        }

        private void lblBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmSelection SelectionForm = new frmSelection(email);
            SelectionForm.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Please select both 'Month' and 'Year'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int month = Convert.ToInt32(cmbMonth.SelectedItem);
            int year = Convert.ToInt32(cmbYear.SelectedItem);

            string connectionString = "Data Source=DESKTOP-B4DG86D\\SQLEXPRESS;Initial Catalog=taskMate;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT Subject, SUM(TimeSpent)/60 AS TotalTimeSpent 
                    FROM StudyRecords 
                    WHERE Email = @Email AND Month = @Month AND Year = @Year 
                    GROUP BY Subject";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(i);
            }
            for (int i = 2024; i <= 2024 + 100; i++)
            {
                cmbYear.Items.Add(i);
            }
        }

       private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNewww NewForm = new frmNewww();
            NewForm.Show();
        }
    }
}
