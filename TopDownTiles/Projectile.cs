using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class Projectile : GameObject
    {
        static private string texturePath = @"graphics/player_east";
        static Texture2D texture;
        public bool isActive { get; set; }
        public float dmg { get; set; }

        public Projectile(TopDownTiles currGame)
        {
            game = currGame;
            isActive = false;
            Width = 15;
            Height = 15;
            //'Start position'. Shouldnt matter since it will never be displayed or calculated here.
            position = new Vector2(200, 200);
        }

        static public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Update()
        {
            Move(direction, speed);
        }
        public void Draw()
        {
            Draw(Projectile.texture);
        }

        public override void CollideWithEnemy(Enemy enemy)
        {
            enemy.ReceiveDamage(dmg);
            isActive = false;
        }
        public override void CollideWithTerrain()
        {
            isActive = false;
            //base.Collide();
        }
    }
}
