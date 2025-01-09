using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing.Drawing2D;
using UAV_TOOL.WilfClass;

namespace UAV_TOOL
{
    public partial class MainForm : Form
    {
        public bool log_to_file_flag = false;
        public string log_file = "log";
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        private delegate void ThreadWorkStrColor(string Msg, Color color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);

        public Thread m_thread_test;
        public string PathIn;
        public string ImageType;
        public string VideoType;
        public double ScaleRatio = 1;
        public int ParamFPS;
        public bool SubDirEnable = false;
        public bool FpsExternal = false;
        public Hashtable m_fps_table = new Hashtable();
        DataUI dataUI;
        public MainForm()
        {
            InitializeComponent();
        }

        private Hashtable LoadFps()
        {
            string[] strs,temp_str;
            string file = "FPS.cmd";
            Hashtable ht = new Hashtable();
            if (File.Exists(file))
            {
                strs = File.ReadAllLines(file);
                for (int i = 0; i < strs.Length; i++)
                {
                    strs[i] = strs[i].Replace(" ", "");
                    temp_str = strs[i].Split(',');
                    if (temp_str.Length<2)
                        continue;
                    temp_str[0] = temp_str[0].ToLower();    
                    if (ht.Contains(temp_str[0]) == false)
                    {
                        ht.Add(temp_str[0], Convert.ToInt32(temp_str[1]));
                    }

                }
            }
            return ht;
        }
        private void StartBtn_Click(object sender, EventArgs e)
        {
            PathIn = PathBox.Text;
            ImageType = FormatImageBox.Text;
            VideoType = FormatVideoBox.Text;
            ParamFPS =   (int)WilfDataPro.CalcExpression(FpsBox.Text);
            ScaleRatio = WilfDataPro.CalcExpression(ScaleBox.Text);
            SubDirEnable = SubDirCB.Checked;
            FpsExternal = FpsExternalCB.Checked;
            if (FpsExternal)
                m_fps_table = LoadFps();
            if (m_thread_test == null)
            {
                AppendMsg("");
                dataUI.Save();
                Thread.Sleep(100);
                m_thread_test = new Thread(new ParameterizedThreadStart(TestThread));
                m_thread_test.Start(this);
                AppendMsg("Thread Is Already Running" + Environment.NewLine);
                StartBtn.Text = "Stop";
            }
            else
            {
                m_thread_test = null;
                Thread.Sleep(100);
                StartBtn.Text = "Start";

            }

        }
        public int GetTrueFPS(int FPS,string DataName)
        {
            
            if (FpsExternal == true && m_fps_table.Count>0)
            {
                DataName = DataName.ToLower();
                if (m_fps_table.Contains(DataName))
                    return (int)m_fps_table[DataName];
            }
            return FPS;
        }
        private void TestThread(object InPam)
        {

            AppendMsg("************************************************" + Environment.NewLine);
            //AppendMsg("-----------------------------------------------" + Environment.NewLine);
            //AppendMsg("===============================================" + Environment.NewLine);
            if (SubDirEnable == false)
            {
                GenerateVideo(PathIn, ImageType, VideoType, ParamFPS, ScaleRatio, ScaleRatio);
                AppendMsg("-----------------------------------------------" + Environment.NewLine);
            }
            else
            {
                string[] DirList = WilfFile.GetDir(PathIn, false);
                for (int i =0;i<DirList.Length;i++)
                {
                    PathIn = DirList[i];
                    AppendMsg("stage:"+i+"//"+DirList.Length + Environment.NewLine);
                    GenerateVideo(PathIn, ImageType, VideoType, ParamFPS, ScaleRatio, ScaleRatio);
                    AppendMsg("-----------------------------------------------" + Environment.NewLine);
                    if (m_thread_test == null)
                        break;
                }
                
            }
            AppendMsg("===============================================" + Environment.NewLine);
            AppendMsg("Thread Is Already Closed" + Environment.NewLine);
        }

