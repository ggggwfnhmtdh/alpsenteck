using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Yolov5App.WilfClass;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;
using Yolov5Net.Scorer.Models.Abstract;
using static System.Formats.Asn1.AsnWriter;
//using static System.Net.WebRequestMethods;
namespace Yolov5App
{
    public partial class MainForm : Form
    {
        public static string DocDir = Application.StartupPath + "DOC";
        DataUI dataUI;
        public bool log_to_file_flag = false;
        public string log_file = "log";
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        //private delegate void ThreadWorkStrColor(string Msg, color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public string BASE_MODELS_Alpsentek = @".\Models\Alpsentek";
        public string BASE_MODELS = @".\Models";

        public Thread m_thread_test;
        public bool ModelEnableV5 = false;
        public bool ModelEnableV8 = false;
        public bool ModelEnableV9 = false;
        public bool ModelEnableV10 = false;
        public bool ModelEnableV11 = false;
        public bool ModelEnableAlpsentek = false;
        public bool SubModeEnable = false;
        public double test_num_ratio = 100;
        public int max_num = int.MaxValue;

        public string OUTPUT_FOLDER = Application.StartupPath + "YoloDotNet_Results";
        public MainForm()
        {
            InitializeComponent();
        }

        public void RunAlpsentekYolo(string ImageDir, string model_file)
        {

            string Dir = ImageDir;
            string SaveDir;
            string SaveRsultDir;
            int image_num;
            StringBuilder sb = new StringBuilder();
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
                SaveRsultDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+"Result";
                SaveDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+ System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(files[0].FullName));
                CreateOutputFolder(SaveRsultDir);
                CreateOutputFolder(SaveDir);
                WilfFile.DelFile(SaveDir, "*");
                sb.Append("file_name,"+"label_name,"+"id,"+"score,"+"x,"+"y,"+"w,"+"h,"+Environment.NewLine);
                image_num =(int)(files.Length*(test_num_ratio/100.0));
                image_num = Math.Min(max_num, image_num);
                for (int i = 0; i<image_num; i++)
                {
                    double ratio = (double)(i*100)/files.Length;
                    AppendLine(System.IO.Path.GetFileName(ImageDir) +" ratio:"+ratio.ToString("F2")+"% "+i+"/"+files.Length+")");
                    var image = SixLabors.ImageSharp.Image.Load<Rgba32>(files[i].FullName);
                    var predictions = scorer.Predict(image);
                    if (predictions.Count>0)
                    {
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

                            sb.Append(System.IO.Path.GetFileName(files[i].FullName)+",");
                            sb.Append(prediction.Label.Name+","+prediction.Label.Id+",");
                            sb.Append(prediction.Score +","+prediction.Rectangle.X+","+prediction.Rectangle.Y+","+prediction.Rectangle.Width+","+prediction.Rectangle.Height+Environment.NewLine);

                        }
                    }
                    else
                    {
                        sb.Append(System.IO.Path.GetFileName(files[i].FullName)+",");
                        sb.Append("NaN"+","+"-1"+",");
                        sb.Append("0" +","+"NaN"+","+"NaN"+","+"NaN"+","+"NaN"+Environment.NewLine);
                    }

