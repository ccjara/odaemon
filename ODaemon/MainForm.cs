using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.IO;
using System.Threading;

namespace ODaemon
{
    public partial class MainForm : Form
    {
        private bool rssActive = false;
        private System.Windows.Forms.Timer rssTimer = new System.Windows.Forms.Timer();
        private List<long> rssItemIds = new List<long>();
        private RSSDaemon rss = new RSSDaemon();

        private string username = "";
        private string password = "";
        private string feedUrl = "";

        public MainForm()
        {
            InitializeComponent();

            rssTimer.Interval = 10000;
            rssTimer.Tick += onRefreshRSS;
            comboBoxRSSInterval.SelectedIndex = 0;

            listBoxRSS.DrawItem += new DrawItemEventHandler(this.DrawItemHandler);
            listBoxRSS.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.MeasureItemHandler);

            rss.importFeedEntries();
            if (rss.getNotificationList().Count > 0)
            {
                updateNotificationList(rss.getNotificationList());
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            FlashWindow.Stop(this);
        }

        private void updateNotificationList(List<RSSNotification> list)
        {
            int minuteAmount = Int32.Parse(comboBoxRSSInterval.Items[comboBoxRSSInterval.SelectedIndex].ToString().Split(' ')[0]);
            DateTime dtNext = DateTime.Now;
            dtNext = dtNext.AddMinutes((double)minuteAmount);
            labelNextUpdateValue.Text = dtNext.ToString("HH:mm");

            uint newItems = 0;
            foreach (RSSNotification n in list)
            {
                if (!rssItemIds.Exists(x => x == n.id))
                {
                    RSSListBoxItem listBoxItem = new RSSListBoxItem(n.id, n.description);
                    listBoxItem.Seen = n.seen;
                    listBoxRSS.Items.Insert(0, listBoxItem);
                    rssItemIds.Add(n.id);
                    if (!n.seen)
                    {
                        newItems++;
                    }
                }
            }
            if (newItems > 0)
            {
                System.Media.SystemSounds.Exclamation.Play();
                FlashWindow.Start(this);
            }
        }

        public async void onRefreshRSS(object sender, EventArgs e)
        {
            try
            {
                List<RSSNotification> list = await rss.fetch(feedUrl, username, password);
                updateNotificationList(list);
            }
            catch(Exception ex)
            {
                buttonSwitchRSS_Click(null, null);
                labelNextUpdateValue.Text = "";
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSwitchRSS_Click(object sender, EventArgs e)
        {
            if (comboBoxRSSInterval.SelectedIndex == -1)
            {
                MessageBox.Show("Es muss ein Update-Intervall festgelegt werden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                rssActive = !rssActive;
                if (rssActive)
                {
                    IniFile ini = new IniFile(Application.StartupPath + "\\config.ini");
                    username = ini.IniReadValue("RSS", "Username");
                    password = ini.IniReadValue("RSS", "Password");
                    feedUrl = ini.IniReadValue("RSS", "FeedUrl");

                    if (username.Length == 0 || password.Length == 0 || feedUrl.Length == 0)
                    {
                        MessageBox.Show("Bitte erst in den Einstellungen die Accountdaten eingeben.", "ODaemon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int minuteAmount = Int32.Parse(comboBoxRSSInterval.Items[comboBoxRSSInterval.SelectedIndex].ToString().Split(' ')[0]);
                        buttonSwitchRSS.ForeColor = Color.DarkGreen;
                        buttonSwitchRSS.Text = "Aktiv";
                        rssTimer.Interval = 60 * 1000 * minuteAmount;
                        rssTimer.Start();
                        onRefreshRSS(null, null);
                        this.Text = "ODaemon - Aktiv";
                    }
                }
                else
                {
                    buttonSwitchRSS.ForeColor = Color.DarkRed;
                    buttonSwitchRSS.Text = "Inaktiv";
                    rssTimer.Stop();
                    this.Text = "ODaemon - Inaktiv";
                }
            }
        }

        private void DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (e.Index != -1 && listBoxRSS.Items.Count > 0)
            {
                FontStyle fStyle = ((RSSListBoxItem) listBoxRSS.Items[e.Index]).Seen ? FontStyle.Regular : FontStyle.Bold;
                e.Graphics.DrawString(listBoxRSS.Items[e.Index].ToString(), new Font(FontFamily.GenericSansSerif, 9, fStyle), new SolidBrush(Color.Black), e.Bounds);
            }
        }

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 22;
        }

        private void listBoxRSS_SelectedValueChanged(object sender, EventArgs e)
        {
            RSSListBoxItem item = (RSSListBoxItem)listBoxRSS.Items[listBoxRSS.SelectedIndex];

            if(!item.Seen) {
                item.Seen = true;
                listBoxRSS.Refresh();
                rss.markItemAsRead(item.Id);
            }
        }

        private void buttonRssSettings_Click(object sender, EventArgs e)
        {
            RSSDaemonSettings settings = new RSSDaemonSettings();
            settings.ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rss.exportFeedEntries();
        }

        private void listBoxRSS_MouseDown(object sender, MouseEventArgs e)
        {
            listBoxRSS.SelectedIndex = listBoxRSS.IndexFromPoint(e.X, e.Y);
        }

        private void markAsReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListBox.ObjectCollection items = listBoxRSS.Items;

            foreach(RSSListBoxItem item in items) {
                if(!item.Seen)
                {
                    item.Seen = true;
                    rss.markItemAsRead(item.Id);
                }
            }
            listBoxRSS.Refresh();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                FlashWindow.Stop(this);
            }
        }
    }
}
