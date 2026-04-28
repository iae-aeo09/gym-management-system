using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GymManagementSystem
{
    public class DBConnection
    {
        private static readonly string connStr =
            ConfigurationManager.ConnectionStrings["GymManagementDb"]?.ConnectionString
            ?? @"Server=DESKTOP-1SFN2LH;Database=GymManagementDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
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
                    conn.Open(); //Freeze 
                    using (SqlCommand cmd = new SqlCommand(@"
IF COL_LENGTH('Members', 'IsFrozen') IS NULL
    ALTER TABLE Members ADD IsFrozen BIT NOT NULL CONSTRAINT DF_Members_IsFrozen DEFAULT(0);

IF COL_LENGTH('Members', 'FrozenFrom') IS NULL
    ALTER TABLE Members ADD FrozenFrom DATE NULL;

IF COL_LENGTH('Members', 'FrozenUntil') IS NULL
    ALTER TABLE Members ADD FrozenUntil DATE NULL;          

IF COL_LENGTH('Members', 'FreezeUsed') IS NULL
    ALTER TABLE Members ADD FreezeUsed BIT NOT NULL CONSTRAINT DF_Members_FreezeUsed DEFAULT(0);

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

        public static void AutoUnfreezeExpiredMembers()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(@"
UPDATE Members
SET IsFrozen = 0,
    FrozenFrom = NULL,
    FrozenUntil = NULL,
    Status = CASE
        WHEN CAST(ExpiryDate AS date) < CAST(GETDATE() AS date) THEN 'Expired'
        ELSE 'Active'
    END
WHERE IsArchived = 0
  AND IsFrozen = 1
  AND FrozenUntil IS NOT NULL
  AND CAST(FrozenUntil AS date) <= CAST(GETDATE() AS date);", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}