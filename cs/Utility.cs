using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using ESRI.ArcGIS.Display;

namespace cs
{
    public class Utility
    {
        // Color转换为IColor
        public static IColor ConvertToRgbColor(Color color)
        {
            IColor pColor = new RgbColor();
            pColor.RGB = color.R + color.G * 256 + color.B * 65536;
            return pColor;
        }

        // IColor转换为Color
        public static Color ConvertToColor(IColor pColor)
        {
            return ColorTranslator.FromOle(pColor.RGB);
        }
    }
}
