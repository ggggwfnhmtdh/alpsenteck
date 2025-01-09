﻿namespace YoloDotNet.Enums
{
    /// <summary>
    /// Strongly typed names for image vision types.
    /// </summary>
    [DataContract]
    public enum ModelType
    {
        [EnumMember(Value = "classify")]
        Classification,

        [EnumMember(Value = "detect")]
        ObjectDetection,

        [EnumMember(Value = "obb")]
        ObbDetection,

        [EnumMember(Value = "segment")]
        Segmentation,

        [EnumMember(Value = "pose")]
        PoseEstimation
    }
}
