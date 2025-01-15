using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;
using Yolov5Net.Scorer.Models.Abstract;
using static System.Formats.Asn1.AsnWriter;
namespace Yolov5App
{
    public partial class MainForm : Form
    {
        DataUI dataUI;
        public bool log_to_file_flag = false;
        public string log_file = "log";
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        //private delegate void ThreadWorkStrColor(string Msg, color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public string BASE_MODELS_Alpsentek = @".\Models\Alpsentek";
        public string BASE_MODELS= @".\Models";

        public Thread m_thread_test;
        public bool ModelEnableV5 = false;
        public bool ModelEnableV8 = false;
        public bool ModelEnableV9 = false;
        public bool ModelEnableV10 = false;
        public bool ModelEnableV11 = false;
        public bool ModelEnableAlpsentek = false;
        public bool SubModeEnable = false;
        public double test_num_ratio = 100;

        public string OUTPUT_FOLDER = System.AppDomain.CurrentDomain.BaseDirectory + "\\YoloDotNet_Results";
        public MainForm()
        {
            InitializeComponent();
        }

        public void RunAlpsentekYolo(string ImageDir, string model_file)
        {

            string Dir = ImageDir;
            string SaveDir;
            if (!Directory.Exists(ImageDir))
            {
                AppendMsg("目录不存在" + Environment.NewLine);
                return;
            }

            AppendMsg("------------------------------------------------" + Environment.NewLine);
            DirectoryInfo directoryInfo = new DirectoryInfo(Dir);
            FileInfo[] files = directoryInfo.GetFiles();
            CreateOutputFolder(OUTPUT_FOLDER);


            if (files.Length>0)
            {
                var scorer = new YoloScorer<YoloCocoPxModel>(model_file);

                var font = new SixLabors.Fonts.Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);
                SaveDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+ System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(files[0].FullName));
                CreateOutputFolder(SaveDir);
                WilfFile.DelFile(SaveDir, "*");

