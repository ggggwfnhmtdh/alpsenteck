using System;
using OpenCvSharp;
using OpenCvSharp.Tracking;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using MaterialSkin;
using MaterialSkin.Controls;
namespace TrackingObject
{
    public delegate void ThreadWorkStr(string Msg);
    public partial class MainForm : Form
    {
        string im_dir = "F:\\LLM\\UAV123\\data_seq\\UAV123\\bike1";
        string[] file_list;
        string filename = "";
        bool play = false;
        Tracker tracker;
        
        VideoCapture capture;
        bool m_mouseDown = false;
        bool m_mouseMove = false;

        System.Drawing.Point startPoint = new System.Drawing.Point();
        System.Drawing.Point endPoint = new System.Drawing.Point();

        Mat currentFrame;
        Mat currentFrame_copy;
        int pic_count_index = 0;
        Rect roi = new Rect();

        bool start_flag = false;
        DataUI m_UI;
        int FPS = 30;
        DateTime start = DateTime.Now; // 获取当前时间
        DateTime end = DateTime.Now; // 获取当前时间

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Video files (*.avi)|*.avi|MP4 files (*.mp4)|*.mp4";
            //ofd.RestoreDirectory = true;
            //ofd.CheckFileExists = true;
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    filename = ofd.FileName;
            //    toolStripStatusLabel1.Text = filename;
            //    capture = new VideoCapture(filename);
            //    if (!capture.IsOpened())
            //    {
            //        toolStripStatusLabel1.Text = " 打开视频文件失败";
            //        return;
            //    }
            //    capture.Read(currentFrame);
            //    if (!currentFrame.Empty())
            //    {
            //        pictureBox1.Image = BitmapConverter.ToBitmap(currentFrame);
            //        timer1.Interval = (int)(1000.0 / capture.Fps);
            //        timer1.Enabled = true;

            //        m_mouseMove = false;
            //        m_mouseDown = false;
            //        pictureBox2.Image = null;
            //    }

            //}
        }
        public void ShowMsg(string Msg)
        {
            if (this.label4.InvokeRequired)
            {
                ThreadWorkStr fc = new ThreadWorkStr(ShowMsg);
                this.Invoke(fc, new object[1] { Msg });
            }
            else
            {

                label4.Text = Msg;

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            m_UI = new DataUI(this);
            m_UI.LoadData();    
            //comboBox1.SelectedIndex = 0;
            label2.Text = "";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(LMap_MouseWheel);
        }

        public void LMap_MouseWheel(object sender, MouseEventArgs e)
        {
            if (start_flag == true)
            {
                //当e.Delta > 0时鼠标滚轮是向上滚动，e.Delta < 0时鼠标滚轮向下滚动
                if (e.Delta < 0)//滚轮向上
                {
                    pic_count_index+=10; //放大
                             //MessageBox.Show("鼠标向上滑动");
                }
                else
                {
                    if (pic_count_index>=10)
                        pic_count_index-=10;//缩小
                    else
                        pic_count_index = file_list.Length+pic_count_index-10; ;
                            //MessageBox.Show("鼠标向下滑动");
                }
                pic_count_index = pic_count_index%file_list.Length;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (MainPicBox.Image == null)
            {
                return;
            }
            FPS = Convert.ToInt32(FpsBox.Text); 
            if(start_flag == false)
            {
                timer1.Interval = (int)(1000.0 / FPS);
                timer1.Enabled = true;
                start_flag = true;
                button2.Text = "Stop";
            }
            else
            {
                timer1.Enabled = false;
                start_flag = false;
                button2.Text = "Start";
            }
            play = start_flag;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (MainPicBox.Image == null)
                return;
            play = false;
            m_mouseDown = true;

            startPoint = e.Location;

            Bitmap bitmap = new Bitmap(file_list[pic_count_index%file_list.Length]);
            // 将Bitmap转换为Mat
            currentFrame_copy = BitmapConverter.ToMat(bitmap);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!m_mouseDown || !m_mouseMove)
                return;
            m_mouseDown = false;
            m_mouseMove = false;

            System.Drawing.Point image_startPoint = ConvertCooridinate(startPoint);
            System.Drawing.Point image_endPoint = ConvertCooridinate(endPoint);
            if (image_startPoint.X < 0)
                image_startPoint.X = 0;
            if (image_startPoint.Y < 0)
                image_startPoint.Y = 0;
            if (image_endPoint.X < 0)
                image_endPoint.X = 0;
            if (image_endPoint.Y < 0)
                image_endPoint.Y = 0;
            if (image_startPoint.X > currentFrame_copy.Cols)
                image_startPoint.X = currentFrame_copy.Cols;
            if (image_startPoint.Y > currentFrame_copy.Rows)
                image_startPoint.Y = currentFrame_copy.Rows;
            if (image_endPoint.X > currentFrame_copy.Cols)
                image_endPoint.X = currentFrame_copy.Cols;
            if (image_endPoint.Y > currentFrame_copy.Rows)
                image_endPoint.Y = currentFrame_copy.Rows;

            label2.Text = String.Format("ROI:({0},{1})-({2},{3})", image_startPoint.X, image_startPoint.Y, image_endPoint.X- image_startPoint.X, image_endPoint.Y-image_startPoint.Y);
            int w = (image_endPoint.X - image_startPoint.X);
            int h = (image_endPoint.Y - image_startPoint.Y);
            if (w > 10 && h > 10)
            {
                roi = new Rect(image_startPoint.X, image_startPoint.Y, w, h);

                Mat roi_mat = currentFrame_copy[roi];
                RioPicBox.Image = BitmapConverter.ToBitmap(roi_mat);
                if (RioPicBox.Image != null)
                {
                    if(tracker != null)
                        tracker.Dispose();
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                        default:
                            tracker = TrackerCSRT.Create();
                            break;
                        case 1:
                            tracker = TrackerGOTURN.Create();
                            break;
                        case 2:
                            tracker = TrackerKCF.Create();
                            break;
                        case 3:
                            tracker = TrackerMIL.Create();
                            break;
                        case 4:
                            break;
                    }
                    tracker.Init(currentFrame_copy, roi);

                }
            }
            currentFrame_copy.Dispose();
            play = start_flag;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainPicBox.Image == null)
                return;
            if (!m_mouseDown) return;

