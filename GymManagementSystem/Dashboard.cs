using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Dashboard : Form
    {
        private readonly Panel panelContentHost;
        private readonly Label lblAnalytics;
        private readonly ComboBox cmbDateFilter;
        private Form activeContentForm;

        public Dashboard()
        {
            InitializeComponent();
            Resize += Dashboard_Resize;
            Shown += Dashboard_Shown;
            ConfigureButtonTheme(btnGoMembers, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnGoPayments, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnGoArchive, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnLogout, ViltrumTheme.Danger, ViltrumTheme.DangerHover);

            panelContentHost = new Panel
            {
                BackColor = panelGrid.BackColor,
                Visible = false
            };
            Controls.Add(panelContentHost);
            panelContentHost.BringToFront();
            panelTop.BringToFront();
            panelNav.BringToFront();

            lblAnalytics = new Label
            {
                AutoSize = true,
                ForeColor = System.Drawing.Color.Gainsboro,
                Font = new System.Drawing.Font("Segoe UI", 9F),
                Text = "Analytics"
            };
            panelGrid.Controls.Add(lblAnalytics);
            lblAnalytics.BringToFront();

            cmbDateFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = ViltrumTheme.Input,
                ForeColor = ViltrumTheme.TextPrimary,
                FlatStyle = FlatStyle.Flat
            };
            cmbDateFilter.Items.AddRange(new object[] { "Today", "Last 7 Days", "This Month", "All Time" });
            cmbDateFilter.SelectedIndex = 2;
            cmbDateFilter.SelectedIndexChanged += FilterChanged;
            panelStats.Controls.Add(cmbDateFilter);
            cmbDateFilter.BringToFront();

            label3.Cursor = Cursors.Hand;
            label3.Click += (s, e) => ShowDashboardHome();
            label3.ForeColor = ViltrumTheme.TextPrimary;
            label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = ViltrumTheme.TextMuted;
            label2.ForeColor = ViltrumTheme.TextMuted;
            label9.ForeColor = ViltrumTheme.TextMuted;

            // Stronger gym-style header typography (override designer font).
            label4.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            label9.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);

            // Expiry alert text should wrap and stay fully visible.
            pnlExpiryAlert.Padding = new Padding(12, 8, 12, 8);
            lblExpiryMessage.AutoSize = false;
            lblExpiryMessage.Dock = DockStyle.Fill;
            lblExpiryMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblExpiryMessage.MaximumSize = new System.Drawing.Size(0, 0);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DBConnection.EnsureFeatureSchema();
            DBConnection.AutoUnfreezeExpiredMembers();
            LoadStats();
            LoadRecentPayments();
            CheckExpiringMemberships();
            LoadAnalytics();
        }
        //Show Members
        private void LoadStats()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string dateFilter = GetPaymentsDateFilterSql();

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
                    $"SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE {dateFilter}");
                lblRevenue.Text = "₱" + Convert.ToDecimal(rev).ToString("N2");
            }
        }

        private object GetScalar(SqlConnection conn, string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            return cmd.ExecuteScalar();
        }
        //Show Recent Payments
        private void LoadRecentPayments()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string dateFilter = GetPaymentsDateFilterSql();
                string query = @"
SELECT TOP 5
    p.ReferenceNo,
    p.MemberName,
    p.Amount,
    p.PaymentMethod,
    p.PaymentDate,
    m.ExpiryDate,
    CASE
        WHEN m.MemberID IS NULL THEN ISNULL(p.Status, 'Paid')
        WHEN m.IsFrozen = 1 THEN 'Frozen'
        WHEN CAST(m.ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired - Inactive'
        WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) BETWEEN 0 AND 7
            THEN CASE WHEN m.IsPaid = 1 THEN 'Expiring - Paid' ELSE 'Expiring - Unpaid' END
        ELSE CASE WHEN m.IsPaid = 1 THEN 'Active - Paid' ELSE 'Active - Unpaid' END
    END AS Status
