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
        public TopDownTiles game;
        public int Width { get; set; }
        public int Height { get; set; }

        public void LoadGame(TopDownTiles currGame)
        {
            game = currGame;
        }

        public void Move(float direction, float speed) {

            Move(direction, speed, true);
        }

        public void Move(float direction, float speed, bool collides)
        {
            var currX = position.X;
            var currY = position.Y;

            //Evaluate collisions
            if (collides)
            {
                currX += (float)(Math.Cos((double)InputManager.floatDirection) * speed);
                currY += (float)(Math.Sin((double)InputManager.floatDirection) * speed);
                game.ui.DebugMessage = "Speed X = " + (int)(Math.Cos((double)InputManager.floatDirection)*speed);
                game.ui.DebugMessage2 = "Speed Y = " + (int)(Math.Sin((double)InputManager.floatDirection)*speed);

                //Horizontal movement part
                if (CheckCornersWalkable(currX, position.Y))
                {
                    position = new Vector2(currX, position.Y);
                }

                //Vertical movement part   
                if (CheckCornersWalkable(position.X, currY))
                {
                    position = new Vector2(position.X, currY);
                }

            }

            //If Objects are supposed to pass beyond colliders. Why would they ever?
            else
            {
                currX += (int)(Math.Cos((double)InputManager.floatDirection) * speed);
                currY += (int)(Math.Sin((double)InputManager.floatDirection) * speed);
                //Set new position.
                position = new Vector2(currX, currY);
            }

            //Check if GameObject is out of map. 
            if (position.X < 0 || position.X > game.tileManager.EndX ||
               position.Y < 0 || position.Y > game.tileManager.EndY)
            {
                throw new Exception("GameObject moved out of the map.");
            }
        }

        public bool CheckCornersWalkable(float posX, float posY)
        {
                    //Upper-right corner
            return (game.tileManager.CheckWalkable((int)posX + Width / 2, (int)posY - Height / 2) &&
                    //Lower-right corner
                    game.tileManager.CheckWalkable((int)posX + Width / 2, (int)posY + Height / 2) &&
                    //Lower-left corner
                    game.tileManager.CheckWalkable((int)posX - Width / 2, (int)posY - Height / 2) &&
                    //Upper-left corner
                    game.tileManager.CheckWalkable((int)posX - Width / 2, (int)posY + Height / 2));

        }
    }
}
