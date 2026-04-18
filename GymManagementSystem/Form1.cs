using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Resize += Form1_Resize;
            Shown += Form1_Shown;
            ConfigureButtonTheme(btnLogin, Color.FromArgb(188, 44, 44), Color.FromArgb(210, 60, 60));
        }

        

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Missing input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                // If your DB actually stores a password hash, change this to SELECT PasswordHash and verify the hash.
                string query = "SELECT COUNT(*) FROM Users WHERE Username=@u AND PasswordHash=@p";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password); // only ok if DB stores plaintext (not recommended)

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        var existingDashboard = Application.OpenForms.OfType<Dashboard>().FirstOrDefault();
                        if (existingDashboard != null)
                        {
                            existingDashboard.Show();
                            existingDashboard.BringToFront();
                        }
                        else
                        {
                            new Dashboard().Show();
                        }
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            int topOffset = panelTop.Bottom;
            int availableHeight = ClientSize.Height - topOffset;
            float scale = Math.Max(1.0f, Math.Min(1.18f, ClientSize.Width / 1366f));
            panelLoginCard.Width = Clamp((int)(ClientSize.Width * 0.40), 480, 760);
            panelLoginCard.Height = Clamp((int)(availableHeight * 0.56), 380, 540);
            panelLoginCard.Left = Math.Max(16, (ClientSize.Width - panelLoginCard.Width) / 2);
            panelLoginCard.Top = topOffset + Math.Max(16, (availableHeight - panelLoginCard.Height) / 2);

            lbGymTitle.Left = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - lbGymTitle.Width) / 2);
            label4.Left = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - label4.Width) / 2);

            int left = 56;
            int controlWidth = Math.Max(280, panelLoginCard.Width - (left * 2));
            int y = Math.Max(18, (panelLoginCard.Height - 316) / 2);

            lblCardHeader.Left = Math.Max(16, (panelLoginCard.Width - lblCardHeader.Width) / 2);
            lblCardHeader.Top = y;

            lbUsername.Location = new Point(left, y + 60);
            txtUsername.Location = new Point(left, y + 86);
            txtUsername.Width = controlWidth;

            label3.Location = new Point(left, y + 136);
            txtPassword.Location = new Point(left, y + 162);
            txtPassword.Width = controlWidth;

            chkShowPassword.Location = new Point(left, y + 212);
            btnLogin.Location = new Point(left, y + 248);
            btnLogin.Width = controlWidth;

            lblError.Location = new Point(left, y + 300);
            lblError.MaximumSize = new Size(controlWidth, 0);

            lbGymTitle.Font = new Font("Segoe UI Semibold", 20f * scale, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 10f * scale, FontStyle.Bold);
            lblCardHeader.Font = new Font("Segoe UI Semibold", 14f * scale, FontStyle.Bold);
            lbUsername.Font = new Font("Segoe UI Semibold", 9.5f * scale, FontStyle.Bold);
            label3.Font = lbUsername.Font;
            txtUsername.Font = new Font("Segoe UI", 10f * scale, FontStyle.Regular);
            txtPassword.Font = txtUsername.Font;
            chkShowPassword.Font = new Font("Segoe UI", 8.8f * scale, FontStyle.Regular);
            btnLogin.Font = new Font("Segoe UI Semibold", 10f * scale, FontStyle.Bold);
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Min(max, Math.Max(min, value));
        }

        private void panelLoginCard_Paint(object sender, PaintEventArgs e)
        {
            // Intentionally empty: dynamic centering is handled via form resize events.
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConfigureButtonTheme(Button button, Color baseColor, Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
            button.BackColor = baseColor;
        }
    }
}
