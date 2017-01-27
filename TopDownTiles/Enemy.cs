using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class Enemy : GameObject
    {
        static private string texturePath = @"graphics/player";
        static Texture2D texture;
        public float maxHP { get; } = 250;
        public float HP { get; private set; }
        public bool isActive { get; set; } 


        public Enemy(TopDownTiles currGame)
        {
            game = currGame;
            Width = 30;
            Height = 30;
            HP = maxHP;

            //Change the default isActive state to false once we get multiple enemies and an enemy array.
            isActive = true;
        }

        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update()
        {
            float randomDir = random.Next(0, 100);

        }

        public void Draw()
        {

        }
    }
}
