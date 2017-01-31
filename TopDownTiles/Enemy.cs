using Microsoft.Xna.Framework;
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


        private double SecondsUntilTurn = 0.5;
        private double SecondsSinceTurn { get; set; } = 0;

        //ISSUE: Width and Height is 0 when Draw() is called.
        //ANSWER:   Apparently the reset was caused because i made an error in the constructor overload?
        //          Should look up constructor overload again.

        //ISSUE: Load game in constructor or LoadGame()? Constructor doesnt seem to work all the time?
        //ANSWER: Probably the same as the one above. Test it out later on.
        public Enemy()
        {
            position = new Vector2(0,0);
            speed = 1;
            Width = 30;
            Height = 30;
            HP = maxHP;
            isActive = false;
        }


        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update(GameTime gameTime)
        {
            if (SecondsSinceTurn >= SecondsUntilTurn)
            {
                float maxRotation = (MathHelper.Pi / 4);
                float randomDir = random.Next(-10, 10);
                direction += ((float)randomDir / 10f) * maxRotation;
                SecondsSinceTurn = 0;
            }
            else
            {
                SecondsSinceTurn += gameTime.ElapsedGameTime.TotalSeconds;
            }
            game.ui.DebugMessage3 = SecondsSinceTurn.ToString();
            Move(direction, speed);

        }
        public override void CollideWithTerrain()
        {
            base.CollideWithTerrain();
            direction += MathHelper.Pi;
        }

        public override void CollideWithProjectile()
        {
            base.CollideWithProjectile();
            this.isActive = false;
        }
        public void Draw()
        {
            if (game != null)
            {
                Draw(texture, Color.Black);
            }
            else
            {
                Console.WriteLine("Enemy.game was null.");
            }
        }
    }
}
