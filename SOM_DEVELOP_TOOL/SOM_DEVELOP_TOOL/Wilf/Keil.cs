using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace SOM_DEVELOP_TOOL
{
    public class Keil
    {
        public static string[] GetFilePath(string ProjectDir, string FileType)
        {
            Hashtable ht = new Hashtable();
            string file_name;
            string[] pFile = WilfFile.GetFile(ProjectDir, ".uvprojx", false);
            List<string> list = new List<string>();
            if (pFile != null && File.Exists(pFile[0]))
            {
                string[] lines = File.ReadAllLines(pFile[0]);
                for (int i = 0; i<lines.Length; i++)
                {
                    string temp_path = WilfDataPro.MidStr(lines[i], "<FilePath>", "</FilePath>");
                    if (temp_path == string.Empty || Path.GetExtension(temp_path).ToUpper() != FileType.ToUpper())
                        continue;
                    file_name = ProjectDir +"\\"+ temp_path;
                    if (File.Exists(file_name))
                    {
                        file_name = Path.GetFullPath(file_name);
                        if (ht.ContainsKey(file_name) == false)
                        {
                            ht.Add(file_name, i);
                            list.Add(file_name);
                        }
                    }

                }
            }
            return list.ToArray();
        }

        public static string[] ClearProject(string ProjectFile)
        {
            Hashtable ht = new Hashtable();
            string OutputDirectory = "";
            string OutputName = "";
            string ListingPath = "";
            string ExeName;
            List<string> list = new List<string>();
            string ProjectDir = Path.GetDirectoryName(ProjectFile);
            string[] LstFile;
            List<string> ProFile= new List<string>();
            if (File.Exists(ProjectFile))
            {
                string[] lines = File.ReadAllLines(ProjectFile);
                for (int i = 0; i<lines.Length; i++)
                {
                    if (OutputDirectory == string.Empty)
                        OutputDirectory = WilfDataPro.MidStr(lines[i], "<OutputDirectory>", "</OutputDirectory>");
                    else if (OutputName == string.Empty)
                        OutputName = WilfDataPro.MidStr(lines[i], "<OutputName>", "</OutputName>");
                    else if (ListingPath == string.Empty)
                        ListingPath = WilfDataPro.MidStr(lines[i], "<ListingPath>", "</ListingPath>");
                    else
                        ;
                }
                ListingPath =  Path.GetFullPath(ProjectDir+"\\"+ListingPath);
                OutputDirectory =  Path.GetFullPath(ProjectDir+"\\"+OutputDirectory);
                LstFile = WilfFile.GetFile(OutputDirectory, ".*", true);
                for (int i = 0; i<LstFile.Length; i++)
                {
                    ExeName = Path.GetExtension(LstFile[i]).ToLower();
                    if (ExeName == ".c" || ExeName == ".h"||ExeName == ".asm"||ExeName == ".axf"||ExeName == ".hex"||ExeName == ".bin")
                    {
                        continue;
                    }
                    else
                    {
                        if (File.Exists(LstFile[i]))
                        {
                            File.Delete(LstFile[i]);
                            ProFile.Add(LstFile[i]);
                        }
                    }
                }
            }

            return ProFile.ToArray();
        }
        public static Hashtable GetOutFileInf(string ProjectFile)
        {
            Hashtable ht = new Hashtable();
            string OutputDirectory="";
            string OutputName="";
            string ListingPath="";
            List<string> list = new List<string>();
            List<string> File_Asm = new List<string>();
            List<string> File_C = new List<string>();
            List<string> File_H = new List<string>();
            List<string> File_ALL = new List<string>();
            string FilePath,ProjectDir,ExeName;

            if (File.Exists(ProjectFile))
            {
                string[] lines = File.ReadAllLines(ProjectFile);
                ProjectDir = Path.GetDirectoryName(ProjectFile);
                for (int i = 0; i<lines.Length; i++)
                {
                    FilePath =  WilfDataPro.MidStr(lines[i], "<FilePath>", "</FilePath>");
                    if (FilePath != string.Empty)
                    {
                        if(FilePath.Contains(":"))
                            FilePath = Path.GetFullPath(FilePath);
                        else
                            FilePath = Path.GetFullPath(ProjectDir +"\\"+ FilePath);
                        ExeName = Path.GetExtension(FilePath).ToLower();   
                        File_ALL.Add(FilePath);
                        if(ExeName == ".c")
                        {
                            File_C.Add(FilePath);   
                        }
                        else if (ExeName == ".h")
                        {
                            File_H.Add(FilePath);
                        }
                        else if (ExeName == ".asm")
                        {
                            File_Asm.Add(FilePath);
                        }
                    }
                    if (OutputDirectory == string.Empty)
                        OutputDirectory = WilfDataPro.MidStr(lines[i], "<OutputDirectory>", "</OutputDirectory>");
                    else if (OutputName == string.Empty)
                        OutputName = WilfDataPro.MidStr(lines[i], "<OutputName>", "</OutputName>");
                    else if (ListingPath == string.Empty)
                        ListingPath = WilfDataPro.MidStr(lines[i], "<ListingPath>", "</ListingPath>");
                    else
                        ;
                }

                if (ListingPath == "")
                    ListingPath = OutputDirectory;
                ht.Add(".map", Path.GetFullPath(ProjectDir+"\\"+ListingPath+OutputName+".map"));
                ht.Add(".lst", Path.GetFullPath(ProjectDir+"\\"+ListingPath+OutputName+".lst"));
                ht.Add(".axf", Path.GetFullPath(ProjectDir+"\\"+OutputDirectory+OutputName+".axf"));
                ht.Add(".hex", Path.GetFullPath(ProjectDir+"\\"+OutputDirectory+OutputName+".hex"));
                ht.Add(".bin", Path.GetFullPath(ProjectDir+"\\"+OutputDirectory+OutputName+".bin"));
                ht.Add(".c", File_C.ToArray());
                ht.Add("ListingPath", Path.GetFullPath(ProjectDir+"\\"+ListingPath));
                ht.Add("OutputDirectory", Path.GetFullPath(ProjectDir+"\\"+OutputDirectory));
            }
            return ht;
        }
    }
}
