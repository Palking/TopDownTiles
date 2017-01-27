using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class Projectile : GameObject
    {
        static private string texturePath = @"graphics/player_east";
        static Texture2D texture;
        public bool isActive { get; set; }

        public Projectile()
        {
            isActive = false;
            Width = 15;
            Height = 15;
            position = new Vector2(0, 0);
        }

        static public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update()
        {
            Move(direction, velocity);
        }
        public void Draw()
        {
            //Create a Rectangle
            Rectangle drawRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            //Draw and rotate our sprite.
            game.spriteBatch.Draw(texture, drawRectangle, null, Color.White, InputManager.floatDirection, SpriteCenter, SpriteEffects.None, 0);
        }
    }
}