                    image.Save(SaveDir + "\\" + System.IO.Path.GetFileName(files[i].FullName));
                    if (m_thread_test == null)
                        break;
                }
                File.WriteAllText(SaveRsultDir+"\\"+System.IO.Path.GetFileName(SaveDir) +".csv", sb.ToString());
            }
        }

        public Image<Rgba32> DrawBox(string im_file, int x, int y, int w, int h, string label_anme = "", int id = 0)
        {
            var font = new SixLabors.Fonts.Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);
            SixLabors.ImageSharp.RectangleF rect = new SixLabors.ImageSharp.RectangleF((float)x, (float)y, (float)w, (float)h);
            YoloLabel label = new YoloLabel(id, label_anme);
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(im_file);
            if (x<0||y<0)
                return image;
            YoloPrediction prediction = new YoloPrediction(label, 0, rect);

            var (x0, y0) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

            image.Mutate(a => a.DrawPolygon(new SixLabors.ImageSharp.Drawing.Processing.Pen(prediction.Label.Color, 1),
                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
            ));

            image.Mutate(a => a.DrawText($"{prediction.Label.Name} ",
                font, prediction.Label.Color, new SixLabors.ImageSharp.PointF(x0, y0)));
            return image;
        }



        public void RunYolo(string ImageDir, string model_file)
        {

            string Dir = ImageDir;
            string SaveDir;
            string SaveRsultDir;
            int image_num;
            StringBuilder sb = new StringBuilder();
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
                SaveRsultDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+"Result";
                SaveDir = OUTPUT_FOLDER +"\\"+ System.IO.Path.GetFileNameWithoutExtension(model_file)+"\\"+ System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(files[0].FullName));
                CreateOutputFolder(SaveRsultDir);
                CreateOutputFolder(SaveDir);
                WilfFile.DelFile(SaveDir, "*");
                sb.Append("file_name,"+"label_name,"+"id,"+"score,"+"x,"+"y,"+"w,"+"h,"+Environment.NewLine);
                image_num =(int)(files.Length*(test_num_ratio/100.0));
                image_num = Math.Min(max_num, image_num);
                for (int i = 0; i<image_num; i++)
                {
                    double ratio = (double)(i*100)/files.Length;
                    AppendLine(System.IO.Path.GetFileName(ImageDir) +" ratio:"+ratio.ToString("F2")+"% "+i+"/"+files.Length+")");
                    var image = SixLabors.ImageSharp.Image.Load<Rgba32>(files[i].FullName);
                    var predictions = scorer.Predict(image);
                    if (predictions.Count>0)
                    {
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

                            sb.Append(System.IO.Path.GetFileName(files[i].FullName)+",");
                            sb.Append(prediction.Label.Name+","+prediction.Label.Id+",");
                            sb.Append(prediction.Score +","+prediction.Rectangle.X+","+prediction.Rectangle.Y+","+prediction.Rectangle.Width+","+prediction.Rectangle.Height+Environment.NewLine);

                        }
                    }
                    else
                    {
                        sb.Append(System.IO.Path.GetFileName(files[i].FullName)+",");
                        sb.Append("NaN"+","+"-1"+",");
                        sb.Append("0" +","+"NaN"+","+"NaN"+","+"NaN"+","+"NaN"+Environment.NewLine);
                    }

                    image.Save(SaveDir + "\\" + System.IO.Path.GetFileName(files[i].FullName));
                    if (m_thread_test == null)
                        break;
                }
                File.WriteAllText(SaveRsultDir+"\\"+System.IO.Path.GetFileName(SaveDir) +".csv", sb.ToString());
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
            max_num = Convert.ToInt32(MaxNumBox.Text);
            test_num_ratio = Convert.ToDouble(TestRatioBox.Text);
            if (max_num<0)
                max_num = int.MaxValue;
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
                if (!File.Exists(model_file))
                {
                    AppendLine("未找到模型文件");
                    return;
                }
                for (int i = 0; i < dir.Length; i++)
                {
                    AppendMsg("global ratio:"+i+"/"+dir.Length + Environment.NewLine);
                    RunAlpsentekYolo(dir[i], model_file);
                    if (m_thread_test == null)
                        break;
                }
                AppendMsg("===============================================" + Environment.NewLine);
            }
            if (ModelEnableV5 == true)
            {
                AppendMsg("************************************************" + Environment.NewLine);
                AppendMsg("Yolo:" + Environment.NewLine);
                model_file = BASE_MODELS+"\\yolov5s.onnx";
                if (!File.Exists(model_file))
                {
                    AppendLine("未找到模型文件");
                    return;
                }
                for (int i = 0; i < dir.Length; i++)
                {
                    AppendMsg("global ratio:"+i+"/"+dir.Length + Environment.NewLine);
                    RunYolo(dir[i], model_file);
                    if (m_thread_test == null)
                        break;
                }
                AppendMsg("===============================================" + Environment.NewLine);
            }



        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataUI = new DataUI(this);
            dataUI.LoadData();
            WilfFile.CreateDirectory(DocDir);
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

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = WilfFile.OpenFile(".jpg");
            Image<Rgba32> im = DrawBox(file, 100, 50, 10, 10, "wwf", 1);
            im.Save("123.jpg");
        }


        private void drawPointUAV123ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] file_list;
            string[] dir = new string[] { ((string)InPath.Text) };
            string anno_base_dir = CfgBox.Text;
            string anno_file;
            string BaseDir = Application.StartupPath+ "DrawPicture";
            string SaveDir;
            Image<Rgba32> im;
            int image_num;
            max_num = Convert.ToInt32(MaxNumBox.Text);
            test_num_ratio = Convert.ToDouble(TestRatioBox.Text);

            SubModeEnable = SubModeCB.Checked;
            if (SubModeEnable == true)
                dir = WilfFile.GetDir(InPath.Text, false);
            if (!Directory.Exists(anno_base_dir))
            {
                AppendMsg("未找到anno路径"+Environment.NewLine);
                return;
            }
            WilfFile.CreateDirectory(BaseDir);
            for (int i = 0; i < dir.Length; i++)
            {
                file_list = WilfFile.GetFile(dir[i], ".jpg", false);
                anno_file = anno_base_dir + "\\" + System.IO.Path.GetFileName(dir[i])+".txt";
                if (file_list.Length>0)
                {
                    SaveDir = BaseDir+"\\"+System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file_list[0]));
                    WilfFile.CreateDirectory(SaveDir);
                    if (!File.Exists(anno_file))
                    {
                        AppendMsg("[Error] anno file :"+anno_file+Environment.NewLine);
                        continue;
                    }
                    List<int[]> corr_xy = WilfDataPro.GetIntDataFromFile(anno_file);

                    image_num =(int)(file_list.Length*(test_num_ratio/100.0));
                    image_num = Math.Min(max_num, image_num);

                    for (int j = 0; j < image_num; j++)
                    {
                        im = DrawBox(file_list[j], corr_xy[j][0], corr_xy[j][1], corr_xy[j][2], corr_xy[j][3]);
                        im.Save(SaveDir + "\\" + System.IO.Path.GetFileName(file_list[j]));
                        AppendLine(System.IO.Path.GetFileName(dir[i]) +":"+ j+"/"+file_list.Length+" "+i+"/"+dir.Length);
                    }
                }

            }
        }

        private void openYoloTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", OUTPUT_FOLDER);
        }

        private void uAVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UAV123 uAV123 = new UAV123();
            dataUI.Save();
            uAV123.GetResult(InPath.Text);
            uAV123.GetAnno(CfgBox.Text);
            uAV123.CalcResult();
           
            
        }
    }

}
