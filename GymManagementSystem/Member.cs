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
        private bool isEmbeddedMode = false;
        private Action sendReminderAction;
        private Action<int> openPaymentsAction;
        private ComboBox cmbStatusFilter;
        private Label lblStatusFilter;
        public Member()
        {
            InitializeComponent();
            Resize += Member_Resize;
            Shown += Member_Shown;
            ConfigureButtonTheme(btnBack, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnAdd, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnUpdate, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnPay, ViltrumTheme.Accent, ViltrumTheme.AccentHover);
            ConfigureButtonTheme(btnArchive, ViltrumTheme.Danger, ViltrumTheme.DangerHover);
            ConfigureButtonTheme(btnFreeze, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnReminders, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            label2.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvMembers.SelectionChanged += dgvMembers_SelectionChanged;
            dgvMembers.DataBindingComplete += dgvMembers_DataBindingComplete;

            lblStatusFilter = new Label
            {
                Text = "Status",
                AutoSize = true,
                ForeColor = Color.Gainsboro
            };
            panelBody.Controls.Add(lblStatusFilter);
            lblStatusFilter.BringToFront();

            cmbStatusFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(58, 69, 88),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            cmbStatusFilter.Items.AddRange(new object[]
            {
                "All",
                "Active - Paid",
                "Active - Unpaid",
                "Expiring - Paid",
                "Expiring - Unpaid",
                "Expired - Inactive",
                "Frozen",
                "Paid",
                "Unpaid"
            });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += FilterChanged;
            panelBody.Controls.Add(cmbStatusFilter);
            cmbStatusFilter.BringToFront();
        }

        public void SetEmbeddedMode()
        {
            isEmbeddedMode = true;
            btnBack.Visible = false;
        }

        public void SetReminderAction(Action action)
        {
            sendReminderAction = action;
            btnReminders.Enabled = true;
        }

        public void SetOpenPaymentsAction(Action<int> action)
        {
            openPaymentsAction = action;
        }

        private void Member_Load(object sender, EventArgs e)
        {
            DBConnection.EnsureFeatureSchema();
            DBConnection.AutoUnfreezeExpiredMembers();
            LoadMembers();
        }
        public void LoadMembers(string search = "") //Get Members
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"
SELECT MemberID, FullName, Email, Phone, [Plan], 
       MembershipFee, JoinDate, ExpiryDate, IsFrozen, FrozenFrom, FrozenUntil, FreezeUsed,
       CASE
           WHEN IsFrozen = 1 THEN 'Frozen'
           WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired - Inactive'
           WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), CAST(ExpiryDate AS date)) BETWEEN 0 AND 7
               THEN CASE WHEN IsPaid = 1 THEN 'Expiring - Paid' ELSE 'Expiring - Unpaid' END
           ELSE CASE WHEN IsPaid = 1 THEN 'Active - Paid' ELSE 'Active - Unpaid' END
       END AS [Status],
       CASE
           WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Unpaid'
           WHEN IsPaid=1 THEN 'Paid'
           ELSE 'Unpaid'
       END AS Payment
FROM Members
WHERE IsArchived=0
  AND (FullName LIKE @s OR Email LIKE @s)
