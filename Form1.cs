using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Apply the solid SteelBlue and hover effect to all active modules
            StyleSecondaryButton(btnReportIssues);
            StyleSecondaryButton(btnLocalEvents);
            StyleSecondaryButton(btnServiceStatus);
            StyleSecondaryButton(btnAdminView);

            btnAdminView.Enabled = false;
            this.Controls.Remove(btnAdminView);
        }

        private void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI Semibold", 10F);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.DodgerBlue;
            btn.MouseLeave += (s, e) => btn.BackColor = Color.SteelBlue;
        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssuesForm.Form1 reportIssuesForm = new ReportIssuesForm.Form1();
            reportIssuesForm.BackRequested += (s, args) => this.Show();
            reportIssuesForm.Show();
            this.Hide();
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm();
            this.Hide();
            eventsForm.ShowDialog();
            this.Show();
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            ServiceRequestStatusForm statusForm = new ServiceRequestStatusForm();
            this.Hide();
            statusForm.ShowDialog();
            this.Show();
        }

        private void btnAdminView_Click(object sender, EventArgs e)
        {
            var currentIssues = ReportIssuesForm.Form1.reportedIssues;

            if (currentIssues == null || currentIssues.Count == 0)
            {
                MessageBox.Show("No issues reported yet. Please submit a report first.",
                                "Empty Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form viewForm = new Form
            {
                Text = "Admin: View Reported Issues",
                Size = new Size(700, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView grid = new DataGridView
            {
                DataSource = currentIssues,
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            viewForm.Controls.Add(grid);
            viewForm.ShowDialog();
        }
    }
}