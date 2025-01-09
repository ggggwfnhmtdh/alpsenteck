using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_DEVELOP_TOOL
{
    public class CfgType
    {
        public CfgType()
        {
            DspProjectDir = @"E:\Temp";
            ArmProjectDir = @"E:Temp";
            DebugFileDir = @"E:\Temp";
            KeilDir = @"D:\Keil_v5";
            LogicDir = @"D:\Program Files\Logic";
            DspVersion = "3";
            ArmVersion = "4";
            ResetChipEnable = true;
            ThemeIndex = 1;
            UpgradeEnable = false;
            
        }
        
        public string ArmProjectDir { get; set; }
        public string DspProjectDir { get; set; }
        public string MlsProjectDir { get; set; }
        public string DebugFileDir { get; set; }

        public string GoProofPatchDir { get; set; }

        public string ArmProjectFile { get; set; }
        public string ArmBinFile { get; set; }
        public string ArmMapFile { get; set; }
        public string ArmAxfFile { get; set; }
        public string ArmHexFile { get; set; }


        public string DspBinFile { get; set; }
        public string DspMapFile { get; set; }
        public string DspAxfFile { get; set; }
        public string DspHexFile { get; set; }

        public string KeilDir { get; set; }
        public string KeilFromelf { get; set; }

        public string LogicDir { get; set; }
        public string LogicAnalyzersDir { get; set; }

        public bool ArmDownloadEnable { get; set; }

        public bool ArmJlinkDownloadEnable { get; set; }

        public bool ResetChipEnable { get; set; }

        public bool UpgradeEnable { get; set; }

        public bool AutoEditEnable { get; set; }


        public string ArmVersion { get; set; }
        public string DspVersion { get; set; }
        public int ThemeIndex { get; set; }

    }

    public class SoftWareInf
    {
        public string UserName { get; set; }
        public UInt32 LimitDateTime { get; set; }
        public string Version { get; set; }
        public string MAC { get; set; }
    }
}
