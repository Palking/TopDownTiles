using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    static class Helper
    {
        public static bool RectangleIntersect(Rectangle rec1, Rectangle rec2)
        {
            return rec1.Intersects(rec2);
        }
    }
}