                for (int i = 0; i<(int)(files.Length*(test_num_ratio/100.0)); i++)
                {
                    double ratio = (double)(i*100)/files.Length;                  
                    AppendLine(System.IO.Path.GetFileName(ImageDir) +" ratio:"+ratio.ToString("F2")+"% "+i+"/"+files.Length+")");
                    var image = SixLabors.ImageSharp.Image.Load<Rgba32>(files[i].FullName);
                    var predictions = scorer.Predict(image);
                    foreach (var prediction in predictions) // draw predictions
                    {
                        var score = Math.Round(prediction.Score, 2);

                        var (x, y) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

                        image.Mutate(a => a.DrawPolygon(new SixLabors.ImageSharp.Drawing.Processing.Pen(prediction.Label.Color, 1),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
                        ));

                        image.Mutate(a => a.DrawText($"{prediction.Label.Name} ({score})",
                            font, prediction.Label.Color, new SixLabors.ImageSharp.PointF(x, y)));
                    }
                    image.Save(SaveDir + "\\" + System.IO.Path.GetFileName(files[i].FullName));
                    if (m_thread_test == null)
                        break;
                }
            }
        }

        public void RunYolo(string ImageDir, string model_file)
        {

            string Dir = ImageDir;
            string SaveDir;
            if (!Directory.Exists(ImageDir))
            {
                AppendMsg("目录不存在" + Environment.NewLine);
                return;
            }

            AppendMsg("------------------------------------------------" + Environment.NewLine);
            DirectoryInfo directoryInfo = new DirectoryInfo(Dir);
            FileInfo[] files = directoryInfo.GetFiles();
            CreateOutputFolder(OUTPUT_FOLDER);


            if (files.Length>0)
            {
                var scorer = new YoloScorer<YoloCocoP5Model>(model_file);

                var font = new SixLabors.Fonts.Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);
                SaveDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+ System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(files[0].FullName));
                CreateOutputFolder(SaveDir);
                WilfFile.DelFile(SaveDir, "*");

                for (int i = 0; i<(int)(files.Length*(test_num_ratio/100.0)); i++)
                {
                    double ratio = (double)(i*100)/files.Length;
                    AppendLine(System.IO.Path.GetFileName(ImageDir) +" ratio:"+ratio.ToString("F2")+"% "+i+"/"+files.Length+")");
                    var image = SixLabors.ImageSharp.Image.Load<Rgba32>(files[i].FullName);
                    var predictions = scorer.Predict(image);
                    foreach (var prediction in predictions) // draw predictions
                    {
                        var score = Math.Round(prediction.Score, 2);

                        var (x, y) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

                        image.Mutate(a => a.DrawPolygon(new SixLabors.ImageSharp.Drawing.Processing.Pen(prediction.Label.Color, 1),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
                            new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
                        ));

                        image.Mutate(a => a.DrawText($"{prediction.Label.Name} ({score})",
                            font, prediction.Label.Color, new SixLabors.ImageSharp.PointF(x, y)));
                    }
                    image.Save(SaveDir + "\\" + System.IO.Path.GetFileName(files[i].FullName));
                    if (m_thread_test == null)
                        break;
                }
            }
        }
        public void CreateOutputFolder(string outputFolder)
        {
            if (Directory.Exists(outputFolder) is false)
                Directory.CreateDirectory(outputFolder);
        }

        public string GetTestModelVx()
        {
            
            if (Directory.Exists(BASE_MODELS_Alpsentek))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(BASE_MODELS_Alpsentek);
                FileInfo[] files = directoryInfo.GetFiles();
                if (files.Length>0)
                {
                    return files[0].FullName;
                }
            }
            return "";
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            string PathIn = InPath.Text;
            ModelEnableAlpsentek = AlpentekCB.Checked;
            ModelEnableV5 = YoloCB.Checked;
            SubModeEnable = SubModeCB.Checked;
            test_num_ratio = Convert.ToDouble(TestRatioBox.Text);

            if (m_thread_test == null)
            {
                AppendMsg("");
                dataUI.Save();
                //SubModeEnable = SubModeCB.Checked;
                Thread.Sleep(100);
                m_thread_test = new Thread(new ParameterizedThreadStart(YoloTest));
                m_thread_test.Start(PathIn);
                AppendMsg("Thread Is  Running" + Environment.NewLine);
                StartBtn.Text = "Stop";
            }
            else
            {
                m_thread_test = null;
                Thread.Sleep(100);
                StartBtn.Text = "Start";

            }
        }

        private void YoloTest(object DataBase)
        {
            string model_file;
            string[] dir = new string[] { ((string)DataBase) };
            if (SubModeEnable == true)
                dir = WilfFile.GetDir((string)DataBase, false);

            if (ModelEnableAlpsentek == true)
            {
                AppendMsg("************************************************" + Environment.NewLine);
                AppendMsg("Alpsentek:" + Environment.NewLine);
                model_file = GetTestModelVx();
                for (int i = 0; i < dir.Length; i++)
                {
                    AppendMsg("global ratio:"+i+"/"+dir.Length + Environment.NewLine);
                    RunAlpsentekYolo(dir[i], model_file);
                }
                AppendMsg("===============================================" + Environment.NewLine);
            }
            if (ModelEnableV5 == true)
            {
                AppendMsg("************************************************" + Environment.NewLine);
                AppendMsg("Yolo:" + Environment.NewLine);
                model_file = BASE_MODELS+"\\yolov5s.onnx";
                for (int i = 0; i < dir.Length; i++)
                {
                    AppendMsg("global ratio:"+i+"/"+dir.Length + Environment.NewLine);
                    RunYolo(dir[i], model_file);
                }
                AppendMsg("===============================================" + Environment.NewLine);
            }

           
           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe",Application.StartupPath);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataUI = new DataUI(this);
            dataUI.LoadData();
        }

        //public void AppendMsgColor(string Msg, Color color)
        //{
        //    if (this.MsgBox.InvokeRequired)
        //    {
        //        ThreadWorkStrColor fc = new ThreadWorkStrColor(AppendMsgColor);
        //        this.Invoke(fc, new object[2] { Msg, color });
        //    }
        //    else
        //    {
        //        if (Msg == "")
        //            this.MsgBox.Clear();
        //        else
        //        {
        //            this.MsgBox.Select(this.MsgBox.Text.Length, 0);
        //            //this.MsgBox.Focus();
        //            MsgBox.SelectionColor = color;
        //            MsgBox.AppendText(Msg);
        //        }

        //        this.MsgBox.Refresh();
        //        this.MsgBox.SelectionStart = MsgBox.Text.Length;
        //        this.MsgBox.ScrollToCaret();
        //    }
        //}


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
                    //WilfFile.WriteAppend(log_file, Msg);
                }
            }
        }

        public void AppendLine(string Msg)
        {
            if (this.MsgBox.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(AppendLine);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {
                if (log_to_file_flag == false)
                {
                    if (Msg == "")
                        this.MsgBox.Clear();
                    else
                        this.MsgBox.AppendText(Msg+Environment.NewLine);

                    this.MsgBox.Refresh();
                    this.MsgBox.SelectionStart = MsgBox.Text.Length;
                    this.MsgBox.ScrollToCaret();
                    if (this.MsgBox.TextLength >100000)
                        MsgBox.Clear();
                }
                else
                {
                    //WilfFile.WriteAppend(log_file, Msg);
                }
            }
        }
    }

}