FROM Payments p
LEFT JOIN Members m ON p.MemberID = m.MemberID
WHERE " + dateFilter + @"
ORDER BY p.PaymentID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecentPayments.DataSource = dt;
            }
        }
        //Check Expiring Members
        private void CheckExpiringMemberships()
        {
            pnlExpiryAlert.Visible = false;
            lblExpiryMessage.Text = string.Empty;

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT FullName, Email, ExpiryDate,
                             DATEDIFF(DAY, GETDATE(), ExpiryDate) AS DaysLeft
                             FROM Members
                             WHERE IsArchived=0
                             AND IsFrozen=0
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
            ShowEmbeddedContent(new Member());
        }

        private void btnGoPayments_Click(object sender, EventArgs e)
        {
            ShowEmbeddedContent(new Payments());
        }

        private void btnGoArchive_Click(object sender, EventArgs e)
        {
            ShowEmbeddedContent(new Archive());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
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
            int sectionGap = 10;
            int navButtonWidth = Math.Max(128, panelNav.Width - 56);
            int navX = (panelNav.Width - navButtonWidth) / 2;
            int navTop = 18;
            int navGap = 10;
            int navBtnH = 44;

            panelStats.Top = panelTop.Bottom + 12;
            panelStats.Left = contentLeft;
            panelStats.Width = contentWidth;

            pnlExpiryAlert.Left = contentLeft;
            pnlExpiryAlert.Width = contentWidth;
            pnlExpiryAlert.Top = panelStats.Bottom + sectionGap;
            pnlExpiryAlert.Height = 58;

            panelGrid.Left = contentLeft;
            panelGrid.Width = contentWidth;
            panelGrid.Top = (pnlExpiryAlert.Visible ? pnlExpiryAlert.Bottom : panelStats.Bottom) + sectionGap;
            panelGrid.Height = Math.Max(220, ClientSize.Height - panelGrid.Top - 12);

            panelContentHost.Left = contentLeft;
            panelContentHost.Top = panelStats.Top;
            panelContentHost.Width = contentWidth;
            panelContentHost.Height = ClientSize.Height - panelStats.Top - 12;

            label3.Width = navButtonWidth;
            label3.Height = 46;
            label3.Left = navX;
            label3.Top = navTop;

            btnGoMembers.Width = navButtonWidth;
            btnGoPayments.Width = navButtonWidth;
            btnGoArchive.Width = navButtonWidth;
            btnLogout.Width = navButtonWidth;
            btnGoMembers.Left = navX;
            btnGoPayments.Left = navX;
            btnGoArchive.Left = navX;
            btnLogout.Left = navX;

            btnGoMembers.Height = navBtnH;
            btnGoPayments.Height = navBtnH;
            btnGoArchive.Height = navBtnH;
            btnLogout.Height = navBtnH;

            btnGoMembers.Top = label3.Bottom + 18;
            btnGoPayments.Top = btnGoMembers.Bottom + navGap;
            btnGoArchive.Top = btnGoPayments.Bottom + navGap;
            btnLogout.Top = panelNav.ClientSize.Height - btnLogout.Height - 18;

            // Center the quote block in header.
            int quoteGap = 6;
            int line1Width = label4.Width + quoteGap + label2.Width;
            int line1StartX = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - line1Width) / 2);
            label4.Left = line1StartX;
            label2.Left = label4.Right + quoteGap;
            label9.Left = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - label9.Width) / 2);

            cmbDateFilter.Width = 150;
            cmbDateFilter.Height = 26;
            cmbDateFilter.Left = panelStats.ClientSize.Width - cmbDateFilter.Width - 16;
            cmbDateFilter.Top = 14;

            // Stats row: reserve space for the date filter so it doesn't collide with Revenue.
            int statsLeftPad = 24;
            int statsRightPad = 16;
            int filterReserved = cmbDateFilter.Width + 16;
            int statsAreaWidth = Math.Max(520, panelStats.ClientSize.Width - statsLeftPad - statsRightPad - filterReserved);
            int statSegment = statsAreaWidth / 4;
            int statsStartX = statsLeftPad;
            PositionStat(label1, lblTotalMembers, statsStartX, statSegment, 0);
            PositionStat(label5, lblActive, statsStartX, statSegment, 1);
            PositionStat(label6, lblExpired, statsStartX, statSegment, 2);
            PositionStat(label7, lblRevenue, statsStartX, statSegment, 3);

            label8.Left = 24;
            label8.Top = 16;

            lblAnalytics.Left = 24;
            lblAnalytics.Top = label8.Bottom + 4;

            dgvRecentPayments.Left = 24;
            dgvRecentPayments.Top = lblAnalytics.Bottom + 8;
            dgvRecentPayments.Width = panelGrid.ClientSize.Width - 48;
            dgvRecentPayments.Height = panelGrid.ClientSize.Height - dgvRecentPayments.Top - 16;
        }

        private void ShowEmbeddedContent(Form form)
        {
            if (activeContentForm != null)
            {
                panelContentHost.Controls.Clear();
                activeContentForm.Close();
                activeContentForm.Dispose();
                activeContentForm = null;
            }

            if (form is Member memberForm)
            {
                memberForm.SetEmbeddedMode();
                memberForm.SetReminderAction(() =>
                {
                    int sentCount = SendRenewalReminders();
                    MessageBox.Show(sentCount > 0
                        ? $"Renewal reminders sent: {sentCount}"
                        : "No reminders were sent (already reminded today or no expiring members).");
                    CheckExpiringMemberships();
                });
                memberForm.SetOpenPaymentsAction(memberId =>
                {
                    ShowEmbeddedContent(new Payments(memberId));
                });
            }
            if (form is Payments paymentsForm) paymentsForm.SetEmbeddedMode();
            if (form is Archive archiveForm) archiveForm.SetEmbeddedMode();

            panelStats.Visible = false;
            pnlExpiryAlert.Visible = false;
            panelGrid.Visible = false;
            panelContentHost.Visible = true;

            activeContentForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelContentHost.Controls.Add(form);
            form.Show();
        }

        private void ShowDashboardHome()
        {
            if (activeContentForm != null)
            {
                panelContentHost.Controls.Clear();
                activeContentForm.Close();
                activeContentForm.Dispose();
                activeContentForm = null;
            }

            panelContentHost.Visible = false;
            panelStats.Visible = true;
            panelGrid.Visible = true;
            pnlExpiryAlert.Visible = !string.IsNullOrWhiteSpace(lblExpiryMessage.Text) && lblExpiryMessage.Text != "label4";

            LoadStats();
            LoadRecentPayments();
            CheckExpiringMemberships();
            LoadAnalytics();
        }

        private void LoadAnalytics()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string dateFilter = GetPaymentsDateFilterSql();
                string query = @"
