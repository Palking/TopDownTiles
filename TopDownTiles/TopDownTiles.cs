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
        public SpriteBatch spriteBatch { get; set; } 
        SpriteFont basicFont;
        public TileManager tileManager { get; } = new TileManager();
        CustomMouse mouse = new CustomMouse();
        public Player player { get; } = new Player();
        public UI ui = new UI();
        public Projectile[] projectiles = new Projectile[50];

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
            ui.LoadGame(this, spriteBatch);
            player.LoadGame(this);
            //TODO create all projectiles here (and set them inactive)
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
            Projectile.LoadContent(this.Content);

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
                player.Update();
            }
            foreach(Projectile proj in projectiles)
            {
                if(proj.isActive)
                {
                    proj.Update();
                }
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
            foreach (Projectile proj in projectiles)
            {
                if (proj.isActive)
                {
                    proj.Draw();
                }
            }
            //draw UI 
            ui.Draw();
            //draw mouse
            mouse.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        void togglePause()
        {
            paused = !paused;
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
