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
    }
}