using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static Yolov5App.WilfClass.UAV123;
using static System.Formats.Asn1.AsnWriter;
using System.DirectoryServices;
namespace Yolov5App.WilfClass
{
    public class UAV123
    {
        public static string annote_spec_file = MainForm.DocDir+"\\"+"UAV123_data_spec.csv";
        public string[] anno_tag_name = new string[] { "sequence","sub_seq","fps","start","end","frames","start_n","end_n","dur","SV","ARC","LR","FM","FOC","POC","OV","BC","IV","VC","CM","SOB" };
        public string[] result_tag_name = new string[] { "file_name", "label_name", "id", "score", "x", "y", "w", "h" };
        public AnnoData[] m_AnnoData;
        public ResultData[] m_ResultData;
        public struct AnnoData
        {
            public double[,] Data;
            public Hashtable Param;
        }

        public struct ResultData
        {
            public Hashtable Header;
            public string dir_name;
            public List<string[,]> Data;
        }

        public void CalcResult()
        {
            string dir_name;
            string file_name;
            List<string[,]> data;
            int anno_file_index, anno_row_index;
            Rectangle AnnoRec;
            Rectangle[] ResultRec;
            double iou_ratio,pass_ratio,avr_ratio;
            StringBuilder sb_file = new StringBuilder();
            StringBuilder dir_file = new StringBuilder();
            StringBuilder debug_msg = new StringBuilder();
            sb_file.Append("dir name,file name,iou ratio"+Environment.NewLine);
            dir_file.Append("dir name,pass ratio,avr iou "+Environment.NewLine);
            int dir_frame_count = 0;
            int dir_annote_frame_count = 0;
            int dir_match_frame_count = 0;
            int dir_miss_frame_count = 0;

            bool ret_result_rec_ok = false;
            bool ret_annote_rec_ok = false;

            for (int i = 0; i < m_ResultData.Length; i++)
            {
                dir_name = m_ResultData[i].dir_name;
                data = m_ResultData[i].Data;
                dir_frame_count = 0;
                dir_annote_frame_count = 0;
                dir_match_frame_count = 0;
                dir_miss_frame_count = 0;
                pass_ratio = 0;
                avr_ratio = 0;
                for (int j = 0; j < data.Count; j++)
                {
                    //if (dir_name == "bird1" && j>=774)
                    //    dir_name = dir_name;
                    Find(m_AnnoData, dir_name, j,out anno_file_index,out anno_row_index);
                    
                    if (anno_file_index<0||anno_row_index<0)
                        continue;
                    dir_frame_count++;
                    debug_msg.Append(dir_name+","+j+","+m_AnnoData[anno_file_index].Param["sequence"]+","+m_AnnoData[anno_file_index].Param["sub_seq"]+","+anno_row_index+Environment.NewLine);
                    ResultRec = GetResultRectangle(i, j);
                    AnnoRec = GetAnnoRectangle(anno_file_index, anno_row_index);
                    ret_annote_rec_ok = CheckRectangleValid(AnnoRec);
                    ret_result_rec_ok = CheckRectangleValid(ResultRec);
                    if (ret_annote_rec_ok == true)
                    {
                        dir_annote_frame_count++;
                    }

                    if (ret_annote_rec_ok == true && ret_result_rec_ok == false)
                    {
                        dir_miss_frame_count++;
                    }

                    if (ret_annote_rec_ok && ret_result_rec_ok)
                    {
                        iou_ratio = CalcIOU_Result(AnnoRec, ResultRec);
                        file_name = m_ResultData[i].Data[j][0, 0];
                        sb_file.Append(dir_name+","+file_name+","+iou_ratio.ToString("F4")+Environment.NewLine);
                        if (iou_ratio>0.5)
                        {
                            dir_match_frame_count++;
                            avr_ratio+=iou_ratio;
                        }
                    }
                }
                pass_ratio = (double)dir_match_frame_count/dir_annote_frame_count;
                if (dir_match_frame_count>0)
                    avr_ratio = avr_ratio/dir_match_frame_count;
                else
                    avr_ratio = 0;
                dir_file.Append(dir_name+","+pass_ratio+","+avr_ratio+Environment.NewLine);
            }
            File.WriteAllText("StatisticalResult_file.csv", sb_file.ToString());
            File.WriteAllText("StatisticalResult_dir.csv", dir_file.ToString());
            File.WriteAllText("debug_msg.csv", debug_msg.ToString());
        }

        public bool CheckRectangleValid(Rectangle rec)
        {
            return (rec.X>=0)&&(rec.Y>=0)&&(rec.Width>=0)&&(rec.Height>=0);
        }

