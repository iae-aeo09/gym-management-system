namespace GymManagementSystem
{
    partial class Member
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Member));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.panelBody = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblMemberCount = new System.Windows.Forms.Label();
            this.btnArchive = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReminders = new System.Windows.Forms.Button();
            this.btnFreeze = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.panelBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.btnBack);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1262, 86);
            this.panelTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(133, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 37);
            this.label1.TabIndex = 8;
            this.label1.Text = "MEMBERS";
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
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dgvMembers
            // 
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMembers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.dgvMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(46)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMembers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMembers.EnableHeadersVisualStyles = false;
            this.dgvMembers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(101)))));
            this.dgvMembers.Location = new System.Drawing.Point(27, 97);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowHeadersVisible = false;
            this.dgvMembers.RowHeadersWidth = 51;
            this.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.Size = new System.Drawing.Size(950, 239);
            this.dgvMembers.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(50)))));
            this.panelBody.Controls.Add(this.label2);
            this.panelBody.Controls.Add(this.btnPay);
            this.panelBody.Controls.Add(this.lblMemberCount);
            this.panelBody.Controls.Add(this.btnArchive);
            this.panelBody.Controls.Add(this.btnUpdate);
            this.panelBody.Controls.Add(this.btnAdd);
            this.panelBody.Controls.Add(this.btnReminders);
            this.panelBody.Controls.Add(this.btnFreeze);
            this.panelBody.Controls.Add(this.txtSearch);
            this.panelBody.Controls.Add(this.dgvMembers);
            this.panelBody.Location = new System.Drawing.Point(129, 100);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(1004, 383);
            this.panelBody.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Search Member";
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.btnPay.FlatAppearance.BorderSize = 0;
            this.btnPay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPay.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(899, 27);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(96, 33);
            this.btnPay.TabIndex = 6;
            this.btnPay.Text = "PAY";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblMemberCount
            // 
            this.lblMemberCount.AutoSize = true;
            this.lblMemberCount.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblMemberCount.Location = new System.Drawing.Point(458, 37);
            this.lblMemberCount.Name = "lblMemberCount";
            this.lblMemberCount.Size = new System.Drawing.Size(44, 16);
            this.lblMemberCount.TabIndex = 3;
            this.lblMemberCount.Text = "label1";
            // 
            // btnArchive
            // 
            this.btnArchive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.btnArchive.FlatAppearance.BorderSize = 0;
            this.btnArchive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArchive.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnArchive.ForeColor = System.Drawing.Color.White;
            this.btnArchive.Location = new System.Drawing.Point(795, 27);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(93, 33);
            this.btnArchive.TabIndex = 5;
            this.btnArchive.Text = "ARCHIVE";
            this.btnArchive.UseVisualStyleBackColor = false;
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(688, 27);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 33);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "RENEW";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(472, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 33);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "ADD MEMBER";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReminders
            // 
            this.btnReminders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnReminders.FlatAppearance.BorderSize = 0;
            this.btnReminders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReminders.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnReminders.ForeColor = System.Drawing.Color.White;
            this.btnReminders.Location = new System.Drawing.Point(588, 27);
            this.btnReminders.Name = "btnReminders";
            this.btnReminders.Size = new System.Drawing.Size(94, 33);
            this.btnReminders.TabIndex = 9;
            this.btnReminders.Text = "REMIND";
            this.btnReminders.UseVisualStyleBackColor = false;
            this.btnReminders.Click += new System.EventHandler(this.btnReminders_Click);
            // 
            // btnFreeze
            // 
            this.btnFreeze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(76)))), ((int)(((byte)(94)))));
            this.btnFreeze.FlatAppearance.BorderSize = 0;
            this.btnFreeze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreeze.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnFreeze.ForeColor = System.Drawing.Color.White;
            this.btnFreeze.Location = new System.Drawing.Point(688, 27);
            this.btnFreeze.Name = "btnFreeze";
            this.btnFreeze.Size = new System.Drawing.Size(101, 33);
            this.btnFreeze.TabIndex = 10;
            this.btnFreeze.Text = "FREEZE";
            this.btnFreeze.UseVisualStyleBackColor = false;
            this.btnFreeze.Click += new System.EventHandler(this.btnFreeze_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(31)))), ((int)(((byte)(40)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(164, 30);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(278, 22);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // Member
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(23)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Member";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Member_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.ResumeLayout(false);

            ViltrumTheme.Apply(this);
            ViltrumTheme.WireCard(this.panelBody, true);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblMemberCount;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnArchive;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnReminders;
        private System.Windows.Forms.Button btnFreeze;
    }
}