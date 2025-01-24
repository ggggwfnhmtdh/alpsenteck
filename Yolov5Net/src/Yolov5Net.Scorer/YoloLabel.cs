using SixLabors.ImageSharp;
//using System.Drawing;

namespace Yolov5Net.Scorer;

/// <summary>
/// Label of detected object.
/// </summary>
public record YoloLabel(int Id, string Name, Color Color, YoloLabelKind Kind)
{
    public YoloLabel(int id, string name) : this(id, name, GetColorById(id), YoloLabelKind.Generic) { }

    // 方法：根据 Id 返回对应的颜色  
    private static Color GetColorById(int id)
    {
        return id switch
        {
            0 => SixLabors.ImageSharp.Color.FromRgb(0x91,0xff,0x57), 
            1 => SixLabors.ImageSharp.Color.FromRgb(0x91, 0xff, 0x57),
            2 => SixLabors.ImageSharp.Color.FromRgb(0xff, 0x6f, 0x00),
            3 => SixLabors.ImageSharp.Color.FromRgb(0xf6, 0xE7, 0x34),
            4 => SixLabors.ImageSharp.Color.FromRgb(0xff, 0x6E, 0x79),
            5 => SixLabors.ImageSharp.Color.FromRgb(0xff, 0xB7, 0xE6),
            6 => SixLabors.ImageSharp.Color.FromRgb(0x7B, 0xFF, 0xD9),
            7 => SixLabors.ImageSharp.Color.FromRgb(0xFF, 0xAC, 0x28),
            8 => SixLabors.ImageSharp.Color.FromRgb(0x3D, 0xE4, 0xE4),
            9 => SixLabors.ImageSharp.Color.FromRgb(0xFF, 0x70, 0xF9),
            // 添加更多情况，根据需要设置颜色  
            _ => SixLabors.ImageSharp.Color.Black , // 默认颜色  
        };
    }

}
