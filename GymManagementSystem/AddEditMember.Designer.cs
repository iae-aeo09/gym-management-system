namespace GymManagementSystem
{
    partial class AddEditMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditMember));
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.cmbPlan = new System.Windows.Forms.ComboBox();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.dtpJoinDate = new System.Windows.Forms.DateTimePicker();
            this.tbExpiryDate = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClearReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelBodyMember = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtExpiry = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBodyMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.btnBack);
            this.panelTop.Controls.Add(this.label7);
            this.panelTop.Controls.Add(this.label8);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1262, 88);
            this.panelTop.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GymManagementSystem.Properties.Resources.vil;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(1163, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 88);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(12, 19);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 42);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(140, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(219, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Create and update gym profiles";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(137, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(237, 37);
            this.label8.TabIndex = 1;
            this.label8.Text = "MEMBER PROFILE";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(243, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(253, 22);
            this.txtName.TabIndex = 0;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(243, 176);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(253, 22);
            this.txtPhone.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(243, 132);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(253, 22);
            this.txtEmail.TabIndex = 2;
            // 
            // cmbPlan
            // 
            this.cmbPlan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.cmbPlan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPlan.ForeColor = System.Drawing.Color.White;
            this.cmbPlan.FormattingEnabled = true;
            this.cmbPlan.Location = new System.Drawing.Point(243, 284);
            this.cmbPlan.Name = "cmbPlan";
            this.cmbPlan.Size = new System.Drawing.Size(253, 24);
            this.cmbPlan.TabIndex = 6;
            this.cmbPlan.SelectedIndexChanged += new System.EventHandler(this.cmbPlan_SelectedIndexChanged_1);
            // 
            // txtFee
            // 
            this.txtFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
            this.txtFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFee.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtFee.Location = new System.Drawing.Point(243, 245);
            this.txtFee.Name = "txtFee";
            this.txtFee.ReadOnly = true;
            this.txtFee.Size = new System.Drawing.Size(253, 22);
            this.txtFee.TabIndex = 5;
            // 
            // dtpJoinDate
            // 
            this.dtpJoinDate.Location = new System.Drawing.Point(243, 209);
            this.dtpJoinDate.Name = "dtpJoinDate";
            this.dtpJoinDate.Size = new System.Drawing.Size(253, 22);
            this.dtpJoinDate.TabIndex = 4;
            this.dtpJoinDate.ValueChanged += new System.EventHandler(this.dtpJoinDate_ValueChanged_1);
            // 
            // tbExpiryDate
            // 
            this.tbExpiryDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(46)))));
            this.tbExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExpiryDate.ForeColor = System.Drawing.Color.Gainsboro;
            this.tbExpiryDate.Location = new System.Drawing.Point(243, 322);
            this.tbExpiryDate.Name = "tbExpiryDate";
            this.tbExpiryDate.ReadOnly = true;
            this.tbExpiryDate.Size = new System.Drawing.Size(253, 22);
            this.tbExpiryDate.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(243, 366);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 34);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClearReset
            // 
            this.btnClearReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnClearReset.FlatAppearance.BorderSize = 0;
            this.btnClearReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClearReset.ForeColor = System.Drawing.Color.White;
            this.btnClearReset.Location = new System.Drawing.Point(243, 366);
            this.btnClearReset.Name = "btnClearReset";
            this.btnClearReset.Size = new System.Drawing.Size(121, 34);
            this.btnClearReset.TabIndex = 9;
            this.btnClearReset.Text = "CLEAR";
            this.btnClearReset.UseVisualStyleBackColor = false;
            this.btnClearReset.Click += new System.EventHandler(this.btnClearReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(375, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 34);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelBodyMember
            // 
            this.panelBodyMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.panelBodyMember.Controls.Add(this.label1);
            this.panelBodyMember.Controls.Add(this.label2);
            this.panelBodyMember.Controls.Add(this.label9);
            this.panelBodyMember.Controls.Add(this.label3);
            this.panelBodyMember.Controls.Add(this.label4);
            this.panelBodyMember.Controls.Add(this.label5);
            this.panelBodyMember.Controls.Add(this.label6);
            this.panelBodyMember.Controls.Add(this.txtName);
            this.panelBodyMember.Controls.Add(this.txtLastName);
            this.panelBodyMember.Controls.Add(this.txtEmail);
            this.panelBodyMember.Controls.Add(this.txtPhone);
            this.panelBodyMember.Controls.Add(this.dtpJoinDate);
            this.panelBodyMember.Controls.Add(this.txtFee);
            this.panelBodyMember.Controls.Add(this.cmbPlan);
            this.panelBodyMember.Controls.Add(this.tbExpiryDate);
            this.panelBodyMember.Controls.Add(this.txtExpiry);
            this.panelBodyMember.Controls.Add(this.btnSave);
            this.panelBodyMember.Controls.Add(this.btnClearReset);
            this.panelBodyMember.Controls.Add(this.btnCancel);
            this.panelBodyMember.Location = new System.Drawing.Point(365, 156);
            this.panelBodyMember.Name = "panelBodyMember";
            this.panelBodyMember.Size = new System.Drawing.Size(550, 446);
            this.panelBodyMember.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(44, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(44, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Email";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Gainsboro;
            this.label9.Location = new System.Drawing.Point(44, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(44, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Phone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(44, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Plan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(44, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Fee";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Gainsboro;
            this.label6.Location = new System.Drawing.Point(44, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Start date";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.ForeColor = System.Drawing.Color.White;
            this.txtLastName.Location = new System.Drawing.Point(243, 88);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(253, 22);
            this.txtLastName.TabIndex = 1;
            // 
            // txtExpiry
            // 
            this.txtExpiry.AutoSize = true;
            this.txtExpiry.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtExpiry.Location = new System.Drawing.Point(44, 325);
            this.txtExpiry.Name = "txtExpiry";
            this.txtExpiry.Size = new System.Drawing.Size(76, 16);
            this.txtExpiry.TabIndex = 15;
            this.txtExpiry.Text = "Expiry Date";
            // 
            // AddEditMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelBodyMember);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "AddEditMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Profile";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddEditMember_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBodyMember.ResumeLayout(false);
            this.panelBodyMember.PerformLayout();
            this.ResumeLayout(false);

            ViltrumTheme.Apply(this);
            ViltrumTheme.WireCard(this.panelBodyMember, true);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cmbPlan;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.DateTimePicker dtpJoinDate;
        private System.Windows.Forms.TextBox tbExpiryDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClearReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelBodyMember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txtExpiry;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtLastName;
    }
}