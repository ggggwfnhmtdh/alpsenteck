using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
//using NPOI.XWPF.UserModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;

namespace SOM_DEVELOP_TOOL
{
    public static class WilfOffice
    {
       
        public static RegData[] Load()
        {
            string TableFile;
            string[] SheetName;
            string Dir;
            int Index=0;
            bool ret;
            TableFile = WilfFile.OpenFile(".xlsx");
            List<RegData> RegList = null;
            if (File.Exists(TableFile)==false)
                return null;
            Dir = Path.GetDirectoryName(TableFile) +"\\"+ Path.GetFileNameWithoutExtension(TableFile);
            WilfFile.CreateDirectory(Dir);
            List<string[][]> Msg = WilfOffice.ReadFromExcelFile(TableFile, out SheetName, 100);
            for (int i = 0; i < Msg.Count; i++)
            {
                File.WriteAllText(Dir+ "\\" + SheetName[i] + ".csv", WilfDataPro.ToString(Msg[i]), Encoding.UTF8);
            }
            ret = FindSheet(SheetName, "CFR",ref Index);
            if (ret == true)
            {
                RegList = WilfOffice.ParseExcelData(Msg, SheetName, "SFR");
                File.WriteAllText(Dir+ "\\"  + "RegisterFile.csv", SomReg.ToString(RegList.ToArray()), Encoding.UTF8);
            }

            ret = FindSheet(SheetName, "trim_reg", ref Index);
            if (ret == true)
            {
                RegList = WilfOffice.ParseExcelData(Msg, SheetName, "trim_reg");
                File.WriteAllText(Dir+ "\\"  + "RegisterFile.csv", SomReg.ToString(RegList.ToArray()), Encoding.UTF8);
            }
            return RegList.ToArray();
        }

        public static bool FindSheet(string[] Sheets,string Name,ref int Index)
        {
            for(int i=0;i<Sheets.Length;i++)
            {
                if (Sheets[i].ToLower() == Name.ToLower())
                    return true;
            }
            return false;
        }

        public static string GetCellValue(XSSFFormulaEvaluator evalor, ICell InCell)
        {
            string Msg = "";
            
            if (InCell == null)
                return "";
            var cellType = InCell.CellType;
            switch (cellType)
            {

                case CellType.Formula:
                    //针对公式列 进行动态计算;注意：公式暂时只支持 数值 字符串类型
                    var formulaValue = evalor.Evaluate(InCell);
                    if (formulaValue.CellType == CellType.Numeric)
                    {
                        Msg = formulaValue.NumberValue.ToString();
                    }
                    else if (formulaValue.CellType == CellType.String)
                    {
                        Msg = formulaValue.StringValue;
                    }

                    break;
                default:
                    Msg = InCell.ToString();
                    break;
            }
            return Msg;
        }

        public static List<RegData> ParseExcelData(List<string[][]> RegSheet, string[] RegName,string ObjRegName)
        {
            List<RegBitData> LstRegBit = new List<RegBitData>();
            List<RegData> LstRegData = new List<RegData>();
            UInt32 TempRegOffset;
            string CurRegName="", LastRegName;
            Hashtable ht = new Hashtable(); //创建一个Hashtable实例

            string[][] CurSheet = null;
            for (int i = 0; i<RegName.Length; i++)
            {
                if (RegName[i] == ObjRegName)
                {
                    CurSheet = RegSheet[i];
                }
            }

            RegData NewRegData = new RegData();
            NewRegData.Data = new List<RegBitData>();
            for (int row = 0; row< CurSheet.GetLength(0); row++)
            {
                if (CurSheet[row] == null||CurSheet[row][0] == "register name")
                {
                    CurRegName = "";
                    continue;
                }

                LastRegName = CurRegName;
                CurRegName = CurSheet[row][0];
                TempRegOffset = (UInt32)WilfDataPro.CalcExpression(CurSheet[row][1]);
                RegBitData RegBit = new RegBitData();
                if (row == 0)
                {
                    NewRegData.Addr = TempRegOffset;
                }
                else if (CurRegName != string.Empty && LastRegName == string.Empty)
                {
                    if (NewRegData.Data.Count != 0)
                    {
                        LstRegData.Add(NewRegData);
                    }

                    NewRegData = new RegData();
                    NewRegData.ModuleName = CurSheet[row][0];
                    NewRegData.Addr = TempRegOffset;
                    NewRegData.Data = new List<RegBitData>();
                    continue;
                }
                try
                {
                    RegBit.ModuleName = NewRegData.ModuleName;
                    RegBit.Addr = NewRegData.Addr;
                    RegBit.StartBit = GetBitRange(CurSheet[row][3], 0);
                    RegBit.BitWidth = GetBitRange(CurSheet[row][3], 1);
                    RegBit.EndBit = RegBit.StartBit + RegBit.BitWidth -1;
                    RegBit.DefaultValue = (UInt32)WilfDataPro.CalcExpression(CurSheet[row][6]);
                    RegBit.RegName = CurSheet[row][2];
                    RegBit.Des = CurSheet[row][7];
                    RegBit.Attribute = CurSheet[row][4];
                    LstRegBit.Add(RegBit);
                    NewRegData.Data.Add(RegBit);
                }
                catch 
                {
                    ;
                }
            }
            LstRegData.Add(NewRegData);
            return LstRegData;
        }

