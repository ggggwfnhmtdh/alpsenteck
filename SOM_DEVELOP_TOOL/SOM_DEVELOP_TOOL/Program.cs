using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SOM_DEVELOP_TOOL
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ReleaseDLL();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            if (args.Length == 0)
                Application.Run(new MainForm());
            else
                Application.Run(new MainForm(args));
        }

        static void ReleaseDLL()
        {
            string[] OthersRes = new string[] { "MaterialSkin.dll", "JLinkARM.dll", "ScanFreq.dll", "GuiLabs.MathParser.dll", "Newtonsoft.Json.dll", "ELFSharp.dll", "ICSharpCode.SharpZipLib.dll" };
            for (int i = 0; i < OthersRes.Length; i++)
            {
                if (File.Exists(OthersRes[i]) == false)
                {
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SOM_DEVELOP_TOOL.File." + OthersRes[i]))
                    {
                        byte[] byData = new byte[stream.Length];
                        stream.Read(byData, 0, byData.Length);
                        File.WriteAllBytes(OthersRes[i], byData);
                    }
                }
            }
        }
    }
}