ORDER BY MemberID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@s", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                string statusFilter = cmbStatusFilter?.SelectedItem?.ToString() ?? "All";
                if (statusFilter != "All")
                {
                    DataView view = dt.DefaultView;
                    if (statusFilter == "Paid" || statusFilter == "Unpaid")
                        view.RowFilter = $"Payment = '{statusFilter}'";
                    else
                        view.RowFilter = $"Status = '{statusFilter}'";
                    dt = view.ToTable();
                }
                dgvMembers.DataSource = dt;
                if (dgvMembers.Columns.Contains("IsFrozen")) dgvMembers.Columns["IsFrozen"].Visible = false;
                if (dgvMembers.Columns.Contains("FrozenFrom")) dgvMembers.Columns["FrozenFrom"].Visible = false;
                if (dgvMembers.Columns.Contains("FrozenUntil")) dgvMembers.Columns["FrozenUntil"].Visible = false;
                if (dgvMembers.Columns.Contains("FreezeUsed")) dgvMembers.Columns["FreezeUsed"].Visible = false;
                lblMemberCount.Text = $"Total: {dt.Rows.Count} members";
                UpdateFreezeButtonState();
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
            if (isEmbeddedMode)
                return;

            // Close this form and show dashboard
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
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member to edit.");
                return;
            }

            var row = dgvMembers.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells["MemberID"].Value);

            // Allow renew/update only if membership is expired.
            DateTime expiry = DateTime.MinValue;
            bool hasExpiry = dgvMembers.Columns.Contains("ExpiryDate")
                             && DateTime.TryParse(row.Cells["ExpiryDate"].Value?.ToString(), out expiry);
            bool isExpired = hasExpiry && expiry.Date < DateTime.Today;

            if (!isExpired)
            {
                string untilText = hasExpiry ? expiry.ToString("MMMM dd, yyyy") : "N/A";
                MessageBox.Show(
                    "Cannot renew/update this member yet.\n" +
                    $"Membership is still active until: {untilText}",
                    "Renew Blocked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

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

            if (isEmbeddedMode && openPaymentsAction != null)
            {
                openPaymentsAction(id);
                return;
            }

            using (Payments payForm = new Payments(id))
            {
                payForm.ShowDialog(this);
            }
            LoadMembers();
        }

        private void btnReminders_Click(object sender, EventArgs e)
        {
            try
            {
                if (sendReminderAction != null)
                {
                    sendReminderAction();
                }
                else
                {
                    int sent = SendRenewalRemindersDirect();
                    MessageBox.Show(sent > 0
                        ? $"Renewal reminders sent: {sent}"
                        : "No reminders were sent (already reminded today or no expiring members).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send reminders: " + ex.Message);
            }
        }

        private void btnFreeze_Click(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a member first.");
                return;
            }

            DataGridViewRow selected = dgvMembers.SelectedRows[0];
            int id = Convert.ToInt32(selected.Cells["MemberID"].Value);
            string name = selected.Cells["FullName"].Value?.ToString() ?? string.Empty;
            bool isFrozen = ToBool(selected.Cells["IsFrozen"].Value);
            bool freezeUsed = dgvMembers.Columns.Contains("FreezeUsed") && ToBool(selected.Cells["FreezeUsed"].Value);

            if (isFrozen)
            {
                UnfreezeMember(id, name);
                return;
            }

            if (freezeUsed)
            {
                MessageBox.Show(
                    $"{name} has already used the one-time freeze option.",
                    "Freeze Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            FreezeMember(id, name, 7);
        }

        private void FreezeMember(int memberIdToFreeze, string memberName, int days)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(@"
UPDATE Members
SET IsFrozen = 1,
    FreezeUsed = 1,
    FrozenFrom = CAST(GETDATE() AS date),
    FrozenUntil = DATEADD(day, @days, CAST(GETDATE() AS date)),
    ExpiryDate = DATEADD(day, @days, ExpiryDate),
    Status = 'Frozen'
WHERE MemberID = @id;", conn, tx);
                    cmd.Parameters.AddWithValue("@days", days);
                    cmd.Parameters.AddWithValue("@id", memberIdToFreeze);
                    cmd.ExecuteNonQuery();

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }

            MessageBox.Show($"{memberName} has been frozen for {days} day(s).");
            LoadMembers();
        }

        private void UnfreezeMember(int memberIdToUnfreeze, string memberName)
        {
            DialogResult confirm = MessageBox.Show(
                $"Unfreeze {memberName} now?",
                "Confirm Unfreeze",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(@"
UPDATE Members
SET IsFrozen = 0,
    FrozenFrom = NULL,
    FrozenUntil = NULL,
    Status = CASE
        WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        ELSE 'Active'
    END
WHERE MemberID = @id;", conn, tx);
                    cmd.Parameters.AddWithValue("@id", memberIdToUnfreeze);
                    cmd.ExecuteNonQuery();

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }

            MessageBox.Show($"{memberName} is now unfrozen.");
            LoadMembers();
        }

        private int? PromptFreezeDays()
        {
            using (Form prompt = new Form())
            {
                prompt.Text = "Freeze Membership";
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.ClientSize = new Size(300, 145);
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;

                Label text = new Label { Left = 16, Top = 16, Width = 260, Text = "Freeze for how many days?" };
                NumericUpDown days = new NumericUpDown
                {
                    Left = 16,
                    Top = 44,
                    Width = 260,
                    Minimum = 1,
                    Maximum = 180,
                    Value = 7
                };
                Button ok = new Button
                {
                    Text = "OK",
                    Left = 116,
                    Width = 76,
                    Top = 92,
                    DialogResult = DialogResult.OK
                };
                Button cancel = new Button
                {
                    Text = "Cancel",
                    Left = 200,
                    Width = 76,
                    Top = 92,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(text);
                prompt.Controls.Add(days);
                prompt.Controls.Add(ok);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = ok;
                prompt.CancelButton = cancel;

                if (prompt.ShowDialog(this) == DialogResult.OK)
                    return (int)days.Value;
            }

            return null;
        }

        private bool ToBool(object value)
        {
            if (value == null || value == DBNull.Value) return false;
            bool parsed;
            return bool.TryParse(value.ToString(), out parsed) && parsed;
        }

        private void dgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateFreezeButtonState();
        }

        private void dgvMembers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvMembers.Rows)
            {
                if (row.IsNewRow) continue;
                string status = row.Cells["Status"]?.Value?.ToString() ?? string.Empty;
                if (status.StartsWith("Expired"))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(96, 50, 58);
                }
                else if (status.StartsWith("Expiring"))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(96, 86, 40);
                }
                else if (status == "Frozen")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(56, 70, 100);
                }
            }
        }

        private void UpdateFreezeButtonState()
        {
            if (dgvMembers.SelectedRows.Count == 0)
            {
                btnFreeze.Text = "FREEZE";
                btnFreeze.Enabled = false;
                return;
            }

            DataGridViewRow row = dgvMembers.SelectedRows[0];
            bool isFrozen = dgvMembers.Columns.Contains("IsFrozen") && ToBool(row.Cells["IsFrozen"].Value);
            bool freezeUsed = dgvMembers.Columns.Contains("FreezeUsed") && ToBool(row.Cells["FreezeUsed"].Value);

            btnFreeze.Text = isFrozen ? "UNFREEZE" : "FREEZE";
            btnFreeze.Enabled = isFrozen || !freezeUsed;
            ApplyResponsiveLayout();
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            LoadMembers(txtSearch.Text.Trim());
        }

        private int SendRenewalRemindersDirect()
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
            btnReminders.Height = buttonHeight;
            btnFreeze.Height = buttonHeight;
            btnArchive.Height = buttonHeight;
            btnUpdate.Height = buttonHeight;
            btnAdd.Height = buttonHeight;

            FitButtonToText(btnAdd, 24);
            FitButtonToText(btnUpdate, 24);
            FitButtonToText(btnArchive, 24);
            FitButtonToText(btnPay, 24);
            FitButtonToText(btnReminders, 24);
            FitButtonToText(btnFreeze, 24);

            int searchX = label2.Right + 10;
            int buttonGroupWidth = btnAdd.Width + btnUpdate.Width + btnReminders.Width + btnFreeze.Width + btnArchive.Width + btnPay.Width + (gap * 5);
            int rightEdge = panelBody.ClientSize.Width - innerMargin;
            int buttonsStartX = rightEdge - buttonGroupWidth;
            int maxSearchWidth = Math.Max(220, buttonsStartX - searchX - 140);
            txtSearch.Location = new Point(searchX, rowY + 1);
            txtSearch.Width = Math.Min(360, maxSearchWidth);
            txtSearch.Height = (int)(24 * scale);

            lblStatusFilter.Location = new Point(searchX, rowY + 34);
            cmbStatusFilter.Location = new Point(lblStatusFilter.Right + 8, rowY + 31);
            cmbStatusFilter.Width = 130;
            cmbStatusFilter.Height = (int)(24 * scale);

            btnPay.Location = new Point(rightEdge - btnPay.Width, rowY);
            btnArchive.Location = new Point(btnPay.Left - gap - btnArchive.Width, rowY);
            btnFreeze.Location = new Point(btnArchive.Left - gap - btnFreeze.Width, rowY);
            btnReminders.Location = new Point(btnFreeze.Left - gap - btnReminders.Width, rowY);
            btnUpdate.Location = new Point(btnReminders.Left - gap - btnUpdate.Width, rowY);
            btnAdd.Location = new Point(btnUpdate.Left - gap - btnAdd.Width, rowY);

            lblMemberCount.Location = new Point(txtSearch.Right + 12, rowY + 8);
            lblMemberCount.Width = Math.Max(80, btnAdd.Left - lblMemberCount.Left - 10);
            lblMemberCount.Top = rowY + 36;

            int gridY = rowY + rowHeight + 50;
            dgvMembers.Location = new Point(innerMargin, gridY);
            dgvMembers.Size = new Size(
                Math.Max(860, panelBody.ClientSize.Width - (innerMargin * 2)),
                Math.Max(240, panelBody.ClientSize.Height - gridY - innerMargin));

            var labelFont = new Font("Segoe UI Semibold", 10f * scale, FontStyle.Bold);
            var buttonFont = new Font("Segoe UI Semibold", 9f * scale, FontStyle.Bold);
            label2.Font = labelFont;
            lblStatusFilter.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            lblMemberCount.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            btnAdd.Font = buttonFont;
            btnUpdate.Font = buttonFont;
            btnArchive.Font = buttonFont;
            btnPay.Font = buttonFont;
            btnReminders.Font = buttonFont;
            btnFreeze.Font = buttonFont;
            cmbStatusFilter.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
        }

        private void FitButtonToText(Button button, int horizontalPadding)
        {
            Size measured = TextRenderer.MeasureText(button.Text, button.Font);
            button.Width = measured.Width + horizontalPadding;
        }

        private void ConfigureButtonTheme(Button button, Color baseColor, Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = ViltrumTheme.Border;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
            button.BackColor = baseColor;
            button.ForeColor = Color.WhiteSmoke;
            button.Cursor = Cursors.Hand;
        }
    }
}