        public static UInt32 GetBitRange(string Str,int Type)
        {
            Str = Str.Trim();
            UInt32 StartBit, BitWidth;
            if(Str.Contains(":"))
            {
                string[] StrArr = Str.Split(':');
                StartBit = Convert.ToUInt32(StrArr[1]);
                BitWidth = Convert.ToUInt32(StrArr[0]) - StartBit + 1;
            }
            else
            {
                StartBit = Convert.ToUInt32(Str);
                BitWidth = 1;
            }
            if (Type == 0)
                return StartBit;
            else
                return BitWidth;
        }
       
        public static List<string[][]> ReadFromExcelFile(string filePath,out string[] SheetName,int ColLimit)
        {
            IWorkbook wk = null;
            string value;
            string extension = System.IO.Path.GetExtension(filePath);
            List<string[][]> OutList = new List<string[][]>();
            Debug.AppendMsg("Open Excel File" + Environment.NewLine);
            FileStream fs = File.OpenRead(filePath);
            if (extension.Equals(".xls"))
            {
                //把xls文件中的数据写入wk中
                wk = new HSSFWorkbook(fs);
            }
            else
            {
                //把xlsx文件中的数据写入wk中
                wk = new XSSFWorkbook(fs);
            }

            fs.Close();
            SheetName = new string[wk.NumberOfSheets];
            Debug.AppendMsg("Deal With Excel File" + Environment.NewLine);
            //读取当前表数据
            for (int k = 0; k < wk.NumberOfSheets; k++)
            {
                Debug.AppendMsg(k + "/" + wk.NumberOfSheets + Environment.NewLine);
                ISheet sheet = wk.GetSheetAt(k);
                SheetName[k] = sheet.SheetName;
                XSSFFormulaEvaluator evalor = new XSSFFormulaEvaluator(wk);
                sheet.ForceFormulaRecalculation = true;
                IRow row = sheet.GetRow(0);  //读取当前行数据
                string[][] RowStr = new string[sheet.LastRowNum+1][];
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    
                    row = sheet.GetRow(i);  //读取当前行数据
                    if (row != null && row.LastCellNum>0 && row.LastCellNum<1000)
                    {
                        RowStr[i] = new string[row.LastCellNum];
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            //读取该行的第j列数据
                            //string value =
                            if (row.GetCell(j) != null)
                                value = GetCellValue(evalor, row.GetCell(j));
                            else
                                value = "";
                            value = value.Replace("\n", " ");
                            value = value.Replace(",", " ");
                            RowStr[i][j] = value;
                        }
                    }
                }
                OutList.Add(RowStr);
            }
            return OutList;
        }

        public static bool IsNumeric(string value)
        {
            if (value == "" || value == null)
                return false;
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        //如果是公式Cell 
                        //则仅读取其Cell单元格的显示值 而不是读取公式
                        if (row.GetCell(j).CellType == CellType.Formula)
                        {
                            row.GetCell(j).SetCellType(CellType.String);
                            dataRow[j] = row.GetCell(j).StringCellValue;
                        }
                        else
                        {
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
