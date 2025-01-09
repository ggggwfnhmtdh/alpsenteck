using SOM_DEVELOP_TOOL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_DEVELOP_TOOL
{
    public enum FileType
    {
        MAP,
        ELF,
        AXF,
        BIN,
        PATCH,
        HEX,
    }
    public static class ProjectInf
    {
       
        public static string GetArmFilePath(string ProjectFile, FileType Type)
        {
            string FileName = "";
            Hashtable ht = Keil.GetOutFileInf(ProjectFile);
            switch (Type)
            {
                case FileType.MAP:
                    FileName = (string)ht[".map"];
                    break;
                case FileType.ELF:
                    FileName = "";
                    break;
                case FileType.AXF:
                    FileName = (string)ht[".axf"];
                    break;
                case FileType.BIN:
                    FileName = (string)ht[".bin"];
                    break;
                case FileType.HEX:
                    FileName = (string)ht[".hex"];
                    break;
                default:
                    FileName = "";
                    break;
            }
            return FileName;
        }

        public static string[] GetFilePath(string ProjectFile,string ExeName = ".c")
        {
            string[] FileName ;
            Hashtable ht = Keil.GetOutFileInf(ProjectFile);
            FileName = (string[])ht[ExeName];
            return FileName;
        }

        
    }
}
