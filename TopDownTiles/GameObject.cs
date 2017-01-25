using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class GameObject
    {
        public Vector2 position { get; set; }


        public void Move(float direction, float speed) {

            var currX = position.X;
            var currY = position.Y;
            currX += (int)(Math.Cos((double)InputManager.floatDirection) * speed);
            currY += (int)(Math.Sin((double)InputManager.floatDirection) * speed);
            position = new Vector2(currX, currY);
        }
    }
}
