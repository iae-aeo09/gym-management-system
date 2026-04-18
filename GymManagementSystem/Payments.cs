using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public partial class Payments : Form
    {

        private int preSelectedMemberId = 0;
        private bool isEmbeddedMode = false;
        public Payments()
        {

            InitializeComponent();
            Resize += Payments_Resize;
            Shown += Payments_Shown;
            dgvPayments.DataBindingComplete += dgvPayments_DataBindingComplete;
            ConfigureButtonTheme(btnBack, Color.FromArgb(80, 91, 109), Color.FromArgb(95, 108, 129));
            ConfigureButtonTheme(btnRecordPayment, Color.FromArgb(188, 44, 44), Color.FromArgb(210, 60, 60));
            ConfigureButtonTheme(btnViewReceipt, Color.FromArgb(80, 91, 109), Color.FromArgb(95, 108, 129));

        }

        public void SetEmbeddedMode()
        {
            isEmbeddedMode = true;
            btnBack.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (isEmbeddedMode)
                return;

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

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            // compatibility if designer hooked different handler name
            btnBack_Click(sender, e);
        }
        // EDIT (THIS FIXES YOUR ERROR)
        public Payments(int memberId)
        {
            InitializeComponent();
            preSelectedMemberId = memberId;
            Resize += Payments_Resize;
            Shown += Payments_Shown;
            dgvPayments.DataBindingComplete += dgvPayments_DataBindingComplete;
            ConfigureButtonTheme(btnBack, Color.FromArgb(80, 91, 109), Color.FromArgb(95, 108, 129));
            ConfigureButtonTheme(btnRecordPayment, Color.FromArgb(188, 44, 44), Color.FromArgb(210, 60, 60));
            ConfigureButtonTheme(btnViewReceipt, Color.FromArgb(80, 91, 109), Color.FromArgb(95, 108, 129));
            // in InitializeComponent() after dgvPayments created or in Payments_Load
            this.dgvPayments.AllowUserToAddRows = false;
        }



        private void Payments_Load(object sender, EventArgs e)
        {
            DBConnection.EnsureFeatureSchema();
            cmbMember.SelectedIndexChanged += cmbMember_SelectedIndexChanged;
            cmbMethod.TextChanged += cmbMethod_TextChanged;
            cmbMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMethod.Items.AddRange(new string[] {
            "Cash", "GCash", "Maya", "Credit Card", "Bank Transfer"
        });
            cmbMethod.SelectedIndex = 0;
            dtpPayDate.Value = DateTime.Today;
            txtAmount.ReadOnly = true;

            LoadMembers();
            LoadPayments();

            if (preSelectedMemberId > 0)
            {
                foreach (DataRowView item in cmbMember.Items)
                {
                    if (Convert.ToInt32(item["MemberID"]) == preSelectedMemberId)
                    {
                        cmbMember.SelectedItem = item;
                        break;
                    }
                }
            }

            UpdateAmountState();
        }
        private void LoadMembers()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MemberID, FullName, [Plan], MembershipFee, Email, ExpiryDate, IsPaid FROM Members WHERE IsArchived=0", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMember.DataSource = dt;
                cmbMember.DisplayMember = "FullName";
                cmbMember.ValueMember = "MemberID";
            }
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAmountState();
        }

        private void cmbMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAmountState();
        }

        private void cmbMethod_TextChanged(object sender, EventArgs e)
        {
            UpdateAmountState();
        }

        private decimal GetPlanAmount(DataRowView row)
        {
            if (row == null) return 0m;

            string plan = (row["Plan"]?.ToString() ?? string.Empty).Trim();
            switch (plan)
            {
                case "Monthly":
                    return 800m;
                case "Quarterly":
                    return 2400m;
                case "Semi-Annual":
                    return 4800m;
                case "Annual":
                    return 9600m;
                default:
                    decimal fallback = 0m;
                    decimal.TryParse(row["MembershipFee"]?.ToString(), out fallback);
                    return fallback;
            }
        }

        private int GetPlanMonths(DataRowView row)
        {
            if (row == null) return 0;
            string plan = (row["Plan"]?.ToString() ?? string.Empty).Trim();
            switch (plan)
            {
                case "Monthly": return 1;
                case "Quarterly": return 3;
                case "Semi-Annual": return 6;
                case "Annual": return 12;
                default: return 0;
            }
        }

        private void UpdateAmountState()
        {
            bool isCash = string.Equals(cmbMethod.SelectedItem?.ToString(), "Cash", StringComparison.OrdinalIgnoreCase);
            txtAmount.ReadOnly = !isCash;

            if (cmbMember.SelectedItem is DataRowView row)
            {
                decimal amount = GetPlanAmount(row);
                txtAmount.Text = amount.ToString("N2");
            }
            else
            {
                txtAmount.Text = string.Empty;
            }
        }
        private void LoadPayments()
        {
            
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = @"SELECT p.ReferenceNo, p.MemberName, p.Amount,
                             p.PaymentMethod, p.PaymentDate, m.ExpiryDate, p.Status
                             FROM Payments p
                             LEFT JOIN Members m ON p.MemberID = m.MemberID
                             ORDER BY p.PaymentID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPayments.DataSource = dt;
                ApplyPaymentGridHighlights();
            }
        }

        private string GenerateRef()
        {
            return "PAY-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

    
        

        private void btnViewReceipt_Click(object sender, EventArgs e)
        {

            if (dgvPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a payment record first.");
                return;
            }

            var row = dgvPayments.SelectedRows[0];

            // don't operate on the 'new' placeholder row
            if (row.IsNewRow)
            {
                MessageBox.Show("Select a valid payment record.");
                return;
            }

            object GetCell(string name) => row.Cells[name]?.Value;
            string GetString(string name) => GetCell(name) is DBNull || GetCell(name) == null ? string.Empty : GetCell(name).ToString();
            bool TryGetDecimal(string name, out decimal val)
            {
                val = 0;
                var obj = GetCell(name);
                if (obj == null || obj is DBNull) return false;
                return decimal.TryParse(obj.ToString(), out val);
            }
            bool TryGetDate(string name, out DateTime dt)
            {
                dt = DateTime.MinValue;
                var obj = GetCell(name);
                if (obj == null || obj is DBNull) return false;
                return DateTime.TryParse(obj.ToString(), out dt);
            }

            string refNo = GetString("ReferenceNo");
            string memberName = GetString("MemberName");
            TryGetDecimal("Amount", out decimal amount);
            string method = GetString("PaymentMethod");
            TryGetDate("PaymentDate", out DateTime date);

            if (string.IsNullOrWhiteSpace(refNo))
            {
                MessageBox.Show("Selected record has missing reference number.");
                return;
            }

            ReceiptDialog.ShowReceipt(this, new ReceiptInfo
            {
                ReferenceNo = refNo,
                MemberName = memberName,
                Amount = amount,
                PaymentMethod = method,
                PaymentDate = date,
                Status = "Paid"
            });
        }

        private void btnRecordPayment_Click_1(object sender, EventArgs e)
        {

            if (cmbMember.SelectedItem == null)
            {
                MessageBox.Show("Please select a member.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal amount))
            {
                MessageBox.Show("Enter a valid amount.");
                return;
            }

            int memberId = Convert.ToInt32(cmbMember.SelectedValue);
            string memberName = ((DataRowView)cmbMember.SelectedItem)["FullName"].ToString();
            string email = ((DataRowView)cmbMember.SelectedItem)["Email"]?.ToString();
            string method = cmbMethod.SelectedItem.ToString();
            string refNo = GenerateRef();
            DateTime payDate = dtpPayDate.Value.Date;
            DateTime expiryDate = Convert.ToDateTime(((DataRowView)cmbMember.SelectedItem)["ExpiryDate"]).Date;
            bool isPaid = Convert.ToBoolean(((DataRowView)cmbMember.SelectedItem)["IsPaid"]);
            DataRowView selectedMember = (DataRowView)cmbMember.SelectedItem;
            decimal expectedAmount = GetPlanAmount(selectedMember);
            int renewalMonths = GetPlanMonths(selectedMember);

            if (expiryDate >= DateTime.Today && isPaid)
            {
                MessageBox.Show(
                    $"Cannot record payment.\nThis member is already paid and active until {expiryDate:MMMM dd, yyyy}.",
                    "Payment Blocked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (amount < expectedAmount)
            {
                MessageBox.Show(
                    $"Entered amount is below required plan amount.\nRequired: ₱{expectedAmount:N2}",
                    "Invalid Payment Amount",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (amount > expectedAmount)
            {
                DialogResult confirmOverpay = MessageBox.Show(
                    $"Entered amount exceeds plan amount (₱{expectedAmount:N2}).\nContinue anyway?",
                    "Amount Validation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirmOverpay != DialogResult.Yes)
                    return;
            }

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();
                try
                {
                    // Insert payment record
                    SqlCommand cmd = new SqlCommand(@"
INSERT INTO Payments (MemberID, MemberName, Amount, PaymentMethod, PaymentDate, ReferenceNo, Status)
VALUES (@mid, @mn, @amt, @mth, @dt, @ref, 'Paid')", conn, tx);
                    cmd.Parameters.AddWithValue("@mid", memberId);
                    cmd.Parameters.AddWithValue("@mn", memberName);
                    cmd.Parameters.AddWithValue("@amt", amount);
                    cmd.Parameters.AddWithValue("@mth", method);
                    cmd.Parameters.AddWithValue("@dt", payDate);
                    cmd.Parameters.AddWithValue("@ref", refNo);
                    cmd.ExecuteNonQuery();

                    DateTime renewalBase = expiryDate >= DateTime.Today ? expiryDate : DateTime.Today;
                    DateTime renewedExpiry = renewalMonths > 0 ? renewalBase.AddMonths(renewalMonths) : renewalBase;

                    // Auto-renew membership on successful payment.
                    SqlCommand upd = new SqlCommand(
                        @"UPDATE Members
                          SET IsPaid=1,
                              ExpiryDate=@newExpiry,
                              Status = CASE 
                                  WHEN IsFrozen=1 THEN 'Frozen'
                                  WHEN CAST(@newExpiry AS date) < CAST(GETDATE() AS date) THEN 'Expired'
                                  ELSE 'Active'
                              END
                          WHERE MemberID=@id", conn, tx);
                    upd.Parameters.AddWithValue("@newExpiry", renewedExpiry);
                    upd.Parameters.AddWithValue("@id", memberId);
                    upd.ExecuteNonQuery();

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }

            // send receipt after payment is recorded
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Member has no email on file; receipt not sent.");
            }
            else
            {
                try
                {
                    EmailHelper.SendReceipt(email, refNo, memberName, amount, method, payDate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to send receipt: " + ex.Message);
                }
            }

            lblRef.Text = $"Reference: {refNo}";
            MessageBox.Show($"Payment recorded!\nRef: {refNo}\nReceipt will be sent to member's email.");

            LoadPayments();
        }

        private void dtpPayDate_ValueChanged(object sender, EventArgs e)
        {
            dtpPayDate.Value = DateTime.Today;
        }

        private void Payments_Shown(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void Payments_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void dgvPayments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyPaymentGridHighlights();
        }

        private void ApplyPaymentGridHighlights()
        {
            if (dgvPayments.Rows.Count == 0 || !dgvPayments.Columns.Contains("ExpiryDate"))
                return;

            foreach (DataGridViewRow row in dgvPayments.Rows)
            {
                if (row.IsNewRow) continue;

                DateTime expiryDate;
                bool hasDate = DateTime.TryParse(row.Cells["ExpiryDate"].Value?.ToString(), out expiryDate);
                if (!hasDate) continue;

                if (expiryDate.Date < DateTime.Today)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(96, 50, 58);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(126, 60, 70);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72);
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(78, 89, 107);
                }
            }
        }

        private void ConfigureButtonTheme(Button button, Color baseColor, Color hoverColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = hoverColor;
            button.FlatAppearance.MouseDownBackColor = hoverColor;
            button.BackColor = baseColor;
        }

        private void ApplyResponsiveLayout()
        {
            const int outerMargin = 24;
            const int innerMargin = 24;
            const int sectionGap = 12;
            float scale = Math.Max(1.0f, Math.Min(1.25f, ClientSize.Width / 1366f));

            panelPayment.Left = outerMargin;
            panelPayment.Top = panelHeader.Bottom + 14;
            panelPayment.Width = Math.Max(980, ClientSize.Width - (outerMargin * 2));
            panelPayment.Height = 202;

            panelHistory.Left = outerMargin;
            panelHistory.Top = panelPayment.Bottom + sectionGap;
            panelHistory.Width = panelPayment.Width;
            panelHistory.Height = Math.Max(280, ClientSize.Height - panelHistory.Top - outerMargin);

            int topRowY = 68;
            int rowGap = 52;
            int columnGap = 40;
            int leftColumnX = innerMargin;
            int columnWidth = (panelPayment.ClientSize.Width - (innerMargin * 2) - columnGap) / 2;
            int rightColumnX = leftColumnX + columnWidth + columnGap;

            lblRef.Location = new Point(Math.Max(innerMargin, panelPayment.ClientSize.Width - innerMargin - lblRef.Width), 12);

            label1.Location = new Point(leftColumnX, topRowY);
            cmbMember.Location = new Point(leftColumnX + 120, topRowY - 3);
            cmbMember.Width = Math.Max(220, columnWidth - 130);

            label3.Location = new Point(rightColumnX, topRowY);
            txtAmount.Location = new Point(rightColumnX + 100, topRowY - 2);
            txtAmount.Width = Math.Max(200, columnWidth - 110);

            label4.Location = new Point(leftColumnX, topRowY + rowGap);
            cmbMethod.Location = new Point(leftColumnX + 120, topRowY + rowGap - 3);
            cmbMethod.Width = Math.Max(220, columnWidth - 130);

            label5.Location = new Point(rightColumnX, topRowY + rowGap);
            dtpPayDate.Location = new Point(rightColumnX + 100, topRowY + rowGap - 2);
            dtpPayDate.Width = Math.Max(200, columnWidth - 110);

            int buttonY = topRowY + rowGap + 36;
            int buttonGap = 12;
            int availableButtonWidth = panelPayment.ClientSize.Width - (innerMargin * 2);
            int totalButtonsWidth = Math.Min(900, availableButtonWidth);
            int buttonStartX = innerMargin + Math.Max(0, (availableButtonWidth - totalButtonsWidth) / 2);
            int buttonWidth = (totalButtonsWidth - buttonGap) / 2;
            btnRecordPayment.Location = new Point(buttonStartX, buttonY);
            btnRecordPayment.Width = Math.Max(260, buttonWidth);
            btnViewReceipt.Location = new Point(btnRecordPayment.Right + buttonGap, buttonY);
            btnViewReceipt.Width = Math.Max(260, buttonWidth);
            btnRecordPayment.Height = (int)(34 * scale);
            btnViewReceipt.Height = (int)(34 * scale);

            label8.Location = new Point(innerMargin, 18);
            dgvPayments.Location = new Point(innerMargin, 52);
            dgvPayments.Size = new Size(
                Math.Max(860, panelHistory.ClientSize.Width - (innerMargin * 2)),
                Math.Max(200, panelHistory.ClientSize.Height - 52 - innerMargin));

            var labelFont = new Font("Segoe UI Semibold", 9f * scale, FontStyle.Bold);
            var buttonFont = new Font("Segoe UI Semibold", 9.5f * scale, FontStyle.Bold);
            label1.Font = labelFont;
            label3.Font = labelFont;
            label4.Font = labelFont;
            label5.Font = labelFont;
            label2.Font = new Font("Segoe UI Semibold", 11f * scale, FontStyle.Bold);
            label8.Font = new Font("Segoe UI Semibold", 11f * scale, FontStyle.Bold);
            btnRecordPayment.Font = buttonFont;
            btnViewReceipt.Font = buttonFont;
            cmbMember.Font = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            cmbMethod.Font = cmbMember.Font;
            txtAmount.Font = cmbMember.Font;
            dtpPayDate.Font = cmbMember.Font;
        }
    }
}
