using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SOM_DEVELOP_TOOL;

namespace SOM_DEVELOP_TOOL
{
    internal class SphinxCodePro
    {
        public struct FileIDInf
        {
            public string Name;
            public List<int> LineNum;
            public List<string> CurID;
            public List<string> ReplaceID;
            public List<string> Var;
            public bool IsRepeat;
        }
        
        public static List<string[]> RepPtrFun;
        public static List<string[]> RepPtrVar;
        public static string PatchHeader = "g_func_lookup_table";

        public static string reg_g_func_lookup_table = @".*function_lookup_table_t\s*g_func_lookup_table";
        public static string reg_terminateCatchingFunName = @"\s*\}";
        public static string reg_carry_pattern = @"^..*=.*";
        public static string reg_g_var_lookup_table = @".*variable_lookup_table_t\s*g_var_lookup_table";

        public static Hashtable InformTable = new Hashtable();
        public static void EditLogFile(List<FileIDInf> FileIdInfLst, UInt32[] NewID, Hashtable ht)
        {
            string rep_str = "";
            int IdIndex = 0;
            bool NeedWrite = false;
            for (int i = 0; i < FileIdInfLst.Count; i++)
            {
                string[] Lines = File.ReadAllLines(FileIdInfLst[i].Name);
                NeedWrite = false;
                for (int j = 0; j < FileIdInfLst[i].LineNum.Count; j++)
                {
                    string EditLine = Lines[FileIdInfLst[i].LineNum[j]];
                    string SubEditLine = EditLine.Substring(EditLine.IndexOf("(") + 1, EditLine.IndexOf(")") - EditLine.IndexOf("(") - 1);
                    string[] str = SubEditLine.Split(',');
                    if (str[0].Trim() == FileIdInfLst[i].CurID[j])
                    {
                        FileIdInfLst[i].ReplaceID.Add(NewID[IdIndex].ToString());
                        
                        ht.Add(NewID[IdIndex], FileIdInfLst[i].Var[j]);
                        if (FileIdInfLst[i].CurID[j] != NewID[IdIndex].ToString())
                        {
                            rep_str = NewID[IdIndex] + "," + str[1];
                            Lines[FileIdInfLst[i].LineNum[j]] = Lines[FileIdInfLst[i].LineNum[j]].Replace(SubEditLine, rep_str);
                            Debug.AppendMsg("EditID:" + FileIdInfLst[i].CurID[j] + "->" + NewID[IdIndex].ToString() + "," + FileIdInfLst[i].Var[j] +"("+ Path.GetFileName(FileIdInfLst[i].Name) + ")"+ Environment.NewLine);
                            NeedWrite = true;
                        }
                        IdIndex++;

                    }
                }
                if (NeedWrite)
                {
                    File.WriteAllLines(FileIdInfLst[i].Name, Lines);
                }
            }
        }
        public static Hashtable ParseLog(string ProjectFile,bool EditIdEnable,bool SortEnable)
        {
            string msg = "";
            int i, j;
            Hashtable ID_Table = new Hashtable();
            string[] FileName;
            string reg_printf = @".*printf_.*\(.*\)";
            List<UInt32> RepeatKey = new List<UInt32>();
            List<FileIDInf> FileIdInfLst = new List<FileIDInf>();
            StringBuilder sb = new StringBuilder();
            UInt32 ID;
            List<UInt32> AllId = new List<uint>(); 
            Random Rnd = new Random();
            int RepeatIdNum = 0;
            int NormalIdNum = 0;
            ID_Table.Clear();
            FileName = ProjectInf.GetFilePath(ProjectFile,".c");
            for (i = 0; i < FileName.Length; i++)
            {
                string[] lines = File.ReadAllLines(FileName[i]);
                FileIDInf NewFileIDInf = new FileIDInf();
                NewFileIDInf.Name = FileName[i];
                NewFileIDInf.LineNum = new List<int>();
                NewFileIDInf.CurID = new List<string>();
                NewFileIDInf.ReplaceID = new List<string>();
                NewFileIDInf.Var = new List<string>();
                msg = FileName[i];
                for (j = 0; j < lines.Length; j++)
                {
                    string line = Regex.Replace(lines[j], @"\s", "");
                    if (line.Length > 0 && line[0] == 'p')
                    {
                        string mstr = search(reg_printf, line);
                        if (mstr != null)
                        {
                            string[] temp = mstr.Substring(mstr.IndexOf("(") + 1, mstr.LastIndexOf(")") - mstr.IndexOf("(") - 1).Split(',');
                            bool Param1IsNum = WilfDataPro.IsNumeric(temp[1]);
                            if (WilfDataPro.IsNumeric(temp[0],true))
                            {
                                if (temp[0].Contains("0x"))
                                    ID = Convert.ToUInt32(temp[0], 16);
                                else
                                    ID = Convert.ToUInt32(temp[0], 10);

                                if (ID == 0)
                                {
                                    if (ID_Table.ContainsKey(ID) == false)
                                    {
                                        ID_Table.Add(ID, "Function");
                                    }
                                    continue;
                                }
                                AllId.Add(ID);
                                if (ID_Table.ContainsKey(ID) == false && ID >0 && ID <= 250)
                                {
                                    if (Param1IsNum == true)
                                        ID_Table.Add(ID, "Const_"+ temp[1]);
                                    else
                                        ID_Table.Add(ID, temp[1]);
                                    if (SortEnable)
                                    {
                                        NewFileIDInf.LineNum.Add(j);
                                        NewFileIDInf.CurID.Add(temp[0]);
                                        NewFileIDInf.Var.Add(temp[1]);
                                        NewFileIDInf.IsRepeat = false;
                                        NormalIdNum++;
                                    }
                                }
                                else
                                {
                                    RepeatKey.Add(ID);
                                    NewFileIDInf.LineNum.Add(j);
                                    NewFileIDInf.CurID.Add(temp[0]);
                                    NewFileIDInf.Var.Add(temp[1]);
                                    NewFileIDInf.IsRepeat = true;
                                    RepeatIdNum++;
                                }
                            }
                        }
                    }
                }
                if (NewFileIDInf.LineNum.Count > 0)
                {
                    FileIdInfLst.Add(NewFileIDInf);
                }
            }

            UInt32[] SortID = AllId.ToArray();
            Array.Sort(SortID);
            bool IsContinue = true;
            for(int k = 1; k < SortID.Length; k++)
            {
                if(SortID[k] != SortID[k-1] + 1)
                {
                    IsContinue = false;
                    break;
                }
            }
            if (RepeatIdNum > 0 || (SortEnable == true && IsContinue == false))
            {
                if (EditIdEnable == true)
                {
                    UInt32 RandData = 0;
                    UInt32[] NewId;
                    if (SortEnable)
                    {
                        NewId = new UInt32[RepeatIdNum + NormalIdNum];
                        for (i = 0; i < NewId.Length; i++)
                        {
                            RandData++;
                            NewId[i] = RandData;
                        }
                    }
                    else
                    {
                        NewId = new UInt32[RepeatIdNum];
                        for (i = 0; i < NewId.Length; i++)
                        {
                            while (true)
                            {
                                RandData++;
                                if (ID_Table.ContainsKey(RandData) == false)
                                    break;

                            }
                            NewId[i] = RandData;
                        }
                    }
                    if(ID_Table.ContainsKey((uint)0))
                    {
                        ID_Table.Clear();
                        ID_Table.Add((uint)0, "Function");
                    }
                    else
                        ID_Table.Clear();
                    EditLogFile(FileIdInfLst, NewId, ID_Table);
                    MessageBox.Show("Warning:Source code has been updated, please re-compile and download");
                }
                else
                {
                    for ( i = 0; i < RepeatKey.Count; i++)
                    {
                        if (ID_Table.ContainsKey(RepeatKey[i]))
                            ID_Table.Remove(RepeatKey[i]);
                    }
                }
            }

            foreach (var key in ID_Table.Keys)
            {
                sb.Append(key.ToString() + "," + (string)ID_Table[key] + Environment.NewLine);
            }
            return ID_Table;
        }

