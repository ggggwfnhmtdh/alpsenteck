using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SOM_DEVELOP_TOOL
{
    public static  class SignalDevice
    {
        [DllImport("ScanFreq.dll")]
        public unsafe static extern void Connect();
        [DllImport("ScanFreq.dll", CallingConvention = CallingConvention.Cdecl)]
        public unsafe static extern void SetPara(float fFrequency, float fAmplitude, float offset, float duty);
        [DllImport("ScanFreq.dll", CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void SetParaFix(float fFrequency, float fAmplitude, float offset, float duty);
        [DllImport("ScanFreq.dll")]
        public unsafe static extern void DisConnect();
        [DllImport("ScanFreq.dll")]
        private unsafe static extern bool GetState();

        public static bool SetVol(float Vol_mV)
        {
            bool ret = SignalDevice.GetState();
            if (ret == true)
            {
                SetPara((float)(13.56*1000*1000), (float)Vol_mV/1000, 0, -1);
                return true;
            }
            else
                return false;
        }

        public static bool SetParam(float Vol_mV, float freq)
        {
            bool ret = SignalDevice.GetState();
            if (ret == true)
            {
                SetPara(freq, (float)Vol_mV / 1000, 0, -1);
                return true;
            }
            else
                return false;
        }
    }
}
