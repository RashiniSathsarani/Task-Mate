using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskMate
{
    public partial class frmSelection : Form
    {
        private string email;

        public frmSelection(string userEmail)
        {
            InitializeComponent();
            email = userEmail;
        }

        private void brnAddTasks_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTasksAdder TasksAdderForm = new frmTasksAdder(email);
            TasksAdderForm.Show();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReportViewer ReportViewerForm = new frmReportViewer(email);
            ReportViewerForm.Show();
        }
    }
}
