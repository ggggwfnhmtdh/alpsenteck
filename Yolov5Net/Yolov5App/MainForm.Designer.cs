namespace Yolov5App
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
            menuStrip1 = new MenuStrip();
            fILEToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            MsgBox = new RichTextBox();
            InPath = new TextBox();
            StartBtn = new Button();
            AlpentekCB = new CheckBox();
            groupBox1 = new GroupBox();
            YoloCB = new CheckBox();
            label1 = new Label();
            TestRatioBox = new TextBox();
            SubModeCB = new CheckBox();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fILEToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1039, 32);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            fILEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            fILEToolStripMenuItem.Size = new Size(60, 28);
            fILEToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(158, 34);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // MsgBox
            // 
            MsgBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MsgBox.BorderStyle = BorderStyle.None;
            MsgBox.Location = new Point(12, 35);
            MsgBox.Name = "MsgBox";
            MsgBox.Size = new Size(1015, 521);
            MsgBox.TabIndex = 1;
            MsgBox.Text = "";
            // 
            // InPath
            // 
            InPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InPath.Location = new Point(6, 29);
            InPath.Name = "InPath";
            InPath.Size = new Size(1003, 30);
            InPath.TabIndex = 2;
            // 
            // StartBtn
            // 
            StartBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StartBtn.Location = new Point(817, 68);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(192, 92);
            StartBtn.TabIndex = 3;
            StartBtn.Text = "Start";
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += StartBtn_Click;
            // 
            // AlpentekCB
            // 
            AlpentekCB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AlpentekCB.AutoSize = true;
            AlpentekCB.Checked = true;
            AlpentekCB.CheckState = CheckState.Checked;
            AlpentekCB.Location = new Point(6, 88);
            AlpentekCB.Name = "AlpentekCB";
            AlpentekCB.Size = new Size(170, 28);
            AlpentekCB.TabIndex = 4;
            AlpentekCB.Text = "AlpentekModel";
            AlpentekCB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(YoloCB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TestRatioBox);
            groupBox1.Controls.Add(SubModeCB);
            groupBox1.Controls.Add(InPath);
            groupBox1.Controls.Add(StartBtn);
            groupBox1.Controls.Add(AlpentekCB);
            groupBox1.Location = new Point(12, 562);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1015, 163);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // YoloCB
            // 
            YoloCB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            YoloCB.AutoSize = true;
            YoloCB.Location = new Point(6, 129);
            YoloCB.Name = "YoloCB";
            YoloCB.Size = new Size(130, 28);
            YoloCB.TabIndex = 8;
            YoloCB.Text = "YoloModel";
            YoloCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(511, 91);
            label1.Name = "label1";
            label1.Size = new Size(26, 24);
            label1.TabIndex = 7;
            label1.Text = "%";
            // 
            // TestRatioBox
            // 
            TestRatioBox.Location = new Point(355, 88);
            TestRatioBox.Name = "TestRatioBox";
            TestRatioBox.Size = new Size(150, 30);
            TestRatioBox.TabIndex = 6;
            TestRatioBox.Text = "100";
            // 
            // SubModeCB
            // 
            SubModeCB.AutoSize = true;
            SubModeCB.Location = new Point(207, 88);
            SubModeCB.Name = "SubModeCB";
            SubModeCB.Size = new Size(120, 28);
            SubModeCB.TabIndex = 5;
            SubModeCB.Text = "SubMode";
            SubModeCB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 734);
            Controls.Add(groupBox1);
            Controls.Add(MsgBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yolov5";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fILEToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private RichTextBox MsgBox;
        private TextBox InPath;
        private Button StartBtn;
        private CheckBox AlpentekCB;
        private GroupBox groupBox1;
        private CheckBox SubModeCB;
        private Label label1;
        private TextBox TestRatioBox;
        private CheckBox YoloCB;
    }
}
