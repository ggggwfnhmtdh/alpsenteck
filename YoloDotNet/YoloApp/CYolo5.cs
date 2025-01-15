
using System;
using System.IO;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
//using ConsoleDemo.Config;
using YoloDotNet.Extensions;
using YoloDotNet.Test.Common;
using YoloDotNet.Test.Common.Enums;
using SkiaSharp;
namespace YoloApp
{
    public class CYolo5
    {
        //public void RunModelV5(ModelType modelType, ModelVersion modelVersion, ImageType imageType, string image_file, bool cuda = false, bool primeGpu = false)
        //{
        //    string SaveDir;

        //    var image =  SixLabors.ImageSharp.Image.Load<Rgba32>(image_file);
        //    using var scorer = new YoloScorer<YoloCocoP5Model>("Assets/yolov5s.onnx");
        //    {
        //        var predictions = scorer.Predict(image);

        //        var font = new  SixLabors.Fonts.Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);

        //        foreach (var prediction in predictions) // draw predictions
        //        {
        //            var score = Math.Round(prediction.Score, 2);

        //            var (x, y) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

        //            image.Mutate(a => a.DrawPolygon(new SixLabors.ImageSharp.Drawing.Processing.Pen(prediction.Label.Color, 1),
        //                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
        //                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
        //                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
        //                new SixLabors.ImageSharp.PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
        //            ));

        //            image.Mutate(a => a.DrawText($"{prediction.Label.Name} ({score})",
        //                font, prediction.Label.Color, new SixLabors.ImageSharp.PointF(x, y)));
        //        }

        //    }

        //}
    }
}
