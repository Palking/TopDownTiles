using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public static class InputManager
    {
        //TODO: figure out a more direct way to do it -> many unnecessary steps propably

        //fields
        //TODO: Move to player
        public static float floatDirection; //keeps track of current direction
        //hold information about last keyboardState
        public static KeyboardState lastState;
        public static KeyboardState currState;
        //Configure player inputs. Allows 2 keys to work for one action.
        public static bool Left()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Right()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Up()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Down()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Shoot()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }

        public static void Update()
        {
            //Do this first to set current KeyboardState
            lastState = currState;
            currState = Keyboard.GetState();


            //update floatDirection
            GetDirection();
        }

        public static void LateUpdate()
        {

        }

        public static float GetDirection()
        {
            //north-east
            if ((Up() && Right()) && !(Down() || Left()))
            {
                floatDirection = (MathHelper.Pi / 4) * 7;
            }
            //north-west
            else if ((Up() && Left()) && !(Right() || Down()))
            {
                floatDirection = (MathHelper.Pi / 4) * 5;
            }
            //south-east
            else if ((Right() && Down()) && !(Left() || Up()))
            {
                floatDirection = MathHelper.Pi / 4;
            }
            //south-west
            else if ((Down() && Left()) && !(Right() || Up()))
            {
                floatDirection = (MathHelper.Pi / 4) * 3;
            }
            //north
            else if (Up() && !Down())
            {
                floatDirection = (MathHelper.Pi / 4) * 6;
            }
            //east
            else if (Right() && !Left())
            {
                // equals standard direction
                floatDirection = 0f;
            }
            //south
            else if (Down() && !Up())
            {
                floatDirection = (MathHelper.Pi / 4) * 2;
            }
            //west
            else if (Left() && !Right())
            {
                floatDirection = (MathHelper.Pi / 4) * 4;
            }

            return floatDirection;
        }
    }
}
