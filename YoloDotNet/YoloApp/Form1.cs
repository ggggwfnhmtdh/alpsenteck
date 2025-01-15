using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
//using ConsoleDemo.Config;
using YoloDotNet.Extensions;
using YoloDotNet.Test.Common;
using YoloDotNet.Test.Common.Enums;
using SkiaSharp;
using System.Reflection.Emit;
using System.Collections;

namespace YoloApp
{
    public partial class MainForm : Form
    {
        DataUI dataUI;
        public bool log_to_file_flag = false;
        public string log_file = "log";
        private delegate void ThreadWorkStr(string Msg);
        private delegate void ThreadWorkTable(int Index);
        private delegate void ThreadWorkStrColor(string Msg, Color color);
        private delegate void ThreadWorkStr2(string Msg0, string Msg1);
        public static readonly string OUTPUT_FOLDER = Application.StartupPath + "YoloDotNet_Results";
        public Thread m_thread_test;
        public bool ModelEnableV5 = false;
        public bool ModelEnableV8 = false;
        public bool ModelEnableV9 = false;
        public bool ModelEnableV10 = false;
        public bool ModelEnableV11 = false;
        public bool ModelEnableAlpsentek = false;
        public bool SubModeEnable = false;


        public MainForm()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            string PathIn = PathInBox.Text;

            if (m_thread_test == null)
            {
                AppendMsg("");
                dataUI.Save();
                ModelEnableV5 = V5CB.Checked;
                ModelEnableV8 = V8CB.Checked;
                ModelEnableV9 = V9CB.Checked;
                ModelEnableV10 =V10CB.Checked;
                ModelEnableV11 =V11CB.Checked;
                ModelEnableAlpsentek =AlpsentekCB.Checked;
                SubModeEnable = SubModeCB.Checked;  
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

            CreateOutputFolder();

            //Action<ModelType, ModelVersion, ImageType, string, bool, bool> runDemoAction = RunDemoX;

            string file_name;
            string dir = (string)DataBase;
            string[] dirs = new string[1];
            if (!Directory.Exists(dir))
            {
                AppendMsg("Thread Is Already Closed" + Environment.NewLine);
                return;
            }

            if(SubModeEnable == false)
            {
                dirs[0] = dir;
            }
            else
            {
                dirs = WilfFile.GetDir(dir, false);
            }
            AppendMsg("************************************************" + Environment.NewLine);
            for (int i = 0; i<dirs.Length; i++)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dirs[i]);
                FileInfo[] files = directoryInfo.GetFiles();

