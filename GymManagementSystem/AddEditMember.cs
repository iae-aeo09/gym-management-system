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
using System.Xml.Linq;

namespace GymManagementSystem
{
    public partial class AddEditMember : Form
    {
        private int memberId = 0;

        // Plans: fee and months
        private readonly (decimal fee, int months)[] plans = {
        (800, 1), (2400, 3), (4800, 6), (9600, 12)
    };
        public AddEditMember()
        {

            InitializeComponent();

            
          

        }
        // EDIT (THIS FIXES YOUR ERROR)
        public AddEditMember(int id)
        {
            InitializeComponent();
            memberId = id;
        }


        private void AddEditMember_Load(object sender, EventArgs e)
        {
            cmbPlan.Items.AddRange(new string[] {
            "Monthly", "Quarterly", "Semi-Annual", "Annual"
        });
            cmbPlan.SelectedIndex = 0;
            dtpJoinDate.Value = DateTime.Today;
            UpdatePlanDetails();

            if (memberId > 0)
                LoadMemberData();
        }
        private void LoadMemberData()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Members WHERE MemberID=@id", conn);
                cmd.Parameters.AddWithValue("@id", memberId);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["FullName"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    cmbPlan.SelectedItem = reader["Plan"].ToString();
                    dtpJoinDate.Value = Convert.ToDateTime(reader["JoinDate"]);
                    txtFee.Text = reader["MembershipFee"].ToString();
                    textBox5.Text = Convert.ToDateTime(reader["ExpiryDate"]).ToString("MMMM dd, yyyy");
                }
            }
            UpdatePlanDetails();
        }


        private void cmbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlanDetails();
        }

        private DateTime GetComputedExpiryDate()
        {
            int idx = cmbPlan.SelectedIndex;
            if (idx < 0 || idx >= plans.Length)
                return dtpJoinDate.Value.Date;

            return dtpJoinDate.Value.Date.AddMonths(plans[idx].months);
        }

        private void UpdatePlanDetails()
        {
            int idx = cmbPlan.SelectedIndex;
            if (idx < 0 || idx >= plans.Length)
            {
                txtFee.Text = string.Empty;
                textBox5.Text = string.Empty;
                return;
            }

            txtFee.Text = plans[idx].fee.ToString("N2");
            textBox5.Text = GetComputedExpiryDate().ToString("MMMM dd, yyyy");
        }

        

        private void dtpJoinDate_ValueChanged_1(object sender, EventArgs e)
        {
            cmbPlan_SelectedIndexChanged(sender, e);
        }

        private string GetPhoneDigits()
        {
            return new string(txtPhone.Text.Where(char.IsDigit).ToArray());
        }

        private bool ValidateMemberInputs()
        {
            var missing = new List<string>();
            if (string.IsNullOrWhiteSpace(txtName.Text)) missing.Add("Name");
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) missing.Add("Email");
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) missing.Add("Phone");
            if (cmbPlan.SelectedItem == null) missing.Add("Plan");
            if (string.IsNullOrWhiteSpace(textBox5.Text)) missing.Add("Expiry Date");

            if (missing.Count > 0)
            {
                MessageBox.Show("Please enter: " + string.Join(", ", missing),
                    "Missing input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // phone: normalize digits then check length
            var phoneDigits = GetPhoneDigits();
            if (phoneDigits.Length != 11)
            {
                MessageBox.Show("Phone must be exactly 11 digits (numbers only).",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // simple email format check
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtFee.Text, out _))
            {
                MessageBox.Show("Membership fee must be a valid number.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!DateTime.TryParse(textBox5.Text, out _))
            {
                MessageBox.Show("Expiry date is invalid.",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateMemberInputs())
                return;

            var phoneDigits = GetPhoneDigits();
            DateTime computedExpiry = GetComputedExpiryDate();
            string computedStatus = computedExpiry.Date < DateTime.Today ? "Expired" : "Active";

            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                string query = memberId == 0
    ? @"INSERT INTO Members (FullName,Email,Phone,[Plan],MembershipFee,JoinDate,ExpiryDate,Status,IsPaid,IsArchived)
       VALUES (@n,@e,@ph,@pl,@f,@j,@ex,@status,0,0)"
    : @"UPDATE Members SET FullName=@n,Email=@e,Phone=@ph,[Plan]=@pl,
       MembershipFee=@f,JoinDate=@j,ExpiryDate=@ex,Status=@status WHERE MemberID=@id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@n", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@e", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@ph", phoneDigits);
                    cmd.Parameters.AddWithValue("@pl", cmbPlan.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@f", decimal.Parse(txtFee.Text));
                    cmd.Parameters.AddWithValue("@j", dtpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@ex", computedExpiry);
                    cmd.Parameters.AddWithValue("@status", computedStatus);
                    if (memberId > 0)
                        cmd.Parameters.AddWithValue("@id", memberId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show(memberId == 0 ? "Member added!" : "Member updated!");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            btnBack_Click(sender, e);
        }

        private void AddEditMember_Load_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbPlan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cmbPlan_SelectedIndexChanged(sender, e);
        }
    }
}
