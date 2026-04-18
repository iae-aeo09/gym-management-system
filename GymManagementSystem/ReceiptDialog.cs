using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public class ReceiptDialog : Form
    {
        private Label lblTitle;
        private Label lblSubTitle;
        private Panel panelReceiptCard;
        private Label lblRefValue;
        private Label lblMemberValue;
        private Label lblAmountValue;
        private Label lblMethodValue;
        private Label lblDateValue;
        private Label lblStatusValue;
        private Button btnClose;
        private Button btnExportPdf;
        private Panel panelTop;
        private Panel panelBottom;
        private ReceiptInfo receipt;

        public ReceiptDialog(ReceiptInfo receiptInfo)
        {
            InitializeComponent();
            receipt = receiptInfo;
            BindReceipt();
        }

        public static void ShowReceipt(IWin32Window owner, ReceiptInfo receiptInfo)
        {
            using (var dialog = new ReceiptDialog(receiptInfo))
            {
                dialog.ShowDialog(owner);
            }
        }

        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblSubTitle = new Label();
            this.lblTitle = new Label();
            this.panelReceiptCard = new Panel();
            this.lblRefValue = new Label();
            this.lblMemberValue = new Label();
            this.lblAmountValue = new Label();
            this.lblMethodValue = new Label();
            this.lblDateValue = new Label();
            this.lblStatusValue = new Label();
            this.panelBottom = new Panel();
            this.btnClose = new Button();
            this.btnExportPdf = new Button();
            this.panelTop.SuspendLayout();
            this.panelReceiptCard.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.FromArgb(34, 43, 58);
            this.panelTop.Controls.Add(this.lblSubTitle);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new Size(520, 82);
            this.panelTop.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.ForeColor = Color.Gainsboro;
            this.lblSubTitle.Location = new Point(27, 49);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new Size(194, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Official member payment evidence";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = Color.WhiteSmoke;
            this.lblTitle.Location = new Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(195, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PAYMENT RECEIPT";
            // 
            // panelReceiptCard
            this.panelReceiptCard.BackColor = Color.FromArgb(46, 57, 74);
            this.panelReceiptCard.Location = new Point(24, 100);
            this.panelReceiptCard.Name = "panelReceiptCard";
            this.panelReceiptCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelReceiptCard.Size = new Size(472, 240);
            this.panelReceiptCard.Controls.Add(CreateCaption("Reference No", 24, 24));
            this.panelReceiptCard.Controls.Add(CreateCaption("Member Name", 24, 60));
            this.panelReceiptCard.Controls.Add(CreateCaption("Amount", 24, 96));
            this.panelReceiptCard.Controls.Add(CreateCaption("Payment Method", 24, 132));
            this.panelReceiptCard.Controls.Add(CreateCaption("Payment Date", 24, 168));
            this.panelReceiptCard.Controls.Add(CreateCaption("Status", 24, 204));

            ConfigureValueLabel(this.lblRefValue, 180, 24);
            ConfigureValueLabel(this.lblMemberValue, 180, 60);
            ConfigureValueLabel(this.lblAmountValue, 180, 96);
            ConfigureValueLabel(this.lblMethodValue, 180, 132);
            ConfigureValueLabel(this.lblDateValue, 180, 168);
            ConfigureValueLabel(this.lblStatusValue, 180, 204);

            this.lblStatusValue.ForeColor = Color.LightGreen;

            this.panelReceiptCard.Controls.Add(this.lblRefValue);
            this.panelReceiptCard.Controls.Add(this.lblMemberValue);
            this.panelReceiptCard.Controls.Add(this.lblAmountValue);
            this.panelReceiptCard.Controls.Add(this.lblMethodValue);
            this.panelReceiptCard.Controls.Add(this.lblDateValue);
            this.panelReceiptCard.Controls.Add(this.lblStatusValue);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = Color.FromArgb(40, 50, 66);
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnExportPdf);
            this.panelBottom.Dock = DockStyle.Bottom;
            this.panelBottom.Location = new Point(0, 356);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new Size(520, 58);
            this.panelBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = Color.FromArgb(188, 44, 44);
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(392, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClose.Size = new Size(104, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.BackColor = Color.FromArgb(80, 91, 109);
            this.btnExportPdf.FlatAppearance.BorderSize = 0;
            this.btnExportPdf.FlatStyle = FlatStyle.Flat;
            this.btnExportPdf.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPdf.ForeColor = Color.White;
            this.btnExportPdf.Location = new Point(24, 14);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new Size(164, 30);
            this.btnExportPdf.TabIndex = 1;
            this.btnExportPdf.Text = "Download && Open PDF";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new EventHandler(this.btnExportPdf_Click);
            // 
            // ReceiptDialog
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(28, 36, 49);
            this.ClientSize = new Size(520, 414);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelReceiptCard);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiptDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Payment Receipt";
            this.WindowState = FormWindowState.Normal;
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelReceiptCard.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Label CreateCaption(string text, int x, int y)
        {
            return new Label
            {
                AutoSize = true,
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = text,
                Location = new Point(x, y)
            };
        }

        private void ConfigureValueLabel(Label label, int x, int y)
        {
            label.AutoSize = true;
            label.ForeColor = Color.WhiteSmoke;
            label.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            label.Location = new Point(x, y);
            label.MaximumSize = new Size(260, 0);
            label.Text = "-";
        }

        private void BindReceipt()
        {
            if (receipt == null) return;

            lblRefValue.Text = receipt.ReferenceNo ?? "-";
            lblMemberValue.Text = receipt.MemberName ?? "-";
            lblAmountValue.Text = "P " + receipt.Amount.ToString("N2");
            lblMethodValue.Text = receipt.PaymentMethod ?? "-";
            lblDateValue.Text = receipt.PaymentDate == DateTime.MinValue
                ? "-"
                : receipt.PaymentDate.ToString("MMMM dd, yyyy");
            lblStatusValue.Text = string.IsNullOrWhiteSpace(receipt.Status) ? "Paid" : receipt.Status;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (receipt == null)
            {
                MessageBox.Show("No receipt data available.");
                return;
            }

            try
            {
                string downloadsPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Downloads");
                if (!Directory.Exists(downloadsPath))
                    downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string fileName = "Receipt-" + (receipt.ReferenceNo ?? DateTime.Now.ToString("yyyyMMddHHmmss")) + ".pdf";
                string path = Path.Combine(downloadsPath, fileName);

                ReceiptPdfExporter.Export(receipt, path);

                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export PDF: " + ex.Message, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
