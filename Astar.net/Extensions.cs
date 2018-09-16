using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar.net
{
    public static class Extensions
    {
        public static Bitmap EnlargeImage(this Bitmap original, int scale)
        {
            Bitmap newimg = new Bitmap(original.Width * scale, original.Height * scale);

            using (Graphics g = Graphics.FromImage(newimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(original, new Rectangle(Point.Empty, newimg.Size));
            }
            return newimg;
        }

    }
}
