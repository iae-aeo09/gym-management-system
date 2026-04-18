using System;
using System.Data.SqlClient;

namespace GymManagementSystem
{
    public class DBConnection
    {
        private static string connStr =
            @"Server=DESKTOP-1SFN2LH;Database=GymManagementDB;User Id=sa;Password=1945;Encrypt=False;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }

        public static string GetConnectionString()
        {
            return connStr;
        }

        public static string GetDatabaseName()
        {
            var builder = new SqlConnectionStringBuilder(connStr);
            return builder.InitialCatalog;
        }

        private static bool schemaEnsured = false;
        private static readonly object schemaLock = new object();

        public static void EnsureFeatureSchema()
        {
            if (schemaEnsured) return;

            lock (schemaLock)
            {
                if (schemaEnsured) return;

                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
IF COL_LENGTH('Members', 'IsFrozen') IS NULL
    ALTER TABLE Members ADD IsFrozen BIT NOT NULL CONSTRAINT DF_Members_IsFrozen DEFAULT(0);

IF COL_LENGTH('Members', 'FrozenFrom') IS NULL
    ALTER TABLE Members ADD FrozenFrom DATE NULL;

IF COL_LENGTH('Members', 'FrozenUntil') IS NULL
    ALTER TABLE Members ADD FrozenUntil DATE NULL;

IF OBJECT_ID('dbo.ReminderLogs', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ReminderLogs(
        ReminderID INT IDENTITY(1,1) PRIMARY KEY,
        MemberID INT NOT NULL,
        ReminderType NVARCHAR(30) NOT NULL,
        SentAt DATETIME NOT NULL DEFAULT(GETDATE()),
        Channel NVARCHAR(20) NULL,
        Notes NVARCHAR(200) NULL
    );
END;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                schemaEnsured = true;
            }
        }
    }
}