using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class Camera
    {
        public Vector2 Position { get; private set; }

        //maybe not needed?
        private const float StartX = 200;
        private const float StartY = 0;
        public Vector2 Offset { get; private set; }

        private readonly Viewport _viewport;
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        //?
        public void Update(TopDownTiles game)
        {
            Position = game.player.position;
            if (InputManager.Shoot())
            {
                float x = Position.X;
                x++;
                Position = new Vector2(x, Position.Y);
            }
            Clamp(game.tileManager);
        }


        //copied stuff
    
        public Camera(Viewport viewport, TopDownTiles game)
        {   
            _viewport = viewport;
            Offset = new Vector2(viewport.Width / 2 * -1, viewport.Height / 2 * -1);
            //Offset = new Vector2(-400, -300);
            Rotation = 0;
            Zoom = 1;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
            //Origin = Vector2.Zero;
            Position = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Position = Vector2.Zero;
        }


            public Matrix GetViewMatrix()
            {
                return
                    Matrix.CreateTranslation(new Vector3(-(Position + Offset), 0.0f)) *
                    Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
            }
        
        public void Clamp(TileManager tileManager)
        {
            if(Position.X - (_viewport.Width /2) < 0)
            {
                Position = new Vector2(_viewport.Width / 2, Position.Y);
            }
            if(Position.X + (_viewport.Width / 2) > tileManager.EndX)
            {
                Position = new Vector2(tileManager.EndX - _viewport.Width / 2, Position.Y);
            }
            if (Position.Y - (_viewport.Height / 2) < 0)
            {
                Position = new Vector2(Position.X, _viewport.Height / 2);
            }
            if (Position.X + (_viewport.Height / 2) > tileManager.EndY)
            {
                Position = new Vector2(Position.X, tileManager.EndY - _viewport.Height / 2);
            }
        }
    }
}
