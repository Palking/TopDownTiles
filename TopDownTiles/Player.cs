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
        public float ShootDelay { get; set; } = 100;
        public float currShootDelay { get; set; } = 0;
        public float BulletVelocity { get; set; } = 4;

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

        public void Update(GameTime gameTime)
        {

            //MOVEMENT HANDLER
            //move when direction buttons are pressed
            if (InputManager.Left() || InputManager.Right() || InputManager.Up() || InputManager.Down())
            {
                Move(InputManager.floatDirection, speed);
            }

            //turn the player(sprite)
            direction = InputManager.floatDirection;

            //SHOOT HANDLER
            if (InputManager.Shoot() && currShootDelay <= 0)
            {
                Shoot();
            }
            if(currShootDelay > 0)
            {
                currShootDelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(currShootDelay < 0)
                {
                    currShootDelay = 0;
                }
            }
            game.ui.DebugMessage = "Shoot Delay: " + currShootDelay.ToString();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Create a Rectangle
            Rectangle drawRectangle = new Rectangle((int)position.X, (int)position.Y, Width, Height);
            //Draw and rotate our sprite.
            spriteBatch.Draw(texture, drawRectangle, null, Color.White, direction, SpriteCenter, SpriteEffects.None, 0);
        }        

        private void Shoot()
        {
            //Fire the bullet.
            for(int i = 0; i < game.projectiles.Length; i++)
            {
                //Find the first inactive bullet.
                if (!game.projectiles[i].isActive)
                {
                    Projectile currProj = game.projectiles[i];
                    currProj.isActive = true;
                    //Should use player.direction ASAP
                    currProj.direction = InputManager.floatDirection;
                    currProj.velocity = BulletVelocity;


                    currProj.position = position;
                    break;
                }
                if(i == (game.projectiles.Length - 1))
                {
                    throw new Exception("Ran out of bullet space.");
                }
            }

            //Reset cooldown.
            currShootDelay = ShootDelay;
        }
    }
}