        public bool CheckRectangleValid(Rectangle[] recs)
        {
            for (int i = 0; i<recs.Length; i++)
            {
                if (CheckRectangleValid(recs[i]) == false)
                    return false;
            }
            return true;
        }
        public double CalcIOU_Result(Rectangle rec0,Rectangle[] rec1)
        {
            double[] ratio;
            int[] Iarea,Uarea;
            ratio = new double[rec1.Length];
            Iarea = new int[rec1.Length];
            Uarea = new int[rec1.Length];

            for (int i = 0; i<rec1.Length; i++)
            {
                Iarea[i] = CalculateIntersectionArea(rec0, rec1[i]);
                Uarea[i] = rec0.Width*rec0.Height + rec1[i].Width*rec1[i].Height - Iarea[i];
                ratio[i] = (double)Iarea[i]/Uarea[i];
            }
            return ratio.Max();
        }

        public static int CalculateIntersectionArea(Rectangle rect1, Rectangle rect2)
        {
            // 计算交集的四条边  
            int xLeft = Math.Max(rect1.X, rect2.X); // 左边界  
            int yTop = Math.Max(rect1.Y, rect2.Y); // 上边界  
            int xRight = Math.Min(rect1.X + rect1.Width, rect2.X + rect2.Width); // 右边界  
            int yBottom = Math.Min(rect1.Y + rect1.Height, rect2.Y + rect2.Height); // 下边界  

            // 确保交集范围的有效性（右边界必须大于左边界，上边界必须大于下边界）  
            if (xRight > xLeft && yBottom > yTop)
            {
                return (xRight - xLeft) * (yBottom - yTop); // 返回交集面积  
            }

            return 0; // 没有交集  
        }

        public Rectangle GetAnnoRectangle(int file_index,int row_index)
        {
            Hashtable ht = new Hashtable();
            Rectangle rec = new Rectangle();
            rec.X = (int)m_AnnoData[file_index].Data[row_index,0];
            rec.Y = (int)m_AnnoData[file_index].Data[row_index, 1];
            rec.Width = (int)m_AnnoData[file_index].Data[row_index, 2];
            rec.Height = (int)m_AnnoData[file_index].Data[row_index, 3];
            return rec;
        }

        public Rectangle[] GetResultRectangle(int dir_index, int file_index)
        {
            int row_index;
            Hashtable ht = new Hashtable();
            List<Rectangle> lst_rec = new List<Rectangle>();

            for (int i = 0; i<(int)m_ResultData[dir_index].Data[file_index].GetLength(0); i++)
            {
                Rectangle rec = new Rectangle();
                ht = m_ResultData[dir_index].Header;
                rec.X = (int)Convert.ToDouble(m_ResultData[dir_index].Data[file_index][i, (int)ht["x"]]);
                rec.Y = (int)Convert.ToDouble(m_ResultData[dir_index].Data[file_index][i, (int)ht["y"]]);
                rec.Width = (int)Convert.ToDouble(m_ResultData[dir_index].Data[file_index][i, (int)ht["w"]]);
                rec.Height = (int)Convert.ToDouble(m_ResultData[dir_index].Data[file_index][i, (int)ht["h"]]);
                lst_rec.Add(rec);
            }
            
            return lst_rec.ToArray();
        }

        public void Find(AnnoData[] annos, string dir_name, int InResultFileIndex,out int file_index, out int RowIndex)
        {
            bool found = false;
            List<int> list = new List<int>();
            int start, end;
            file_index = -1;
            RowIndex = -1;
            int annos_index;

            if (dir_name == "bird1"&&InResultFileIndex>=253)
                dir_name = dir_name;
            for (int i = 0; i < annos.Length; i++)
            {
                if (annos[i].Param["sequence"].ToString() == dir_name||annos[i].Param["sub_seq"].ToString() == dir_name)
                {
                    list.Add(i);
                }
            }

            for (int i = 0; i< list.Count; i++)
            {
                annos_index = list[i];
                start = Convert.ToInt32(annos[annos_index].Param["start_n"].ToString());
                end = Convert.ToInt32(annos[annos_index].Param["end_n"].ToString());
                if(InResultFileIndex>=start-1 && InResultFileIndex<=end-1)
                {
                    file_index =annos_index;
                    RowIndex = InResultFileIndex - (start-1);
                    found = true;
                    break;
                }
            }
            if(found == false)
            {
                file_index = -1;
                RowIndex = -1;
            }
        }

