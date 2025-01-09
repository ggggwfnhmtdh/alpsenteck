namespace SOM_DEVELOP_TOOL
{
    partial class FormRTT
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRTT));
            this.MsgBox0 = new System.Windows.Forms.RichTextBox();
            this.LogStatusMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setT0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addrToFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPTIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkLogStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stabilityTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticalLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MsgBox1 = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LogStatusBox = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.LogStatusMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MsgBox0
            // 
            this.MsgBox0.BackColor = System.Drawing.SystemColors.Menu;
            this.MsgBox0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox0.ContextMenuStrip = this.LogStatusMenu;
            this.MsgBox0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MsgBox0.Location = new System.Drawing.Point(0, 0);
            this.MsgBox0.Name = "MsgBox0";
            this.MsgBox0.Size = new System.Drawing.Size(350, 525);
            this.MsgBox0.TabIndex = 0;
            this.MsgBox0.Text = "";
            // 
            // LogStatusMenu
            // 
            this.LogStatusMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.LogStatusMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.setT0ToolStripMenuItem,
            this.setTnToolStripMenuItem,
            this.addrToFunctionToolStripMenuItem});
            this.LogStatusMenu.Name = "LogStatusMenu";
            this.LogStatusMenu.Size = new System.Drawing.Size(198, 124);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click_1);
            // 
            // setT0ToolStripMenuItem
            // 
            this.setT0ToolStripMenuItem.Name = "setT0ToolStripMenuItem";
            this.setT0ToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.setT0ToolStripMenuItem.Text = "SetT0";
            this.setT0ToolStripMenuItem.Click += new System.EventHandler(this.setT0ToolStripMenuItem_Click);
            // 
            // setTnToolStripMenuItem
            // 
            this.setTnToolStripMenuItem.Name = "setTnToolStripMenuItem";
            this.setTnToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.setTnToolStripMenuItem.Text = "SetTn";
            this.setTnToolStripMenuItem.Click += new System.EventHandler(this.setTnToolStripMenuItem_Click);
            // 
            // addrToFunctionToolStripMenuItem
            // 
            this.addrToFunctionToolStripMenuItem.Name = "addrToFunctionToolStripMenuItem";
            this.addrToFunctionToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.addrToFunctionToolStripMenuItem.Text = "AddrToFunction";
            this.addrToFunctionToolStripMenuItem.Click += new System.EventHandler(this.addrToFunctionToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.oPTIONToolStripMenuItem,
            this.tOOLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.AutoSize = false;
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openLogToolStripMenuItem1,
            this.debugToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(43, 21);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openLogToolStripMenuItem1
            // 
            this.openLogToolStripMenuItem1.Name = "openLogToolStripMenuItem1";
            this.openLogToolStripMenuItem1.Size = new System.Drawing.Size(185, 26);
            this.openLogToolStripMenuItem1.Text = "OpenLogFile";
            this.openLogToolStripMenuItem1.Click += new System.EventHandler(this.openLogToolStripMenuItem1_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // oPTIONToolStripMenuItem
            // 
            this.oPTIONToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLogToolStripMenuItem,
            this.closeLogToolStripMenuItem,
            this.checkLogStatusToolStripMenuItem,
            this.stabilityTestToolStripMenuItem});
            this.oPTIONToolStripMenuItem.Name = "oPTIONToolStripMenuItem";
            this.oPTIONToolStripMenuItem.Size = new System.Drawing.Size(81, 36);
            this.oPTIONToolStripMenuItem.Text = "OPTION";
            // 
            // openLogToolStripMenuItem
            // 
            this.openLogToolStripMenuItem.Name = "openLogToolStripMenuItem";
            this.openLogToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.openLogToolStripMenuItem.Text = "OpenLog";
            this.openLogToolStripMenuItem.Click += new System.EventHandler(this.openLogToolStripMenuItem_Click);
            // 
            // closeLogToolStripMenuItem
            // 
            this.closeLogToolStripMenuItem.Name = "closeLogToolStripMenuItem";
            this.closeLogToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.closeLogToolStripMenuItem.Text = "CloseLog";
            this.closeLogToolStripMenuItem.Click += new System.EventHandler(this.closeLogToolStripMenuItem_Click);
            // 
            // checkLogStatusToolStripMenuItem
            // 
            this.checkLogStatusToolStripMenuItem.Name = "checkLogStatusToolStripMenuItem";
            this.checkLogStatusToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.checkLogStatusToolStripMenuItem.Text = "CheckLogStatus";
            this.checkLogStatusToolStripMenuItem.Click += new System.EventHandler(this.checkLogStatusToolStripMenuItem_Click);
            // 
            // stabilityTestToolStripMenuItem
            // 
            this.stabilityTestToolStripMenuItem.Name = "stabilityTestToolStripMenuItem";
            this.stabilityTestToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.stabilityTestToolStripMenuItem.Text = "StabilityTest";
            this.stabilityTestToolStripMenuItem.Click += new System.EventHandler(this.stabilityTestToolStripMenuItem_Click);
            // 
            // tOOLToolStripMenuItem
            // 
            this.tOOLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkLogToolStripMenuItem,
            this.parseARToolStripMenuItem,
            this.statisticalLogToolStripMenuItem});
            this.tOOLToolStripMenuItem.Name = "tOOLToolStripMenuItem";
            this.tOOLToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.tOOLToolStripMenuItem.Text = "TOOL";
            // 
            // checkLogToolStripMenuItem
            // 
            this.checkLogToolStripMenuItem.Name = "checkLogToolStripMenuItem";
            this.checkLogToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.checkLogToolStripMenuItem.Text = "CheckLogID";
            this.checkLogToolStripMenuItem.Click += new System.EventHandler(this.checkLogToolStripMenuItem_Click);
            // 
            // parseARToolStripMenuItem
            // 
            this.parseARToolStripMenuItem.Name = "parseARToolStripMenuItem";
            this.parseARToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.parseARToolStripMenuItem.Text = "ParseAR";
            this.parseARToolStripMenuItem.Click += new System.EventHandler(this.parseARToolStripMenuItem_Click);
            // 
            // statisticalLogToolStripMenuItem
            // 
            this.statisticalLogToolStripMenuItem.Name = "statisticalLogToolStripMenuItem";
            this.statisticalLogToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.statisticalLogToolStripMenuItem.Text = "StatisticalLog";
            this.statisticalLogToolStripMenuItem.Click += new System.EventHandler(this.statisticalLogToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MsgBox1
            // 
            this.MsgBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MsgBox1.ContextMenuStrip = this.LogStatusMenu;
            this.MsgBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MsgBox1.Location = new System.Drawing.Point(0, 0);
            this.MsgBox1.Name = "MsgBox1";
            this.MsgBox1.Size = new System.Drawing.Size(322, 525);
            this.MsgBox1.TabIndex = 4;
            this.MsgBox1.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 67);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MsgBox0);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MsgBox1);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(673, 525);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // LogStatusBox
            // 
            this.LogStatusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogStatusBox.AutoSize = true;
            this.LogStatusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LogStatusBox.Location = new System.Drawing.Point(680, 72);
            this.LogStatusBox.Name = "LogStatusBox";
            this.LogStatusBox.Size = new System.Drawing.Size(79, 18);
            this.LogStatusBox.TabIndex = 7;
            this.LogStatusBox.Text = "LogStatus:";
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TimeLabel.Location = new System.Drawing.Point(679, 338);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(45, 18);
            this.TimeLabel.TabIndex = 8;
            this.TimeLabel.Text = "Time:";
            // 
            // FormRTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 594);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.LogStatusBox);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormRTT";
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRTT_FormClosing);
            this.Load += new System.EventHandler(this.FormRTT_Load);
            this.LogStatusMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox MsgBox0;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPTIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkLogStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogToolStripMenuItem1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip LogStatusMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.RichTextBox MsgBox1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem setT0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setTnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOOLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addrToFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parseARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stabilityTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticalLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.Label LogStatusBox;
        private System.Windows.Forms.Label TimeLabel;
    }
}