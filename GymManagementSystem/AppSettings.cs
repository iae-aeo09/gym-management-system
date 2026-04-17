using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem
{
    using System.Configuration;

    public static class AppSettings
    {
        // false = FAKE (no real email)
        // true = REAL (Gmail)
        public static bool UseRealEmail =
            bool.TryParse(ConfigurationManager.AppSettings["UseRealEmail"], out var v) && v;

        // SMTP settings (store app password here locally; do NOT commit real secrets)
        public static string SmtpUser => ConfigurationManager.AppSettings["SmtpUser"] ?? string.Empty;
        public static string SmtpPass => ConfigurationManager.AppSettings["SmtpPass"] ?? string.Empty;
    }
}
