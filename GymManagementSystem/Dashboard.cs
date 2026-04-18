using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Dashboard : Form
    {
        private readonly Panel panelContentHost;
        private readonly Label lblAnalytics;
        private readonly ComboBox cmbDateFilter;
        private readonly Button btnBackupDb;
        private Form activeContentForm;

        public Dashboard()
        {
            InitializeComponent();
            Resize += Dashboard_Resize;
            Shown += Dashboard_Shown;
            ConfigureButtonTheme(btnGoMembers, System.Drawing.Color.FromArgb(80, 91, 109), System.Drawing.Color.FromArgb(95, 108, 129));
            ConfigureButtonTheme(btnGoPayments, System.Drawing.Color.FromArgb(80, 91, 109), System.Drawing.Color.FromArgb(95, 108, 129));
            ConfigureButtonTheme(btnGoArchive, System.Drawing.Color.FromArgb(80, 91, 109), System.Drawing.Color.FromArgb(95, 108, 129));
            ConfigureButtonTheme(btnLogout, System.Drawing.Color.FromArgb(188, 44, 44), System.Drawing.Color.FromArgb(210, 60, 60));

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
                BackColor = System.Drawing.Color.FromArgb(58, 69, 88),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cmbDateFilter.Items.AddRange(new object[] { "Today", "Last 7 Days", "This Month", "All Time" });
            cmbDateFilter.SelectedIndex = 2;
            cmbDateFilter.SelectedIndexChanged += FilterChanged;
            panelStats.Controls.Add(cmbDateFilter);
            cmbDateFilter.BringToFront();

            btnBackupDb = new Button
            {
                Text = "Backup DB",
                ForeColor = System.Drawing.Color.White
            };
            ConfigureButtonTheme(btnBackupDb, System.Drawing.Color.FromArgb(57, 130, 245), System.Drawing.Color.FromArgb(80, 150, 255));
            btnBackupDb.Click += btnBackupDb_Click;
            panelNav.Controls.Add(btnBackupDb);
            btnBackupDb.BringToFront();

            label3.Cursor = Cursors.Hand;
            label3.Click += (s, e) => ShowDashboardHome();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DBConnection.EnsureFeatureSchema();
            LoadStats();
            LoadRecentPayments();
            CheckExpiringMemberships();
            LoadAnalytics();
        }

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

        private void LoadRecentPayments()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string dateFilter = GetPaymentsDateFilterSql();
                string query = @"SELECT TOP 5 ReferenceNo, MemberName, Amount, 
                             PaymentMethod, PaymentDate, Status 
                             FROM Payments
                             WHERE " + dateFilter + @" ORDER BY PaymentID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRecentPayments.DataSource = dt;
            }
        }

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
            new Form1().Show();
            this.Close();
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
            int navButtonWidth = Math.Max(128, panelNav.Width - 56);

            panelStats.Left = contentLeft;
            panelStats.Width = contentWidth;
            pnlExpiryAlert.Left = contentLeft;
            pnlExpiryAlert.Width = contentWidth;
            panelGrid.Left = contentLeft;
            panelGrid.Width = contentWidth;

            panelContentHost.Left = contentLeft;
            panelContentHost.Top = panelStats.Top;
            panelContentHost.Width = contentWidth;
            panelContentHost.Height = ClientSize.Height - panelStats.Top - 12;

            label3.Width = navButtonWidth;
            label3.Left = (panelNav.Width - label3.Width) / 2;
            btnGoMembers.Width = navButtonWidth;
            btnGoPayments.Width = navButtonWidth;
            btnGoArchive.Width = navButtonWidth;
            btnLogout.Width = navButtonWidth;
            btnGoMembers.Left = (panelNav.Width - btnGoMembers.Width) / 2;
            btnGoPayments.Left = btnGoMembers.Left;
            btnGoArchive.Left = btnGoMembers.Left;
            btnLogout.Left = btnGoMembers.Left;
            btnBackupDb.Width = navButtonWidth;
            btnBackupDb.Height = btnGoMembers.Height;
            btnBackupDb.Left = btnGoMembers.Left;
            btnBackupDb.Top = btnLogout.Bottom + 14;

            // Center the quote block in header.
            int quoteGap = 6;
            int line1Width = label4.Width + quoteGap + label2.Width;
            int line1StartX = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - line1Width) / 2);
            label4.Left = line1StartX;
            label2.Left = label4.Right + quoteGap;
            label9.Left = Math.Max(pbIcon.Right + 24, (panelTop.ClientSize.Width - label9.Width) / 2);

            int statSegment = panelStats.ClientSize.Width / 4;
            PositionStat(label1, lblTotalMembers, statSegment, 0);
            PositionStat(label5, lblActive, statSegment, 1);
            PositionStat(label6, lblExpired, statSegment, 2);
            PositionStat(label7, lblRevenue, statSegment, 3);
            cmbDateFilter.Width = 150;
            cmbDateFilter.Height = 26;
            cmbDateFilter.Left = panelStats.ClientSize.Width - cmbDateFilter.Width - 16;
            cmbDateFilter.Top = 14;

            lblAnalytics.Left = 24;
            lblAnalytics.Top = 20;
            dgvRecentPayments.Top = lblAnalytics.Bottom + 8;
            dgvRecentPayments.Height = panelGrid.ClientSize.Height - dgvRecentPayments.Top - 20;
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

        private void btnBackupDb_Click(object sender, EventArgs e)
        {
            try
            {
                string dbName = DBConnection.GetDatabaseName();
                string backupDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GymBackups");
                Directory.CreateDirectory(backupDir);
                string backupFile = Path.Combine(backupDir, $"{dbName}-{DateTime.Now:yyyyMMdd-HHmmss}.bak");

                using (SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    conn.Open();
                    string sql = $"BACKUP DATABASE [{dbName}] TO DISK = @path WITH INIT, STATS = 10";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@path", backupFile);
                        cmd.CommandTimeout = 120;
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Database backup created:\n{backupFile}", "Backup Complete");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup failed: " + ex.Message, "Backup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PositionStat(Label caption, Label value, int width, int index)
        {
            int startX = (width * index) + 20;
            caption.Left = startX;
            value.Left = startX;
        }

        private void ConfigureButtonTheme(Button button, System.Drawing.Color baseColor, System.Drawing.Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
            button.BackColor = baseColor;
        }

        private void lblActive_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}