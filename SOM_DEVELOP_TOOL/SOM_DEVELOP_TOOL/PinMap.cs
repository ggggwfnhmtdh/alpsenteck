using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.Formula.Functions;
using System.Xml.Linq;
using System.Collections;

namespace SOM_DEVELOP_TOOL
{
    public  class PinMap
    {
        public static bool IsMatch(string Name,string Obj)
        {
            if (Name == Obj)
                return true;
            else if (Obj.Contains("/"))
            {
                string[] strings = Obj.Split('/');
                for(int i=0;i<strings.Length;i++) 
                {
                    if (strings[i] == Name)
                    { return true;}
                }
            }
            return false;
        }

        public static string[] GetSignalPin(string SignalName, string[][] database)
        {
            int row = database.Length;
            int col;
            Hashtable ht = new Hashtable();
            List<string> list = new List<string>();
            int k = 0;
            for (int i = 0; i<row; i++)
            {
                col = database[i].Length;
                for (int j = 0; j<col; j++)
                {
                    if (database[i][j].Contains(SignalName))
                    {
                        string[] temps = database[i][j].Split('/');
                        for (int m = 0; m<temps.Length; m++)
                        {
                            if (ht.ContainsKey(temps[m]) == false)
                            {
                                ht.Add(temps[m], k++);
                                list.Add(temps[m]);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public static string[,] LoadFpgaPinMap(string FileName)
        {
            string[] lines;
            string[,] OutStr;
            string[] ColStr;
            int Row, Col;
            if (File.Exists(FileName) == false)
                return null;
            lines = File.ReadAllLines(FileName);
            ColStr = lines[0].Split(',');
            OutStr = new string[lines.Length, ColStr.Length];
            for(Row=0;Row<OutStr.GetLength(0);Row++)
            {
                lines[Row] = WilfDataPro.RemoiveMulSapce(lines[Row]);
                ColStr = lines[Row].Split(',');
                for (Col=0; Col<OutStr.GetLength(1); Col++)
                {
                    OutStr[Row, Col] = ColStr[Col];
                }
            }
            return OutStr;
        }

        //public List<string> ReadSsv(string FileName)
        //{
        //    var allines = new List<string>();   
        //    using (var reader = new StreamReader(FileName))
        //    {
        //        using (var csv = new CsvReader()
        //        {
        //            while(csv.Read())
        //            {
        //                allines.Add(csv.Context.Record);
        //            }
        //        }
        //    }
        //    return allines;
        //}

        public static void Parse(string pin_file, string[] find_name, int[] ColIndex, string[,] Fpag_pin_map)
        {
            string[] all_line;
            if (File.Exists(pin_file) == false)
                return ;
            all_line= File.ReadAllLines(pin_file);
            List<int[]>[] MatchInf = new List<int[]>[find_name.Length];
            string[][] database = new string[all_line.Length][];
            for (int i = 0; i<MatchInf.Length; i++)
            {
                MatchInf[i] = new List<int[]>(); 
            }
            for (int i=0;i<all_line.Length;i++)
            {
                string cur_line = WilfDataPro.RemoiveMulSapce(all_line[i],false);
                string[] element = cur_line.Split(',');
                database[i] = new string[element.Length];
                for (int index = 0; index<find_name.Length; index++)
                {
                    for (int j = 0; j<element.Length; j++)
                    {
                        database[i][j] = element[j];
                        if (IsMatch(find_name[index], element[j]))
                        {
                            MatchInf[index].Add(new int[2] { i,j});
                            break;
                        }

                    }
                }
            }
            StringBuilder msg = new StringBuilder();
            msg.Append("SignalName"+",");
            msg.Append("PinName"+",");
            for (int index = 0; index<ColIndex.Length; index++)
            {
                //if(index == ColIndex.Length-1)
                //    msg.Append(database[0][ColIndex[index]]+Environment.NewLine);
                //else
                    msg.Append(database[0][ColIndex[index]]+",");
            }
            msg.Append("AF"+",");
            msg.Append("FPGA_PIN"+Environment.NewLine);
            for (int i=0;i<MatchInf.Length;i++) 
            {
                
                for (int j = 0; j<MatchInf[i].Count; j++)
                {
                    msg.Append(find_name[i]+",");
                    int[] var = MatchInf[i][j];
                    int Row = var[0];
                    int Col = var[1];
                    msg.Append(database[Row][Col]+",");
                    for (int index=0;index<ColIndex.Length; index++)
                    {                       
                        msg.Append(database[Row][ColIndex[index]]+",");
                    }
                    msg.Append(database[0][Col]+",");
                    msg.Append(Fpag_pin_map[i,1]);
                    msg.Append(Environment.NewLine);
                }
                
            }
            

            string save_file = WilfFile.FileNameInjectStr(pin_file, "_result");
            File.WriteAllText(save_file, msg.ToString());

            string[] str = GetSignalPin("FMC_", database);
            msg.Clear();
            for (int i=0;i<str.Length;i++) 
            {
                msg.Append((string)str[i]+Environment.NewLine);
            }
            save_file = WilfFile.FileNameInjectStr(pin_file, "_fmc");
            File.WriteAllText(save_file, msg.ToString());
        }
    }
}
