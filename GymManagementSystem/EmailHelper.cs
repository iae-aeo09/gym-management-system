using System;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public static class EmailHelper
    {
        public static void SendReceipt(string toEmail, string refNo, string name, decimal amount, string method)
        {
            SendReceipt(toEmail, refNo, name, amount, method, DateTime.Today);
        }

        public static void SendReceipt(string toEmail, string refNo, string name, decimal amount, string method, DateTime paymentDate)
        {
            // FAKE EMAIL MODE
            if (!AppSettings.UseRealEmail)
            {
                MessageBox.Show($@"
[FAKE EMAIL MODE]

To: {toEmail}

Reference: {refNo}
Name: {name}
Amount: ₱{amount:N2}
Method: {method}
Date: {paymentDate:MMMM dd, yyyy}

(Email not actually sent)
");
                return;
            }

            // REAL EMAIL MODE
            string tempPdfPath = string.Empty;
            try
            {
                string smtpUser = (AppSettings.SmtpUser ?? string.Empty).Trim();

                // Gmail app passwords are shown with spaces; SMTP expects one token
                string smtpPass = (AppSettings.SmtpPass ?? string.Empty)
                    .Replace(" ", string.Empty)
                    .Trim();

                if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPass))
                {
                    MessageBox.Show("SMTP credentials are not configured. Set SmtpUser and SmtpPass in App.config.");
                    return;
                }

                // Generate a receipt PDF and attach it to the email.
                tempPdfPath = Path.Combine(Path.GetTempPath(), $"Receipt-{refNo}.pdf");
                ReceiptPdfExporter.Export(new ReceiptInfo
                {
                    ReferenceNo = refNo,
                    MemberName = name,
                    Amount = amount,
                    PaymentMethod = method,
                    PaymentDate = paymentDate,
                    Status = "Paid"
                }, tempPdfPath);

                // Force modern TLS for Gmail
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress(smtpUser, "Viltrum Gym");
                        mail.To.Add(toEmail);
                        mail.Subject = $"Payment Receipt - {refNo}";
                        mail.Body = $@"Dear {name},

Your payment has been received.

Reference: {refNo}
Amount: ₱{amount:N2}
Method: {method}
Status: PAID

Thank you for being a member!";

                        if (File.Exists(tempPdfPath))
                            mail.Attachments.Add(new Attachment(tempPdfPath));

                        smtp.Send(mail);
                    }
                }
            }
            catch (SmtpException ex)
            {
                var details = ex.InnerException != null
                    ? $"{ex.Message}\nInner: {ex.InnerException.Message}"
                    : ex.Message;

                MessageBox.Show($"Email Error ({ex.StatusCode}): {details}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Email Error: " + ex.Message);
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(tempPdfPath) && File.Exists(tempPdfPath))
                        File.Delete(tempPdfPath);
                }
                catch
                {
                    // Best effort temp-file cleanup.
                }
            }
        }

        public static void SendRenewalReminder(string toEmail, string name, DateTime expiryDate, int daysLeft)
        {
            if (!AppSettings.UseRealEmail)
                return;

            try
            {
                string smtpUser = (AppSettings.SmtpUser ?? string.Empty).Trim();
                string smtpPass = (AppSettings.SmtpPass ?? string.Empty).Replace(" ", string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPass))
                    return;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    using (var mail = new MailMessage())
                    {
                        mail.From = new MailAddress(smtpUser, "Viltrum Gym");
                        mail.To.Add(toEmail);
                        mail.Subject = "Membership Renewal Reminder";
                        mail.Body = $@"Dear {name},

This is a reminder that your membership expires on {expiryDate:MMMM dd, yyyy} ({daysLeft} day(s) left).

Please renew on or before the expiry date to keep your membership active.

Thank you,
Viltrum Gym";
                        smtp.Send(mail);
                    }
                }
            }
            catch
            {
                // Non-blocking reminder send.
            }
        }
    }
}