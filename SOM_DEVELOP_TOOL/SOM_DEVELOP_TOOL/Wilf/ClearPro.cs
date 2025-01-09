using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace SOM_DEVELOP_TOOL
{
    public static class ClearPro
    {
        public static string search(string expr, string InStr)
        {
            MatchCollection mc = Regex.Matches(InStr, expr);

            foreach (Match m in mc)
            {
                return m.ToString();
            }
            return "";
        }
        public static void ClearDirectory(string Dir)
        {
            string[] dir = WilfFile.GetDir(Dir,true);
            string[] pro_reg = new string[] 
            { 
                @".*\\keil_Arm_fw_proj.*\\Objects$",
                @".*\\keil_Arm_fw_proj.*\\dsp_patch_bin\\LabviewScript2C$",
                @".*\\Sphinx_0p3_ram_gen.*\\testing_app\\system_integration\\bin\\Py_210123_eval$",
                @".*\\Sphinx_0p3_ram_gen.*\\doc$",
                @".*\\Sphinx_0p3_ram_gen.*\\.svn$"

            };

            if (Dir == null || Directory.Exists(Dir) == false)
                return;
            for(int i=0;i<dir.Length;i++)
            {
                for (int j = 0; j < pro_reg.Length; j++)
                {
                    string match_res = search(pro_reg[j], dir[i]);
                    if (match_res != "")
                    {
                        if (match_res.Contains("LabviewScript"))
                        {
                            string file0 = match_res + "\\JLinkARM.dll";
                            if(File.Exists(file0))
                            {
                                File.Delete(file0);
                                Debug.AppendMsg(file0 + Environment.NewLine);
                            }
                        }
                        else
                        {
                            try
                            {
                                Directory.Delete(match_res, true);
                                Debug.AppendMsg(match_res + Environment.NewLine);
                            }
                            catch 
                            {
                                Debug.AppendMsg("Error:" + match_res + Environment.NewLine);
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
