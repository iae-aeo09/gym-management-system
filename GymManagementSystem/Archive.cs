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
    }
}
