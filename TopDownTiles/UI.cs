using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class UI
    {
        //SpriteBatch spriteBatch;
        SpriteFont basicFont;
        TopDownTiles game;

        public void LoadGame(TopDownTiles current)
        {
            game = current;
        }
        public void LoadContent(ContentManager content)
        {
            basicFont = content.Load<SpriteFont>(@"fonts/BasicFont");
        }
        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                //display PAUSED message
                if (game.paused)
                {
                    spriteBatch.DrawString(basicFont, "PAUSED", new Vector2(350f, 280f), Color.White, 0f, new Vector2(0, 0), 5f, SpriteEffects.None, 1);
                }
                spriteBatch.DrawString(basicFont, "Hello world", game.player.position, Color.White);
            }
            catch
            {
                throw new Exception("Failed to display UI text.");
            }
        }
    }
}
