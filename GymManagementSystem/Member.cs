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
            AddEditMember form = new AddEditMember();
            this.Hide();
            form.ShowDialog();
            this.Show();
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

            AddEditMember form = new AddEditMember(id);
            this.Hide();
            form.ShowDialog();
            this.Show();
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

            Payments payForm = new Payments(id);
            this.Hide();
            payForm.ShowDialog();
            this.Show();
            LoadMembers();
        }
    }
}
