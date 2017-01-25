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
    public class TopDownTiles : Game
    {
        public bool paused { get; set; } = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont basicFont;
        TileManager tileManager = new TileManager();
        CustomMouse mouse = new CustomMouse();
        public Player player { get; } = new Player();
        UI ui = new UI();

        public TopDownTiles()
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
            //player start position
            player.position = new Vector2(285f , 85f);
            ui.LoadGame(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            basicFont = Content.Load<SpriteFont>(@"fonts/BasicFont");
            // TODO: use this.Content to load your game content here
            tileManager.LoadContent(this.Content);
            mouse.LoadContent(this.Content);
            player.LoadContent(this.Content);
            ui.LoadContent(this.Content);

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

            //INPUT HANDLING
            if (!InputManager.currState.IsKeyDown(Keys.P) && InputManager.lastState.IsKeyDown(Keys.P))
            {
                togglePause();
            }

            InputManager.Update();
            mouse.Update();
            base.Update(gameTime);
            // TODO: Add your update logic here
            if (!paused)
            {
                player.Update(tileManager);
            }
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
            //foreground Layer1
            //player Layer2
            player.Draw(spriteBatch);
            //draw mouse Layer3
            mouse.Draw(spriteBatch);

            //draw UI 
            ui.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        void togglePause()
        {
            paused = !paused;
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
    //We probably wont need this. Delete this soon.
    //public enum Direction {north, east, south, west};


}
