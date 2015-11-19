using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;

namespace ODaemon
{
    public partial class RSSDaemonSettings : Form
    {
        private IniFile ini;
        private Dictionary<CheckBox, String> checkBoxMapper = new Dictionary<CheckBox, string>();

        public RSSDaemonSettings()
        {
            InitializeComponent();

            ini = new IniFile(Application.StartupPath + "\\config.ini");

            textBoxFeedUrl.Text = ini.IniReadValue("RSS", "FeedUrl");
            textBoxUsername.Text = ini.IniReadValue("RSS", "Username");
            textBoxPassword.Text = ini.IniReadValue("RSS", "Password");
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("RSS", "FeedUrl", textBoxFeedUrl.Text);
            ini.IniWriteValue("RSS", "Username", textBoxUsername.Text);
            ini.IniWriteValue("RSS", "Password", textBoxPassword.Text);

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
