using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections;
using System.IO;
using static MaterialSkin.MaterialSkinManager;

namespace SOM_DEVELOP_TOOL
{
    public partial class LoadForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        int Time = 60;
        public static SoftWareInf m_inf = new SoftWareInf();
        public static int ThemeIndex = 0;
        public string Version;
        public static bool TimeOut = false;
        public static UInt32 TIME_LIMIT = 20251201;
        public LoadForm()
        {
            InitializeComponent();
            Version = GetCompileVersion();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            SetTheme(2);
            if (File.Exists("debug.a"))
            {
                Time = 3;
            }
        }

        public bool CheckOutTime()
        {
            UInt32 TimeNow = Convert.ToUInt32(DateTime.Now.ToString("yyyyMMdd"));
            SoftWareInf sInf;
            UInt32 refLimitDateTime;
            if (!File.Exists(Json.m_SoftInfFile))
            {
                UpDateFile();
            }
            sInf = Json.LoadSoftInf(Json.m_SoftInfFile);
            if (sInf == null)
                return true;
            if (Version != sInf.Version)
            {
                UpDateFile();
                sInf = Json.LoadSoftInf(Json.m_SoftInfFile);
            }
            refLimitDateTime = Convert.ToUInt32(AES.Decrypt(sInf.MAC));
            if (sInf.LimitDateTime == refLimitDateTime)
            {
                TimeOut = TimeNow > sInf.LimitDateTime;
                return TimeOut;
            }
            else
            {
                MessageBox.Show("Please don't modify the permission!!!");
                return true;
            }
        }
        private void SetTheme(int colorSchemeIndex)
        {
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.BlueGrey800,
                        Primary.BlueGrey900,
                        Primary.BlueGrey500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Green600,
                        Primary.Green700,
                        Primary.Green200,
                        Accent.Red100,
                        TextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal500 : Primary.Indigo500,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal700 : Primary.Indigo700,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal200 : Primary.Indigo100,
                        Accent.Pink200,
                        TextShade.WHITE);
                    break;
                case 3:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Pink500,
                        Primary.Pink600,
                        Primary.Pink100,
                        Accent.Pink100,
                        TextShade.WHITE);
                    break;
            }
            Invalidate();
        }

        private void UpDateFile()
        {
            string Dir = Path.GetDirectoryName(Json.m_SoftInfFile);
            try
            {
                if (!Directory.Exists(Dir))
                    Directory.CreateDirectory(Dir);
            }
            catch
            {
                MessageBox.Show("Please Run As Administrator");
                return;
            }
            m_inf.UserName = easy_log.HostUserName.ToUpper();
            m_inf.LimitDateTime = TIME_LIMIT;
            m_inf.Version = Version;
            m_inf.MAC = AES.Encrypt(m_inf.LimitDateTime.ToString());
            Json.SaveCfg(m_inf);
        }
        private void LoadForm_Load(object sender, EventArgs e)
        {
            StartBtn.Visible = false;
            if (TimeOut == true)
            {
                SetTheme(1);
                this.Text = "License is out of date, please contact with wilf.wang@innostar-semmi.com";
                materialRadioButton1.Visible = false;
                materialRadioButton2.Visible = false;
                materialRadioButton4.Visible = false;
                StartBtn.Enabled = false;
            }

        }

        public string GetCompileVersion()
        {
            string OriginVersion = "" + System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);

            return OriginVersion;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            materialLabel1.Text = Time.ToString();
            if (Time > 0)
            {
                Time--;
            }
            else
            {
                materialLabel1.Visible = false;
                StartBtn.Text = "  GO TO START!!!";
                timer1.Enabled = false;
            }
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            StartBtn.Visible = true;
            ThemeIndex = 3;
            SetTheme(ThemeIndex);
        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StartBtn.Visible = true;
            ThemeIndex = 2;
            SetTheme(ThemeIndex);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            //if (EnableFlag == false)
            //{
            //    if(File.Exists(easy_log.HelpFIle))
            //    {
            //        System.Diagnostics.Process.Start(easy_log.HelpFIle);
            //        materialRaisedButton1.Text = "  GO TO START!!!";
            //        EnableFlag = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Can't Find Help File");
            //    }

            //}
            //else
            //{
            //    UpDateFile();
            //    Close();
            //}
            UpDateFile();
            Close();
        }

        private void materialRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            StartBtn.Visible = true;
            ThemeIndex = 1;
            SetTheme(ThemeIndex);
        }

        private void materialRadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
