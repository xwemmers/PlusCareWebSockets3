using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlusCareWebSockets3
{
    public partial class LoggingForm : Form
    {
        public LoggingForm()
        {
            InitializeComponent();
        }

        public void ShowLog(string str)
        {
            txtLog.Text = str;
        }
    }
}