        public static string GetInformParam(string str,int ParamIndex)
        {
            string outstr = str.Substring(str.IndexOf('(') + 1, str.IndexOf(')') - str.IndexOf('(') - 1).Trim();
            string[] Params = outstr.Split(',');
            if (Params.Length >= (ParamIndex + 1))
                return Params[ParamIndex];
            else
                return null;
        }

        private static int GetInformType(string str)
        {
            string linestr = GetInformParam(str,0);
            return Convert.ToInt32(linestr);
        }

        public static string GetFunctionName(string str)
        {
            return str.Substring(str.IndexOf(' ') + 1, str.IndexOf('(') - str.IndexOf(' ') - 1).Trim();
        }

        public static string search(string expr, string InStr)
        {
            MatchCollection mc = Regex.Matches(InStr, expr);

            foreach (Match m in mc)
            {
                return m.ToString();
            }
            return null;
        }

        public static bool IsRepeat(List<string> array)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < array.Count; i++)
            {
                if (ht.Contains(array[i]))
                {
                    return true;
                }
                else
                {
                    ht.Add(array[i], array[i]);
                }
            }
            return false;
        }
        /// <summary>
        /// 复制文件夹及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns></returns>
        public static int CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                //如果目标路径不存在,则创建目标路径
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest);//复制文件
                }
                //得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    CopyFolder(folder, dest);//构建目标路径,递归复制文件
                }
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }

        }
        public static void EditFile(string Dir)
        {
            string[] IgnorDir = new string[] { ".svn", ".vscode", "doc", "testing_app", "testing_scripts" };
            string NewDir = Dir + "_lookover";
            string[] FileName = WilfFile.GetFile(Dir, ".c", true);
            string ObjString;
            string RepString;
            string wFileNmae;
            Directory.CreateDirectory(NewDir);
            Debug.AppendMsg("Start Backup Folder" + Environment.NewLine);
            CopyFolder(Dir, NewDir);
            for (int i=0;i<IgnorDir.Length;i++)
            {
                if (Directory.Exists(NewDir + "\\" + IgnorDir[i]))
                {
                    try
                    {
                        Directory.Delete(NewDir + "\\" + IgnorDir[i], true);
                    }
                    catch
                    {
                        Debug.AppendMsg("Delete fail" + Environment.NewLine);
                    }
                }
            }
            Debug.AppendMsg("Folder Backup OK"+Environment.NewLine);
            for (int i = 0; i < FileName.Length; i++)
            {
                string FileContent = File.ReadAllText(FileName[i]);
                for (int k = 0; k < RepPtrFun.Count; k++)
                {
                    ObjString = PatchHeader + "." + RepPtrFun[k][0] + "(";
                    RepString = RepPtrFun[k][1] + "(";
                    if (FileContent.Contains(ObjString))
                    {
                        FileContent = FileContent.Replace(ObjString, RepString);

                    }

                    ObjString = PatchHeader + "." + RepPtrFun[k][0] + ")";
                    RepString = RepPtrFun[k][1] + ")";
                    if (FileContent.Contains(ObjString))
                    {
                        FileContent = FileContent.Replace(ObjString, RepString);
                    }

                    ObjString = PatchHeader + "." + RepPtrFun[k][0] + ",";
                    RepString = RepPtrFun[k][1] + ",";
                    if (FileContent.Contains(ObjString))
                    {
                        FileContent = FileContent.Replace(ObjString, RepString);
                    }
                }
                wFileNmae = FileName[i].Replace(Dir, NewDir);
                Directory.CreateDirectory(Path.GetDirectoryName(wFileNmae));        
                File.WriteAllText(wFileNmae, FileContent);
                Debug.AppendMsg(Path.GetFileName(FileName[i]) + Environment.NewLine);
                
            }
            Debug.AppendMsg("Over" + Environment.NewLine);
        }
        
        
    }
}
