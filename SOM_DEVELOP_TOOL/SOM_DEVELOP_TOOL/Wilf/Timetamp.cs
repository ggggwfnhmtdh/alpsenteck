using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_DEVELOP_TOOL
{
    public static class Timetamp
    {
        public static System.Diagnostics.Stopwatch[] Timer = new System.Diagnostics.Stopwatch[64];
        public static bool[] TimerFlag = new bool[64];
        public static void TimeStart(int TimerID)
        {
            Timer[TimerID].Start();
            TimerFlag[TimerID] = true;
        }

        public static void TimeStop(int TimerID)
        {
            Timer[TimerID].Reset();
            TimerFlag[TimerID] = false;
        }



        public static bool GetTimeFlag(int TimerID)
        {
            return TimerFlag[TimerID];
        }
        public static double GetTime(int TimerID)
        {
            return Timer[TimerID].Elapsed.TotalMilliseconds;
        }
    }
}
