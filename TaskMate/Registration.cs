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
    public partial class frmRegistration : Form
    {
        private String connectionString = "Data Source=DESKTOP-B4DG86D\\SQLEXPRESS;Initial Catalog=taskMate;Integrated Security=True;";

        public frmRegistration()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void lblBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmFront FrontFrom = new frmFront();
            FrontFrom.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string subject1 = txtSubject1.Text;
            string subject2 = txtSubject2.Text;
            string subject3 = txtSubject3.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(subject1) || string.IsNullOrEmpty(subject2) || string.IsNullOrEmpty (subject3) || string.IsNullOrEmpty(subject3))
            {
                MessageBox.Show("Please enter 'Email', 'Password' and 'your 3 main Subjects'", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Users (Email, Password, Subject1, Subject2, Subject3) " +
                    "VALUES (@Email, @Password, @Subject1, @Subject2, @Subject3)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("Password", password);
                cmd.Parameters.AddWithValue("@Subject1", subject1);
                cmd.Parameters.AddWithValue("@Subject2", subject2);
                cmd.Parameters.AddWithValue("@Subject3", subject3);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration successful!");
                    txtEmail.Clear();
                    txtPassword.Clear();
                    txtSubject1.Clear();
                    txtSubject2.Clear();
                    txtSubject3.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            } 
        }
    }
}
