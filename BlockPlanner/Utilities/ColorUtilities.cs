using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BlockPlanner.Utilities
{
    public abstract class ColorUtilities
    {
        public static Color GetRandomColor()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var rndColor = Color.FromArgb(
                255,
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256));

            var factor = 1.5;
            var red = (int)(rndColor.R * factor);
            var green = (int)(rndColor.G * factor);
            var blue = (int)(rndColor.B * factor);

            red = (red > 255) ? 255 : red;
            green = (green > 255) ? 255 : green;
            blue = (blue > 255) ? 255 : blue;

            return Color.FromArgb(rndColor.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
