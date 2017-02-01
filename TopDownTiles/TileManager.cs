using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopDownTiles
{
    public class TileManager
    {
        static string[] tileTexturePaths = { @"graphics/Tile0", @"graphics/Tile1" };
        Texture2D[] tileTextures = new Texture2D[tileTexturePaths.Length];
        public const int TILE_START_X = 0; //200 is normal
        public const int TILE_START_Y = 0;
        private const int TILE_SIZE = 50;

        //map, to be moved (background child class, maybe even elsewhere?
        int[,] mapTiles = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0 },
                            { 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0 },
                            { 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public int EndX
        {
            get
            {
                int val = (mapTiles.GetLength(1) * TILE_SIZE) + TILE_START_X;
                return val;
            }
        }

        public int EndY
        {
            get
            {
                int val = (mapTiles.GetLength(0) * TILE_SIZE) + TILE_START_Y;
                return val;
            }
        }

        public int TileSize
        {
            get { return TILE_SIZE; }
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < tileTexturePaths.Length; i++)
            {
                tileTextures[i] = content.Load<Texture2D>(tileTexturePaths[i]);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapTiles.GetLength(0); i++)
            {
                for (int j = 0; j < mapTiles.GetLength(1); j++)
                {
                    Rectangle drawRectangle = new Rectangle(TILE_START_X + (j * TILE_SIZE), TILE_START_Y + (i * TILE_SIZE), TILE_SIZE, TILE_SIZE);
                    //find texture by using array's int value as indexer
                    spriteBatch.Draw(tileTextures[mapTiles[i, j]], drawRectangle, Color.White);
                }
            }

        }
        //checks the exact type of tyle, returns sprite number
        public int CheckTileType(Vector2 position)
        {
            int index1 = ((int)position.X - TILE_START_X) / TILE_SIZE; //1. index 
            int index2 = ((int)position.Y - TILE_START_Y) / TILE_SIZE; //2. index
            return mapTiles[index1, index2];
        }
        //checks if tile is walkable
        public bool CheckWalkable(int posX, int posY)
        {
            int index1 = (posX - TILE_START_X) / TILE_SIZE; //1. index 
            int index2 = (posY - TILE_START_Y) / TILE_SIZE; //2. index
            if (mapTiles[index2, index1] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
