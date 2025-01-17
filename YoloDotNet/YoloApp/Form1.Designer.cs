namespace YoloApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StartBtn = new Button();
            MsgBox = new RichTextBox();
            menuStrip1 = new MenuStrip();
            fILEToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            PathInBox = new TextBox();
            V5CB = new CheckBox();
            V8CB = new CheckBox();
            V9CB = new CheckBox();
            V10CB = new CheckBox();
            V11CB = new CheckBox();
            AlpsentekCB = new CheckBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            SubModeCB = new CheckBox();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // StartBtn
            // 
            StartBtn.Location = new Point(930, 110);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(175, 92);
            StartBtn.TabIndex = 0;
            StartBtn.Text = "Start";
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += Start_Click;
            // 
            // MsgBox
            // 
            MsgBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MsgBox.BorderStyle = BorderStyle.None;
            MsgBox.Location = new Point(0, 35);
            MsgBox.Name = "MsgBox";
            MsgBox.Size = new Size(1487, 685);
            MsgBox.TabIndex = 1;
            MsgBox.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fILEToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1487, 32);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            fILEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            fILEToolStripMenuItem.Size = new Size(60, 28);
            fILEToolStripMenuItem.Text = "FILE";
            fILEToolStripMenuItem.Click += fILEToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(158, 34);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // PathInBox
            // 
            PathInBox.Location = new Point(12, 18);
            PathInBox.Name = "PathInBox";
            PathInBox.Size = new Size(919, 30);
            PathInBox.TabIndex = 3;
            // 
            // V5CB
            // 
            V5CB.AutoSize = true;
            V5CB.Enabled = false;
            V5CB.Location = new Point(17, 29);
            V5CB.Name = "V5CB";
            V5CB.Size = new Size(59, 28);
            V5CB.TabIndex = 4;
            V5CB.Text = "V5";
            V5CB.UseVisualStyleBackColor = true;
            // 
            // V8CB
            // 
            V8CB.AutoSize = true;
            V8CB.Checked = true;
            V8CB.CheckState = CheckState.Checked;
            V8CB.Location = new Point(17, 63);
            V8CB.Name = "V8CB";
            V8CB.Size = new Size(59, 28);
            V8CB.TabIndex = 5;
            V8CB.Text = "V8";
            V8CB.UseVisualStyleBackColor = true;
            // 
            // V9CB
            // 
            V9CB.AutoSize = true;
            V9CB.Location = new Point(17, 97);
            V9CB.Name = "V9CB";
            V9CB.Size = new Size(59, 28);
            V9CB.TabIndex = 6;
            V9CB.Text = "V9";
            V9CB.UseVisualStyleBackColor = true;
            // 
            // V10CB
            // 
            V10CB.AutoSize = true;
            V10CB.Location = new Point(82, 29);
            V10CB.Name = "V10CB";
            V10CB.Size = new Size(70, 28);
            V10CB.TabIndex = 7;
            V10CB.Text = "V10";
            V10CB.UseVisualStyleBackColor = true;
            // 
            // V11CB
            // 
            V11CB.AutoSize = true;
            V11CB.Location = new Point(82, 63);
            V11CB.Name = "V11CB";
            V11CB.Size = new Size(70, 28);
            V11CB.TabIndex = 8;
            V11CB.Text = "V11";
            V11CB.UseVisualStyleBackColor = true;
            // 
            // AlpsentekCB
            // 
            AlpsentekCB.AutoSize = true;
            AlpsentekCB.Checked = true;
            AlpsentekCB.CheckState = CheckState.Checked;
            AlpsentekCB.Location = new Point(82, 97);
            AlpsentekCB.Name = "AlpsentekCB";
            AlpsentekCB.Size = new Size(131, 28);
            AlpsentekCB.TabIndex = 9;
            AlpsentekCB.Text = "Alpsenteck";
            AlpsentekCB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(V10CB);
            groupBox1.Controls.Add(AlpsentekCB);
            groupBox1.Controls.Add(V5CB);
            groupBox1.Controls.Add(V11CB);
            groupBox1.Controls.Add(V8CB);
            groupBox1.Controls.Add(V9CB);
            groupBox1.Location = new Point(12, 54);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(224, 139);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Model";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(PathInBox);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Controls.Add(StartBtn);
            groupBox2.Location = new Point(0, 726);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1487, 250);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(SubModeCB);
            groupBox3.Location = new Point(267, 66);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(300, 127);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Option";
            // 
            // SubModeCB
            // 
            SubModeCB.AutoSize = true;
            SubModeCB.Location = new Point(15, 29);
            SubModeCB.Name = "SubModeCB";
            SubModeCB.Size = new Size(125, 28);
            SubModeCB.TabIndex = 10;
            SubModeCB.Text = "Sub Mode";
            SubModeCB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1487, 954);
            Controls.Add(groupBox2);
            Controls.Add(MsgBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YoloApp";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartBtn;
        private RichTextBox MsgBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fILEToolStripMenuItem;
        private TextBox PathInBox;
        private ToolStripMenuItem openToolStripMenuItem;
        private CheckBox V5CB;
        private CheckBox V8CB;
        private CheckBox V9CB;
        private CheckBox V10CB;
        private CheckBox V11CB;
        private CheckBox AlpsentekCB;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckBox SubModeCB;
    }
}
