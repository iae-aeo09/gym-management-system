using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace GymManagementSystem
{
    public static class EmailHelper
    {
        public static void SendReceipt(string toEmail, string refNo, string name, decimal amount, string method)
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

(Email not actually sent)
");
                return;
            }

            // REAL EMAIL MODE
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
                        mail.From = new MailAddress(smtpUser, "FitPro Gym");
                        mail.To.Add(toEmail);
                        mail.Subject = $"Payment Receipt - {refNo}";
                        mail.Body = $@"Dear {name},

Your payment has been received.

Reference: {refNo}
Amount: ₱{amount:N2}
Method: {method}
Status: PAID

Thank you for being a member!";

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
        }
    }
}