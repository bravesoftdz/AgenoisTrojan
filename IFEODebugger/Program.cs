using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IFEODebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageBox.Show("Your System Was Corrupted By Agenois", ":)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string s = ""; //Temporary way to keep process alive, High CPU Usage
            while (s != "kill")
                s = Console.ReadLine();
        }
    }
}
