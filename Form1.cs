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
            // Apply the solid blue and hover effect to the active features
            StyleSecondaryButton(btnReportIssues);
            StyleSecondaryButton(btnLocalEvents);
            StyleSecondaryButton(btnAdminView);

            
            StyleComingSoonButton(btnServiceStatus);
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

        private void StyleComingSoonButton(Button btn)
        {
            
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.DarkGray;
            btn.BackColor = Color.LightGray;
            btn.ForeColor = Color.DimGray;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            btn.Cursor = Cursors.Default;

           
            btn.MouseEnter += (s, e) => btn.BackColor = Color.LightGray;
            btn.MouseLeave += (s, e) => btn.BackColor = Color.LightGray;
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
            // Create an instance of your new form
            LocalEventsForm eventsForm = new LocalEventsForm();

            // Hide the main menu
            this.Hide();

            // Show the new form. (Using ShowDialog pauses the code here until the events form is closed)
            eventsForm.ShowDialog();

            // Once the user closes the events form, show the main menu again
            this.Show();
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            ShowComingSoon("Service Request Status");
        }

        private void ShowComingSoon(string featureName)
        {
            MessageBox.Show(
                $"{featureName} isn't available yet. This feature is coming in a future update. (Part 3)",
                "Feature Not Available",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Dynamically generates the data grid to view the list in memory
        private void btnAdminView_Click(object sender, EventArgs e)
        {
            // Access the static list from your ReportIssues form
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

            // Add these two lines right here to fix the text cutoff:
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            viewForm.Controls.Add(grid);
            viewForm.ShowDialog();
        }
    }
}