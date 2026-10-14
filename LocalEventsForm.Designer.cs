namespace MunicipalServicesApp
{
    partial class LocalEventsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBack = new Button();
            label1 = new Label();
            cmbCategory = new ComboBox();
            label2 = new Label();
            dtpDate = new DateTimePicker();
            btnSearch = new Button();
            btnClear = new Button();
            label3 = new Label();
            dgvEvents = new DataGridView();
            label4 = new Label();
            lblRecommendation = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(140, 54);
            btnBack.TabIndex = 0;
            btnBack.Text = "Back to Main Menu";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 91);
            label1.Name = "label1";
            label1.Size = new Size(175, 25);
            label1.TabIndex = 1;
            label1.Text = "Search By Category";
            // 
            // cmbCategory
            // 
            cmbCategory.Anchor = AnchorStyles.None;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(38, 129);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(200, 23);
            cmbCategory.TabIndex = 2;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(23, 197);
            label2.Name = "label2";
            label2.Size = new Size(138, 25);
            label2.TabIndex = 3;
            label2.Text = "Search By Date";
            // 
            // dtpDate
            // 
            dtpDate.Anchor = AnchorStyles.None;
            dtpDate.Location = new Point(38, 236);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(200, 23);
            dtpDate.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.None;
            btnSearch.Location = new Point(38, 298);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(87, 35);
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.None;
            btnClear.Location = new Point(151, 298);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(87, 35);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(198, 12);
            label3.Name = "label3";
            label3.Size = new Size(457, 40);
            label3.TabIndex = 7;
            label3.Text = "Local Events and Announcements";
            // 
            // dgvEvents
            // 
            dgvEvents.Anchor = AnchorStyles.None;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.BackgroundColor = SystemColors.ActiveCaption;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEvents.Location = new Point(324, 111);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.Size = new Size(464, 257);
            dgvEvents.TabIndex = 8;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.MidnightBlue;
            label4.Location = new Point(12, 360);
            label4.Name = "label4";
            label4.Size = new Size(236, 30);
            label4.TabIndex = 9;
            label4.Text = "Recommended For You";
            // 
            // lblRecommendation
            // 
            lblRecommendation.Anchor = AnchorStyles.None;
            lblRecommendation.AutoSize = true;
            lblRecommendation.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRecommendation.Location = new Point(48, 390);
            lblRecommendation.Name = "lblRecommendation";
            lblRecommendation.Size = new Size(150, 21);
            lblRecommendation.TabIndex = 10;
            lblRecommendation.Text = "lblRecommendation";
            // 
            // LocalEventsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(lblRecommendation);
            Controls.Add(label4);
            Controls.Add(dgvEvents);
            Controls.Add(label3);
            Controls.Add(btnClear);
            Controls.Add(btnSearch);
            Controls.Add(dtpDate);
            Controls.Add(label2);
            Controls.Add(cmbCategory);
            Controls.Add(label1);
            Controls.Add(btnBack);
            Name = "LocalEventsForm";
            Text = "LocalEventsForm";
            Load += LocalEventsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Label label1;
        private ComboBox cmbCategory;
        private Label label2;
        private DateTimePicker dtpDate;
        private Button btnSearch;
        private Button btnClear;
        private Label label3;
        private DataGridView dgvEvents;
        private Label label4;
        private Label lblRecommendation;
    }
}