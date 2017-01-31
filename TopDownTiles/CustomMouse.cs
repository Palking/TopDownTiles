using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class CustomMouse
    {
        static string[] mouseTexturePaths = { @"graphics/mouse" };
        Texture2D[] mouseTextures = new Texture2D[mouseTexturePaths.Length];
        public Vector2 position { get; private set; }
        const int WIDTH = 20;
        const int HEIGHT = 20;

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < mouseTexturePaths.Length; i++)
            {
                mouseTextures[i] = content.Load<Texture2D>(mouseTexturePaths[i]);
            }
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            position = new Vector2(mouse.X, mouse.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //check for hover overs etc. here
            //center the mouse
            Rectangle drawRectangle = new Rectangle((int)position.X - HEIGHT / 2, (int)position.Y - WIDTH / 2, WIDTH, HEIGHT);
            spriteBatch.Draw(mouseTextures[0], drawRectangle, Color.White);
        }
    }
}
