using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
namespace SOM_DEVELOP_TOOL
{

    public static class Json
    {
        public static string m_CfgFile = @".\Database\PathInf.data";
        public static string m_SoftInfFile = @".\SoftwareInf.data";
        public static void SaveCfg(CfgType Cfg)
        {
            string Dir = Path.GetDirectoryName(m_CfgFile);
            WilfFile.CreateDirectory(Dir);
            //var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };
            string str = JsonConvert.SerializeObject(Cfg,Formatting.Indented).ToString();
            File.WriteAllText(m_CfgFile, str);
        }

        public static void BackupCfg()
        {
            if (File.Exists(m_CfgFile))
            {
                File.Copy(m_CfgFile, WilfFile.FileNameInjectStr(m_CfgFile, "_" + WilfFile.GetTimeStr()), true);
            }
        }

        public static CfgType LoadCfg(string filename = "")
        {
            if(File.Exists(filename) == true)
            {
                File.Copy(filename, m_CfgFile, true);
            }
            StringBuilder sb = new StringBuilder();
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            CfgType Cfg = new CfgType();
            if (File.Exists(m_CfgFile))
            {
                string[] str = File.ReadAllLines(m_CfgFile);
                for (int i = 0; i < str.Length; i++)
                {
                    if(str[i].Contains("\\\\") == false && str[i].Contains("\\") == true)
                    {
                        str[i] = str[i].Replace("\\", "\\\\");
                    }
                    sb.Append(str[i] + Environment.NewLine);
                }
                //var settings = new JsonSerializerSettings() { ContractResolver = new OcrDeserializeResolver() };
                Cfg = JsonConvert.DeserializeObject<CfgType>(sb.ToString());
            }
            else
            {
                SaveCfg(Cfg);
            }
            return Cfg;
        }

        public static void SaveCfg(SoftWareInf Cfg)
        {
            //var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };
            string str = JsonConvert.SerializeObject(Cfg, Formatting.Indented).ToString();
            File.WriteAllText(m_SoftInfFile, str);
        }

        public static SoftWareInf LoadSoftInf(string filename = "")
        {
            if (File.Exists(filename) == false)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            var jSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            SoftWareInf Cfg = new SoftWareInf();
            if (File.Exists(m_SoftInfFile))
            {
                string str = File.ReadAllText(m_SoftInfFile);
                //var settings = new JsonSerializerSettings() { ContractResolver = new OcrDeserializeResolver() };
                Cfg = JsonConvert.DeserializeObject<SoftWareInf>(str);
            }
            return Cfg;
        }
    }

}
