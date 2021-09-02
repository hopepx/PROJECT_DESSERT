using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAPProjectDesserts
{
    static class Program
    {
        public static string ConnectionString { get; set; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hopep\source\repos\SAPProjectDesserts XUEWEN YES\SAPProjectDesserts\SAPProjectDesserts\Database.mdf;Integrated Security=True";
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        public static int ID { get; set; }
    }
}
