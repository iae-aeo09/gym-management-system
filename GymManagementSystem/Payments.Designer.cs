namespace GymManagementSystem
{
    partial class Payments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payments));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelPayment = new System.Windows.Forms.Panel();
            this.lblRef = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewReceipt = new System.Windows.Forms.Button();
            this.btnRecordPayment = new System.Windows.Forms.Button();
            this.dtpPayDate = new System.Windows.Forms.DateTimePicker();
            this.cmbMember = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.panelHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.label7);
            this.panelHeader.Controls.Add(this.label6);
            this.panelHeader.Controls.Add(this.btnBack);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1280, 78);
            this.panelHeader.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Gainsboro;
            this.label7.Location = new System.Drawing.Point(147, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Record, verify, and track member dues";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(144, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 38);
            this.label6.TabIndex = 1;
            this.label6.Text = "PAYMENTS";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(13, 23);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 42);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelPayment
            // 
            this.panelPayment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.panelPayment.Controls.Add(this.lblRef);
            this.panelPayment.Controls.Add(this.label2);
            this.panelPayment.Controls.Add(this.label5);
            this.panelPayment.Controls.Add(this.label4);
            this.panelPayment.Controls.Add(this.label3);
            this.panelPayment.Controls.Add(this.label1);
            this.panelPayment.Controls.Add(this.btnViewReceipt);
            this.panelPayment.Controls.Add(this.btnRecordPayment);
            this.panelPayment.Controls.Add(this.dtpPayDate);
            this.panelPayment.Controls.Add(this.cmbMember);
            this.panelPayment.Controls.Add(this.txtAmount);
            this.panelPayment.Controls.Add(this.cmbMethod);
            this.panelPayment.Location = new System.Drawing.Point(86, 102);
            this.panelPayment.Margin = new System.Windows.Forms.Padding(2);
            this.panelPayment.Name = "panelPayment";
            this.panelPayment.Size = new System.Drawing.Size(1108, 202);
            this.panelPayment.TabIndex = 1;
            // 
            // lblRef
            // 
            this.lblRef.AutoSize = true;
            this.lblRef.ForeColor = System.Drawing.Color.Silver;
            this.lblRef.Location = new System.Drawing.Point(886, 12);
            this.lblRef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRef.Name = "lblRef";
            this.lblRef.Size = new System.Drawing.Size(50, 16);
            this.lblRef.TabIndex = 8;
            this.lblRef.Text = "Ref: ----";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Payment Processing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(551, 126);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Payment Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(26, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Payment Method:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(551, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(26, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Member Name:";
            // 
            // btnViewReceipt
            // 
            this.btnViewReceipt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnViewReceipt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(140)))), ((int)(((byte)(160)))));
            this.btnViewReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReceipt.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewReceipt.ForeColor = System.Drawing.Color.White;
            this.btnViewReceipt.Location = new System.Drawing.Point(460, 157);
            this.btnViewReceipt.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewReceipt.Name = "btnViewReceipt";
            this.btnViewReceipt.Size = new System.Drawing.Size(418, 33);
            this.btnViewReceipt.TabIndex = 6;
            this.btnViewReceipt.Text = "Open Payment Receipt";
            this.btnViewReceipt.UseVisualStyleBackColor = false;
            this.btnViewReceipt.Click += new System.EventHandler(this.btnViewReceipt_Click);
            // 
            // btnRecordPayment
            // 
            this.btnRecordPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.btnRecordPayment.FlatAppearance.BorderSize = 0;
            this.btnRecordPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecordPayment.ForeColor = System.Drawing.Color.White;
            this.btnRecordPayment.Location = new System.Drawing.Point(30, 157);
            this.btnRecordPayment.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecordPayment.Name = "btnRecordPayment";
            this.btnRecordPayment.Size = new System.Drawing.Size(406, 33);
            this.btnRecordPayment.TabIndex = 5;
            this.btnRecordPayment.Text = "Record Payment + Mark as Paid";
            this.btnRecordPayment.UseVisualStyleBackColor = false;
            this.btnRecordPayment.Click += new System.EventHandler(this.btnRecordPayment_Click_1);
            // 
            // dtpPayDate
            // 
            this.dtpPayDate.CalendarForeColor = System.Drawing.Color.White;
            this.dtpPayDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.dtpPayDate.Checked = false;
            this.dtpPayDate.Location = new System.Drawing.Point(656, 122);
            this.dtpPayDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpPayDate.Name = "dtpPayDate";
            this.dtpPayDate.Size = new System.Drawing.Size(222, 22);
            this.dtpPayDate.TabIndex = 4;
            this.dtpPayDate.ValueChanged += new System.EventHandler(this.dtpPayDate_ValueChanged);
            // 
            // cmbMember
            // 
            this.cmbMember.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.cmbMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMember.ForeColor = System.Drawing.Color.White;
            this.cmbMember.FormattingEnabled = true;
            this.cmbMember.Location = new System.Drawing.Point(210, 69);
            this.cmbMember.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMember.Name = "cmbMember";
            this.cmbMember.Size = new System.Drawing.Size(273, 24);
            this.cmbMember.TabIndex = 3;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.ForeColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(656, 70);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(222, 22);
            this.txtAmount.TabIndex = 2;
            // 
            // cmbMethod
            // 
            this.cmbMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.cmbMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMethod.ForeColor = System.Drawing.Color.White;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Location = new System.Drawing.Point(210, 121);
            this.cmbMethod.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(273, 24);
            this.cmbMethod.TabIndex = 1;
            this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.cmbMethod_SelectedIndexChanged);
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.dgvPayments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(46)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPayments.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPayments.EnableHeadersVisualStyles = false;
            this.dgvPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(101)))));
            this.dgvPayments.Location = new System.Drawing.Point(24, 52);
            this.dgvPayments.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPayments.MultiSelect = false;
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(46)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.RowHeadersWidth = 51;
            this.dgvPayments.RowTemplate.Height = 28;
            this.dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayments.Size = new System.Drawing.Size(951, 324);
            this.dgvPayments.TabIndex = 0;
            // 
            // panelHistory
            // 
            this.panelHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.panelHistory.Controls.Add(this.label8);
            this.panelHistory.Controls.Add(this.dgvPayments);
            this.panelHistory.Location = new System.Drawing.Point(86, 319);
            this.panelHistory.Margin = new System.Windows.Forms.Padding(2);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(1108, 390);
            this.panelHistory.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(26, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(155, 25);
            this.label8.TabIndex = 1;
            this.label8.Text = "Payment History";
            // 
            // Payments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelHistory);
            this.Controls.Add(this.panelPayment);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Payments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payments";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Payments_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelPayment.ResumeLayout(false);
            this.panelPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            this.ResumeLayout(false);

            ViltrumTheme.Apply(this);
            ViltrumTheme.WireCard(this.panelPayment, true);
            ViltrumTheme.WireCard(this.panelHistory, true);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelPayment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cmbMember;
        private System.Windows.Forms.DateTimePicker dtpPayDate;
        private System.Windows.Forms.Button btnRecordPayment;
        private System.Windows.Forms.Button btnViewReceipt;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRef;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.Label label8;
    }
}