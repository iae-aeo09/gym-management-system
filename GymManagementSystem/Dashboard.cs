using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            Resize += Dashboard_Resize;
            Shown += Dashboard_Shown;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadStats();
            LoadRecentPayments();
            CheckExpiringMemberships();
        }

        private void LoadStats()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                lblTotalMembers.Text = GetScalar(conn,
                    "SELECT COUNT(*) FROM Members WHERE IsArchived=0").ToString();

                lblActive.Text = GetScalar(conn,
                    @"SELECT COUNT(*) FROM Members
                      WHERE IsArchived=0
                        AND CAST(ExpiryDate AS date) >= CAST(GETDATE() AS date)").ToString();

                lblExpired.Text = GetScalar(conn,
                    @"SELECT COUNT(*) FROM Members
                      WHERE IsArchived=0
                        AND CAST(ExpiryDate AS date) < CAST(GETDATE() AS date)").ToString();

                object rev = GetScalar(conn,
                    "SELECT ISNULL(SUM(Amount),0) FROM Payments");
                lblRevenue.Text = "₱" + Convert.ToDecimal(rev).ToString("N2");
            }
        }

        private object GetScalar(SqlConnection conn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd.ExecuteScalar();
        }

        private void LoadRecentPayments()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT TOP 5 ReferenceNo, MemberName, Amount, 
                             PaymentMethod, PaymentDate, Status 
                             FROM Payments ORDER BY PaymentID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecentPayments.DataSource = dt;
            }
        }

        private void CheckExpiringMemberships()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT FullName, Email, ExpiryDate,
                             DATEDIFF(DAY, GETDATE(), ExpiryDate) AS DaysLeft
                             FROM Members
                             WHERE IsArchived=0
                             AND DATEDIFF(DAY, GETDATE(), ExpiryDate) BETWEEN 0 AND 7";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    pnlExpiryAlert.Visible = true;
                    string msg = "⚠ Expiring soon: ";
                    foreach (DataRow row in dt.Rows)
                    {
                        msg += $"{row["FullName"]} ({row["DaysLeft"]} days) — Notified: {row["Email"]};  ";
                    }
                    lblExpiryMessage.Text = msg;
                }
            }
        }

        private void btnGoMembers_Click(object sender, EventArgs e)
        {
            new Member().Show();
            this.Close();
        }

        private void btnGoPayments_Click(object sender, EventArgs e)
        {
            new Payments().Show();
            this.Close();
        }

        private void btnGoArchive_Click(object sender, EventArgs e)
        {
            new Archive().Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnlExpiryAlert_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Shown(object sender, System.EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void Dashboard_Resize(object sender, System.EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            int contentLeft = panelNav.Right + 12;
            int contentRight = ClientSize.Width - 12;
            int contentWidth = System.Math.Max(860, contentRight - contentLeft);

            panelStats.Left = contentLeft;
            panelStats.Width = contentWidth;
            pnlExpiryAlert.Left = contentLeft;
            pnlExpiryAlert.Width = contentWidth;
            panelGrid.Left = contentLeft;
            panelGrid.Width = contentWidth;

            int statSegment = panelStats.ClientSize.Width / 4;
            PositionStat(label1, lblTotalMembers, statSegment, 0);
            PositionStat(label5, lblActive, statSegment, 1);
            PositionStat(label6, lblExpired, statSegment, 2);
            PositionStat(label7, lblRevenue, statSegment, 3);
        }

        private void PositionStat(Label caption, Label value, int width, int index)
        {
            int startX = (width * index) + 20;
            caption.Left = startX;
            value.Left = startX;
        }

        private void lblActive_Click(object sender, EventArgs e)
        {

        }
    }
}