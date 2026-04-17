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
    public partial class Member : Form
    {
        private int memberId = 0;
        public Member()
        {
            InitializeComponent();
            Resize += Member_Resize;
            Shown += Member_Shown;
        }

        private void Member_Load(object sender, EventArgs e)
        {
            LoadMembers();
        }
        public void LoadMembers(string search = "")
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
SELECT MemberID, FullName, Email, Phone, [Plan], 
       MembershipFee, JoinDate, ExpiryDate,
       CASE
           WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
           ELSE 'Active'
       END AS [Status],
       CASE WHEN IsPaid=1 THEN 'Paid' ELSE 'Unpaid' END AS Payment
FROM Members
WHERE IsArchived=0
  AND (FullName LIKE @s OR Email LIKE @s)
ORDER BY MemberID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@s", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMembers.DataSource = dt;
                lblMemberCount.Text = $"Total: {dt.Rows.Count} members";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadMembers(txtSearch.Text.Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddEditMember form = new AddEditMember())
            {
                form.ShowDialog(this);
            }
            LoadMembers();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Close this form and show dashboard
            this.Close();
            new Dashboard().Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to edit.");
                return;
            }

            int id = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberID"].Value);

            using (AddEditMember form = new AddEditMember(id))
            {
                form.ShowDialog(this);
            }
            LoadMembers();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to archive.");
                return;
            }

            string name = dgvMembers.SelectedRows[0].Cells["FullName"].Value?.ToString() ?? "";
            int id = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberID"].Value);

            DialogResult confirm = MessageBox.Show(
                $"Archive {name}?",
                "Confirm Archive",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Members SET IsArchived=1, Status='Archived' WHERE MemberID=@id",
                        conn);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"{name} has been archived.");
                LoadMembers();
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            int id = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberID"].Value);

            using (Payments payForm = new Payments(id))
            {
                payForm.ShowDialog(this);
            }
            LoadMembers();
        }

        private void Member_Shown(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void Member_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            const int outerMargin = 24;
            const int innerMargin = 24;
            const int rowY = 24;
            const int rowHeight = 33;
            const int gap = 10;
            float scale = Math.Max(1.0f, Math.Min(1.2f, ClientSize.Width / 1366f));

            panelBody.Left = outerMargin;
            panelBody.Top = panelTop.Bottom + 14;
            panelBody.Width = Math.Max(980, ClientSize.Width - (outerMargin * 2));
            panelBody.Height = Math.Max(420, ClientSize.Height - panelBody.Top - outerMargin);

            label2.Location = new Point(innerMargin, rowY + 4);

            int buttonHeight = (int)(34 * scale);
            btnPay.Height = buttonHeight;
            btnArchive.Height = buttonHeight;
            btnUpdate.Height = buttonHeight;
            btnAdd.Height = buttonHeight;

            FitButtonToText(btnAdd, 24);
            FitButtonToText(btnUpdate, 24);
            FitButtonToText(btnArchive, 24);
            FitButtonToText(btnPay, 24);

            int searchX = label2.Right + 10;
            int buttonGroupWidth = btnAdd.Width + btnUpdate.Width + btnArchive.Width + btnPay.Width + (gap * 3);
            int rightEdge = panelBody.ClientSize.Width - innerMargin;
            int buttonsStartX = rightEdge - buttonGroupWidth;
            int maxSearchWidth = Math.Max(220, buttonsStartX - searchX - 140);
            txtSearch.Location = new Point(searchX, rowY + 1);
            txtSearch.Width = Math.Min(360, maxSearchWidth);
            txtSearch.Height = (int)(24 * scale);

            btnPay.Location = new Point(rightEdge - btnPay.Width, rowY);
            btnArchive.Location = new Point(btnPay.Left - gap - btnArchive.Width, rowY);
            btnUpdate.Location = new Point(btnArchive.Left - gap - btnUpdate.Width, rowY);
            btnAdd.Location = new Point(btnUpdate.Left - gap - btnAdd.Width, rowY);

            lblMemberCount.Location = new Point(txtSearch.Right + 12, rowY + 8);
            lblMemberCount.Width = Math.Max(80, btnAdd.Left - lblMemberCount.Left - 10);

            int gridY = rowY + rowHeight + 22;
            dgvMembers.Location = new Point(innerMargin, gridY);
            dgvMembers.Size = new Size(
                Math.Max(860, panelBody.ClientSize.Width - (innerMargin * 2)),
                Math.Max(240, panelBody.ClientSize.Height - gridY - innerMargin));

            var labelFont = new Font("Segoe UI Semibold", 10f * scale, FontStyle.Bold);
            var buttonFont = new Font("Segoe UI Semibold", 9f * scale, FontStyle.Bold);
            label2.Font = labelFont;
            lblMemberCount.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            btnAdd.Font = buttonFont;
            btnUpdate.Font = buttonFont;
            btnArchive.Font = buttonFont;
            btnPay.Font = buttonFont;
        }

        private void FitButtonToText(Button button, int horizontalPadding)
        {
            Size measured = TextRenderer.MeasureText(button.Text, button.Font);
            button.Width = measured.Width + horizontalPadding;
        }
    }
}