            m_mouseMove = true;
            endPoint = e.Location;

            MainPicBox.Invalidate();
        }
        private System.Drawing.Point ConvertCooridinate(System.Drawing.Point point)
        {
            System.Reflection.PropertyInfo rectangleProperty = this.MainPicBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle pictureBox = (Rectangle)rectangleProperty.GetValue(this.MainPicBox, null);

            int zoomedWidth = pictureBox.Width;
            int zoomedHeight = pictureBox.Height;

            int imageWidth = MainPicBox.Image.Width;
            int imageHeight = MainPicBox.Image.Height;

            double zoomRatex = (double)(zoomedWidth) / (double)(imageWidth);
            double zoomRatey = (double)(zoomedHeight) / (double)(imageHeight);
            int black_left_width = (zoomedWidth == this.MainPicBox.Width) ? 0 : (this.MainPicBox.Width - zoomedWidth) / 2;
            int black_top_height = (zoomedHeight == this.MainPicBox.Height) ? 0 : (this.MainPicBox.Height - zoomedHeight) / 2;

            int zoomedX = point.X - black_left_width;
            int zoomedY = point.Y - black_top_height;

            System.Drawing.Point outPoint = new System.Drawing.Point();
            outPoint.X = (int)((double)zoomedX / zoomRatex);
            outPoint.Y = (int)((double)zoomedY / zoomRatey);

            return outPoint;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!m_mouseDown || !m_mouseMove)
                return;
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Blue, 2);
            Rectangle rect = new Rectangle(startPoint.X, startPoint.Y, (endPoint.X - startPoint.X), (endPoint.Y - startPoint.Y));
            g.DrawRectangle(p, rect);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {          
            if (play)
            {
                start = end;
                end = DateTime.Now; // 获取当前时间
                if (pic_count_index == file_list.Length)
                    pic_count_index = 0;
                Bitmap bitmap = new Bitmap(file_list[pic_count_index++]);
                // 将Bitmap转换为Mat
                currentFrame = BitmapConverter.ToMat(bitmap);
                if (currentFrame.Empty())
                {
                    play = false;
                    MainPicBox.Image = null;
                    RioPicBox.Image = null;

                    timer1.Enabled = false;
                    return;
                }
                
                if (RioPicBox.Image != null && tracker != null)
                {
                    tracker.Update(currentFrame, ref roi);
                    Cv2.Rectangle(currentFrame, roi, Scalar.Yellow);
                    //label2.Text = String.Format("ROI:({0},{1})-({2},{3})", roi.X, roi.Y, roi.X+roi.Width, roi.Y+roi.Height);
                }
                
                MainPicBox.Image.Dispose();
                MainPicBox.Image = BitmapConverter.ToBitmap(currentFrame);
                bitmap.Dispose();
                currentFrame.Dispose();

                if (end>start)
                {
                    TimeSpan duration = end - start; // 计算时间差
                    ShowMsg(pic_count_index.ToString("D5") +":"+duration.TotalMilliseconds.ToString("F1")+"ms");
                }
            }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            pic_count_index = Convert.ToInt32(PicIndexBox.Text);  
            m_UI.Save();
            im_dir =InPath.Text;
            file_list = WilfFile.GetFile(im_dir, ".jpg", false);
            if (pic_count_index<file_list.Length)
            {
                Bitmap bitmap = new Bitmap(file_list[pic_count_index]);
                // 将Bitmap转换为Mat
                currentFrame = BitmapConverter.ToMat(bitmap);
                MainPicBox.Image = BitmapConverter.ToBitmap(currentFrame);
                play = false;
            }
        }

        private void FpsBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FPS = Convert.ToInt32(FpsBox.Text);
                timer1.Interval = (int)(1000.0 / FPS);
            }
            catch 
            { 
            
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            InPath.Text = path;
        }

        private void PicIndexBox_TextChanged(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (file_list != null)
            {
                pic_count_index = Convert.ToInt32(PicIndexBox.Text);
                if (pic_count_index<file_list.Length)
                {
                    bitmap = new Bitmap(file_list[pic_count_index]);
                    // 将Bitmap转换为Mat
                    currentFrame = BitmapConverter.ToMat(bitmap);
                    MainPicBox.Image = BitmapConverter.ToBitmap(currentFrame);
                }
            }
        }
    }
}
