using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class GameObject
    {
        public TopDownTiles game;
        public static readonly Random random = new Random();
        public Vector2 position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float direction { get; set; }
        public float speed { get; set; }

        //TODO implement generic constructor.

        public Vector2 SpriteCenter
        {
            get
            {
                return new Vector2(Width / 2, Height / 2);
            }
        }

        public Rectangle RectangleHitbox
        {
            get
            {
                return new Rectangle((int)position.X - Width / 2, (int)position.Y - Height / 2, Width, Height);
            }
        }

        public void LoadGame(TopDownTiles currGame)
        {
            game = currGame;
        }


        public void Move(float currDirection, float speed) {

            Move(currDirection, speed, true);
        }


        //Needs texture if the child class doesnt have a texture for each single implementation. (i.e. Projectiles).
        public virtual void Draw(Texture2D currTexture, Color color)
        {
            //Create a Rectangle
            Rectangle drawRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            //Draw and rotate our sprite.
            game.spriteBatch.Draw(currTexture, drawRectangle, null, color, direction, SpriteCenter, SpriteEffects.None, 0);
        }
        public virtual void Draw(Texture2D currTexture)
        {
            Draw(currTexture, Color.White);
        }



        public void Move(float currDirection, float speed, bool collides)
        {
            var currX = position.X;
            var currY = position.Y;

            //Evaluate collisions
            if (collides)
            {
                currX += (float)(Math.Cos((double)currDirection) * speed);
                currY += (float)(Math.Sin((double)currDirection) * speed);

                //Colliding is pretty abstract at a high level. Inheriting classes must implement own version.
                if (!CheckCornersWalkable(currX, currY))
                {
                    CollideWithTerrain();
                }

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
                currX += (int)(Math.Cos((double)currDirection) * speed);
                currY += (int)(Math.Sin((double)currDirection) * speed);
                //Set new position.
                position = new Vector2(currX, currY);
            }

            //Check for collision with Entities.
            foreach (Projectile proj in game.projectiles)
            {
                if (proj.isActive)
                {
                    if (this.RectangleHitbox.Intersects(proj.RectangleHitbox))
                    {
                        if (proj != this)
                        {
                            CollideWithProjectile();
                        }
                    }
                }
            }

            foreach (Enemy enemy in game.enemies)
            {
                if (enemy.isActive)
                {
                    if (this.RectangleHitbox.Intersects(enemy.RectangleHitbox))
                    {
                        if (enemy != this)
                        {
                            CollideWithEnemy(enemy);
                        }
                    }
                }
            }

            //Check if GameObject is out of map. 
            if (position.X < 0 || position.X > game.tileManager.EndX ||
               position.Y < 0 || position.Y > game.tileManager.EndY)
            {
                throw new Exception("GameObject moved out of the map.");
            }
        }

        //Bugged. Keeping it for later.
        //public void MoveTowards(Vector2 target)
        //{
        //    //1. get x and y difference
        //    //2. set direction; probably 8 checks done by myself? could also make enemy 'smarter' by allowing every possible direction
        //    Vector2 difference = target - position;
        //    float newDir = (float) Math.Atan2((double)difference.X, (double)difference.Y) - (float)(Math.PI / 4);
        //    Move(newDir, speed);
        //}

        public virtual void CollideWithTerrain()
        {
            //Console.WriteLine("Something collided.");
        }

        public virtual void CollideWithEnemy(Enemy enemy)
        {

        }

        public virtual void CollideWithProjectile()
        {

        }
        /// <summary>
        /// Checks for the current GameObject if all for borders are at walkable tiles.
        /// </summary>
        /// <param name="posX">X position of the center.</param>
        /// <param name="posY">Y position of the center.</param>
        /// <returns>Returns true if all for are walkable. False if atleast one isnt.</returns>
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
