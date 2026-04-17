using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Archive : Form
    {
        public Archive()
        {
            InitializeComponent();
            Resize += Archive_Resize;
            Shown += Archive_Shown;
        }

        private void Archive_Load(object sender, EventArgs e)
        {


            LoadArchived();
        }
        private void LoadArchived()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
    @"SELECT MemberID, FullName, Email, Phone, [Plan], MembershipFee, JoinDate, ExpiryDate
      FROM Members
      WHERE IsArchived = 1",
    conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvArchived.DataSource = dt;
                lblCount.Text = $"Total: {dt.Rows.Count}";
            }
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (dgvArchived.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to restore.");
                return;
            }

            string name = dgvArchived.SelectedRows[0].Cells["FullName"].Value.ToString();
            int id = Convert.ToInt32(dgvArchived.SelectedRows[0].Cells["MemberID"].Value);

            DialogResult confirm = MessageBox.Show(
                $"Restore {name} to active members?", "Confirm Restore",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        @"UPDATE Members
                          SET IsArchived=0,
                              Status = CASE 
                                  WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
                                  ELSE 'Active'
                              END
                          WHERE MemberID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show($"{name} restored to Member List.");
                LoadArchived();
            }
        }

      

        private void btnBack_Click_2(object sender, EventArgs e)
        {
            this.Close();
            new Dashboard().Show();
        }

        private void Archive_Shown(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void Archive_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            const int outerMargin = 24;
            const int innerMargin = 24;
            const int rowY = 24;
            float scale = Math.Max(1.0f, Math.Min(1.2f, ClientSize.Width / 1366f));

            panelBody.Left = outerMargin;
            panelBody.Top = panelTop.Bottom + 14;
            panelBody.Width = Math.Max(980, ClientSize.Width - (outerMargin * 2));
            panelBody.Height = Math.Max(460, ClientSize.Height - panelBody.Top - outerMargin);

            label2.Location = new Point(innerMargin, rowY + 6);
            lblCount.Location = new Point(label2.Right + 12, rowY + 9);

            btnRestore.Location = new Point(panelBody.ClientSize.Width - innerMargin - btnRestore.Width, rowY);
            btnRestore.Height = (int)(36 * scale);

            int gridY = rowY + btnRestore.Height + 20;
            dgvArchived.Location = new Point(innerMargin, gridY);
            dgvArchived.Size = new Size(
                Math.Max(860, panelBody.ClientSize.Width - (innerMargin * 2)),
                Math.Max(300, panelBody.ClientSize.Height - gridY - innerMargin));

            label2.Font = new Font("Segoe UI Semibold", 10f * scale, FontStyle.Bold);
            lblCount.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            btnRestore.Font = new Font("Segoe UI Semibold", 10f * scale, FontStyle.Bold);
        }
    }
}