        public void GetAnno(string dir)
        {
            string file_name;
            StringBuilder stringBuilder = new StringBuilder();
            List<int> lst = new List<int>();
            string[] files = WilfFile.GetFile(dir, ".txt", false);
            m_AnnoData = ParseAnnoSpec(annote_spec_file);
            for (int i = 0; i < files.Length; i++)
            {
                file_name = Path.GetFileNameWithoutExtension(files[i]);
                int index = FindAnnoDataIndex(m_AnnoData, file_name);
                m_AnnoData[index].Data = ParseAnnoFile(files[i]);
            }

            for (int i = 0; i < m_AnnoData.Length; i++)
            {
                for (int j = 0; j<m_AnnoData[i].Data.GetLength(0); j++)
                {
                    stringBuilder.Append(m_AnnoData[i].Param["sequence"]+",");
                    stringBuilder.Append(m_AnnoData[i].Param["sub_seq"]+",");
                    stringBuilder.Append(m_AnnoData[i].Param["start_n"]+",");
                    stringBuilder.Append(m_AnnoData[i].Param["end_n"]+",");
                    stringBuilder.Append(m_AnnoData[i].Param["fps"]+Environment.NewLine);
                }
            }
            File.WriteAllText("test.csv", stringBuilder.ToString());
        }
        public void GetResult(string dir)
        {
            List<int> lst = new List<int>();
            string[] files = WilfFile.GetFile(dir, ".csv", false);
            m_ResultData = new ResultData[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                m_ResultData[i].dir_name = Path.GetFileNameWithoutExtension(files[i]);
                List<string[,]> ResultData = ParseResult(files[i], out m_ResultData[i].Header);
                m_ResultData[i].Data = ResultData;
            }
        }

        public List<string[,]> ParseResult(string ResultFile,out Hashtable ht_header)
        {
            string[,] Content;
            string[] lines = File.ReadAllLines(ResultFile);
            string[] header;
            List<string>  name = new List<string>();
            Hashtable ht_num = new Hashtable();
            Hashtable ht_start_index = new Hashtable();   
            header = lines[0].Split(',');
            ht_header = WilfDataPro.StringToHashtable(lines[0]);
            Content = new string[lines.Length-1,header.Length];

            for (int i = 1; i < lines.Length; i++)
            {
                string[] strs = lines[i].Replace(" ", "").Trim(',').Split(",");
                if (ht_num.ContainsKey(strs[0]) == true)
                {
                    ht_num[strs[0]] =  ((int)ht_num[strs[0]])+1;
                   
                }
                else
                {
                    ht_num.Add(strs[0],1);
                    ht_start_index.Add(strs[0],i-1);
                    name.Add(strs[0]);
                }
                for (int j = 0; j<strs.Length; j++)
                {
                    Content[i-1,j] = strs[j];
                }
            }

            List<string[,]> lst = new List<string[,]>();
            for (int i = 0; i<name.Count; i++)
            {
                int num = (int)ht_num[name[i]];
                int index = (int) ht_start_index[name[i]];
                string[,] new_data = new string[num,ht_header.Count];
                for(int row = 0;row<num;row++)
                {
                    for (int col = 0; col<ht_header.Count; col++)
                    {
                        new_data[row, col] = Content[index+row, col];
                    }
                }
                lst.Add(new_data);  
            }
            return lst;
        }

        public int FindAnnoDataIndex(AnnoData[] annoDatas, string dir_name)
        {
            for (int i = 0; i < annoDatas.Length; i++)
            {
                if (annoDatas[i].Param["sequence"].ToString() == dir_name || annoDatas[i].Param["sub_seq"].ToString() == dir_name)
                {
                    return i;
                }
            }
            return -1;
        }

        public AnnoData[] ParseAnnoSpec(string SpecFile)
        {
            AnnoData[] annoDatas;
            Hashtable ht = new Hashtable(); 
            string[] lines = File.ReadAllLines(SpecFile);
            annoDatas = new AnnoData[lines.Length-1];
            string[] tag = lines[0].Split(",");
            for (int i = 0; i< tag.Length; i++)
            {
                ht.Add(tag[i], i);
            }
            for (int i = 1; i < lines.Length; i++)
            {
               string[] strs = lines[i].Replace(" ","").Trim(',').Split(",");
                annoDatas[i-1].Param = new Hashtable();
               for (int j = 0;j<strs.Length;j++)
               {
                    annoDatas[i-1].Param.Add(tag[j], strs[j]);
                }
            }
            return annoDatas;
        }

        public double[,] ParseAnnoFile(string anno_file)
        {
            List<double[]> all_data = new List<double[]>(); 
            string file_name = Path.GetFileNameWithoutExtension(anno_file);
            string[] lines = File.ReadAllLines(anno_file);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] strs = lines[i].Replace(" ","").Split(',');
                double[] line_data = new double[strs.Length];
                for (int j = 0; j < strs.Length; j++)
                {
                    if (WilfDataPro.IsNumeric(strs[j]) == true)
                        line_data[j] = Convert.ToDouble(strs[j]);
                    else
                        line_data[j] = -1;
                }
                all_data.Add(line_data);
            }
            double[,] temp = new double[all_data.Count, all_data[0].Length];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = all_data[i][j];
                }
            }
            return temp;
        }
    }
}
