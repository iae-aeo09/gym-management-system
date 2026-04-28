using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace GymManagementSystem
{
    public static class ReceiptPdfExporter
    {
        public static void Export(ReceiptInfo receipt, string outputPath)
        {
            if (receipt == null) throw new ArgumentNullException(nameof(receipt));

            StringBuilder content = new StringBuilder();

            // Header bar
            content.AppendLine("0.13 0.17 0.24 rg");
            content.AppendLine("40 760 515 62 re f");

            // Header title
            content.AppendLine("BT");
            content.AppendLine("1 1 1 rg");
            content.AppendLine("/F1 16 Tf");
            content.AppendLine("56 798 Td");
            content.AppendLine("(" + EscapePdfText("VILTRUM GYM MEMBERSHIP") + ") Tj");
            content.AppendLine("ET");

            // Header subtitle
            content.AppendLine("BT");
            content.AppendLine("0.88 0.9 0.93 rg");
            content.AppendLine("/F1 10 Tf");
            content.AppendLine("56 778 Td");
            content.AppendLine("(" + EscapePdfText("Official Payment Receipt") + ") Tj");
            content.AppendLine("ET");

            // White card body (taller to fit plan/expiry/benefits cleanly)
            content.AppendLine("1 1 1 rg");
            content.AppendLine("40 430 515 330 re f");
            content.AppendLine("0.84 0.86 0.9 RG");
            content.AppendLine("1 w");
            content.AppendLine("40 430 515 330 re S");

            // Two-column receipt details
            string amountText = "PHP " + receipt.Amount.ToString("N2", CultureInfo.InvariantCulture);
            string dateText = receipt.PaymentDate == DateTime.MinValue
                ? "-"
                : receipt.PaymentDate.ToString("MMMM dd, yyyy");
            string statusText = string.IsNullOrWhiteSpace(receipt.Status) ? "Paid" : receipt.Status;
            string planText = string.IsNullOrWhiteSpace(receipt.Plan) ? "-" : receipt.Plan;
            string expiryText = receipt.ExpiryDate == DateTime.MinValue ? "-" : receipt.ExpiryDate.ToString("MMMM dd, yyyy");
            string benefitsText = string.IsNullOrWhiteSpace(receipt.Benefits)
                ? "-"
                : receipt.Benefits.Replace("\r", "").Replace("\n", ", ");
            if (benefitsText.Length > 95) benefitsText = benefitsText.Substring(0, 92) + "...";

            string[,] rows = new[,]
            {
                { "Reference No", Safe(receipt.ReferenceNo) },
                { "Member Name", Safe(receipt.MemberName) },
                { "Amount", amountText },
                { "Method", Safe(receipt.PaymentMethod) },
                { "Payment Date", dateText },
                { "Status", statusText },
                { "Plan", planText },
                { "Expiry Date", expiryText },
                { "Benefits", benefitsText }
            };

            int rowY = 734;
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                // Label
                content.AppendLine("BT");
                content.AppendLine("0.34 0.37 0.43 rg");
                content.AppendLine("/F1 10 Tf");
                content.AppendLine("60 " + rowY + " Td");
                content.AppendLine("(" + EscapePdfText(rows[i, 0]) + ") Tj");
                content.AppendLine("ET");

                // Value
                content.AppendLine("BT");
                content.AppendLine("0.12 0.12 0.12 rg");
                content.AppendLine("/F1 10 Tf");
                content.AppendLine("180 " + rowY + " Td");
                content.AppendLine("(" + EscapePdfText(rows[i, 1]) + ") Tj");
                content.AppendLine("ET");

                // Divider
                if (i < rows.GetLength(0) - 1)
                {
                    int lineY = rowY - 8;
                    content.AppendLine("0.91 0.92 0.94 RG");
                    content.AppendLine("0.6 w");
                    content.AppendLine("56 " + lineY + " m");
                    content.AppendLine("534 " + lineY + " l");
                    content.AppendLine("S");
                }

                rowY -= 30;
            }

            // Footer (always below details block)
            int footerY = Math.Max(438, rowY - 10);
            content.AppendLine("BT");
            content.AppendLine("0.35 0.39 0.45 rg");
            content.AppendLine("/F1 10 Tf");
            content.AppendLine("56 " + footerY + " Td");
            content.AppendLine("(" + EscapePdfText("Thank you for your payment.") + ") Tj");
            content.AppendLine("ET");

            byte[] contentBytes = Encoding.ASCII.GetBytes(content.ToString());
            string obj1 = "1 0 obj << /Type /Catalog /Pages 2 0 R >> endobj\n";
            string obj2 = "2 0 obj << /Type /Pages /Kids [3 0 R] /Count 1 >> endobj\n";
            string obj3 = "3 0 obj << /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >> endobj\n";
            string obj4 = "4 0 obj << /Type /Font /Subtype /Type1 /BaseFont /Helvetica >> endobj\n";
            string obj5Header = "5 0 obj << /Length " + contentBytes.Length + " >> stream\n";
            string obj5Footer = "endstream\nendobj\n";

            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                WriteAscii(fs, "%PDF-1.4\n");
                long xref1 = fs.Position; WriteAscii(fs, obj1);
                long xref2 = fs.Position; WriteAscii(fs, obj2);
                long xref3 = fs.Position; WriteAscii(fs, obj3);
                long xref4 = fs.Position; WriteAscii(fs, obj4);
                long xref5 = fs.Position; WriteAscii(fs, obj5Header);
                fs.Write(contentBytes, 0, contentBytes.Length);
                WriteAscii(fs, "\n" + obj5Footer);

                long xrefStart = fs.Position;
                WriteAscii(fs, "xref\n");
                WriteAscii(fs, "0 6\n");
                WriteAscii(fs, "0000000000 65535 f \n");
                WriteAscii(fs, xref1.ToString("D10") + " 00000 n \n");
                WriteAscii(fs, xref2.ToString("D10") + " 00000 n \n");
                WriteAscii(fs, xref3.ToString("D10") + " 00000 n \n");
                WriteAscii(fs, xref4.ToString("D10") + " 00000 n \n");
                WriteAscii(fs, xref5.ToString("D10") + " 00000 n \n");
                WriteAscii(fs, "trailer << /Size 6 /Root 1 0 R >>\n");
                WriteAscii(fs, "startxref\n");
                WriteAscii(fs, xrefStart.ToString() + "\n");
                WriteAscii(fs, "%%EOF");
            }
        }

        private static void WriteAscii(FileStream fs, string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            fs.Write(bytes, 0, bytes.Length);
        }

        private static string EscapePdfText(string text)
        {
            return Safe(text).Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
        }

        private static string Safe(string text)
        {
            return text ?? string.Empty;
        }
    }
}