SELECT
    (SELECT COUNT(*) FROM Members
      WHERE IsArchived=0
        AND YEAR(JoinDate)=YEAR(GETDATE())
        AND MONTH(JoinDate)=MONTH(GETDATE())) AS NewMembers,
    (SELECT COUNT(*) FROM Payments
      WHERE " + dateFilter + @") AS PaymentsThisMonth,
    (SELECT ISNULL(SUM(Amount),0) FROM Payments
      WHERE " + dateFilter + @") AS RevenueThisMonth,
    (SELECT COUNT(*) FROM Members WHERE IsArchived=0 AND IsFrozen=1) AS FrozenMembers;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return;
                    int newMembers = Convert.ToInt32(reader["NewMembers"]);
                    int paymentsThisMonth = Convert.ToInt32(reader["PaymentsThisMonth"]);
                    decimal revenueThisMonth = Convert.ToDecimal(reader["RevenueThisMonth"]);
                    int frozenMembers = Convert.ToInt32(reader["FrozenMembers"]);

                    lblAnalytics.Text =
                        $"This Month: New Members {newMembers} | Payments {paymentsThisMonth} | Revenue P {revenueThisMonth:N2} | Frozen {frozenMembers}";
                }
            }
        }

        private int SendRenewalReminders()
        {
            int sent = 0;
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
SELECT m.MemberID, m.FullName, m.Email, m.ExpiryDate,
       DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) AS DaysLeft
FROM Members m
WHERE m.IsArchived=0
  AND m.IsFrozen=0
  AND DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(m.ExpiryDate AS date)) BETWEEN 0 AND 7
  AND NOT EXISTS (
      SELECT 1 FROM ReminderLogs r
      WHERE r.MemberID = m.MemberID
        AND r.ReminderType = 'Renewal'
        AND CAST(r.SentAt AS date) = CAST(GETDATE() AS date)
  );";

                DataTable members = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.Fill(members);
                }

                foreach (DataRow row in members.Rows)
                {
                    string email = row["Email"]?.ToString();
                    if (string.IsNullOrWhiteSpace(email))
                        continue;

                    int daysLeft = Convert.ToInt32(row["DaysLeft"]);
                    string name = row["FullName"]?.ToString();
                    DateTime expiry = Convert.ToDateTime(row["ExpiryDate"]);

                    EmailHelper.SendRenewalReminder(email, name, expiry, daysLeft);

                    using (SqlCommand log = new SqlCommand(
                        "INSERT INTO ReminderLogs (MemberID, ReminderType, Channel, Notes) VALUES (@id,'Renewal','Email',@notes)",
                        conn))
                    {
                        log.Parameters.AddWithValue("@id", Convert.ToInt32(row["MemberID"]));
                        log.Parameters.AddWithValue("@notes", $"Days left: {daysLeft}; expiry {expiry:yyyy-MM-dd}");
                        log.ExecuteNonQuery();
                    }

                    sent++;
                }
            }

            return sent;
        }

        private string GetPaymentsDateFilterSql()
        {
            string selected = cmbDateFilter?.SelectedItem?.ToString() ?? "This Month";
            switch (selected)
            {
                case "Today":
                    return "CAST(PaymentDate AS date) = CAST(GETDATE() AS date)";
                case "Last 7 Days":
                    return "CAST(PaymentDate AS date) >= DATEADD(day, -6, CAST(GETDATE() AS date))";
                case "This Month":
                    return "YEAR(PaymentDate)=YEAR(GETDATE()) AND MONTH(PaymentDate)=MONTH(GETDATE())";
                default:
                    return "1=1";
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadStats();
            LoadRecentPayments();
            LoadAnalytics();
        }

        private void PositionStat(Label caption, Label value, int width, int index)
        {
            PositionStat(caption, value, 20, width, index);
        }

        private void PositionStat(Label caption, Label value, int startX, int width, int index)
        {
            int x = startX + (width * index);
            int captionY = 18;
            int valueY = 48;

            caption.Top = captionY;
            value.Top = valueY;

            // Center within segment for clean alignment.
            caption.Left = x + Math.Max(0, (width - caption.Width) / 2);
            value.Left = x + Math.Max(0, (width - value.Width) / 2);
        }

        private void ConfigureButtonTheme(Button button, System.Drawing.Color baseColor, System.Drawing.Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = ViltrumTheme.Border;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
            button.BackColor = baseColor;
            button.ForeColor = System.Drawing.Color.WhiteSmoke;
            button.Cursor = Cursors.Hand;
        }

        private void lblActive_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}