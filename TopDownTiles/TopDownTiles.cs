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
        public SpriteBatch spriteBatch { get; private set; } 
        public TileManager tileManager { get; } = new TileManager();
        public CustomMouse mouse = new CustomMouse();
        public Player player { get; } = new Player();
        public UI ui = new UI();
        public Projectile[] projectiles = new Projectile[50];
        public Camera _camera { get; private set; }
        //Maybe use a list instead?
        public Enemy[] enemies = new Enemy[10];

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
            player.position = new Vector2(300f , 300f);
            ui.LoadGame(this, spriteBatch);
            player.LoadGame(this);
            _camera = new Camera(GraphicsDevice.Viewport, this);

            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = new Projectile(this);
            }

            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].LoadGame(this);
            }
            //Temporal enemy creation
            enemies[0].isActive = true;
            enemies[0].position = new Vector2(500,350);

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
            ui.LoadContent(this.Content);
            Projectile.LoadContent(this.Content);
            Enemy.LoadContent(this.Content);
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

            //Display info, Debug
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                Console.WriteLine("EndX: " + tileManager.EndX.ToString());
                Console.WriteLine("EndY: " + tileManager.EndY.ToString());
            }

            //Should be moved to InputManager.
            //INPUT HANDLING
            if (!InputManager.currState.IsKeyDown(Keys.P) && InputManager.lastState.IsKeyDown(Keys.P))
            {
                togglePause();
            }

            InputManager.Update();
            mouse.Update();
            ui.Update();
            base.Update(gameTime);
            // TODO: Add your update logic here
            if (!paused)
            {
                player.Update(gameTime);

                //Update projectiles.
                foreach (Projectile proj in projectiles)
                {
                    if (proj.isActive)
                    {
                        proj.Update();
                    }
                }

                //Update enemies.
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isActive)
                    {
                        enemy.Update(gameTime);
                    }
                }
            }
            _camera.Update(this);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);

            //background Layer0
            tileManager.Draw(spriteBatch);
            //foreground Layer1
            //player Layer2
            player.Draw();
            foreach (Projectile proj in projectiles)
            {
                if (proj.isActive)
                {
                    proj.Draw();
                }
            }
            foreach(Enemy enemy in enemies)
            {
                if (enemy.isActive)
                {
                    enemy.Draw();
                }
            }
            ui.DrawDynamic();
            spriteBatch.End();
            spriteBatch.Begin();
            //draw UI 
            ui.DrawStatic();
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
}
