using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using NPOI.SS.Formula.Functions;

namespace SOM_DEVELOP_TOOL
{
    public struct CaseType
    {
        public bool IsOk;
        public bool IsInner;
        public string FunName;
        public UInt32 Addr;
        public UInt32[] ParamValue;
        public string[] Param;
        public string CaseName;
    }

    public static class TestCase
    {
        public static CaseType[] CaseArray;
        private static string[] InnerFun = new string[] { "SLEEP", "SETOVERTIME", "READMULTMETER","READU8", "READU32", "WRITEU8", "WRITEU32" };
        private static void ParseFile(string FileName,bool IsFile = false)
        {
            List<CaseType> LstCase = new List<CaseType>();
            StringBuilder sb = new StringBuilder();
            string[] CaseStr;
            if (IsFile && File.Exists(FileName))
            {
                CaseStr = File.ReadAllLines(FileName);
            }
            else
            {
                CaseStr = new string[1];
                CaseStr[0] = FileName;
            }
            for (int i = 0; i<CaseStr.Length; i++)
            {
                string[] ItemStr;
                StringBuilder Msg = new StringBuilder();
               
                if(CaseStr[i].Contains("//"))
                {
                    CaseStr[i] = CaseStr[i].Substring(0, CaseStr[i].IndexOf("//"));
                }
                CaseStr[i] = WilfDataPro.RemoiveMulSapce(CaseStr[i]);
                CaseStr[i] = CaseStr[i].Trim(';');
                if (CaseStr[i] == "")
                    continue;
                ItemStr = CaseStr[i].Split(';');
                for (int k = 0; k<ItemStr.Length; k++)
                {
                    CaseType TC = new CaseType();
                    TC.CaseName= ItemStr[k];    
                    TC.FunName = ItemStr[k].Substring(0, ItemStr[k].IndexOf('('));
                    string ParamStr = WilfDataPro.MidStr(ItemStr[k], "(", ")").Trim();
                    if (ParamStr.Contains(',') || (ParamStr.Length>0))
                        TC.Param = WilfDataPro.MidStr(ItemStr[k], "(", ")").Split(',');
                   else
                        TC.Param = new string[0];
                    TC.ParamValue = new UInt32[TC.Param.Length];
                    LstCase.Add(TC);
                }
            }
            CaseArray = LstCase.ToArray();
        }

        private static bool IsInner(string FunName)
        {
            for(int i=0;i<InnerFun.Length;i++) 
            {
                if (InnerFun[i].ToUpper() == FunName.ToUpper())
                    return true;    
            }
            return false;
        }

        public static void PrintfCase(CaseType[] CaseItem)
        {
            for(int i=0;i<CaseItem.Length;i++)
            {
                Debug.AppendMsg(CaseItem[i].FunName+Environment.NewLine);
            }
        }
        public static void GetCase(string FileName,bool IsFile=false)
        {
            ParseFile(FileName, IsFile);
            for (int i = 0; i<CaseArray.Length; i++)
            {
                if (IsInner(CaseArray[i].FunName))
                {
                    CaseArray[i].IsInner= true;
                    CaseArray[i].IsOk= true;
                }
                else
                {
                    CaseArray[i].IsInner= false;
                    CaseArray[i].Addr = MapParser.GetAddr(CaseArray[i].FunName);
                    CaseArray[i].IsOk =  (CaseArray[i].Addr != 0xFFFFFFFF);
                    
                }

                for (int j = 0; j<CaseArray[i].Param.Length; j++) 
                {
                    bool ret =  WilfDataPro.IsNumeric(CaseArray[i].Param[j]);
                    if (ret == true)
                    {
                        CaseArray[i].ParamValue[j] = WilfDataPro.ToDec(CaseArray[i].Param[j]);  
                    }
                    else
                    {
                        CaseArray[i].ParamValue[j]  = Convert.ToUInt32(MapParser.CalcExpress(CaseArray[i].Param[j]));
                        if (CaseArray[i].ParamValue[j] == 0xFFFFFFFF)
                            CaseArray[i].IsOk =  false;
                    }
                } 
            }
        }
    }
}
