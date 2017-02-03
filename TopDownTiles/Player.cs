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
        public float ShootDelay { get; set; } = 300;
        public float currShootDelay { get; set; } = 0;
        public float TrippleShotDelay { get; set; } = 2000;
        public float currTrippleShotDelay { get; set; } = 0;
        public float BulletSpeed { get; set; } = 4;
        public float dmg { get; set; } = 50;

        public Player()
        {
            //Set start values.
            Width = 35;
            Height = 35;
            speed = 3;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update(GameTime gameTime)
        {
            game.ui.DebugMessage3 = "X: " + position.X;
            game.ui.DebugMessage4 = "Y: " + position.Y; 
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

            //TRIPLLE SHOT HANDLER
            if(InputManager.TrippleShot() && currTrippleShotDelay <= 0)
            {
                TrippleShot();
            }
            if(currTrippleShotDelay > 0)
            {
                currTrippleShotDelay -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(currTrippleShotDelay < 0)
                {
                    currTrippleShotDelay = 0;
                }
            }
        }

        public void Draw()
        {
            Draw(texture);
        }        

        private void Shoot()
        {
            Shoot(direction);
        }
        private void Shoot(float shotDirection)
        {
            //Fire the bullet.
            for(int i = 0; i < game.projectiles.Length; i++)
            {
                //Find the first inactive bullet.
                if (!game.projectiles[i].isActive)
                {
                    Projectile currProj = game.projectiles[i];
                    currProj.isActive = true;
                    currProj.direction = shotDirection;
                    currProj.speed = BulletSpeed;
                    currProj.dmg = dmg;

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
        public void TrippleShot()
        {
            //Calls delay on normal shoot. Might be alright considering Shoot has low CD and is more of an attack speed type than
            //an actual cooldown. Cant really re-use current version of Shoot() if CD shouldnt be called.
            Shoot();
            float shot1 = direction + (MathHelper.Pi * 1 / 8);
            float shot2 = direction - (MathHelper.Pi * 1 / 8);
            Shoot(shot1);
            Shoot(shot2);
            currTrippleShotDelay = TrippleShotDelay;
        }
    }
}
