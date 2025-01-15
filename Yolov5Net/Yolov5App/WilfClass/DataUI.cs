using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Yolov5App
{
    public class DataUI
    {
        public MainForm gForm;
        public string[] gSaveItemName;
        public string[] gSaveItemContent;
        public string FileName = Application.StartupPath + "\\UI.cmd";
        public string FileNameWithHistory = Application.StartupPath + "\\History.data";
        public DataUI(MainForm InForm)
        {
            gForm = InForm;
        }

        public void SaveHistory(List<string> InData)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < InData.Count;i++)
            {
                sb.Append(InData[i]+Environment.NewLine);
            }
            File.WriteAllText(FileNameWithHistory, sb.ToString());
        }

        public List<string> LoadHistory()
        {
            List<string> sb = new List<string> ();
            if(!File.Exists(FileNameWithHistory))
            {
                return sb;
            }
            string[] LineStr = File.ReadAllLines(FileNameWithHistory);
            for (int i = 0; i < LineStr.Length; i++)
            {
                LineStr[i].Trim();
                if(LineStr[i] != "")
                    sb.Add(LineStr[i] + Environment.NewLine);
            }
            return sb;
        }

        public List<Control> GetAllControls()
        {
            List<Control> MyControl = new List<Control>();
            foreach (Control item in gForm.Controls)
            {
                if (item is GroupBox)
                {
                    foreach (Control MyItem in item.Controls)
                    {
                        if (MyItem is TextBox || MyItem is ComboBox || MyItem is CheckBox)
                        {
                            MyControl.Add(MyItem);
                        }
                    }
                }
                else if (item is TabControl)
                {
                    foreach (TabPage page in ((TabControl)item).TabPages)
                    {
                        foreach (Control MyItem in page.Controls)
                        {
                            if (MyItem is TextBox || MyItem is ComboBox || MyItem is CheckBox)
                            {
                                MyControl.Add(MyItem);
                            }
                            else if (MyItem is GroupBox)
                            {
                                foreach (Control MyItemX in MyItem.Controls)
                                {
                                    if (MyItemX is TextBox || MyItemX is ComboBox || MyItemX is CheckBox)
                                    {
                                        MyControl.Add(MyItemX);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (item is TextBox || item is ComboBox || item is CheckBox)
                {
                    MyControl.Add(item);
                }
            }
            return MyControl;
        }

        public void Save()
        {
            string SaveStr = "";
            List<Control> MyControl = GetAllControls();

            foreach (Control MyItem in MyControl)
            {
                if (MyItem is TextBox || MyItem is ComboBox)
                {
                    SaveStr += MyItem.Name + "=" + MyItem.Text.Trim() + Environment.NewLine;
                }
                else if (MyItem is CheckBox)
                {
                    CheckBox Temp = (CheckBox)MyItem;
                    SaveStr += Temp.Name + "=" + Temp.Checked + Environment.NewLine;
                }
            }
            //if(File.Exists(FileName))
            //{
            //    File.Delete(FileName);
            //}
            File.WriteAllText(FileName, SaveStr);
        }

        private int ParseConfig()
        {
            string[] SaveStr;
            if (!File.Exists(FileName))
                return 0;
            else
                SaveStr = File.ReadAllLines(FileName);

            gSaveItemName = new string[SaveStr.Length];
            gSaveItemContent = new string[SaveStr.Length];
            for (int i = 0; i < SaveStr.Length; i++)
            {
                int Index = SaveStr[i].IndexOf('=');
                gSaveItemName[i] = SaveStr[i].Substring(0, Index);
                gSaveItemContent[i] = SaveStr[i].Substring(Index + 1, SaveStr[i].Length - Index - 1);
            }
            return SaveStr.Length;
        }

        private string FineControlText(string Name)
        {
            for (int i = 0; i < gSaveItemName.Length; i++)
            {
                if (gSaveItemName[i] == Name)
                    return gSaveItemContent[i];
            }
            return "";
        }

        public void LoadData()
        {
            int Len;
            string Content;
            Len = ParseConfig();
            if (Len == 0)
                return;
            List<Control> MyControl = GetAllControls();
            foreach (Control MyItem in MyControl)
            {
                if (MyItem is TextBox || MyItem is ComboBox)
                {
                    Content = FineControlText(MyItem.Name);
                    if (Content != string.Empty)
                        MyItem.Text = Content;
                }
                else if (MyItem is CheckBox)
                {
                    CheckBox Temp = (CheckBox)MyItem;
                    Content = FineControlText(MyItem.Name);
                    if (Content != string.Empty)
                        Temp.Checked = Convert.ToBoolean(Content);
                }
            }

        }
    }
}
