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
        public float uiWidth { get; private set; } = 200;
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
            DebugMessage2 = "X: " + game.mouse.position.X + ", " + "Y: " + game.mouse.position.Y;
        }

        public void DrawStatic()
        {
            //Draw backgroud
            Texture2D uiBackground = new Texture2D(game.GraphicsDevice, (int)uiWidth, game.GraphicsDevice.Viewport.Height);
            Color[] data = new Color[game.GraphicsDevice.Viewport.Height * (int)uiWidth];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            uiBackground.SetData(data);
            Vector2 uiPos = new Vector2(0, 0);
            spriteBatch.Draw(uiBackground, uiPos, Color.White);

            //display PAUSED message
            if (game.paused)
            {
                spriteBatch.DrawString(basicFont, "PAUSED", new Vector2(350f, 280f), Color.White, 0f, new Vector2(0, 0), 5f, SpriteEffects.None, 1);
            }
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

        public void DrawDynamic()
        {
            spriteBatch.DrawString(basicFont, "Player", game.player.position - new Vector2(20, 40), Color.Red);

            foreach(Enemy enemy in game.enemies)
            {
                if (enemy.isActive)
                {
                    spriteBatch.DrawString(basicFont, enemy.HP.ToString() + "/" + enemy.maxHP.ToString(),
                                            enemy.position - new Vector2(20, 40), Color.Red);
                }
            }
        }
    }
}
