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
            dToolStripMenuItem = new ToolStripMenuItem();
            openYoloTestToolStripMenuItem = new ToolStripMenuItem();
            optionToolStripMenuItem = new ToolStripMenuItem();
            drawPointUAV123ToolStripMenuItem = new ToolStripMenuItem();
            uAVToolStripMenuItem = new ToolStripMenuItem();
            MsgBox = new RichTextBox();
            InPath = new TextBox();
            StartBtn = new Button();
            AlpentekCB = new CheckBox();
            groupBox1 = new GroupBox();
            label4 = new Label();
            CfgBox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            MaxNumBox = new TextBox();
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { fILEToolStripMenuItem, optionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(661, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            fILEToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, dToolStripMenuItem, openYoloTestToolStripMenuItem });
            fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            fILEToolStripMenuItem.Size = new Size(43, 22);
            fILEToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(158, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // dToolStripMenuItem
            // 
            dToolStripMenuItem.Name = "dToolStripMenuItem";
            dToolStripMenuItem.Size = new Size(158, 22);
            dToolStripMenuItem.Text = "Debug";
            dToolStripMenuItem.Click += dToolStripMenuItem_Click;
            // 
            // openYoloTestToolStripMenuItem
            // 
            openYoloTestToolStripMenuItem.Name = "openYoloTestToolStripMenuItem";
            openYoloTestToolStripMenuItem.Size = new Size(158, 22);
            openYoloTestToolStripMenuItem.Text = "OpenYoloTest";
            openYoloTestToolStripMenuItem.Click += openYoloTestToolStripMenuItem_Click;
            // 
            // optionToolStripMenuItem
            // 
            optionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { drawPointUAV123ToolStripMenuItem, uAVToolStripMenuItem });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            optionToolStripMenuItem.Size = new Size(60, 22);
            optionToolStripMenuItem.Text = "Option";
            // 
            // drawPointUAV123ToolStripMenuItem
            // 
            drawPointUAV123ToolStripMenuItem.Name = "drawPointUAV123ToolStripMenuItem";
            drawPointUAV123ToolStripMenuItem.Size = new Size(189, 22);
            drawPointUAV123ToolStripMenuItem.Text = "DrawPoint(UAV123)";
            drawPointUAV123ToolStripMenuItem.Click += drawPointUAV123ToolStripMenuItem_Click;
            // 
            // uAVToolStripMenuItem
            // 
            uAVToolStripMenuItem.Name = "uAVToolStripMenuItem";
            uAVToolStripMenuItem.Size = new Size(189, 22);
            uAVToolStripMenuItem.Text = "UAV_ResultPro";
            uAVToolStripMenuItem.Click += uAVToolStripMenuItem_Click;
            // 
            // MsgBox
            // 
            MsgBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MsgBox.BorderStyle = BorderStyle.None;
            MsgBox.Location = new Point(8, 25);
            MsgBox.Margin = new Padding(2, 2, 2, 2);
            MsgBox.Name = "MsgBox";
            MsgBox.Size = new Size(646, 426);
            MsgBox.TabIndex = 1;
            MsgBox.Text = "";
            // 
            // InPath
            // 
            InPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InPath.Location = new Point(4, 14);
            InPath.Margin = new Padding(2, 2, 2, 2);
            InPath.Name = "InPath";
            InPath.Size = new Size(515, 23);
            InPath.TabIndex = 2;
            // 
            // StartBtn
            // 
            StartBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StartBtn.Location = new Point(520, 63);
            StartBtn.Margin = new Padding(2, 2, 2, 2);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(122, 65);
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
            AlpentekCB.Location = new Point(4, 73);
            AlpentekCB.Margin = new Padding(2, 2, 2, 2);
            AlpentekCB.Name = "AlpentekCB";
            AlpentekCB.Size = new Size(116, 21);
            AlpentekCB.TabIndex = 4;
            AlpentekCB.Text = "AlpentekModel";
            AlpentekCB.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(CfgBox);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(MaxNumBox);
            groupBox1.Controls.Add(YoloCB);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(TestRatioBox);
            groupBox1.Controls.Add(SubModeCB);
            groupBox1.Controls.Add(InPath);
            groupBox1.Controls.Add(StartBtn);
            groupBox1.Controls.Add(AlpentekCB);
            groupBox1.Location = new Point(8, 455);
            groupBox1.Margin = new Padding(2, 2, 2, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(2, 2, 2, 2);
            groupBox1.Size = new Size(646, 128);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(530, 41);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(75, 17);
            label4.TabIndex = 13;
            label4.Text = "Config Path";
            // 
            // CfgBox
            // 
            CfgBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CfgBox.Location = new Point(4, 40);
            CfgBox.Margin = new Padding(2, 2, 2, 2);
            CfgBox.Name = "CfgBox";
            CfgBox.Size = new Size(515, 23);
            CfgBox.TabIndex = 12;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(530, 16);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 11;
            label3.Text = "File Path";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(325, 111);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(62, 17);
            label2.TabIndex = 10;
            label2.Text = "Max num";
            // 
            // MaxNumBox
            // 
            MaxNumBox.Location = new Point(226, 107);
            MaxNumBox.Margin = new Padding(2, 2, 2, 2);
            MaxNumBox.Name = "MaxNumBox";
            MaxNumBox.Size = new Size(97, 23);
            MaxNumBox.TabIndex = 9;
            MaxNumBox.Text = "-1";
            // 
            // YoloCB
            // 
            YoloCB.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            YoloCB.AutoSize = true;
            YoloCB.Location = new Point(4, 102);
            YoloCB.Margin = new Padding(2, 2, 2, 2);
            YoloCB.Name = "YoloCB";
            YoloCB.Size = new Size(91, 21);
            YoloCB.TabIndex = 8;
            YoloCB.Text = "YoloModel";
            YoloCB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(325, 77);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(19, 17);
            label1.TabIndex = 7;
            label1.Text = "%";
            // 
            // TestRatioBox
            // 
            TestRatioBox.Location = new Point(226, 75);
            TestRatioBox.Margin = new Padding(2, 2, 2, 2);
            TestRatioBox.Name = "TestRatioBox";
            TestRatioBox.Size = new Size(97, 23);
            TestRatioBox.TabIndex = 6;
            TestRatioBox.Text = "100";
            // 
            // SubModeCB
            // 
            SubModeCB.AutoSize = true;
            SubModeCB.Location = new Point(132, 75);
            SubModeCB.Margin = new Padding(2, 2, 2, 2);
            SubModeCB.Name = "SubModeCB";
            SubModeCB.Size = new Size(84, 21);
            SubModeCB.TabIndex = 5;
            SubModeCB.Text = "SubMode";
            SubModeCB.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 589);
            Controls.Add(groupBox1);
            Controls.Add(MsgBox);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 2, 2, 2);
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
        private Label label2;
        private TextBox MaxNumBox;
        private ToolStripMenuItem dToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private ToolStripMenuItem drawPointUAV123ToolStripMenuItem;
        private Label label3;
        private Label label4;
        private TextBox CfgBox;
        private ToolStripMenuItem openYoloTestToolStripMenuItem;
        private ToolStripMenuItem uAVToolStripMenuItem;
    }
}
