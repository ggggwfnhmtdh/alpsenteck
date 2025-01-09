using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
namespace SOM_DEVELOP_TOOL
{
    public partial class LogForm : MaterialForm
    {
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        private delegate void ThreadWorkStrColor(string Msg, Color color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public bool IsOpen = false;
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            IsOpen = true;
            int x = SystemInformation.PrimaryMonitorMaximizedWindowSize.Width - this.Width-10;
            int y = 0;
            this.Location = new Point(x, y);
            //this.TopMost = true;
        }

        public void AppendMsg(string Msg)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(AppendMsg);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {
                if (MainForm.log_to_file_flag == false)
                {
                    if (Msg == "")
                        this.MsgBox.Clear();
                    else
                        this.MsgBox.AppendText(Msg);

                    if (MsgBox.Text.Length>=90000)
                    {
                        MsgBox.Clear();
                    }

                    this.MsgBox.Refresh();
                    this.MsgBox.SelectionStart = MsgBox.Text.Length;
                    this.MsgBox.ScrollToCaret();
                }
                else
                {
                    WilfFile.WriteAppend(MainForm.log_file, Msg);
                }
            }
        }

        public void AppendMsgColor(string Msg, Color color)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStrColor fc = new ThreadWorkStrColor(AppendMsgColor);
                this.Invoke(fc, new object[2] { Msg, color });
            }
            else
            {
                if (Msg == "")
                    this.MsgBox.Clear();
                else
                {
                    this.MsgBox.Select(this.MsgBox.Text.Length, 0);
                    //this.MsgBox.Focus();
                    MsgBox.SelectionColor = color;
                    MsgBox.AppendText(Msg);
                }

                this.MsgBox.Refresh();
                this.MsgBox.SelectionStart = MsgBox.Text.Length;
                this.MsgBox.ScrollToCaret();
            }
        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsOpen = false;
            this.Dispose();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppendMsg("");
        }
    }
}
