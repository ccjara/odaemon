namespace ODaemon
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonRssSettings = new System.Windows.Forms.Button();
            this.labelNextUpdateValue = new System.Windows.Forms.Label();
            this.buttonSwitchRSS = new System.Windows.Forms.Button();
            this.labelNextUpdate = new System.Windows.Forms.Label();
            this.labelRSSInterval = new System.Windows.Forms.Label();
            this.comboBoxRSSInterval = new System.Windows.Forms.ComboBox();
            this.listBoxRSS = new System.Windows.Forms.ListBox();
            this.contextMenuRSS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuRSS.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRssSettings
            // 
            this.buttonRssSettings.Location = new System.Drawing.Point(194, 211);
            this.buttonRssSettings.Name = "buttonRssSettings";
            this.buttonRssSettings.Size = new System.Drawing.Size(151, 27);
            this.buttonRssSettings.TabIndex = 16;
            this.buttonRssSettings.Text = "Einstellungen";
            this.buttonRssSettings.UseVisualStyleBackColor = true;
            this.buttonRssSettings.Click += new System.EventHandler(this.buttonRssSettings_Click);
            // 
            // labelNextUpdateValue
            // 
            this.labelNextUpdateValue.AutoSize = true;
            this.labelNextUpdateValue.Location = new System.Drawing.Point(111, 218);
            this.labelNextUpdateValue.Name = "labelNextUpdateValue";
            this.labelNextUpdateValue.Size = new System.Drawing.Size(64, 13);
            this.labelNextUpdateValue.TabIndex = 14;
            this.labelNextUpdateValue.Text = "                   ";
            // 
            // buttonSwitchRSS
            // 
            this.buttonSwitchRSS.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonSwitchRSS.Location = new System.Drawing.Point(194, 178);
            this.buttonSwitchRSS.Name = "buttonSwitchRSS";
            this.buttonSwitchRSS.Size = new System.Drawing.Size(151, 27);
            this.buttonSwitchRSS.TabIndex = 13;
            this.buttonSwitchRSS.Text = "Inaktiv";
            this.buttonSwitchRSS.UseVisualStyleBackColor = true;
            this.buttonSwitchRSS.Click += new System.EventHandler(this.buttonSwitchRSS_Click);
            // 
            // labelNextUpdate
            // 
            this.labelNextUpdate.AutoSize = true;
            this.labelNextUpdate.Location = new System.Drawing.Point(9, 218);
            this.labelNextUpdate.Name = "labelNextUpdate";
            this.labelNextUpdate.Size = new System.Drawing.Size(93, 13);
            this.labelNextUpdate.TabIndex = 12;
            this.labelNextUpdate.Text = "Nächstes Update:";
            // 
            // labelRSSInterval
            // 
            this.labelRSSInterval.AutoSize = true;
            this.labelRSSInterval.Location = new System.Drawing.Point(9, 185);
            this.labelRSSInterval.Name = "labelRSSInterval";
            this.labelRSSInterval.Size = new System.Drawing.Size(44, 13);
            this.labelRSSInterval.TabIndex = 11;
            this.labelRSSInterval.Text = "Intervall";
            // 
            // comboBoxRSSInterval
            // 
            this.comboBoxRSSInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRSSInterval.FormattingEnabled = true;
            this.comboBoxRSSInterval.Items.AddRange(new object[] {
            "1 Minute",
            "3 Minuten",
            "5 Minuten",
            "15 Minuten",
            "30 Minuten",
            "60 Minuten"});
            this.comboBoxRSSInterval.Location = new System.Drawing.Point(62, 182);
            this.comboBoxRSSInterval.Name = "comboBoxRSSInterval";
            this.comboBoxRSSInterval.Size = new System.Drawing.Size(126, 21);
            this.comboBoxRSSInterval.TabIndex = 10;
            // 
            // listBoxRSS
            // 
            this.listBoxRSS.ContextMenuStrip = this.contextMenuRSS;
            this.listBoxRSS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxRSS.FormattingEnabled = true;
            this.listBoxRSS.Location = new System.Drawing.Point(12, 12);
            this.listBoxRSS.Name = "listBoxRSS";
            this.listBoxRSS.Size = new System.Drawing.Size(333, 160);
            this.listBoxRSS.TabIndex = 9;
            this.listBoxRSS.SelectedValueChanged += new System.EventHandler(this.listBoxRSS_SelectedValueChanged);
            this.listBoxRSS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxRSS_MouseDown);
            // 
            // contextMenuRSS
            // 
            this.contextMenuRSS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markAsReadToolStripMenuItem});
            this.contextMenuRSS.Name = "contextMenuRSS";
            this.contextMenuRSS.Size = new System.Drawing.Size(211, 26);
            // 
            // markAsReadToolStripMenuItem
            // 
            this.markAsReadToolStripMenuItem.Name = "markAsReadToolStripMenuItem";
            this.markAsReadToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.markAsReadToolStripMenuItem.Text = "Alle als gelesen markieren";
            this.markAsReadToolStripMenuItem.Click += new System.EventHandler(this.markAsReadToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 247);
            this.Controls.Add(this.buttonRssSettings);
            this.Controls.Add(this.labelNextUpdateValue);
            this.Controls.Add(this.listBoxRSS);
            this.Controls.Add(this.buttonSwitchRSS);
            this.Controls.Add(this.comboBoxRSSInterval);
            this.Controls.Add(this.labelNextUpdate);
            this.Controls.Add(this.labelRSSInterval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ODaemon - Inaktiv";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuRSS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxRSS;
        private System.Windows.Forms.ComboBox comboBoxRSSInterval;
        private System.Windows.Forms.Label labelRSSInterval;
        private System.Windows.Forms.Label labelNextUpdate;
        private System.Windows.Forms.Button buttonSwitchRSS;
        private System.Windows.Forms.Label labelNextUpdateValue;
        private System.Windows.Forms.Button buttonRssSettings;
        private System.Windows.Forms.ContextMenuStrip contextMenuRSS;
        private System.Windows.Forms.ToolStripMenuItem markAsReadToolStripMenuItem;
    }
}