        private void GenerateVideo(string ImageFileDir,string ImageType,string VideoType, int FPS=30,double width=1,double height=1)
        {
            Image bmp;
            string[] FileList;
            string VideoName, ImageFileName;
            string SaveDir = Application.StartupPath + "\\Video";
            string DirName = Path.GetFileNameWithoutExtension(ImageFileDir);
            string ParamStr = "";
            WilfFile.CreateDirectory(SaveDir);
            FPS = GetTrueFPS(FPS, DirName);
            FileList = WilfFile.GetFile(ImageFileDir, ImageType, false);

            if (FileList.Length > 0)
            {
                bmp = WilfFile.LoadImage (FileList[0]);
                VideoName = SaveDir +"\\"+DirName+"_FPS="+FPS+"_Size="+bmp.Width+"x"+bmp.Height+ VideoType;
                ImageFileName =  SaveDir +"\\"+DirName+"_FPS="+FPS+"_Size="+bmp.Width+"x"+bmp.Height+ ".txt";
                GenerateImageFile(ImageFileName, FileList);
                ParamStr += "-r "+FPS.ToString()+" ";
                ParamStr += "-f concat ";
                ParamStr += "-safe 0 ";
                ParamStr += " -i "+ ImageFileName + " ";
                ParamStr += "-c:v libx264 ";
                ParamStr += "-pix_fmt yuv420p ";
                ParamStr += "-vf  "+AddQuotes("scale=iw*"+width.ToString()+":ih*"+height.ToString())+" ";
                ParamStr += "-y ";
                ParamStr += AddQuotes(VideoName);
                StartExternalProgram(ParamStr);   //ffmpeg -i input.mp4 -vf "scale=iw*1.5:ih*1.5" output.mp4
                bmp.Dispose();
            }
        }

        private string GenerateMergeVideoParam(string inputFiles,string outputFile)
        {
            int numVideos = inputFiles.Length;
            string ParamStr;
           if (numVideos == 2)
            {
                // 横向排列两个视频  
                ParamStr = ($"-i \"{inputFiles[0]}\" -i \"{inputFiles[1]}\" -filter_complex hstack \"{outputFile}\"");
            }
            else if (numVideos == 3)
            {
                // 垂直排列三个视频  
                ParamStr = ($"-i \"{inputFiles[0]}\" -i \"{inputFiles[1]}\" -i \"{inputFiles[2]}\" -filter_complex \"[0:v][1:v]vstack[v12];[v12][2:v]vstack\" \"{outputFile}\"");
            }
            else
            {
                /// 对于四个以上的视频，进行 2x2 的排列  
                string inputs = string.Join(" ", inputFiles.Select(f => $"-i \"{f}\""));
                string filters = "";

                // 构建 hstack 过滤器  
                for (int i = 0; i < numVideos; i++)
                {
                    if (i % 2 == 0)
                    {
                        filters += $"[{i}:v]";
                    }
                    else
                    {
                        filters += $"[{i}:v]hstack=inputs=2[v{(i / 2)}];";
                    }
                }

                // 在循环后添加未处理的视频过滤器  
                if (numVideos % 2 == 1)
                {
                    filters += $"[v{(numVideos / 2)}]";
                }

                filters += $"vstack=inputs={Math.Ceiling(numVideos / 2.0)}";

                ParamStr = ($"{inputs} -filter_complex \"{filters}\" \"{outputFile}\"");
            }
            return ParamStr;
        }
        private void MergeVideo(string[] video_files,string Align)  //ffmpeg -i video1.mp4 -i video2.mp4 -filter_complex vstack output.mp4
        {
            string VideoName="";
            string VideoFullName = "";
            string SaveDir = Application.StartupPath + "\\MergeVideo";
            string DirName;
            string ParamStr = "";
            string HV = "";
            WilfFile.CreateDirectory(SaveDir);

            
            if (video_files.Length>1)
            {

                DirName = Path.GetFileName(Path.GetDirectoryName(video_files[0]));
                SaveDir += "\\"+DirName;
                for (int i = 0; i < video_files.Length; i++)
                {
                    VideoName += Path.GetFileNameWithoutExtension(video_files[i])+"_";
                }
                VideoName = VideoName.Trim('_')+Path.GetExtension(video_files[0]); ;
                VideoFullName = SaveDir +"\\"+VideoName;
                ParamStr = ffpmeg.GenerateVideoArgs(video_files, Align, VideoFullName);

                ParamStr += " -y";
                
                WilfFile.CreateDirectory(SaveDir);
                StartExternalProgram(ParamStr);   //ffmpeg -i input.mp4 -vf "scale=iw*1.5:ih*1.5" output.mp4
            }
        }

