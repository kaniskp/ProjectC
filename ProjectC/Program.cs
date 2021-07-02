using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectC
{
    static class Program
    {
        public static string username;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new loginForm());
        }
        public static string outputDB;
        public static int sum;
        public static string menu;
        public static string type;
        public static string price;
        public static string date;
        public static string name;


    }
}
