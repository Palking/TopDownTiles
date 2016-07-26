using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace TopDownTiles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileManager tileManager = new TileManager();
        CustomMouse mouse = new CustomMouse();
        Player player = new Player();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            tileManager.LoadContent(this.Content);
            mouse.LoadContent(this.Content);
            player.LoadContent(this.Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputManager.Update();
            player.Update(tileManager);
            mouse.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //background Layer0
            tileManager.Draw(spriteBatch);
            //forground Layer1
            //player Layer2
            player.Draw(spriteBatch);
            //draw mouse Layer3
            mouse.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
    public class TileManager
    {
        static string[] tileTexturePaths = {@"graphics/Tile0", @"graphics/Tile1" };
        Texture2D[] tileTextures = new Texture2D[tileTexturePaths.Length];
        const int TILE_START_X = 200;
        const int TILE_START_Y = 0;
        const int TILE_SIZE = 50;

        //map, to be moved (background child class, maybe even elsewhere?
        int[,] mapTiles = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 0 },
                            { 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
                            { 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0 },
                            { 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public int StartX
        {
            get{ return TILE_START_X; }
        }

        public int StartY
        {
            get { return TILE_START_Y; }
        }

        public int TileSize
        {
            get { return TILE_SIZE; }
        }

        public void LoadContent(ContentManager content)
        {
            for (int i=0; i < tileTexturePaths.Length; i++)
            {
                tileTextures[i] = content.Load<Texture2D>(tileTexturePaths[i]);
            }
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            for(int i = 0; i < mapTiles.GetLength(0); i++)
            {
                for(int j = 0; j < mapTiles.GetLength(1); j++)
                {
                    Rectangle drawRectangle = new Rectangle(TILE_START_X + (j * TILE_SIZE),TILE_START_Y + (i * TILE_SIZE),TILE_SIZE,TILE_SIZE );
                    //find texture by using array's int value as indexer
                    spriteBatch.Draw(tileTextures[mapTiles[i, j]], drawRectangle, Color.White);
                    /*switch (mapTiles[i, j])
                    {
                        case 0:
                            spriteBatch.Draw(tileTextures[0], drawRectangle, Color.White);
                            break;
                        case 1:
                            spriteBatch.Draw(tileTextures[1], drawRectangle, Color.White);
                            break;
                    }*/

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
            if (mapTiles[index2,index1] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class CustomMouse
    {
        static string[] mouseTexturePaths = { @"graphics/mouse" };
        Texture2D[] mouseTextures = new Texture2D[mouseTexturePaths.Length];
        private Vector2 position;
        const int WIDTH = 20;
        const int HEIGHT = 20;

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < mouseTexturePaths.Length; i++)
            {
                mouseTextures[i] = content.Load<Texture2D>(mouseTexturePaths[i]);
            }
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            position.X = mouse.X;
            position.Y = mouse.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //check for hover overs etc. here
            //center the mouse
            Rectangle drawRectangle = new Rectangle((int)position.X - HEIGHT / 2, (int)position.Y - WIDTH / 2, WIDTH, HEIGHT);
            spriteBatch.Draw(mouseTextures[0], drawRectangle, Color.White);
        }
    }
    public enum Direction {north, east, south, west};

    public class Player
    {
        //private string texturePath = @"graphics/player";
        //Texture2D texture;

        static string[] texturePaths = { @"graphics/player_north", @"graphics/player_east", @"graphics/player_south", @"graphics/player_west" };
        Texture2D[] textures = new Texture2D[texturePaths.Length];

        //startposition
        private int posX = 250+WIDTH/2, posY = 50+HEIGHT/2;
        public Direction direction = Direction.east;
        //alternate option, not needed for this code
        //private Vector2 position;
        private static int WIDTH = 35, HEIGHT = 35;
        //
        private Vector2 spriteCenter = new Vector2(WIDTH/2,HEIGHT/2);
        int speed = 3;

        //properties
        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Vector2 GetCenter()
        {
            float x = (float)posX + (float) WIDTH/2;
            float y = (float)posY + (float)HEIGHT / 2;
            Vector2 center = new Vector2();
            center.X = x;
            center.Y = y;
            return center;
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < texturePaths.Length; i++)
            {
                textures[i] = content.Load<Texture2D>(texturePaths[i]);
            }
        }

        public void Update(TileManager tileManager)
        {
            Move(tileManager);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRectangle = new Rectangle(posX, posY, WIDTH, HEIGHT);
            int directionIndex = (int)direction;
            //spriteBatch.Draw(textures[directionIndex], drawRectangle, Color.White);

            //rotation test
            spriteBatch.Draw(textures[0], drawRectangle, null, Color.White, InputManager.floatDirection, spriteCenter, SpriteEffects.None, 0);
        }

        //to be called in Update only
        private void Move(TileManager tileManager)
        {

            ////THIS VERSION ONLY WORKS FOR 4 DIRECTIONS & HAS FAST DIAGONAL MOVE
            ////move left
            //if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            //{
            //    //turn as soon as button as pressed; even on walls
            //    direction = Direction.west;

            //    //clamping && to check for both limits of the character -> no corner glitching
            //    if (tileManager.CheckWalkable(posX-speed,posY) && tileManager.CheckWalkable(posX - speed, posY + HEIGHT-1))
            //    {
            //        posX -= speed;
            //    }
            //    else
            //    {
            //        if (posX % tileManager.TileSize < speed)
            //        {
            //            //move diffe
            //            posX -= posX % tileManager.TileSize;
            //        }
            //    }
            //}
            ////move right
            //if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    direction = Direction.east;
            //    if (tileManager.CheckWalkable(posX + WIDTH + speed, posY) && tileManager.CheckWalkable(posX + WIDTH + speed, posY + HEIGHT-1))
            //    {
            //        posX += speed;
            //    }
            //    else
            //    {
            //        if (tileManager.TileSize - ((posX + WIDTH) % tileManager.TileSize) <= speed)
            //        {
            //            posX += tileManager.TileSize - ((posX + WIDTH)% tileManager.TileSize);
            //        }
            //    }
            //}
            ////move up
            //if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            //{
            //    direction = Direction.north;
            //    if (tileManager.CheckWalkable(posX, posY - speed) && tileManager.CheckWalkable(posX + WIDTH-1, posY - speed))
            //    {
            //        posY -= speed;
            //    }
            //    else
            //    {
            //        if (posY % tileManager.TileSize < speed)
            //        {
            //            posY -= posY % tileManager.TileSize;
            //        }
            //    }
            //}
            ////move down
            //if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    direction = Direction.south;
            //    if (tileManager.CheckWalkable(posX, posY + HEIGHT + speed) && tileManager.CheckWalkable(posX + WIDTH-1, posY + HEIGHT + speed))
            //    {
            //        posY += speed;
            //    }
            //    else
            //    {
            //        if (tileManager.TileSize - ((posY+HEIGHT) % tileManager.TileSize) <= speed)
            //        {
            //            posY += tileManager.TileSize - ((posY + HEIGHT)% tileManager.TileSize);
            //        }
            //    }
            //}

            //8 direction support; if any move buttons are pressed, then move based on direction
            if(Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)||
                Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W)||
                Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)||
                Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)
                )
            {
                //+(MathHelper.Pi*1.5) to turn to the right position
                posX += (int)(Math.Cos((double)InputManager.floatDirection+(MathHelper.Pi*1.5)) * speed);
                posY += (int)(Math.Sin((double)InputManager.floatDirection+(MathHelper.Pi*1.5)) * speed);
                //collision check

            }

        }
    }

    //is it worth it to do an extra InputManager? propably not
    public static class InputManager
    {
        //TODO: figure out a more direct way to do it -> many unnecessary steps propably
        //fields
        public static float floatDirection; //keeps track of current direction


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
        private static bool[] DirectionKeys = new bool[4];


        public static void Update()
        {
            DirectionKeys[0] = Up();
            DirectionKeys[1] = Right();
            DirectionKeys[2] = Down();
            DirectionKeys[3] = Left();
            GetDirection();
        }

        public static float GetDirection()
        {
            //north-east
            if ((DirectionKeys[0]&& DirectionKeys[1])&&!(DirectionKeys[2]|| DirectionKeys[3]))
            {
                floatDirection = MathHelper.Pi / 4;
            }
            //north-west
            else if ((DirectionKeys[0] && DirectionKeys[3]) && !(DirectionKeys[1] || DirectionKeys[2]))
            {
                floatDirection = (MathHelper.Pi / 4) * 7;
            }
            //south-east
            else if ((DirectionKeys[1] && DirectionKeys[2]) && !(DirectionKeys[3] || DirectionKeys[0]))
            {
                floatDirection = (MathHelper.Pi / 4) * 3;
            }
            //south-west
            else if ((DirectionKeys[2] && DirectionKeys[3]) && !(DirectionKeys[1] || DirectionKeys[0]))
            {
                floatDirection = (MathHelper.Pi / 4) * 5;
            }
            //north
            else if (DirectionKeys[0]&&!(DirectionKeys[2]))
            {
                floatDirection = 0f;
            }
            //east
            else if (DirectionKeys[1] && !(DirectionKeys[3]))
            {
                floatDirection = (MathHelper.Pi / 4) * 2;
            }
            //south
            else if (DirectionKeys[2] && !(DirectionKeys[0]))
            {
                floatDirection = (MathHelper.Pi / 4) * 4;
            }
            //west
            else if (DirectionKeys[3] && !(DirectionKeys[1]))
            {
                floatDirection =(MathHelper.Pi / 4) * 6;
            }

            return floatDirection;
        }
    }
}