        private void WaterMarkVideo(string[] video_files)  //ffmpeg -i video1.mp4 -i video2.mp4 -filter_complex vstack output.mp4
        {
            string SaveDir = Application.StartupPath + "\\WaterMarkVideo";
            string DirName;
            string ParamStr = "";
            string VideoName;
            WilfFile.CreateDirectory(SaveDir);

            DirName = Path.GetFileName(Path.GetDirectoryName(video_files[0]));
            SaveDir += "\\"+DirName;
            WilfFile.CreateDirectory(SaveDir);

            for (int i = 0;i < video_files.Length;i++)
            {
                VideoName = SaveDir +"\\"+Path.GetFileName(video_files[i]);
                ParamStr = ffpmeg.GenerateWatermarkCommand(video_files[i]) + " ";
                ParamStr += "-y ";
                ParamStr += AddQuotes(VideoName)+" ";
                if (File.Exists(VideoName))
                    File.Delete(VideoName);
                StartExternalProgram(ParamStr);   //ffmpeg -i input.mp4 -vf "scale=iw*1.5:ih*1.5" output.mp4
            }
        }

        private void GenerateImageFile(string ImageFilePath,string[] FileList)
        {
            StringBuilder str = new StringBuilder();
            AppendMsg(ImageFilePath+Environment.NewLine);
            foreach (string File in FileList)
            {
                str.Append("file " + AddQuotes(File,false) +Environment.NewLine);
            }
            File.WriteAllText(ImageFilePath, str.ToString());
        }

        private string AddQuotes(string InStr,bool double_q = true)
        {
            if(double_q == true)
                return "\"" + InStr + "\"";
            else          
                return "\'" + InStr + "\'";
        }
        public void StartExternalProgram(string examinerNo)
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = exePath + "\\ffmpeg.exe";
            AppendMsg(fileName +" "+ examinerNo+Environment.NewLine);
            //使用进程
            if (!File.Exists(fileName))
            {
                MessageBox.Show("The ffmpeg.exe program is missing");
                return;
            }
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.FileName = fileName;
            myProcess.StartInfo.CreateNoWindow = true;
            //传参，参数以空格分隔，如果某个参数为空，可以传入“”
            myProcess.StartInfo.Arguments = examinerNo;
            myProcess.StartInfo.WorkingDirectory = exePath;//设置要启动的进程的初始目录
            myProcess.Start();//启动
            myProcess.WaitForExit(15000);//等待exe程序处理完，超时15秒
            string xmldata = myProcess.StandardOutput.ReadToEnd();//读取exe中内存流数据
            if (!string.IsNullOrEmpty(xmldata))
            {
                
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath);
        }

        public void AppendMsgColor(string Msg, Color color)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStrColor fc = new ThreadWorkStrColor(AppendMsgColor);
                this.Invoke(fc, new object[2] { Msg, color });
            }
            else
            {
                if (Msg == "")
                    this.MsgBox.Clear();
                else
                {
                    this.MsgBox.Select(this.MsgBox.Text.Length, 0);
                    //this.MsgBox.Focus();
                    MsgBox.SelectionColor = color;
                    MsgBox.AppendText(Msg);
                }

                this.MsgBox.Refresh();
                this.MsgBox.SelectionStart = MsgBox.Text.Length;
                this.MsgBox.ScrollToCaret();
            }
        }


        public void AppendMsg(string Msg)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(AppendMsg);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {
                if (log_to_file_flag == false)
                {
                    if (Msg == "")
                        this.MsgBox.Clear();
                    else
                        this.MsgBox.AppendText(Msg);

                    this.MsgBox.Refresh();
                    this.MsgBox.SelectionStart = MsgBox.Text.Length;
                    this.MsgBox.ScrollToCaret();
                }
                else
                {
                    WilfFile.WriteAppend(log_file, Msg);
                }
            }
        }

        private void openInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PathBox.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataUI = new DataUI(this);
            dataUI.LoadData();
        }

        private void mergeVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void RunMergeVideo(string Align)
        {
            string[] files;
            AppendMsg("");
            AppendMsg("************************************************" + Environment.NewLine);
            files = WilfFile.OpenFiles(FormatVideoBox.Text);
            if (files != null && files.Length>1)
            {
                MergeVideo(files, Align);
            }
            AppendMsg("===============================================" + Environment.NewLine);
            AppendMsg("Over" + Environment.NewLine);
        }

        private void RunWaterMarkVideo()
        {
            string[] files;
            AppendMsg("");
            AppendMsg("************************************************" + Environment.NewLine);
            files = WilfFile.OpenFiles(FormatVideoBox.Text);
            if (files != null && files.Length>0)
            {
                WaterMarkVideo(files);
            }
            AppendMsg("===============================================" + Environment.NewLine);
            AppendMsg("Over" + Environment.NewLine);
        }


        private void algnVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunMergeVideo("y");
        }

        private void algnHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunMergeVideo("x");
        }

        private void autoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunMergeVideo("a");
        }

        private void waterMarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunWaterMarkVideo();
        }
    }
}
