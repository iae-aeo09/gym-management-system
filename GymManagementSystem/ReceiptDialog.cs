using System;
using System.Drawing;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public class ReceiptDialog : Form
    {
        private Label lblTitle;
        private Label lblSubTitle;
        private RichTextBox rtbReceipt;
        private Button btnClose;
        private Panel panelTop;
        private Panel panelBottom;

        public ReceiptDialog(string receiptContent)
        {
            InitializeComponent();
            rtbReceipt.Text = receiptContent;
        }

        public static void ShowReceipt(IWin32Window owner, string receiptContent)
        {
            using (var dialog = new ReceiptDialog(receiptContent))
            {
                dialog.ShowDialog(owner);
            }
        }

        private void InitializeComponent()
        {
            this.panelTop = new Panel();
            this.lblSubTitle = new Label();
            this.lblTitle = new Label();
            this.rtbReceipt = new RichTextBox();
            this.panelBottom = new Panel();
            this.btnClose = new Button();
            this.panelTop.SuspendLayout();
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
            // rtbReceipt
            // 
            this.rtbReceipt.BackColor = Color.FromArgb(46, 57, 74);
            this.rtbReceipt.BorderStyle = BorderStyle.None;
            this.rtbReceipt.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.rtbReceipt.ForeColor = Color.WhiteSmoke;
            this.rtbReceipt.Location = new Point(24, 100);
            this.rtbReceipt.Name = "rtbReceipt";
            this.rtbReceipt.ReadOnly = true;
            this.rtbReceipt.Size = new Size(472, 240);
            this.rtbReceipt.TabIndex = 1;
            this.rtbReceipt.Text = "";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = Color.FromArgb(40, 50, 66);
            this.panelBottom.Controls.Add(this.btnClose);
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
            this.btnClose.Size = new Size(104, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            // 
            // ReceiptDialog
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(28, 36, 49);
            this.ClientSize = new Size(520, 414);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.rtbReceipt);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceiptDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Payment Receipt";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
