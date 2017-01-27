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

        public Projectile(TopDownTiles currGame)
        {
            game = currGame;
            isActive = false;
            Width = 15;
            Height = 15;
            //'Start position'. Shouldnt matter since it will never be displayed or calculated here.
            position = new Vector2(200, 200);
        }

        static public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update()
        {
            Move(direction, speed);
        }
        public void Draw()
        {
            //Create a Rectangle
            Rectangle drawRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            //Draw and rotate our sprite.
            game.spriteBatch.Draw(texture, drawRectangle, null, Color.White, direction, SpriteCenter, SpriteEffects.None, 0);
        }

        public override void Collide()
        {
            isActive = false;
            //base.Collide();
        }
    }
}
