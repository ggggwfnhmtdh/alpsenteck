﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace SOM_DEVELOP_TOOL
{
    public static class WilfFile
    {
        [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        public static UInt32 TimeCount = 0;
        public static List<string> list;

        public static bool IsWebPathExist(string URL)
        {
            try
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(URL);
                request.Timeout = 500;
                System.Net.WebResponse response = request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string[] GetDirOnlyNmae(string Dir, bool FindSub)
        {
            list = new List<string>();
            GetDirs(Dir, FindSub);
            for(int i=0; i<list.Count;i++)
            {
                list[i] = Path.GetFileName(list[i]);
            }
            return list.ToArray();
        }
        public static string[] GetDir(string Dir, bool FindSub)
        {
            list = new List<string>();
            GetDirs(Dir, FindSub);
            return list.ToArray();
        }
        public static string[] GetFile(string Dir,string FileType,bool FindSub)
        {
            list = new List<string>();
            GetFiles(Dir,FileType, FindSub);
            list.Sort(StrCmpLogicalW);  //对文件名进行排序
            return list.ToArray();
        }

        public static void DelFile(string Dir, string FileType)
        {
            list = new List<string>();
            GetFiles(Dir, FileType,true);
            for(int i=0;i<list.Count;i++)
            {
                File.Delete(list[i]);
            }
        }

        public static void DelFile(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            di.Delete(true);
        }
        public static void GetDirs(string dirs, bool FindSub)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {
                    list.Add(fsinfo.FullName);
                    //递归调用
                    if (FindSub == true)
                        GetDirs(fsinfo.FullName, FindSub);
                }
            }
        }


        private static void GetFiles(string dirs,string FileType,bool FindSub)
        {
            //绑定到指定的文件夹目录
            DirectoryInfo dir = new DirectoryInfo(dirs);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                //判断是否为空文件夹　　
                if (fsinfo is DirectoryInfo)
                {
                    //递归调用
                    if(FindSub == true)
                        GetFiles(fsinfo.FullName, FileType, FindSub);
                }
                else
                {
                    if(fsinfo.Extension == FileType || FileType == ".*")
                        list.Add(fsinfo.FullName);
                }
            }
        }
        public static string GetTimeStr(bool NeedCount=true)
        {
            if (NeedCount == false)
            {
                return DateTime.Now.ToString("yyyyMMdd_HHmmssffff");
            }
            else
            {
                return DateTime.Now.ToString("yyyyMMdd_HHmmssffff") + "_" + (TimeCount++).ToString();
            }
        }
        public static void Write<T>(string FileName, T FileContent)
        {
            if (FileContent is string)
            {
                File.WriteAllText(FileName, (string)Convert.ChangeType(FileContent, typeof(T)));
            }
            else if (FileContent is byte[])
            {
                File.WriteAllBytes(FileName, (byte[])Convert.ChangeType(FileContent, typeof(T)));
            }
        }

        public static void WriteAppend<T>(string FileName, T FileContent)
        {
            if (FileContent is string)
            {
                FileStream fs = new FileStream(FileName, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write((string)Convert.ChangeType(FileContent, typeof(T)));
                sw.Close();
                fs.Close();
            }
            else if (FileContent is byte[])
            {
                FileStream BinStream = new FileStream(FileName, FileMode.Append);
                BinaryWriter BinWriter = new BinaryWriter(BinStream);
                BinWriter.Write((byte[])Convert.ChangeType(FileContent, typeof(T)));
                BinWriter.Close();
                BinStream.Close();
            }
        }

        public static string Read(string FileName)
        {
           return File.ReadAllText(FileName);
        }

        public static byte[] ReadBin(string FileName)
        {
            return File.ReadAllBytes(FileName);
        }

        public static byte[] ReadBin(string FileName,int StartIndex,int Len)
        {
            byte[] DataArray;
            FileStream BinStream = new FileStream(FileName, FileMode.OpenOrCreate);
            BinaryReader BinReader = new BinaryReader(BinStream);
            BinReader.BaseStream.Seek(StartIndex, SeekOrigin.Begin);
            DataArray = BinReader.ReadBytes(Len);
            BinReader.Close();
            BinStream.Close();
            return DataArray;
        }   
        
        public static string OpenFile(string FileType)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文件|*" + FileType + "|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;                           
            }
            return null;
        }
        [STAThread]
        public static string OpenDir()
        {
            //string sPath = "";
            //FolderBrowserDialog folder = new FolderBrowserDialog();
            //folder.Description = "加载校准数据目录";
            //if(folder.ShowDialog() == DialogResult.OK)
            //{
            //    sPath = folder.SelectedPath;
            //    return sPath;
            //}
            //return null;

            var fsd = new FolderSelectDialog();
            fsd.Title = "What to select";
            fsd.InitialDirectory = @"c:\";
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                return (fsd.FileName);
            }
            return null;
        }

        public static int[] GetIntDataFromCsv(string FileNmae)
        {
            string[] FileContetent = File.ReadAllLines(FileNmae);
            string[] data_str;
            if(FileContetent.Length==1)
            {
                if(FileContetent[0].Contains(","))
                    data_str = FileContetent[0].Split(',');
                else if (FileContetent[0].Contains(";"))
                    data_str = FileContetent[0].Split(';');
                else
                    data_str = FileContetent[0].Split(' ');
            }
            else
            {
                data_str = FileContetent;
            }
            int[] data = new int[data_str.Length];
            for(int i=0;i<data.Length;i++)
            {
                data[i] = Convert.ToInt32(data_str[i]);
            }
            return data;
        }

        public static string[] GetStringDataFromCsv(string FileNmae)
        {
            string[] FileContetent = File.ReadAllLines(FileNmae);
            string[] data_str;
            if (FileContetent.Length==1)
            {
                if (FileContetent[0].Contains(","))
                    data_str = FileContetent[0].Split(',');
                else if (FileContetent[0].Contains(";"))
                    data_str = FileContetent[0].Split(';');
                else
                    data_str = FileContetent[0].Split(' ');
            }
            else
            {
                data_str = FileContetent;
            }
            
            return data_str;
        }
        public static List<double[]> GetDataFromCsv(string FileNmae)
        {
            List<double[]> MyData = new List<double[]>();
            string[] FileContetent = File.ReadAllLines(FileNmae);
            for (int i = 0; i < FileContetent.Length; i++)
            {
                FileContetent[i] = FileContetent[i].Replace("\n", "").Trim();
                FileContetent[i] = FileContetent[i].Trim(',');
                if (FileContetent[i] == "")
                    continue;
                string[] Line = FileContetent[i].Split(',');
                double[] LineData = new double[Line.Length];
                for (int j = 0; j < Line.Length; j++)
                {
                    LineData[j] = Convert.ToDouble(Line[j]);
                }
                MyData.Add(LineData);
            }
            return MyData;
        }

        public static List<double[]> GetDataFromCsv(string FileNmae,out string[] Title)
        {
            List<double[]> MyData = new List<double[]>();
            string[] FileContetent = File.ReadAllLines(FileNmae);
            int Offset = 1;
            FileContetent[0] = FileContetent[0].Trim(' ').Trim(',').Trim(';');
            Title = FileContetent[0].Split(',');
            for (int i = Offset; i < FileContetent.Length; i++)
            {
                FileContetent[i] = FileContetent[i].Replace("\n", "").Trim();
                FileContetent[i] = FileContetent[i].Trim(',');
                if (FileContetent[i] == "")
                    continue;
                string[] Line = FileContetent[i].Split(',');
                double[] LineData = new double[Line.Length];
                for (int j = 0; j < Line.Length; j++)
                {
                    LineData[j] = Convert.ToDouble(Line[j]);
                }
                MyData.Add(LineData);
            }
            return MyData;
        }

        public static double[,] GetDataFromArrayCsv(string FileNmae)
        {
            List<double[]> MyData = new List<double[]>();
            MyData = GetDataFromCsv(FileNmae);
            double[,] OutData = new double[MyData.Count, MyData[0].Length];
            for (int i = 0; i < OutData.GetLength(0);i++)
            {
                for (int j = 0; j < OutData.GetLength(1); j++)
                {
                    OutData[i, j] = MyData[i][j];
                }
            }
            return OutData;
        }


        public static double[,] GetDataFromArrayCsv(string FileNmae,int StartRow,int RowNum)
        {
            List<double[]> MyData = new List<double[]>();
            MyData = GetDataFromCsv(FileNmae);
            double[,] OutData = new double[RowNum, MyData[0].Length];
            for (int i = 0; i < RowNum; i++)
            {
                for (int j = 0; j < OutData.GetLength(1); j++)
                {
                    OutData[i, j] = MyData[i + StartRow][j];
                }
            }
            return OutData;
        }

        public static void CreateDirectory(string Dir)
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }

        public static string FileNameInjectStr(string FileName,string Context,bool KeepExe = true)
        {
            string OutFileName = "";
            FileName = Path.GetFullPath(FileName);
            OutFileName = Path.GetDirectoryName(FileName);
            OutFileName += "\\";
            OutFileName += Path.GetFileNameWithoutExtension(FileName);
            OutFileName += Context;
            if(KeepExe == true)
                OutFileName += Path.GetExtension(FileName);
            return OutFileName;
        }

        public static long GetFileSize(string FileName)
        {
            FileInfo fileInfo = null;
            if (File.Exists(FileName))
            {
                fileInfo = new System.IO.FileInfo(FileName);
                return fileInfo.Length;
            }
            else
                return 0;
        }    
        
        public static bool OpenYesNo(string ContentStr)
        {
            if (MessageBox.Show(ContentStr, "",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
