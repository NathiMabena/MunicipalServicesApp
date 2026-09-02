namespace ReportIssuesForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnAttachMedia = new Button();
            lblFilePath = new Label();
            openFileDialogMedia = new OpenFileDialog();
            progressBarStatus = new ProgressBar();
            lblFeedback = new Label();
            btnSubmitReport = new Button();
            btnBack = new Button();
            label4 = new Label();
            panelLocation = new Panel();
            txtLocation = new TextBox();
            panelCategory = new Panel();
            cmbCategory = new ComboBox();
            panelDescription = new Panel();
            rtbDescription = new RichTextBox();
            panelLocation.SuspendLayout();
            panelCategory.SuspendLayout();
            panelDescription.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 111);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 0;
            label1.Text = "Enter Location";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 177);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 2;
            label2.Text = "Select Category";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 256);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 4;
            label3.Text = "Issue Description";
            // 
            // btnAttachMedia
            // 
            btnAttachMedia.BackColor = Color.White;
            btnAttachMedia.FlatAppearance.BorderColor = Color.SteelBlue;
            btnAttachMedia.FlatStyle = FlatStyle.Flat;
            btnAttachMedia.Location = new Point(31, 415);
            btnAttachMedia.Name = "btnAttachMedia";
            btnAttachMedia.Size = new Size(206, 31);
            btnAttachMedia.TabIndex = 6;
            btnAttachMedia.Text = "Media Attachment";
            btnAttachMedia.UseVisualStyleBackColor = false;
            btnAttachMedia.Click += btnAttachMedia_Click;
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Location = new Point(31, 449);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(152, 15);
            lblFilePath.TabIndex = 7;
            lblFilePath.Text = "No File Attached (Optional)";
            // 
            // openFileDialogMedia
            // 
            openFileDialogMedia.FileName = "openFileDialog1";
            // 
            // progressBarStatus
            // 
            progressBarStatus.Location = new Point(31, 498);
            progressBarStatus.Name = "progressBarStatus";
            progressBarStatus.Size = new Size(163, 23);
            progressBarStatus.TabIndex = 8;
            // 
            // lblFeedback
            // 
            lblFeedback.AutoSize = true;
            lblFeedback.Location = new Point(31, 532);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Size = new Size(120, 15);
            lblFeedback.TabIndex = 9;
            lblFeedback.Text = "Status Awaiting Input";
            // 
            // btnSubmitReport
            // 
            btnSubmitReport.BackColor = Color.SteelBlue;
            btnSubmitReport.FlatAppearance.BorderSize = 0;
            btnSubmitReport.FlatStyle = FlatStyle.Flat;
            btnSubmitReport.ForeColor = Color.White;
            btnSubmitReport.Location = new Point(382, 498);
            btnSubmitReport.Name = "btnSubmitReport";
            btnSubmitReport.Size = new Size(96, 49);
            btnSubmitReport.TabIndex = 10;
            btnSubmitReport.Text = "Submit Report";
            btnSubmitReport.UseVisualStyleBackColor = false;
            btnSubmitReport.Click += btnSubmitReport_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.SteelBlue;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(524, 498);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(96, 49);
            btnBack.TabIndex = 11;
            btnBack.Text = "Back to Main Menu";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 22F);
            label4.ForeColor = Color.SteelBlue;
            label4.Location = new Point(195, 13);
            label4.Name = "label4";
            label4.Padding = new Padding(24, 24, 0, 0);
            label4.Size = new Size(225, 65);
            label4.TabIndex = 12;
            label4.Text = "Report Issues";
            // 
            // panelLocation
            // 
            panelLocation.BackColor = Color.White;
            panelLocation.Controls.Add(txtLocation);
            panelLocation.Location = new Point(149, 111);
            panelLocation.Name = "panelLocation";
            panelLocation.Padding = new Padding(8);
            panelLocation.Size = new Size(344, 38);
            panelLocation.TabIndex = 13;
            // 
            // txtLocation
            // 
            txtLocation.BorderStyle = BorderStyle.None;
            txtLocation.Dock = DockStyle.Fill;
            txtLocation.Location = new Point(8, 8);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(328, 16);
            txtLocation.TabIndex = 2;
            // 
            // panelCategory
            // 
            panelCategory.Controls.Add(cmbCategory);
            panelCategory.Location = new Point(149, 177);
            panelCategory.Name = "panelCategory";
            panelCategory.Padding = new Padding(8);
            panelCategory.Size = new Size(346, 38);
            panelCategory.TabIndex = 14;
            // 
            // cmbCategory
            // 
            cmbCategory.Dock = DockStyle.Fill;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Items.AddRange(new object[] { "Roads", "Sanitation", "Water and Utilities", "Electricity" });
            cmbCategory.Location = new Point(8, 8);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(330, 23);
            cmbCategory.TabIndex = 4;
            // 
            // panelDescription
            // 
            panelDescription.Controls.Add(rtbDescription);
            panelDescription.Location = new Point(151, 232);
            panelDescription.Name = "panelDescription";
            panelDescription.Padding = new Padding(8);
            panelDescription.Size = new Size(346, 138);
            panelDescription.TabIndex = 15;
            // 
            // rtbDescription
            // 
            rtbDescription.Dock = DockStyle.Fill;
            rtbDescription.Location = new Point(8, 8);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(330, 122);
            rtbDescription.TabIndex = 6;
            rtbDescription.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(684, 599);
            Controls.Add(panelDescription);
            Controls.Add(panelCategory);
            Controls.Add(panelLocation);
            Controls.Add(label4);
            Controls.Add(btnBack);
            Controls.Add(btnSubmitReport);
            Controls.Add(lblFeedback);
            Controls.Add(progressBarStatus);
            Controls.Add(lblFilePath);
            Controls.Add(btnAttachMedia);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Padding = new Padding(24, 24, 0, 0);
            Text = "ReportIssues";
            Load += Form1_Load;
            panelLocation.ResumeLayout(false);
            panelLocation.PerformLayout();
            panelCategory.ResumeLayout(false);
            panelDescription.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnAttachMedia;
        private Label lblFilePath;
        private OpenFileDialog openFileDialogMedia;
        private ProgressBar progressBarStatus;
        private Label lblFeedback;
        private Button btnSubmitReport;
        private Button btnBack;
        private Label label4;
        private Panel panelLocation;
        private TextBox txtLocation;
        private Panel panelCategory;
        private ComboBox cmbCategory;
        private Panel panelDescription;
        private RichTextBox rtbDescription;
    }
}
