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
    public partial class frmTasksAdder : Form
    {
        private string email;

        public frmTasksAdder(string userEmail)
        {
            InitializeComponent();
            email = userEmail;
            

            for (int d = 1; d <= 31; d++)
            {
                cmbDate.Items.Add(d);
            }
            for (int m = 1; m <= 12; m++)
            {
                cmbMonth.Items.Add(m);
            }
            for (int y = 2024; y <= 2024 + 100; y++)
            {
                cmbYear.Items.Add(y);
            }

            cmbSubject.Items.AddRange(new string[] { "Subject 01", "Subject 02", "Subject 03" });
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbDate.SelectedItem == null || cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null || cmbSubject.SelectedItem == null || string.IsNullOrEmpty(txtTimeSpent.Text))
            {
                MessageBox.Show("All fields must be filled out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int date = Convert.ToInt32(cmbDate.SelectedItem);
            int month = Convert.ToInt32(cmbMonth.SelectedItem);
            int year = Convert.ToInt32(cmbYear.SelectedItem);
            string subject = cmbSubject.SelectedItem.ToString();
            int timeSpent;

            if (!int.TryParse(txtTimeSpent.Text, out timeSpent))
            {
                MessageBox.Show("Time spent must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=DESKTOP-B4DG86D\\SQLEXPRESS;Initial Catalog=taskMate;Integrated Security=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO StudyRecords (Email, Date, Month, Year, Subject, TimeSpent) VALUES (@Email, @Date, @Month, @Year, @Subject, @TimeSpent)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Subject", subject);
                    cmd.Parameters.AddWithValue("@TimeSpent", timeSpent);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Task added successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtTimeSpent.Clear();
            cmbDate.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;
        }

        private void lblBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmSelection SelectionForm = new frmSelection(email);
            SelectionForm.Show();
        }

        private void frmTasksAdder_Load(object sender, EventArgs e)
        {

        }
    }
}
