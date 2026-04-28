using System;
using System.Windows.Forms;

namespace GymManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DBConnection.EnsureFeatureSchema();
                DBConnection.AutoUnfreezeExpiredMembers();
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n\n" + ex.StackTrace,
                    "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}