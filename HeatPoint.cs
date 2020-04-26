//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace tABI_Project
{
    public struct HeatPoint
    {
        public int X;
        public int Y;
        public byte Intensity;

        public HeatPoint(int in_x, int in_y, byte in_intensity)
        {
            X = in_x;
            Y = in_y;
            Intensity = in_intensity;
        }
    }
}
