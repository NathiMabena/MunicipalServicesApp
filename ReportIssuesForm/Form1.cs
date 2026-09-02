namespace ReportIssuesForm
{
    public partial class Form1 : Form
    {
        // Add this event declaration to fix CS0103
        public event EventHandler BackRequested;
        public static List<Issue> reportedIssues = new List<Issue>();
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StyleInputPanel(panelLocation);
            StyleInputPanel(panelCategory);
            StyleInputPanel(panelDescription);
            StyleSecondaryButton(btnAttachMedia);

            txtLocation.TextChanged += UpdateProgress;
            cmbCategory.SelectedIndexChanged += UpdateProgress;
            rtbDescription.TextChanged += UpdateProgress;
        }

        private void StyleSecondaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.SteelBlue;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.SteelBlue;
            btn.Font = new Font("Segoe UI Semibold", 10F);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(235, 242, 248);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.White;
        }

        private void StyleInputPanel(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(208, 213, 219), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };
        }

        private void btnSubmitReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text) ||
                cmbCategory.SelectedItem == null ||
                string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please fill in Location, Category, and Description before submitting.",
                    "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Issue newIssue = new Issue
            {
                Location = txtLocation.Text,
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text,
                AttachedFilePath = lblFilePath.Text
            };

            reportedIssues.Add(newIssue);

            // 1. Show the success pop-up FIRST (this pauses the code so the user sees it)
            MessageBox.Show("Thanks! Your report has been submitted successfully and added to the system.",
                            "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 2. Clear the form ONLY after they click 'OK' on the pop-up
            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            lblFilePath.Text = "No File Attached (Optional)";

            // 3. Reset the progress bar back to 0%
            UpdateProgress(this, EventArgs.Empty);
        }





        private void btnBack_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        private void btnAttachMedia_Click(object sender, EventArgs e)
        {
            if (openFileDialogMedia.ShowDialog() == DialogResult.OK)
            {
                lblFilePath.Text = openFileDialogMedia.FileName;
                // Trigger the progress calculation manually
                UpdateProgress(this, EventArgs.Empty);
            }
        }

        private void UpdateProgress(object sender, EventArgs e)
        {
            int progress = 0;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) progress += 25;
            if (cmbCategory.SelectedIndex != -1) progress += 25;
            if (!string.IsNullOrWhiteSpace(rtbDescription.Text)) progress += 25;

            // Updated to look for the new "Optional" default text
            if (lblFilePath.Text != "No File Attached (Optional)" && lblFilePath.Text != "") progress += 25;

            progressBarStatus.Value = progress;

            // Dynamic color and text feedback based on your new rules
            if (progress == 0)
            {
                lblFeedback.Text = "Status: Awaiting Input";
                lblFeedback.ForeColor = Color.Gray;
            }
            else if (progress <= 50)
            {
                lblFeedback.Text = $"Status: Form {progress}% Complete";
                lblFeedback.ForeColor = Color.DarkOrange;
            }
            else if (progress == 75)
            {
                lblFeedback.Text = "Status: Form 75% Almost Done: You are able to submit now!";
                lblFeedback.ForeColor = Color.FromArgb(46, 125, 50); // Professional dark green
            }
            else if (progress == 100)
            {
                lblFeedback.Text = "Status: Form 100% Complete: Ready to submit!";
                lblFeedback.ForeColor = Color.FromArgb(46, 125, 50);
            }
        }
    }
}
