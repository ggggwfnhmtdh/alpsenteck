﻿namespace YoloDotNet.Test.Common
{
    using System;
    using System.IO;

    using YoloDotNet.Enums;
    using YoloDotNet.Test.Common.Enums;

    public static class SharedConfig
    {
        private const string BASE_MODELS = @".\models";
        private const string BASE_MEDIA = @".\media";
        private const string BASE_MODELS_Alpsentek = @".\models\Alpsentek";

        public static string GetTestModelVx(ModelType modelType)
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
        /// <summary>
        /// Test models for Yolo V8
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetTestModelV8(ModelType modelType) => modelType switch
        {
            ModelType.Classification => Path.Combine(BASE_MODELS, "yolov8s-cls.onnx"),
            ModelType.ObjectDetection => Path.Combine(BASE_MODELS, "yolov8s.onnx"),
            ModelType.ObbDetection => Path.Combine(BASE_MODELS, "yolov8s-obb.onnx"),
            ModelType.Segmentation => Path.Combine(BASE_MODELS, "yolov8s-seg.onnx"),
            ModelType.PoseEstimation => Path.Combine(BASE_MODELS, "yolov8s-pose.onnx"),
            _ => throw new ArgumentException("Unknown modeltype.")
        };

        /// <summary>
        /// Test models for Yolo V9
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetTestModelV9(ModelType modelType) => modelType switch
        {
            ModelType.ObjectDetection => Path.Combine(BASE_MODELS, "yolov9s.onnx"),
            _ => throw new ArgumentException("Unknown modeltype.")
        };

        /// <summary>
        /// Test models for Yolo V10
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetTestModelV10(ModelType modelType) => modelType switch
        {
            ModelType.ObjectDetection => Path.Combine(BASE_MODELS, "yolov10s.onnx"),
            _ => throw new ArgumentException("Unknown modeltype.")
        };

        /// <summary>
        /// Test models for Yolo V11
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetTestModelV11(ModelType modelType) => modelType switch
        {
            ModelType.Classification => Path.Combine(BASE_MODELS, "yolov11s-cls.onnx"),
            ModelType.ObjectDetection => Path.Combine(BASE_MODELS, "yolov11s.onnx"),
            ModelType.ObbDetection => Path.Combine(BASE_MODELS, "yolov11s-obb.onnx"),
            ModelType.Segmentation => Path.Combine(BASE_MODELS, "yolov11s-seg.onnx"),
            ModelType.PoseEstimation => Path.Combine(BASE_MODELS, "yolov11s-pose.onnx"),
            _ => throw new ArgumentException("Unknown modeltype.")
        };

        /// <summary>
        /// Test images
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetTestImage(ImageType imageType) => imageType switch
        {
            ImageType.Hummingbird => Path.Combine(BASE_MEDIA, "hummingbird.jpg"),
            ImageType.Street => Path.Combine(BASE_MEDIA, "street.jpg"),
            ImageType.People => Path.Combine(BASE_MEDIA, "people.jpg"),
            ImageType.Crosswalk => Path.Combine(BASE_MEDIA, "crosswalk.jpg"),
            ImageType.Island => Path.Combine(BASE_MEDIA, "island.jpg"),
            _ => throw new ArgumentException("Unknown modeltype.")
        };

        public static string GetTestImage(string imageName) => Path.Combine(BASE_MEDIA, imageName);
    }
}
