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
        SpriteBatch spriteBatch;
        SpriteFont basicFont;
        TopDownTiles game;
        public string DebugMessage { get; set; }
        public string DebugMessage2 { get; set; }
        public string DebugMessage3 { get; set; }
        public string DebugMessage4 { get; set; }

        public void LoadGame(TopDownTiles current, SpriteBatch currSpriteBatch)
        {
            game = current;
            spriteBatch = currSpriteBatch;
        }
        public void LoadContent(ContentManager content)
        {
            basicFont = content.Load<SpriteFont>(@"fonts/BasicFont");
        }
        public void Update()
        {
            int inactiveBullets = 0;
            foreach(Projectile proj in game.projectiles)
            {
                if (!proj.isActive)
                {
                    inactiveBullets++;
                }
            }
            DebugMessage3 = "Unused Bullets: " + inactiveBullets.ToString();

            DebugMessage4 = "X: " + game.mouse.position.X + ", " + "Y: " + game.mouse.position.Y;
        }

        public void Draw()
        {
            try
            {
                //display PAUSED message
                if (game.paused)
                {
                    spriteBatch.DrawString(basicFont, "PAUSED", new Vector2(350f, 280f), Color.White, 0f, new Vector2(0, 0), 5f, SpriteEffects.None, 1);
                }
                spriteBatch.DrawString(basicFont, "Player", game.player.position - new Vector2(20,40), Color.Red);
                spriteBatch.DrawString(basicFont, "Debug menu:", new Vector2(10, 5), Color.Black);
                if (DebugMessage != null)
                {
                    spriteBatch.DrawString(basicFont, DebugMessage, new Vector2(10, 25), Color.Black);
                }
                if (DebugMessage2 != null)
                {
                    spriteBatch.DrawString(basicFont, DebugMessage2, new Vector2(10, 45), Color.Black);
                }
                if (DebugMessage3 != null)
                {
                    spriteBatch.DrawString(basicFont, DebugMessage3, new Vector2(10, 65), Color.Black);
                }
                if (DebugMessage4 != null)
                {
                    spriteBatch.DrawString(basicFont, DebugMessage4, new Vector2(10, 85), Color.Black);
                }
            }
            catch
            {
                throw new Exception("Failed to display UI text.");
            }
        }
    }
}
