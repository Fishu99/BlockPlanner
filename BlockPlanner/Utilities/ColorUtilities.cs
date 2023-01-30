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
            var rndColor = System.Windows.Media.Color.FromArgb(
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256),
                (byte)rnd.Next(256));
            return rndColor;
        }
    }
}
