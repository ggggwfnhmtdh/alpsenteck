using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingObject
{
    public class ffpmeg
    {
        public static string GenerateWatermarkCommand(string inputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
                throw new ArgumentException("输入文件不能为空");

            // 获取视频文件名（去掉路径和扩展名）
            string videoName = Path.GetFileName(inputFile);

            // 生成最简洁的 ffmpeg 命令
            string filterComplex = $"drawtext=text='{videoName}':x=10:y=10:fontsize=20:fontcolor=yellow";

            string command = $"-i \"{inputFile}\" -vf \"{filterComplex}\"";

            return command;
        }



        public static string GenerateVideoArgs(string[] VideoFiles,string arrangement,string OutFile)
        {
            int videoCount = VideoFiles.Length;
            if (videoCount == 0)
                throw new ArgumentException("视频数量不能为零。");

            if (videoCount == 1)
            {
                return $"-i \"video1.mp4\" -c copy \"output.mp4\"";
            }

            // 构建输入文件参数  
            List<string> inputArgs = new List<string>();
            for (int i = 0; i < videoCount; i++)
            {
                inputArgs.Add($"-i \""+VideoFiles[i]+"\"");
            }

            string inputs = string.Join(" ", inputArgs);
            string filter = string.Empty;

            switch (arrangement.ToLower())
            {
                case "x":
                    filter = $"hstack=inputs={videoCount}";
                    break;

                case "y":
                    filter = $"vstack=inputs={videoCount}";
                    break;

                case "a":
                    filter = GenerateSquareLayout(VideoFiles);
                    break;

                default:
                    throw new ArgumentException("未识别的排列方式。有效选项为 'x'、'y' 或 'a'。");
            }

            return $"{inputs} -filter_complex \"{filter}\" \""+OutFile+"\"";
        }
        public static string GenerateSquareLayout(string[] inputFiles)
        {
            if (inputFiles == null || inputFiles.Length == 0)
                throw new ArgumentException("文件列表不能为空");

            int videoCount = inputFiles.Length;

            // 计算最接近正方形的行列数
            int cols = (int)Math.Ceiling(Math.Sqrt(videoCount));
            int rows = (int)Math.Ceiling((double)videoCount / cols);

            // 生成 hstack 和 vstack 参数
            string filterComplex = "";
            string[] rowLabels = new string[rows];

            for (int row = 0; row < rows; row++)
            {
                int start = row * cols;
                int end = Math.Min(start + cols, videoCount);
                string[] rowInputs = new string[end - start];
                for (int i = start; i < end; i++)
                {
                    rowInputs[i - start] = $"[{i}:v]";
                }
                string rowLabel = $"row{row}";
                filterComplex += string.Join("", rowInputs) + $"hstack=inputs={end - start}[{rowLabel}];";
                rowLabels[row] = $"[{rowLabel}]";
            }

            // 垂直排列所有行
            filterComplex += string.Join("", rowLabels) + $"vstack=inputs={rows}";

            return filterComplex;
        }

    }
}

