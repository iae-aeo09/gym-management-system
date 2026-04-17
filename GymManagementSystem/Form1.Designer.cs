namespace GymManagementSystem
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelTop = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbGymTitle = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.panelLoginCard = new System.Windows.Forms.Panel();
            this.lblCardHeader = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.panelLoginCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(53)))), ((int)(((byte)(67)))));
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.lbGymTitle);
            this.panelTop.Controls.Add(this.pbIcon);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1262, 96);
            this.panelTop.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(74)))), ((int)(((byte)(72)))));
            this.label4.Location = new System.Drawing.Point(534, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "ONLY THE STRONG SURVIVE";
            // 
            // lbGymTitle
            // 
            this.lbGymTitle.AutoSize = true;
            this.lbGymTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lbGymTitle.ForeColor = System.Drawing.Color.White;
            this.lbGymTitle.Location = new System.Drawing.Point(444, 10);
            this.lbGymTitle.Name = "lbGymTitle";
            this.lbGymTitle.Size = new System.Drawing.Size(411, 50);
            this.lbGymTitle.TabIndex = 1;
            this.lbGymTitle.Text = "VILTRUM GYM SYSTEM";
            // 
            // pbIcon
            // 
            this.pbIcon.BackgroundImage = global::GymManagementSystem.Properties.Resources.vil;
            this.pbIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbIcon.Location = new System.Drawing.Point(0, 0);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(99, 96);
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // panelLoginCard
            // 
            this.panelLoginCard.BackColor = System.Drawing.Color.White;
            this.panelLoginCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLoginCard.Controls.Add(this.lblCardHeader);
            this.panelLoginCard.Controls.Add(this.lbUsername);
            this.panelLoginCard.Controls.Add(this.label3);
            this.panelLoginCard.Controls.Add(this.txtUsername);
            this.panelLoginCard.Controls.Add(this.txtPassword);
            this.panelLoginCard.Controls.Add(this.chkShowPassword);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.lblError);
            this.panelLoginCard.Location = new System.Drawing.Point(391, 179);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Size = new System.Drawing.Size(498, 362);
            this.panelLoginCard.TabIndex = 2;
            this.panelLoginCard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLoginCard_Paint);
            // 
            // lblCardHeader
            // 
            this.lblCardHeader.AutoSize = true;
            this.lblCardHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblCardHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.lblCardHeader.Location = new System.Drawing.Point(149, 22);
            this.lblCardHeader.Name = "lblCardHeader";
            this.lblCardHeader.Size = new System.Drawing.Size(216, 32);
            this.lblCardHeader.TabIndex = 0;
            this.lblCardHeader.Text = "LOGIN TO SYSTEM";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lbUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lbUsername.Location = new System.Drawing.Point(57, 86);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(100, 23);
            this.lbUsername.TabIndex = 1;
            this.lbUsername.Text = "USERNAME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.label3.Location = new System.Drawing.Point(57, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "PASSWORD";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUsername.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.Location = new System.Drawing.Point(61, 112);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(377, 32);
            this.txtUsername.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(61, 186);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(377, 32);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.ForeColor = System.Drawing.Color.DimGray;
            this.chkShowPassword.Location = new System.Drawing.Point(61, 236);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(125, 20);
            this.chkShowPassword.TabIndex = 5;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(61, 276);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(377, 42);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblError.Location = new System.Drawing.Point(58, 330);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 16);
            this.lblError.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelLoginCard);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.panelLoginCard.ResumeLayout(false);
            this.panelLoginCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbGymTitle;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Panel panelLoginCard;
        private System.Windows.Forms.Label lblCardHeader;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblError;
    }
}