                for (int j = 0; j < files.Length; j++)
                {
                    file_name = files[j].FullName;
                    if(ModelEnableV8)
                        RunModel(ModelType.ObjectDetection, ModelVersion.V8, ImageType.Street, file_name, false, false);
                    if (ModelEnableV9)
                        RunModel(ModelType.ObjectDetection, ModelVersion.V9, ImageType.Street, file_name, false, false);
                    if (ModelEnableV10)
                        RunModel(ModelType.ObjectDetection, ModelVersion.V10, ImageType.Street, file_name, false, false);
                    if (ModelEnableV11)
                        RunModel(ModelType.ObjectDetection, ModelVersion.V11, ImageType.Street, file_name, false, false);
                    if (ModelEnableAlpsentek)
                        RunModel(ModelType.ObjectDetection, ModelVersion.Vx, ImageType.Street, file_name, false, false);
                    //if (ModelEnableV5)
                    //RunModel(ModelType.ObjectDetection, ModelVersion.V8, ImageType.Street, file_name, false, false);

                    AppendLine("Dir:"+i+"/"+dirs.Length + "    File:"+j+"/"+files.Length);
                    if (m_thread_test == null)
                        break;
                }
                AppendMsg("-----------------------------------------------" + Environment.NewLine);
            }
            //ObjectDetectionOnVideo();
            DisplayOutputFolder();
            //DisplayOnnxMetaDataExample();
            AppendMsg("Thread Is Already Closed" + Environment.NewLine);
        }
        public void CreateOutputFolder()
        {
            var outputFolder = OUTPUT_FOLDER;

            if (Directory.Exists(outputFolder) is false)
                Directory.CreateDirectory(outputFolder);
        }

        public List<ObjectDetection> ParseAnnotate(string AnnotateFile)
        {
            string[] items;
            List<ObjectDetection> lst = new List<ObjectDetection>();
            string[] strs = File.ReadAllLines(AnnotateFile);
            for (int i = 0; i < strs.Length; i++)
            {
                items = strs[i].Replace(" ", "").Split(',');
                for (int j = 0; j < items.Length; j+=2)
                {
                    ObjectDetection ob = new ObjectDetection();


                }
            }
            return lst;
        }
        public void AnnotateImage(string[] ImageFile, string AnnotateFile)
        {
            string SaveDir;
            if (ImageFile.Length>0)
            {
                SaveDir = OUTPUT_FOLDER + "\\"+Path.GetFileName(Path.GetDirectoryName(ImageFile[0]));
                if (Directory.Exists(SaveDir) == false)
                    Directory.CreateDirectory(SaveDir);
                for (int i = 0; i<ImageFile.Length; i++)
                {
                    using var image = SKImage.FromEncodedData(ImageFile[i]);
                    List<ObjectDetection> result = new List<ObjectDetection>();
                    SKImage resultImage = image.Draw(result);
                    resultImage.Save(SaveDir+"\\"+Path.GetFileName(ImageFile[i]), SKEncodedImageFormat.Jpeg);
                }
            }
        }
        public void RunModel(ModelType modelType, ModelVersion modelVersion, ImageType imageType, string image_file, bool cuda = false, bool primeGpu = false)
        {
            string SaveDir;
            var modelPath = modelVersion switch
            {
                ModelVersion.V8 => SharedConfig.GetTestModelV8(modelType),
                ModelVersion.V9 => SharedConfig.GetTestModelV9(modelType),
                ModelVersion.V10 => SharedConfig.GetTestModelV10(modelType),
                ModelVersion.V11 => SharedConfig.GetTestModelV11(modelType),
                ModelVersion.Vx => SharedConfig.GetTestModelVx(modelType),
                _ => throw new ArgumentException("Unkown yolo version")
            };

            var imagePath = image_file;

            using var yolo = new Yolo(new YoloOptions()
            {
                OnnxModel = modelPath,
                Cuda = cuda,
                PrimeGpu = primeGpu,
                ModelType = modelType,
            });

            using var image = SKImage.FromEncodedData(imagePath);

            var device = cuda ? "GPU" : "CPU";
            device += device == "CPU" ? "" : primeGpu ? ", primed: yes" : ", primed: no";
            AppendLine($"{yolo.OnnxModel.ModelType,-16} {modelVersion,-5}device: {device}");

            SKImage resultImage = SKImage.Create(new SKImageInfo());
            List<LabelModel> labels = new();

            switch (modelType)
            {
                case ModelType.Classification:
                    {
                        var result = yolo.RunClassification(image, 1);
                        labels = result.Select(x => new LabelModel { Name = x.Label }).ToList();
                        resultImage = image.Draw(result);
                        break;
                    }
                case ModelType.ObjectDetection:
                    {
                        var result = yolo.RunObjectDetection(image, 0.23, 0.7);
                        labels = result.Select(x => x.Label).ToList();
                        resultImage = image.Draw(result);
                        break;
                    }
                case ModelType.ObbDetection:
                    {
                        var result = yolo.RunObbDetection(image, 0.23, 0.7);
                        labels = result.Select(x => x.Label).ToList();
                        resultImage = image.Draw(result);
                        break;
                    }
                case ModelType.Segmentation:
                    {
                        var result = yolo.RunSegmentation(image, 0.23, 0.65, 0.7);
                        labels = result.Select(x => x.Label).ToList();

                        resultImage = image.Draw(result);
                        break;
                    }
                case ModelType.PoseEstimation:
                    {
                        var result = yolo.RunPoseEstimation(image, 0.23, 0.7);
                        labels = result.Select(x => x.Label).ToList();
                        resultImage = image.Draw(result, CustomKeyPointColorMap.KeyPointOptions);
                        break;
                    }
            }

            DisplayDetectedLabels(labels);
            SaveDir = OUTPUT_FOLDER + "\\"+Path.GetFileNameWithoutExtension(modelPath)+"\\"+Path.GetFileName(Path.GetDirectoryName(image_file));
            if (Directory.Exists(SaveDir) == false)
                Directory.CreateDirectory(SaveDir);
            resultImage.Save(SaveDir+"\\"+Path.GetFileName(image_file), SKEncodedImageFormat.Jpeg);
        }



        public void ObjectDetectionOnVideo()
        {
            var videoOptions = new VideoOptions
            {
                VideoFile = SharedConfig.GetTestImage("walking.mp4"),
                OutputDir = OUTPUT_FOLDER,
                //GenerateVideo = false,
                //DrawLabels = false,
                //FPS = 30,
                //Width = 640, // Resize video...
                //Height = -2, // -2 = automatically calculate dimensions to keep proportions
                //Quality = 28,
                //DrawConfidence = true,
                //KeepAudio = true,
                //KeepFrames = false,
                DrawSegment = DrawSegment.Default,
                KeyPointOptions = CustomKeyPointColorMap.KeyPointOptions
            };

            AppendLine("Running Object Detection on video with Yolo v8...");

            using var yolo = new Yolo(new YoloOptions
            {
                OnnxModel = SharedConfig.GetTestModelV8(ModelType.ObjectDetection),
                ModelType = ModelType.ObjectDetection,
                Cuda = true
            });

            int currentLineCursor = 0;

            // Listen to events...
            yolo.VideoStatusEvent += (sender, e) =>
            {
                currentLineCursor = Console.CursorTop;
            };

            yolo.VideoProgressEvent += (object sender, EventArgs e) =>
            {
                Console.SetCursorPosition(20, currentLineCursor);
                AppendMsg(new string(' ', 4));
                Console.SetCursorPosition(20, currentLineCursor);
                AppendMsg(sender.ToString());
            };

            yolo.VideoCompleteEvent += (object sender, EventArgs e) =>
            {
                AppendLine("Complete!");
            };

            Dictionary<int, List<ObjectDetection>> detections = yolo.RunObjectDetection(videoOptions, 0.25);
            AppendMsg(Environment.NewLine);
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
                    if(this.MsgBox.TextLength >100000)
                        MsgBox.Clear();
                }
                else
                {
                    WilfFile.WriteAppend(log_file, Msg);
                }
            }
        }

        public void DisplayDetectedLabels(IEnumerable<LabelModel> labels)
        {
            if (!labels.Any())
                return;

            var ls = labels.GroupBy(x => x.Name)
                .ToDictionary(x => x.Key, x => x.Count());

            foreach (var label in ls)
                AppendLine("[Inf]"+$"{label.Key} ({label.Value})");

        }

        public void DisplayOnnxMetaDataExample()
        {
            AppendMsg(Environment.NewLine);
            AppendLine("Internal ONNX properties");
            AppendLine(new string('-', 58));

            using var yolo = new Yolo(new YoloOptions
            {
                OnnxModel = SharedConfig.GetTestModelV8(ModelType.ObjectDetection),
                ModelType = ModelType.ObjectDetection
            });

            // Display internal ONNX properties...
            foreach (var property in yolo.OnnxModel.GetType().GetProperties())
            {
                var value = property.GetValue(yolo.OnnxModel);
                AppendLine($"{property.Name,-20}{value!}");

                if (property.Name == nameof(yolo.OnnxModel.CustomMetaData))
                {
                    var customMetaData = (Dictionary<string, string>)value!;

                    foreach (var data in customMetaData)
                        AppendLine($"{"",-20}{data.Key,-20}{data.Value}");
                }
            }

            var labels = yolo.OnnxModel.Labels;

            AppendMsg(Environment.NewLine);
            AppendLine($"Labels ({labels.Length}):");
            AppendLine(new string('-', 58));

            // Display labels and its corresponding color
            for (var i = 0; i < 3; i++)
            {
                // Capitalize first letter in label
                var label = char.ToUpper(labels[i].Name[0]) + labels[i].Name[1..];
                AppendLine($"index: {i,-8} label: {label,-20} color: {labels[i].Color}");
            }

            AppendLine("...");
            AppendMsg(Environment.NewLine);
        }

        public void DisplayOutputFolder()
        {
            //if(Directory.Exists(OUTPUT_FOLDER))
            //System.Diagnostics.Process.Start(OUTPUT_FOLDER);
            AppendLine("Result Output Path:"+OUTPUT_FOLDER+Environment.NewLine);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            dataUI = new DataUI(this);
            dataUI.LoadData();
        }

        private void fILEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Application.StartupPath);
        }

    }
}
