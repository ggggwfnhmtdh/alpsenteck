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
using System.IO;
namespace SOM_DEVELOP_TOOL
{
    public partial class IdForm : MaterialForm
    {
        public static string IdRangeStr = "";
        public static string oldIdRangeStr = "";
        public IdForm()
        {
            InitializeComponent();
        }

        public CheckBox[] IdCB;
        public List<int> Id;
        public List<string> IdName;
        private void IdForm_Load(object sender, EventArgs e)
        {
            bool ret = LoadIdTable(out Id,out IdName);
            if (ret)
                AddIdCbBox(Id.ToArray(), IdName.ToArray());
            else
            {
                IdRangeStr = oldIdRangeStr;
                this.Close();
            }
        }

        public void AddIdCbBox(int[] Id,string[] Name)
        {
            int offset = 0;
            int H = 0;
            bool IdEnable = false;
            IdCB = new CheckBox[Id.Length];
            int Col,Row;
            UInt32[] OldIdRange;
            if (oldIdRangeStr == "")
            {
                Int32[] RangeData = WilfDataPro.ParseRange2("0:255");
                OldIdRange = WilfDataPro.IndexToBit(RangeData, 8);
            }
            else
            {
                Int32[] RangeData = WilfDataPro.ParseRange2(oldIdRangeStr);
                OldIdRange = WilfDataPro.IndexToBit(RangeData, 8);
            }

            for (int i = 0; i < IdCB.Length; i++)
            {
                Row = i % 40;
                Col = i / 40;
                IdEnable = (OldIdRange[Id[i] / 32] & (1 << (Id[i] % 32)))>0;
                IdCB[i] = new CheckBox();
                IdCB[i].Name = "idCB" + i;
                IdCB[i].Text = "ID"+Id[i] +","+ Name[i];
                IdCB[i].Checked = IdEnable;
                IdCB[i].AutoSize = true;
                offset = Col * 450;
                H = Row * 20;
                IdCB[i].Location = new Point(12 + offset, 75 + H);
                IdCB[i].CheckedChanged += new System.EventHandler(this.CheckBoxChange);
                if (IdEnable == true)
                {
                    IdCB[i].BackColor = Color.Red;
                }

            }
            this.Controls.AddRange(IdCB);
            //groupBox1.Controls.AddRange(IdCB);

        }

        public void CheckBoxChange(object sender, EventArgs e)
        {
            CheckBox CurCB = (CheckBox)sender;
            if (CurCB.Checked)
                CurCB.BackColor = Color.Green;
            else
                CurCB.BackColor = this.BackColor;
            
        }

        public bool LoadIdTable(out List<int> Id, out List<string> Name)
        {
            string IdTableFile = "ID_Table_CPU0.data";
            Id = new List<int>();
            Name = new List<string>();
            //bool use_default_id_table = WilfFile.OpenYesNo("Whether To Use Default ID_TABLE File? ");
            //if (use_default_id_table == false)
            //{
            //    IdTableFile = WilfFile.OpenFile(".data");
            //}

            if (File.Exists(IdTableFile))
            {
                string[] lines = File.ReadAllLines(IdTableFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] str = lines[i].Split(',');
                    string Hd = str[0].Substring(0, 2);
                    if(Hd == "ID")
                    {
                        Id.Add(Convert.ToInt32(str[0].Replace("ID","")));
                        Name.Add(str[1]);
                    }
                }
                SortId(Id, Name);
                return true;
            }
            else
            {
                //MessageBox.Show("Warning: There is no ID_TABLE File" + Environment.NewLine);
                return false;
            }
        }

        public void SortId(List<int> Id,  List<string> Name)
        {
            int Temp;
            string TempStr;
            int length = Id.Count; // 数组未排序序列的长度
            do
            {
                for (int j = 0; j < length - 1; j++) // 数组未排序序列的倒第二数
                {
                    if (Id[j] > Id[j + 1])
                    {
                        Temp = Id[j];
                        Id[j] = Id[j+1];
                        Id[j+1] = Temp;

                        TempStr = Name[j];
                        Name[j] = Name[j + 1];
                        Name[j + 1] = TempStr;
                    }
                }
                length--; // 每次遍历后，就会确定一个最大值，未排序序列的长度减1
            }
            while (length > 1); // 当数组未排序序列只剩最后一个数时，就不需要排序了
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdCB != null && Id != null)
            {
                IdRangeStr = "";
                for (int i = 0; i < IdCB.Length; i++)
                {
                    if (IdCB[i].Checked == true)
                    {
                        IdRangeStr += Id[i] + ",";
                    }
                }
                IdRangeStr = IdRangeStr.Trim(',');
            }
            this.Close();
        }

        private void enableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IdCB.Length; i++)
            {
                IdCB[i].Checked = true;
            }
        }

        private void disableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IdCB.Length; i++)
            {
                IdCB[i].Checked = false;
            }
        }
    }
}
