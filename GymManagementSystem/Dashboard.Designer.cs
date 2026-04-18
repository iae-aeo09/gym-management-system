namespace GymManagementSystem
{
    partial class Dashboard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.panelTop = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelNav = new System.Windows.Forms.Panel();
            this.btnGoMembers = new System.Windows.Forms.Button();
            this.btnGoPayments = new System.Windows.Forms.Button();
            this.btnGoArchive = new System.Windows.Forms.Button();
            this.lblTotalMembers = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.lblExpired = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.dgvRecentPayments = new System.Windows.Forms.DataGridView();
            this.panelStats = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblExpiryMessage = new System.Windows.Forms.Label();
            this.pnlExpiryAlert = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.panelNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentPayments)).BeginInit();
            this.panelStats.SuspendLayout();
            this.panelGrid.SuspendLayout();
            this.pnlExpiryAlert.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(43)))), ((int)(((byte)(58)))));
            this.panelTop.Controls.Add(this.pbIcon);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.label9);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1262, 90);
            this.panelTop.TabIndex = 0;
            // 
            // pbIcon
            // 
            this.pbIcon.BackgroundImage = global::GymManagementSystem.Properties.Resources.vil;
            this.pbIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbIcon.Location = new System.Drawing.Point(0, 0);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(99, 90);
            this.pbIcon.TabIndex = 15;
            this.pbIcon.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cascadia Code", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(539, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(340, 45);
            this.label4.TabIndex = 12;
            this.label4.Text = "TODAY, WE BECOME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cascadia Code", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(885, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 45);
            this.label2.TabIndex = 16;
            this.label2.Text = "STRONGER.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cascadia Code", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Gainsboro;
            this.label9.Location = new System.Drawing.Point(539, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(460, 45);
            this.label9.TabIndex = 17;
            this.label9.Text = "NO LIMITS. NO EXCUSES.";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(28, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 51);
            this.label3.TabIndex = 11;
            this.label3.Text = "DASHBOARD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(28, 271);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(139, 51);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(66)))));
            this.panelNav.Controls.Add(this.btnGoMembers);
            this.panelNav.Controls.Add(this.label3);
            this.panelNav.Controls.Add(this.btnLogout);
            this.panelNav.Controls.Add(this.btnGoPayments);
            this.panelNav.Controls.Add(this.btnGoArchive);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNav.Location = new System.Drawing.Point(0, 90);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(202, 583);
            this.panelNav.TabIndex = 1;
            // 
            // btnGoMembers
            // 
            this.btnGoMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(91)))), ((int)(((byte)(109)))));
            this.btnGoMembers.FlatAppearance.BorderSize = 0;
            this.btnGoMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoMembers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoMembers.ForeColor = System.Drawing.Color.White;
            this.btnGoMembers.Location = new System.Drawing.Point(28, 77);
            this.btnGoMembers.Name = "btnGoMembers";
            this.btnGoMembers.Size = new System.Drawing.Size(139, 51);
            this.btnGoMembers.TabIndex = 7;
            this.btnGoMembers.Text = "MEMBERS";
            this.btnGoMembers.UseVisualStyleBackColor = false;
            this.btnGoMembers.Click += new System.EventHandler(this.btnGoMembers_Click);
            // 
            // btnGoPayments
            // 
            this.btnGoPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(91)))), ((int)(((byte)(109)))));
            this.btnGoPayments.FlatAppearance.BorderSize = 0;
            this.btnGoPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoPayments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoPayments.ForeColor = System.Drawing.Color.White;
            this.btnGoPayments.Location = new System.Drawing.Point(28, 140);
            this.btnGoPayments.Name = "btnGoPayments";
            this.btnGoPayments.Size = new System.Drawing.Size(139, 51);
            this.btnGoPayments.TabIndex = 8;
            this.btnGoPayments.Text = "PAYMENTS";
            this.btnGoPayments.UseVisualStyleBackColor = false;
            this.btnGoPayments.Click += new System.EventHandler(this.btnGoPayments_Click);
            // 
            // btnGoArchive
            // 
            this.btnGoArchive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(91)))), ((int)(((byte)(109)))));
            this.btnGoArchive.FlatAppearance.BorderSize = 0;
            this.btnGoArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoArchive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoArchive.ForeColor = System.Drawing.Color.White;
            this.btnGoArchive.Location = new System.Drawing.Point(28, 205);
            this.btnGoArchive.Name = "btnGoArchive";
            this.btnGoArchive.Size = new System.Drawing.Size(139, 51);
            this.btnGoArchive.TabIndex = 9;
            this.btnGoArchive.Text = "ARCHIVE";
            this.btnGoArchive.UseVisualStyleBackColor = false;
            this.btnGoArchive.Click += new System.EventHandler(this.btnGoArchive_Click);
            // 
            // lblTotalMembers
            // 
            this.lblTotalMembers.AutoSize = true;
            this.lblTotalMembers.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalMembers.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTotalMembers.Location = new System.Drawing.Point(39, 59);
            this.lblTotalMembers.Name = "lblTotalMembers";
            this.lblTotalMembers.Size = new System.Drawing.Size(27, 32);
            this.lblTotalMembers.TabIndex = 0;
            this.lblTotalMembers.Text = "0";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblActive.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblActive.Location = new System.Drawing.Point(263, 59);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(27, 32);
            this.lblActive.TabIndex = 1;
            this.lblActive.Text = "0";
            this.lblActive.Click += new System.EventHandler(this.lblActive_Click);
            // 
            // lblExpired
            // 
            this.lblExpired.AutoSize = true;
            this.lblExpired.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblExpired.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblExpired.Location = new System.Drawing.Point(465, 59);
            this.lblExpired.Name = "lblExpired";
            this.lblExpired.Size = new System.Drawing.Size(27, 32);
            this.lblExpired.TabIndex = 2;
            this.lblExpired.Text = "0";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblRevenue.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblRevenue.Location = new System.Drawing.Point(660, 59);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(27, 32);
            this.lblRevenue.TabIndex = 3;
            this.lblRevenue.Text = "0";
            // 
            // dgvRecentPayments
            // 
            this.dgvRecentPayments.AllowUserToAddRows = false;
            this.dgvRecentPayments.AllowUserToDeleteRows = false;
            this.dgvRecentPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentPayments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(45)))), ((int)(((byte)(61)))));
            this.dgvRecentPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecentPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(89)))), ((int)(((byte)(107)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentPayments.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecentPayments.EnableHeadersVisualStyles = false;
            this.dgvRecentPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(101)))));
            this.dgvRecentPayments.Location = new System.Drawing.Point(24, 52);
            this.dgvRecentPayments.Name = "dgvRecentPayments";
            this.dgvRecentPayments.ReadOnly = true;
            this.dgvRecentPayments.RowHeadersVisible = false;
            this.dgvRecentPayments.RowHeadersWidth = 51;
            this.dgvRecentPayments.RowTemplate.Height = 28;
            this.dgvRecentPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentPayments.Size = new System.Drawing.Size(990, 280);
            this.dgvRecentPayments.TabIndex = 4;
            // 
            // panelStats
            // 
            this.panelStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(57)))), ((int)(((byte)(74)))));
            this.panelStats.Controls.Add(this.label1);
            this.panelStats.Controls.Add(this.label5);
            this.panelStats.Controls.Add(this.label6);
            this.panelStats.Controls.Add(this.label7);
            this.panelStats.Controls.Add(this.lblTotalMembers);
            this.panelStats.Controls.Add(this.lblActive);
            this.panelStats.Controls.Add(this.lblExpired);
            this.panelStats.Controls.Add(this.lblRevenue);
            this.panelStats.Location = new System.Drawing.Point(208, 104);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(1042, 108);
            this.panelStats.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(42, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total Members";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(266, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Active";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Gainsboro;
            this.label6.Location = new System.Drawing.Point(465, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Expired";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(660, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Revenue";
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(57)))), ((int)(((byte)(74)))));
            this.panelGrid.Controls.Add(this.label8);
            this.panelGrid.Controls.Add(this.dgvRecentPayments);
            this.panelGrid.Location = new System.Drawing.Point(208, 286);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(1042, 349);
            this.panelGrid.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(24, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 25);
            this.label8.TabIndex = 5;
            this.label8.Text = "Recent Payments";
            // 
            // lblExpiryMessage
            // 
            this.lblExpiryMessage.AutoSize = true;
            this.lblExpiryMessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblExpiryMessage.Location = new System.Drawing.Point(12, 16);
            this.lblExpiryMessage.Name = "lblExpiryMessage";
            this.lblExpiryMessage.Size = new System.Drawing.Size(44, 16);
            this.lblExpiryMessage.TabIndex = 11;
            this.lblExpiryMessage.Text = "label4";
            // 
            // pnlExpiryAlert
            // 
            this.pnlExpiryAlert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlExpiryAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.pnlExpiryAlert.Controls.Add(this.lblExpiryMessage);
            this.pnlExpiryAlert.Location = new System.Drawing.Point(208, 224);
            this.pnlExpiryAlert.Name = "pnlExpiryAlert";
            this.pnlExpiryAlert.Size = new System.Drawing.Size(1042, 49);
            this.pnlExpiryAlert.TabIndex = 5;
            this.pnlExpiryAlert.Visible = false;
            this.pnlExpiryAlert.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlExpiryAlert_Paint);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.pnlExpiryAlert);
            this.Controls.Add(this.panelNav);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.panelNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentPayments)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            this.panelGrid.PerformLayout();
            this.pnlExpiryAlert.ResumeLayout(false);
            this.pnlExpiryAlert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Label lblTotalMembers;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblExpired;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.DataGridView dgvRecentPayments;
        private System.Windows.Forms.Button btnGoMembers;
        private System.Windows.Forms.Button btnGoPayments;
        private System.Windows.Forms.Button btnGoArchive;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblExpiryMessage;
        private System.Windows.Forms.Panel pnlExpiryAlert;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Label label9;
    }
}