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
        public Payments()
        {

            InitializeComponent();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            new Dashboard().Show();
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
            // in InitializeComponent() after dgvPayments created or in Payments_Load
            this.dgvPayments.AllowUserToAddRows = false;
        }



        private void Payments_Load(object sender, EventArgs e)
        {
            cmbMember.SelectedIndexChanged += cmbMember_SelectedIndexChanged;
            cmbMethod.TextChanged += cmbMethod_TextChanged;
            cmbMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMethod.Items.AddRange(new string[] {
            "Cash", "GCash", "PayPal", "Bank Transfer"
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
                    "SELECT MemberID, FullName, [Plan], MembershipFee, Email FROM Members WHERE IsArchived=0", conn);
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
                string query = @"SELECT ReferenceNo, MemberName, Amount, 
                             PaymentMethod, PaymentDate, Status 
                             FROM Payments ORDER BY PaymentID DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPayments.DataSource = dt;
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

            string receipt = $@"
============================
   FITPRO GYM SYSTEM
   Official Payment Receipt
============================
Reference : {refNo}
Member    : {memberName}
Amount    : ₱{amount:N2}
Method    : {method}
Date      : {date:MMMM dd, yyyy}
Status    : PAID ✓
============================";

            ReceiptDialog.ShowReceipt(this, receipt);
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

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();

                // Insert payment record
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Payments (MemberID, MemberName, Amount, PaymentMethod, PaymentDate, ReferenceNo, Status)
            VALUES (@mid, @mn, @amt, @mth, @dt, @ref, 'Paid')", conn);
                cmd.Parameters.AddWithValue("@mid", memberId);
                cmd.Parameters.AddWithValue("@mn", memberName);
                cmd.Parameters.AddWithValue("@amt", amount);
                cmd.Parameters.AddWithValue("@mth", method);
                cmd.Parameters.AddWithValue("@dt", payDate);
                cmd.Parameters.AddWithValue("@ref", refNo);
                cmd.ExecuteNonQuery();

                // Mark member as paid, but keep status based on expiry date.
                SqlCommand upd = new SqlCommand(
                    @"UPDATE Members
                      SET IsPaid=1,
                          Status = CASE 
                              WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
                              ELSE 'Active'
                          END
                      WHERE MemberID=@id", conn);
                upd.Parameters.AddWithValue("@id", memberId);
                upd.ExecuteNonQuery();
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
                    EmailHelper.SendReceipt(email, refNo, memberName, amount, method);
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
    }
    }
    

