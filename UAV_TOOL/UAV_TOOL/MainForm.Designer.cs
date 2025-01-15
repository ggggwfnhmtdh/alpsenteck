namespace UAV_TOOL
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.StartBtn = new System.Windows.Forms.Button();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.FpsBox = new System.Windows.Forms.TextBox();
            this.ImSizeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FormatVideoBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algnHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.algnVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waterMarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.FormatImageBox = new System.Windows.Forms.TextBox();
            this.MsgBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ScaleBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FpsExternalCB = new System.Windows.Forms.CheckBox();
            this.SubDirCB = new System.Windows.Forms.CheckBox();
            this.concatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartBtn.Location = new System.Drawing.Point(625, 465);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(163, 112);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // PathBox
            // 
            this.PathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathBox.BackColor = System.Drawing.Color.GreenYellow;
            this.PathBox.Location = new System.Drawing.Point(12, 323);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(776, 28);
            this.PathBox.TabIndex = 1;
            this.PathBox.Text = "D:\\Test\\bike1";
            // 
            // FpsBox
            // 
            this.FpsBox.Location = new System.Drawing.Point(7, 34);
            this.FpsBox.Name = "FpsBox";
            this.FpsBox.Size = new System.Drawing.Size(100, 28);
            this.FpsBox.TabIndex = 2;
            this.FpsBox.Text = "30";
            // 
            // ImSizeBox
            // 
            this.ImSizeBox.Location = new System.Drawing.Point(7, 70);
            this.ImSizeBox.Name = "ImSizeBox";
            this.ImSizeBox.Size = new System.Drawing.Size(100, 28);
            this.ImSizeBox.TabIndex = 3;
            this.ImSizeBox.Text = "1280x720";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "FPS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Size";
            // 
            // FormatVideoBox
            // 
            this.FormatVideoBox.Location = new System.Drawing.Point(7, 142);
            this.FormatVideoBox.Name = "FormatVideoBox";
            this.FormatVideoBox.Size = new System.Drawing.Size(100, 28);
            this.FormatVideoBox.TabIndex = 6;
            this.FormatVideoBox.Text = ".mp4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Format-video";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.tOOLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 32);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openInputToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(60, 28);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(209, 34);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openInputToolStripMenuItem
            // 
            this.openInputToolStripMenuItem.Name = "openInputToolStripMenuItem";
            this.openInputToolStripMenuItem.Size = new System.Drawing.Size(209, 34);
            this.openInputToolStripMenuItem.Text = "Open Input";
            this.openInputToolStripMenuItem.Click += new System.EventHandler(this.openInputToolStripMenuItem_Click);
            // 
            // tOOLToolStripMenuItem
            // 
            this.tOOLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeVideoToolStripMenuItem,
            this.waterMarkToolStripMenuItem});
            this.tOOLToolStripMenuItem.Name = "tOOLToolStripMenuItem";
            this.tOOLToolStripMenuItem.Size = new System.Drawing.Size(75, 28);
            this.tOOLToolStripMenuItem.Text = "TOOL";
            // 
            // mergeVideoToolStripMenuItem
            // 
            this.mergeVideoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algnHToolStripMenuItem,
            this.algnVToolStripMenuItem,
            this.autoToolStripMenuItem,
            this.concatToolStripMenuItem});
            this.mergeVideoToolStripMenuItem.Name = "mergeVideoToolStripMenuItem";
            this.mergeVideoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.mergeVideoToolStripMenuItem.Text = "MergeVideo";
            this.mergeVideoToolStripMenuItem.Click += new System.EventHandler(this.mergeVideoToolStripMenuItem_Click);
            // 
            // algnHToolStripMenuItem
            // 
            this.algnHToolStripMenuItem.Name = "algnHToolStripMenuItem";
            this.algnHToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.algnHToolStripMenuItem.Text = "Align-X";
            this.algnHToolStripMenuItem.Click += new System.EventHandler(this.algnHToolStripMenuItem_Click);
            // 
            // algnVToolStripMenuItem
            // 
            this.algnVToolStripMenuItem.Name = "algnVToolStripMenuItem";
            this.algnVToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.algnVToolStripMenuItem.Text = "Align-Y";
            this.algnVToolStripMenuItem.Click += new System.EventHandler(this.algnVToolStripMenuItem_Click);
            // 
            // autoToolStripMenuItem
            // 
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.autoToolStripMenuItem.Text = "Auto";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.autoToolStripMenuItem_Click);
            // 
            // waterMarkToolStripMenuItem
            // 
            this.waterMarkToolStripMenuItem.Name = "waterMarkToolStripMenuItem";
            this.waterMarkToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.waterMarkToolStripMenuItem.Text = "WaterMark";
            this.waterMarkToolStripMenuItem.Click += new System.EventHandler(this.waterMarkToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Format-image";
            // 
            // FormatImageBox
            // 
            this.FormatImageBox.Location = new System.Drawing.Point(7, 104);
            this.FormatImageBox.Name = "FormatImageBox";
            this.FormatImageBox.Size = new System.Drawing.Size(100, 28);
            this.FormatImageBox.TabIndex = 9;
            this.FormatImageBox.Text = ".jpg";
            // 
            // MsgBox
            // 
            this.MsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MsgBox.Location = new System.Drawing.Point(12, 49);
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(776, 268);
            this.MsgBox.TabIndex = 11;
            this.MsgBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ScaleBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.FpsExternalCB);
            this.groupBox1.Controls.Add(this.SubDirCB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FpsBox);
            this.groupBox1.Controls.Add(this.ImSizeBox);
            this.groupBox1.Controls.Add(this.FormatImageBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.FormatVideoBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 357);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 220);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // ScaleBox
            // 
            this.ScaleBox.Location = new System.Drawing.Point(286, 36);
            this.ScaleBox.Name = "ScaleBox";
            this.ScaleBox.Size = new System.Drawing.Size(100, 28);
            this.ScaleBox.TabIndex = 13;
            this.ScaleBox.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Scale";
            // 
            // FpsExternalCB
            // 
            this.FpsExternalCB.AutoSize = true;
            this.FpsExternalCB.Location = new System.Drawing.Point(174, 40);
            this.FpsExternalCB.Name = "FpsExternalCB";
            this.FpsExternalCB.Size = new System.Drawing.Size(106, 22);
            this.FpsExternalCB.TabIndex = 12;
            this.FpsExternalCB.Text = "External";
            this.FpsExternalCB.UseVisualStyleBackColor = true;
            // 
            // SubDirCB
            // 
            this.SubDirCB.AutoSize = true;
            this.SubDirCB.Location = new System.Drawing.Point(8, 179);
            this.SubDirCB.Name = "SubDirCB";
            this.SubDirCB.Size = new System.Drawing.Size(142, 22);
            this.SubDirCB.TabIndex = 11;
            this.SubDirCB.Text = "SubDirectory";
            this.SubDirCB.UseVisualStyleBackColor = true;
            // 
            // concatToolStripMenuItem
            // 
            this.concatToolStripMenuItem.Name = "concatToolStripMenuItem";
            this.concatToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.concatToolStripMenuItem.Text = "Concat";
            this.concatToolStripMenuItem.Click += new System.EventHandler(this.concatToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 591);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIDEO_TOOL";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.TextBox FpsBox;
        private System.Windows.Forms.TextBox ImSizeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FormatVideoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FormatImageBox;
        private System.Windows.Forms.RichTextBox MsgBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem openInputToolStripMenuItem;
        private System.Windows.Forms.CheckBox SubDirCB;
        private System.Windows.Forms.CheckBox FpsExternalCB;
        private System.Windows.Forms.TextBox ScaleBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem tOOLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algnVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem algnHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waterMarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem concatToolStripMenuItem;
    }
}

