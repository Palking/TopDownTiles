﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class Player : GameObject
    {
        private string texturePath = @"graphics/player_east";
        //Texture2D texture;
        Texture2D texture;

        //startposition
        private int posX = 250 + WIDTH / 2, posY = 50 + HEIGHT / 2;
        //alternate option, not needed for this code
        //private Vector2 position;
        private static int WIDTH = 35, HEIGHT = 35;
        private Vector2 spriteCenter = new Vector2(WIDTH / 2, HEIGHT / 2);
        int speed = 3;

        //properties
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Vector2 GetCenter()
        {
            float x = (float)posX + (float)WIDTH / 2;
            float y = (float)posY + (float)HEIGHT / 2;
            Vector2 center = new Vector2();
            center.X = x;
            center.Y = y;
            return center;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update(TileManager tileManager)
        {
            Move(tileManager);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRectangle = new Rectangle(posX, posY, WIDTH, HEIGHT);
            //spriteBatch.Draw(textures[directionIndex], drawRectangle, Color.White);

            //rotation
            spriteBatch.Draw(texture, drawRectangle, null, Color.White, InputManager.floatDirection, spriteCenter, SpriteEffects.None, 0);
        }

        //to be called in Update only
        private void Move(TileManager tileManager)
        {

            ////THIS VERSION ONLY WORKS FOR 4 DIRECTIONS & HAS FAST DIAGONAL MOVE
            ////ONLY STAYS FOR REFERENCE UNTIL 8 DIRECTION COLLISION WORKS
            ////move left
            //if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            //{
            //    //turn as soon as button as pressed; even on walls
            //    direction = Direction.west;

            //    //clamping && to check for both limits of the character -> no corner glitching
            //    if (tileManager.CheckWalkable(posX-speed,posY) && tileManager.CheckWalkable(posX - speed, posY + HEIGHT-1))
            //    {
            //        posX -= speed;
            //    }
            //    else
            //    {
            //        if (posX % tileManager.TileSize < speed)
            //        {
            //            //move diffe
            //            posX -= posX % tileManager.TileSize;
            //        }
            //    }
            //}
            ////move right
            //if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    direction = Direction.east;
            //    if (tileManager.CheckWalkable(posX + WIDTH + speed, posY) && tileManager.CheckWalkable(posX + WIDTH + speed, posY + HEIGHT-1))
            //    {
            //        posX += speed;
            //    }
            //    else
            //    {
            //        if (tileManager.TileSize - ((posX + WIDTH) % tileManager.TileSize) <= speed)
            //        {
            //            posX += tileManager.TileSize - ((posX + WIDTH)% tileManager.TileSize);
            //        }
            //    }
            //}
            ////move up
            //if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            //{
            //    direction = Direction.north;
            //    if (tileManager.CheckWalkable(posX, posY - speed) && tileManager.CheckWalkable(posX + WIDTH-1, posY - speed))
            //    {
            //        posY -= speed;
            //    }
            //    else
            //    {
            //        if (posY % tileManager.TileSize < speed)
            //        {
            //            posY -= posY % tileManager.TileSize;
            //        }
            //    }
            //}
            ////move down
            //if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    direction = Direction.south;
            //    if (tileManager.CheckWalkable(posX, posY + HEIGHT + speed) && tileManager.CheckWalkable(posX + WIDTH-1, posY + HEIGHT + speed))
            //    {
            //        posY += speed;
            //    }
            //    else
            //    {
            //        if (tileManager.TileSize - ((posY+HEIGHT) % tileManager.TileSize) <= speed)
            //        {
            //            posY += tileManager.TileSize - ((posY + HEIGHT)% tileManager.TileSize);
            //        }
            //    }
            //}

            //8 direction support; if any move buttons are pressed, then move based on direction
            if (InputManager.Left() || InputManager.Right() || InputManager.Up() || InputManager.Down()
                )
            {
                posX += (int)(Math.Cos((double)InputManager.floatDirection) * speed);
                posY += (int)(Math.Sin((double)InputManager.floatDirection) * speed);
                //collision check

            }

        }
    }
}
