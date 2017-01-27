using Microsoft.Xna.Framework;
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
        Texture2D texture;
        int speed = 3;

        public Player()
        {
            //Set start values.
            Width = 35;
            Height = 35;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update()
        {
            Move(game.tileManager);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Create a Rectangle
            Rectangle drawRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            //Draw and rotate our sprite.
            spriteBatch.Draw(texture, drawRectangle, null, Color.White, InputManager.floatDirection, SpriteCenter, SpriteEffects.None, 0);
        }

        //to be called in Update only
        private void Move(TileManager tileManager)
        {
            //8 direction support; if any move buttons are pressed, then move based on direction
            if (InputManager.Left() || InputManager.Right() || InputManager.Up() || InputManager.Down()
                )
            {
                Move(InputManager.floatDirection, speed);
            }
        }
    }
}
