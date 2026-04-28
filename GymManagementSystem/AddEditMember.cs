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
        private Panel panelPlanBenefits;
        private Label lblPlanBenefitsTitle;
        private Label lblSelectedPlanSummary;
        private FlowLayoutPanel flowPlanCards;
        private Panel[] planCards;

        // Plans: fee and months
        private readonly (decimal fee, int months)[] plans = {
        (800, 1), (2100, 3), (3800, 6), (6500, 12)
    };
        public AddEditMember()
        {

            InitializeComponent();
            Resize += AddEditMember_Resize;
            Shown += AddEditMember_Shown;
            ConfigureButtonTheme(btnBack, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnSave, ViltrumTheme.Accent, ViltrumTheme.AccentHover);
            ConfigureButtonTheme(btnClearReset, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnCancel, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            InitializePlanBenefitsCard();

        }
        // EDIT (THIS FIXES YOUR ERROR)
        public AddEditMember(int id)
        {
            InitializeComponent();
            memberId = id;
            Resize += AddEditMember_Resize;
            Shown += AddEditMember_Shown;
            ConfigureButtonTheme(btnBack, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnSave, ViltrumTheme.Accent, ViltrumTheme.AccentHover);
            ConfigureButtonTheme(btnClearReset, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            ConfigureButtonTheme(btnCancel, ViltrumTheme.SurfaceAlt, ViltrumTheme.Accent);
            InitializePlanBenefitsCard();
        }


        private void AddEditMember_Load(object sender, EventArgs e)
        {
            cmbPlan.Items.AddRange(new string[] {
            "Monthly", "Quarterly", "Semi-Annual", "Annual"
        });
            cmbPlan.SelectedIndex = -1;
            cmbPlan.Enabled = false;
            // Show plan cards only when user is choosing a plan.
            cmbPlan.DropDown += (s, args) =>
            {
                if (panelPlanBenefits != null) panelPlanBenefits.Visible = true;
            };
            cmbPlan.DropDownClosed += (s, args) =>
            {
                if (panelPlanBenefits != null) panelPlanBenefits.Visible = cmbPlan.SelectedIndex >= 0;
            };
            dtpJoinDate.Value = DateTime.Today;
            txtName.TextChanged += RequiredDetailsChanged;
            txtLastName.TextChanged += RequiredDetailsChanged;
            txtEmail.TextChanged += RequiredDetailsChanged;
            txtPhone.TextChanged += RequiredDetailsChanged;
            UpdatePlanAvailability();

            if (memberId > 0)
                LoadMemberData();

            UpdateClearResetButtonCaption();
        }

        private void UpdateClearResetButtonCaption()
        {
            btnClearReset.Text = memberId > 0 ? "RESET" : "CLEAR";
        }

        private void ClearNewMemberForm()
        {
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            dtpJoinDate.Value = DateTime.Today;
            cmbPlan.SelectedIndex = -1;
            cmbPlan.Enabled = false;
            txtFee.Text = string.Empty;
            tbExpiryDate.Text = string.Empty;
            UpdatePlanAvailability();
        }

        private void btnClearReset_Click(object sender, EventArgs e)
        {
            if (memberId > 0)
            {
                LoadMemberData();
                return;
            }

            ClearNewMemberForm();
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
                    string fullName = reader["FullName"].ToString();
                    SplitName(fullName, out string firstName, out string lastName);
                    txtName.Text = firstName;
                    txtLastName.Text = lastName;
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    cmbPlan.SelectedItem = reader["Plan"].ToString();
                    // Renewal should start from today's date (for demo + real renewal flow).
                    dtpJoinDate.Value = DateTime.Today;
                    txtFee.Text = reader["MembershipFee"].ToString();
                    tbExpiryDate.Text = Convert.ToDateTime(reader["ExpiryDate"]).ToString("MMMM dd, yyyy");
                }
            }
            UpdatePlanDetails();
        }

        private void SplitName(string fullName, out string firstName, out string lastName)
        {
            fullName = (fullName ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                firstName = string.Empty;
                lastName = string.Empty;
                return;
            }

            string[] parts = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                firstName = parts[0];
                lastName = string.Empty;
                return;
            }

            firstName = parts[0];
            lastName = string.Join(" ", parts.Skip(1));
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
            if (!cmbPlan.Enabled)
            {
                txtFee.Text = string.Empty;
                tbExpiryDate.Text = string.Empty;
                lblSelectedPlanSummary.Text = "Select a plan to preview benefits and savings.";
                UpdatePlanCardHighlight(-1);
                return;
            }

            int idx = cmbPlan.SelectedIndex;
            if (idx < 0 || idx >= plans.Length)
            {
                txtFee.Text = string.Empty;
                tbExpiryDate.Text = string.Empty;
                lblSelectedPlanSummary.Text = "Select a plan to preview benefits and savings.";
                UpdatePlanCardHighlight(-1);
                return;
            }

            txtFee.Text = plans[idx].fee.ToString("N2");
            tbExpiryDate.Text = GetComputedExpiryDate().ToString("MMMM dd, yyyy");
            lblSelectedPlanSummary.Text = GetSelectedPlanSummary(idx);
            UpdatePlanCardHighlight(idx);
        }

        private bool HasRequiredDetailsForPlan()
        {
            return !string.IsNullOrWhiteSpace(txtName.Text)
                && !string.IsNullOrWhiteSpace(txtLastName.Text)
                && !string.IsNullOrWhiteSpace(txtEmail.Text)
                && !string.IsNullOrWhiteSpace(txtPhone.Text);
        }

        private void UpdatePlanAvailability()
        {
            bool canChoosePlan = HasRequiredDetailsForPlan();
            if (cmbPlan.Enabled == canChoosePlan)
                return;

            cmbPlan.Enabled = canChoosePlan;
            if (!canChoosePlan)
            {
                cmbPlan.SelectedIndex = -1;
                UpdatePlanCardHighlight(-1);
            }
            if (panelPlanBenefits != null)
                panelPlanBenefits.Visible = canChoosePlan && cmbPlan.SelectedIndex >= 0;

            UpdatePlanDetails();
        }

        private void RequiredDetailsChanged(object sender, EventArgs e)
        {
            UpdatePlanAvailability();
        }

        private void InitializePlanBenefitsCard()
        {
            panelPlanBenefits = new Panel
            {
                BackColor = ViltrumTheme.Surface,
                BorderStyle = BorderStyle.FixedSingle
            };
            panelBodyMember.Controls.Add(panelPlanBenefits);
            panelPlanBenefits.BringToFront();
            panelPlanBenefits.Visible = false;

            lblPlanBenefitsTitle = new Label
            {
                AutoSize = true,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Text = "Plan Benefits and Savings"
            };
            panelPlanBenefits.Controls.Add(lblPlanBenefitsTitle);

            lblSelectedPlanSummary = new Label
            {
                AutoSize = false,
                ForeColor = Color.FromArgb(166, 214, 255),
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Text = "Select a plan to preview benefits and savings."
            };
            panelPlanBenefits.Controls.Add(lblSelectedPlanSummary);

            flowPlanCards = new FlowLayoutPanel
            {
                WrapContents = true,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            panelPlanBenefits.Controls.Add(flowPlanCards);

            planCards = new[]
            {
                BuildPlanCard("Monthly", "P800 / mo", string.Empty, new[] { "Gym access", "Use of all equipment", "Free orientation", "Free WiFi" }, 0),
                BuildPlanCard("Quarterly", "P2,100 / 3 mo", "Save P300", new[] { "Gym access", "Use of all equipment", "Free orientation", "Free WiFi", "1 guest pass" }, 1),
                BuildPlanCard("Semi-Annual", "P3,800 / 6 mo", "Save P1,000", new[] { "Gym access", "Use of all equipment", "Free orientation", "Free WiFi", "2 guest passes" }, 2),
                BuildPlanCard("Annual", "P6,500 / yr", "Save P3,100", new[] { "Gym access", "Use of all equipment", "Free orientation", "Free WiFi", "3 guest passes", "1 month free on renewal" }, 3)
            };

            foreach (Panel card in planCards)
                flowPlanCards.Controls.Add(card);
        }

        private Panel BuildPlanCard(string title, string price, string saveTag, string[] benefits, int idx)
        {
            Panel card = new Panel
            {
                Width = 170,
                Height = 212,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = ViltrumTheme.SurfaceAlt,
                Tag = idx,
                Margin = new Padding(0, 0, 10, 10)
            };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 8)
            };
            card.Controls.Add(lblTitle);

            Label lblPrice = new Label
            {
                Text = price,
                ForeColor = Color.WhiteSmoke,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 30)
            };
            card.Controls.Add(lblPrice);

            if (!string.IsNullOrWhiteSpace(saveTag))
            {
                Label lblSave = new Label
                {
                    Text = saveTag,
                    ForeColor = Color.FromArgb(202, 238, 214),
                    BackColor = Color.FromArgb(36, 88, 64),
                    Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold),
                    AutoSize = true,
                    Padding = new Padding(6, 2, 6, 2),
                    Location = new Point(10, 56)
                };
                card.Controls.Add(lblSave);
            }

            Label lblBenefits = new Label
            {
                AutoSize = false,
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 8.4F, FontStyle.Regular),
                Location = new Point(10, 82),
                Size = new Size(148, 120),
                Text = string.Join("\n", benefits.Select(b => "- " + b))
            };
            card.Controls.Add(lblBenefits);

            // Make the whole card clickable to select the plan.
            WireClickToSelectPlan(card);
            WireClickToSelectPlan(lblTitle);
            WireClickToSelectPlan(lblPrice);
            foreach (Control child in card.Controls)
                WireClickToSelectPlan(child);

            return card;
        }

        private void WireClickToSelectPlan(Control control)
        {
            if (control == null) return;
            control.Cursor = Cursors.Hand;
            control.Click -= PlanCard_Click;
            control.Click += PlanCard_Click;
        }

        private void PlanCard_Click(object sender, EventArgs e)
        {
            // Sender can be a nested label; walk up to find the card panel with Tag=int index.
            Control c = sender as Control;
            while (c != null && !(c is Panel && c.Tag is int))
                c = c.Parent;

            if (c == null || !(c.Tag is int idx)) return;
            if (!cmbPlan.Enabled) return;

            if (idx >= 0 && idx < cmbPlan.Items.Count)
            {
                cmbPlan.SelectedIndex = idx;
                // Ensure it stays visible after selection.
                if (panelPlanBenefits != null) panelPlanBenefits.Visible = true;
            }
        }

        private void UpdatePlanCardHighlight(int selectedIdx)
        {
            if (planCards == null) return;
            for (int i = 0; i < planCards.Length; i++)
            {
                bool selected = i == selectedIdx;
                planCards[i].BackColor = selected ? Color.FromArgb(34, 78, 66) : ViltrumTheme.SurfaceAlt;
                planCards[i].Padding = selected ? new Padding(1) : Padding.Empty;
            }
        }

        private string GetSelectedPlanSummary(int idx)
        {
            switch (idx)
            {
                case 0:
                    return "Selected: Monthly | No bundle discount | Core gym access plan.";
                case 1:
                    return "Selected: Quarterly | Save P300 | Includes 1 guest pass.";
                case 2:
                    return "Selected: Semi-Annual | Save P1,000 | Includes 2 guest passes.";
                case 3:
                    return "Selected: Annual | Save P3,100 | Includes 3 guest passes + 1 free month on renewal.";
                default:
                    return "Select a plan to preview benefits and savings.";
            }
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
            if (string.IsNullOrWhiteSpace(txtName.Text)) missing.Add("First Name");
            if (string.IsNullOrWhiteSpace(txtLastName.Text)) missing.Add("Last Name");
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) missing.Add("Email");
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) missing.Add("Phone");
            if (cmbPlan.SelectedItem == null) missing.Add("Plan");
            if (string.IsNullOrWhiteSpace(tbExpiryDate.Text)) missing.Add("Expiry Date");

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

            if (!DateTime.TryParse(tbExpiryDate.Text, out _))
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
            string fullName = $"{txtName.Text.Trim()} {txtLastName.Text.Trim()}".Trim();

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
                    cmd.Parameters.AddWithValue("@n", fullName);
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

        private void AddEditMember_Shown(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void AddEditMember_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyResponsiveLayout()
        {
            int topOffset = panelTop.Bottom;
            int availableHeight = ClientSize.Height - topOffset;
            float scale = Math.Max(1.0f, Math.Min(1.22f, ClientSize.Width / 1366f));
            panelBodyMember.Width = Clamp((int)(ClientSize.Width * 0.44), 560, 920);
            panelBodyMember.Height = Clamp((int)(availableHeight * 0.68), 420, 700);
            panelBodyMember.Left = Math.Max(16, (ClientSize.Width - panelBodyMember.Width) / 2);
            panelBodyMember.Top = topOffset + Math.Max(16, (availableHeight - panelBodyMember.Height) / 2);

            pictureBox1.Left = panelTop.ClientSize.Width - pictureBox1.Width;

            int leftPad = Math.Max(24, (int)(panelBodyMember.Width * 0.05));
            int rightPad = leftPad;
            int detailsGap = 18;
            // Keep the plan cards visible: at narrower widths, placing the card below the buttons
            // can push it outside the panel. Prefer a right-side layout sooner.
            bool showSideBenefits = panelBodyMember.Width >= 620;
            int benefitsWidth = showSideBenefits ? Math.Max(280, (int)(panelBodyMember.Width * 0.36)) : 0;
            int labelWidth = Math.Max(110, (int)(panelBodyMember.Width * 0.18));
            int availableRight = panelBodyMember.Width - rightPad - (showSideBenefits ? (benefitsWidth + detailsGap) : 0);
            int inputX = leftPad + labelWidth + 16;
            int inputWidth = Math.Max(210, availableRight - inputX);

            int startY = Math.Max(38, (int)(panelBodyMember.Height * 0.12));
            int rowGap = Math.Max(36, (int)(panelBodyMember.Height * 0.095));

            label1.Location = new Point(leftPad, startY + 3);
            txtName.Location = new Point(inputX, startY);
            txtName.Width = inputWidth;

            label9.Location = new Point(leftPad, startY + rowGap + 3);
            txtLastName.Location = new Point(inputX, startY + rowGap);
            txtLastName.Width = inputWidth;

            label2.Location = new Point(leftPad, startY + (rowGap * 2) + 3);
            txtEmail.Location = new Point(inputX, startY + (rowGap * 2));
            txtEmail.Width = inputWidth;

            label3.Location = new Point(leftPad, startY + (rowGap * 3) + 3);
            txtPhone.Location = new Point(inputX, startY + (rowGap * 3));
            txtPhone.Width = inputWidth;

            label6.Location = new Point(leftPad, startY + (rowGap * 4) + 3);
            dtpJoinDate.Location = new Point(inputX, startY + (rowGap * 4));
            dtpJoinDate.Width = inputWidth;

            label5.Location = new Point(leftPad, startY + (rowGap * 5) + 3);
            txtFee.Location = new Point(inputX, startY + (rowGap * 5));
            txtFee.Width = inputWidth;

            label4.Location = new Point(leftPad, startY + (rowGap * 6) + 3);
            cmbPlan.Location = new Point(inputX, startY + (rowGap * 6));
            cmbPlan.Width = inputWidth;

            txtExpiry.Location = new Point(leftPad, startY + (rowGap * 7) + 3);
            tbExpiryDate.Location = new Point(inputX, startY + (rowGap * 7));
            tbExpiryDate.Width = inputWidth;

            int buttonY = Math.Min(panelBodyMember.Height - 54, startY + (rowGap * 8));
            int buttonGap = 10;
            int buttonWidth = Math.Max(100, (inputWidth - (buttonGap * 2)) / 3);
            btnSave.Location = new Point(inputX, buttonY);
            btnSave.Width = buttonWidth;
            btnClearReset.Location = new Point(inputX + buttonWidth + buttonGap, buttonY);
            btnClearReset.Width = buttonWidth;
            btnCancel.Location = new Point(inputX + (buttonWidth + buttonGap) * 2, buttonY);
            btnCancel.Width = buttonWidth;
            btnSave.Height = (int)(36 * scale);
            btnClearReset.Height = (int)(36 * scale);
            btnCancel.Height = (int)(36 * scale);

            // Align the benefits panel with the form fields column.
            int detailsRight = inputX + inputWidth;
            int cardX = showSideBenefits ? (detailsRight + detailsGap) : leftPad;
            int cardY = showSideBenefits ? Math.Max(18, startY - 2) : (buttonY + btnSave.Height + 12);
            int cardWidth = showSideBenefits ? benefitsWidth : Math.Max(220, panelBodyMember.Width - (leftPad * 2));
            int maxSideHeight = Math.Max(220, buttonY - cardY - 10);
            int cardHeight = showSideBenefits
                ? maxSideHeight
                : Math.Max(140, panelBodyMember.Height - cardY - 14);

            panelPlanBenefits.Location = new Point(cardX, cardY);
            panelPlanBenefits.Size = new Size(cardWidth, cardHeight);
            lblPlanBenefitsTitle.Location = new Point(12, 10);
            lblSelectedPlanSummary.Location = new Point(12, 38);
            lblSelectedPlanSummary.Size = new Size(cardWidth - 24, 40);
            flowPlanCards.Location = new Point(12, 82);
            flowPlanCards.Size = new Size(cardWidth - 24, cardHeight - 94);

            if (planCards != null)
            {
                int cardWidthResponsive = Math.Max(145, Math.Min(190, (flowPlanCards.ClientSize.Width - 10) / 2));
                foreach (Panel card in planCards)
                {
                    card.Width = cardWidthResponsive;
                }
            }

            Font labelFont = new Font("Segoe UI Semibold", 9.5f * scale, FontStyle.Bold);
            Font inputFont = new Font("Segoe UI", 9f * scale, FontStyle.Regular);
            label1.Font = labelFont;
            label9.Font = labelFont;
            label2.Font = labelFont;
            label3.Font = labelFont;
            label4.Font = labelFont;
            label5.Font = labelFont;
            label6.Font = labelFont;
            txtExpiry.Font = labelFont;
            txtName.Font = inputFont;
            txtLastName.Font = inputFont;
            txtEmail.Font = inputFont;
            txtPhone.Font = inputFont;
            dtpJoinDate.Font = inputFont;
            txtFee.Font = inputFont;
            cmbPlan.Font = inputFont;
            tbExpiryDate.Font = inputFont;
            btnSave.Font = new Font("Segoe UI Semibold", 9.5f * scale, FontStyle.Bold);
            btnClearReset.Font = btnSave.Font;
            btnCancel.Font = btnSave.Font;
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Min(max, Math.Max(min, value));
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
