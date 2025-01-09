using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
namespace SOM_DEVELOP_TOOL
{
    public static class Exe
    {
        public static string OutDir = Application.StartupPath + "\\ExeData";

        public static string PathAddQuotes(string InStr)
        {
            return "\"" + InStr + "\"";
        }

        public static string SignFw(string ToolDir,string FwBin)
        {
            string key_file = ToolDir+"\\key.txt";
            string file_out_name="";
            string Exe= ToolDir + "\\IS_GenerateBin.exe";
            string Param = "-f YYYY -key XXXX -sign -start 0x00001000 -bypass -o ZZZZ";
            if (!File.Exists(Exe))
            {
                MessageBox.Show("Please put IS_GenerateBin.exe file in "+ToolDir);
                return "";
            }
            if (!File.Exists(key_file))
            {
                MessageBox.Show("Please put key.txt file in "+ToolDir);
                return "";
            }
            file_out_name = WilfFile.FileNameInjectStr(FwBin,"_add_header_and_signed_");
            Param = Param.Replace("XXXX", PathAddQuotes(key_file));
            Param = Param.Replace("YYYY", PathAddQuotes(FwBin));
            Param = Param.Replace("ZZZZ", PathAddQuotes(file_out_name));
            if (File.Exists(file_out_name))
            {
                File.Delete(file_out_name);
            }
            Run(PathAddQuotes(Exe), Param,false);
            Thread.Sleep(200);
            if (File.Exists(file_out_name))
            {
                return file_out_name;
            }
            else
            {
                return null;
            }
        }
        public static string ArmDumpAsm(string formelf_file,string axf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(axf_file) + ".bat";
            
            string OutputFile = Path.GetDirectoryName(axf_file)  + "\\" + Path.GetFileNameWithoutExtension(axf_file) + ".asm";
            string Param = "--text -c -o  XXXX YYYY";
            Param = Param.Replace("XXXX", PathAddQuotes(OutputFile));
            Param = Param.Replace("YYYY", PathAddQuotes(axf_file));
            File.WriteAllText(SrciptFile, formelf_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(formelf_file) == false || File.Exists(axf_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            Run(PathAddQuotes(formelf_file), Param);
            return OutputFile;
        }

        public static string ArmDumpBin(string formelf_file, string axf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(axf_file) + ".bat";
            
            string OutputFile = Path.GetDirectoryName(axf_file)  + "\\" + Path.GetFileNameWithoutExtension(axf_file) + ".bin";
            string Param = "--bin -o  XXXX YYYY";
            Param = Param.Replace("XXXX", PathAddQuotes(OutputFile));
            Param = Param.Replace("YYYY", PathAddQuotes(axf_file));
            File.WriteAllText(SrciptFile, formelf_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(formelf_file) == false || File.Exists(axf_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            Run(PathAddQuotes(formelf_file), Param);
            return OutputFile;
        }

        public static string ArmDumpHex(string formelf_file, string axf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(axf_file) + ".bat";
            
            string OutputFile = Path.GetDirectoryName(axf_file)  + "\\" + Path.GetFileNameWithoutExtension(axf_file) + ".hex";
            string Param = "--i32combined -o  XXXX YYYY";
            Param = Param.Replace("XXXX", PathAddQuotes(OutputFile));
            Param = Param.Replace("YYYY", PathAddQuotes(axf_file));
            File.WriteAllText(SrciptFile, formelf_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(formelf_file) == false || File.Exists(axf_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return"";
            }
            else
            Run(PathAddQuotes(formelf_file), Param);
            return OutputFile;
        }

        public static string ArmAddr2Line(string exe_file,string axf_file,UInt32 Addr)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(axf_file) + ".bat";
            
            string Param = "-a XXXX -e YYYY";
            Param = Param.Replace("XXXX", PathAddQuotes("0x" + Addr.ToString("X8")));
            Param = Param.Replace("YYYY", PathAddQuotes(axf_file));
            File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            return Run(PathAddQuotes(exe_file), Param);
        }

        public static string DspAddr2Line(string exe_file,string ConfigDir,string elf_file,UInt32 Addr)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(elf_file) + ".bat";
           
            string Param = "-a XXXX -e YYYY --xtensa-system=ZZZZ --xtensa-core=Py_210123_eval";
            Param = Param.Replace("XXXX", "0x" + Addr.ToString("X8"));
            Param = Param.Replace("YYYY", PathAddQuotes(elf_file));
            Param = Param.Replace("ZZZZ", PathAddQuotes(ConfigDir));
            File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            return Run(PathAddQuotes(exe_file), Param);
        }

        public static string DspDumpBin(string exe_file, string ConfigDir, string elf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(elf_file) + ".bat";
            
            string OutputFile = OutDir + "\\" + Path.GetFileNameWithoutExtension(elf_file) + ".bin";
            string Param = "-O binary  --xtensa-system=XXXX --xtensa-core=Py_210123_eval --xtensa-params= YYYY ZZZZ";
            Param = Param.Replace("XXXX", PathAddQuotes(ConfigDir));
            Param = Param.Replace("YYYY", PathAddQuotes(elf_file));
            Param = Param.Replace("ZZZZ", PathAddQuotes(OutputFile));
            File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            return Run(PathAddQuotes(exe_file), Param);
        }

        public static string DspDumpHex(string exe_file, string ConfigDir, string elf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(elf_file) + ".bat";
            
            string OutputFile = OutDir + "\\" + Path.GetFileNameWithoutExtension(elf_file) + ".hex";
            string Param = "-O ihex  --xtensa-system=XXXX --xtensa-core=Py_210123_eval --xtensa-params= YYYY ZZZZ";
            Param = Param.Replace("XXXX", PathAddQuotes(ConfigDir));
            Param = Param.Replace("YYYY", PathAddQuotes(elf_file));
            Param = Param.Replace("ZZZZ", PathAddQuotes(OutputFile));
            File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
                return Run(PathAddQuotes(exe_file), Param);
        }

        public static string DspDumpAllInf(string SymbolFile,string MapFile)
        {
            return null;
            //string CodeInfFile = "";
            //string[] AllLines;
            //StringBuilder sb = new StringBuilder();
            //StringBuilder sb_patch = new StringBuilder();
            //AllLines = File.ReadAllLines(SymbolFile);
            //MapParser.GetDspHash(MapFile);
            //string FileName;
            //UInt32 Addr;
            //UInt32 Size;
            //string Space;
            //CodeInfFile = Path.GetDirectoryName(SymbolFile) + "\\CodeInf.csv";
            //sb.Append("Function/Var,Address,Size(byte),Type,Space,FileName" + Environment.NewLine);
            //Hashtable ht = new Hashtable();
            //for (int i = 0; i < AllLines.Length; i++)
            //{
            //    string[] Item = AllLines[i].Split(' ');
            //    if (MapParser.DspNameToFileHash.ContainsKey(Item[2]))
            //        FileName = Path.GetFileNameWithoutExtension(MapParser.DspNameToFileHash[Item[2]].ToString());
            //    else
            //        FileName = "";
            //    Size = Convert.ToUInt32(Item[0],16);
            //    if (MapParser.DspNameHash.ContainsKey(Item[2]))
            //        Addr = (UInt32)MapParser.DspNameHash[Item[2]];
            //    else
            //        Addr = 0;
                
            //    if (Addr > 0 && FileName != ""&& Size>0)
            //    {                         
            //        if ((Addr >= 0x50000000 && Addr < 0x50000000 + 0x10000) ||(Addr >= 0x50040000 && Addr < 0x50040000+ 0x8000) || (Addr >= 0x500a0000 && Addr < 0x500a0000 + 0x00004200))
            //        {
            //            Space = "ROM";
            //            if (FileName.Contains("_patch"))
            //            {
            //                Space = "Patch";                        
            //                Debug.AppendMsg("Error:" + Item[2] + " is not in Patch Space" + Environment.NewLine);                          
            //            }
            //        }
            //        else
            //        {

            //            Space = "RAM";
            //        }
            //        string Msg = Item[2] + ",0x" + Addr.ToString("X8") + "," + Size.ToString() + "," + Item[1] + "," + Space + "," + FileName + ".c";
            //        ht.Add(Item[2], Msg);
            //        sb.Append(Msg + Environment.NewLine);
            //    }
            //}
            //sb_patch.Append(Environment.NewLine);
            //sb_patch.Append("Function/Var In Patch,Address,Size(byte),Type,Space,FileName" + "," + "Function/Var In ROM,Address,Size(byte),Type,Space,FileName" + Environment.NewLine);

            //foreach (var temp in ht.Keys)
            //{
            //    if(ht.ContainsKey(temp.ToString()+"_patch"))
            //    {
            //        sb_patch.Append(ht[temp + "_patch"] + "," + ht[temp] + Environment.NewLine);
            //    }
            //}
            //File.WriteAllText(CodeInfFile,sb.ToString());
            
            //WilfFile.WriteAppend(CodeInfFile, sb_patch.ToString());
            //return CodeInfFile;
        }

        public static string DspDumpSymbol(string exe_file, string ConfigDir, string elf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(elf_file) + ".bat";
            string OutputFile = OutDir + "\\" + Path.GetFileNameWithoutExtension(elf_file) + ".Symbol";
            string Param = "--size-sort YYYY --xtensa-core=Py_210123_eval  >ZZZZ";
            Param = Param.Replace("YYYY", PathAddQuotes(elf_file));
            Param = Param.Replace("ZZZZ", PathAddQuotes(OutputFile));
            File.WriteAllText(SrciptFile, exe_file + " " + Param);
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return OutputFile;
            }
            else
            {
                RunBat(SrciptFile);
                File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
                return OutputFile;
            }
        }

        public static string DspDumpAsm(string exe_file, string ConfigDir, string elf_file)
        {
            StackTrace st = new StackTrace(new StackFrame(true));
            StackFrame sf = st.GetFrame(0);
            string SrciptFile = OutDir + "\\" + sf.GetMethod().Name + "_" + Path.GetFileNameWithoutExtension(elf_file) + ".bat";
            string OutputFile = OutDir + "\\" + Path.GetFileNameWithoutExtension(elf_file) + ".asm";
            string Param = "-d  --xtensa-system=XXXX --xtensa-core=Py_210123_eval --xtensa-params= YYYY >ZZZZ";
            Param = Param.Replace("XXXX", PathAddQuotes(ConfigDir));
            Param = Param.Replace("YYYY", PathAddQuotes(elf_file));
            Param = Param.Replace("ZZZZ", PathAddQuotes(OutputFile));
            File.WriteAllText(SrciptFile, exe_file +" "+ Param);
            if (File.Exists(exe_file) == false)
            {
                Debug.AppendMsg("Error: don't find file" + Environment.NewLine);
                return "";
            }
            else
            {
                RunBat(SrciptFile);
                File.WriteAllText(SrciptFile, exe_file + " " + Param + Environment.NewLine + "pause");
                return "";
            }
        }


        public static void RunBat(string BatFile)
        {
            Process proc = new Process();
            string targetDir = string.Format(Path.GetDirectoryName(BatFile));

            proc.StartInfo.WorkingDirectory = targetDir;
            proc.StartInfo.FileName = BatFile;
            proc.StartInfo.Arguments = string.Format("10");
            proc.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            proc.StartInfo.CreateNoWindow = true;

            proc.Start();
            proc.WaitForExit();
        }
        public static string Run(string exe_file,string ParmStr,bool Wait=true)
        {
            object output = null;
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = Exe.PathAddQuotes(exe_file);//可执行程序路径
                    p.StartInfo.Arguments = ParmStr;//参数以空格分隔，如果某个参数为空，可以传入""
                    p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                    p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                    p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                    p.Start();
                    if (Wait == true)
                    {
                        p.WaitForExit();
                        //正常运行结束放回代码为0
                        if (p.ExitCode != 0)
                        {
                            output = p.StandardError.ReadToEnd();
                            output = output.ToString().Replace(System.Environment.NewLine, string.Empty);
                            output = output.ToString().Replace("\n", string.Empty);
                            throw new Exception(output.ToString());
                        }
                        else
                        {
                            output = p.StandardOutput.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Debug.AppendMsg(ee.Message);
            }
            return (string)output;
        }
    }
}
