using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInstaller;
namespace Uninstall
{
    internal class Program
    {
        public struct FileData
        {
            public int Type;
            public string FileName;
        }
        static void Main(string[] args)
        {
            string Dir;
            string ProductCode;
            string[] FileList;
            string UninstallFile;
            Dir = System.Windows.Forms.Application.StartupPath;
            UninstallFile = Dir + "\\Uninstall.lst";
            FileList = GetFile(Dir, ".msi");
            
            if (FileList.Length == 0)
            {
                if (File.Exists(UninstallFile))
                {
                    FileData[] FD = GetFileList(UninstallFile);
                    for (int i = 0; i<FD.Length; i++)
                    {
                        Console.WriteLine("Uninstall:"+FD[i].FileName+Environment.NewLine);
                        try
                        {
                            if (FD[i].Type == 1)
                            {
                                if (File.Exists(FD[i].FileName))
                                {
                                    File.Delete(FD[i].FileName);
                                }
                            }
                            else if (FD[i].Type == 2)
                            {
                                if (Directory.Exists(FD[i].FileName))
                                {
                                    Directory.Delete(FD[i].FileName, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString()+Environment.NewLine);
                            continue;
                        }

                    }
                    for (int i = 0; i<FD.Length; i++)
                    {
                        Console.WriteLine("Uninstall:"+FD[i].FileName+Environment.NewLine);
                        if (FD[i].Type == 0)
                        {
                            Uninstall(FD[i].FileName);
                            break;
                        }
                    }
                    Console.WriteLine("Uninstall Successfully"+Environment.NewLine);
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                ProductCode = GetProductCode(FileList[0]);
                sb.Append("0"+","+ProductCode+Environment.NewLine);
                DirectoryInfo dir = new DirectoryInfo(Dir);
                FileInfo[] inf = dir.GetFiles();
                string Name;
                for (int i = 0; i<inf.Length; i++)
                {
                    Name = Path.GetFileName(inf[i].FullName);
                    if (Name != "Uninstall.exe")
                    {
                        sb.Append("1"+","+inf[i].FullName.Replace(Dir, ".")+Environment.NewLine);
                    }
                }
                File.WriteAllText(UninstallFile, sb.ToString()+Environment.NewLine, Encoding.UTF8);
                Console.WriteLine("OK"+Environment.NewLine);
                Console.ReadLine();

            }
           

        }

        public static void Uninstall(string ProductCode)
        {
            string sysroot = System.Environment.SystemDirectory;
            System.Diagnostics.Process.Start(sysroot+"\\"+"msiexec.exe", "/x "+ProductCode);
        }

        public static FileData[] GetFileList(string InFile)
        {
            List<FileData> lst = new List<FileData>();
            string[] str = File.ReadAllLines(InFile);
            for(int i=0;i<str.Length;i++)
            {
                if (str[i].Trim() == string.Empty)
                    continue;
                FileData FD = new FileData();
                string[] Item = str[i].Trim().Split(',');
                FD.Type = Convert.ToInt32(Item[0]);
                FD.FileName= Item[1];    
                lst.Add(FD);
            }
            return lst.ToArray();
        }
        static string[] GetFile(string Dir, string Type)
        {
            DirectoryInfo dir = new DirectoryInfo(Dir);
            FileInfo[] inf = dir.GetFiles();
            List<string> FileList = new List<string>();
            for (int i = 0; i<inf.Length; i++)
            {
                if (inf[i].Extension.Equals(Type) || Type == "*")
                {
                    FileList.Add(inf[i].FullName);
                }
            }
            return FileList.ToArray();
        }

        static void RemoveFile(string[] FlieList)
        {
            for(int i=0; i<FlieList.Length;i++)
            {
                try
                {
                    File.Delete(FlieList[i]);
                }
                catch 
                {
                    continue;
                }
            }
        }

        private static string GetProductCode(string MsiFile)
        {
            System.Type oType = System.Type.GetTypeFromProgID("WindowsInstaller.Installer");
            Installer inst = System.Activator.CreateInstance(oType) as Installer;
            Database DB = inst.OpenDatabase(MsiFile, MsiOpenDatabaseMode.msiOpenDatabaseModeReadOnly);
            string str = " SELECT * FROM Property WHERE Property = 'ProductCode' ";

            WindowsInstaller.View thisView = DB.OpenView(str);
            thisView.Execute();
            WindowsInstaller.Record thisRecord = thisView.Fetch();
            string result = thisRecord.get_StringData(2);

            return result;
        }

    }
}
