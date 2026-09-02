namespace MunicipalServicesApp
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
            btnReportIssues = new Button();
            btnLocalEvents = new Button();
            btnServiceStatus = new Button();
            btnAdminView = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 22F);
            label1.ForeColor = Color.SteelBlue;
            label1.Location = new Point(193, 22);
            label1.Name = "label1";
            label1.Padding = new Padding(24, 24, 0, 0);
            label1.Size = new Size(386, 65);
            label1.TabIndex = 0;
            label1.Text = "Municipal Services Portal";
            // 
            // btnReportIssues
            // 
            btnReportIssues.BackColor = Color.SteelBlue;
            btnReportIssues.FlatStyle = FlatStyle.Flat;
            btnReportIssues.ForeColor = Color.White;
            btnReportIssues.Location = new Point(301, 132);
            btnReportIssues.Name = "btnReportIssues";
            btnReportIssues.Size = new Size(158, 66);
            btnReportIssues.TabIndex = 1;
            btnReportIssues.Text = "Report Issues";
            btnReportIssues.UseVisualStyleBackColor = false;
            btnReportIssues.Click += btnReportIssues_Click;
            // 
            // btnLocalEvents
            // 
            btnLocalEvents.FlatStyle = FlatStyle.Flat;
            btnLocalEvents.Location = new Point(301, 221);
            btnLocalEvents.Name = "btnLocalEvents";
            btnLocalEvents.Size = new Size(158, 66);
            btnLocalEvents.TabIndex = 2;
            btnLocalEvents.Text = "Local Events and Announcements";
            btnLocalEvents.UseVisualStyleBackColor = true;
            btnLocalEvents.Click += btnLocalEvents_Click;
            // 
            // btnServiceStatus
            // 
            btnServiceStatus.FlatStyle = FlatStyle.Flat;
            btnServiceStatus.Location = new Point(301, 310);
            btnServiceStatus.Name = "btnServiceStatus";
            btnServiceStatus.Size = new Size(158, 66);
            btnServiceStatus.TabIndex = 3;
            btnServiceStatus.Text = "Service Request Status";
            btnServiceStatus.UseVisualStyleBackColor = true;
            btnServiceStatus.Click += btnServiceStatus_Click;
            // 
            // btnAdminView
            // 
            btnAdminView.BackColor = Color.SteelBlue;
            btnAdminView.FlatAppearance.BorderColor = Color.Lavender;
            btnAdminView.FlatStyle = FlatStyle.Flat;
            btnAdminView.ForeColor = Color.White;
            btnAdminView.Location = new Point(12, 397);
            btnAdminView.Name = "btnAdminView";
            btnAdminView.Size = new Size(102, 41);
            btnAdminView.TabIndex = 4;
            btnAdminView.Text = "View Data (Admin)";
            btnAdminView.UseVisualStyleBackColor = false;
            btnAdminView.Click += btnAdminView_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            label2.ForeColor = SystemColors.GrayText;
            label2.Location = new Point(168, 97);
            label2.Name = "label2";
            label2.Size = new Size(462, 19);
            label2.TabIndex = 5;
            label2.Text = "Welcome! Select an option below to engage with your local municipality";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 248);
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(btnAdminView);
            Controls.Add(btnServiceStatus);
            Controls.Add(btnLocalEvents);
            Controls.Add(btnReportIssues);
            Controls.Add(label1);
            Name = "Form1";
            Text = "MainMenu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnReportIssues;
        private Button btnLocalEvents;
        private Button btnServiceStatus;
        private Button btnAdminView;
        private Label label2;
    }
}
