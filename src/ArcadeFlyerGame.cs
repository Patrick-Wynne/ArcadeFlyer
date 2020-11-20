using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // The player
        private Player player;

        // An enemy
        private Enemy enemy;

        private List<Projectile> projectiles;
        private List<Enemy> enemies;

        private Texture2D playerProjectileSprite;
        private Texture2D enemyProjectileSprite;
        private Timer enemyCreationTimer;
        private Background bg;
        // Screen width
        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Screen height
        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            private set { screenHeight = value; }
        }

        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            // Initialize the player to be in the top left
            player = new Player(this, new Vector2(0.0f, 0.0f));

            // Initialize an enemy to be on the right side
            enemy = new Enemy(this, new Vector2(screenWidth, 0));

            projectiles = new List<Projectile>();

            enemies = new List<Enemy>();
            enemies.Add(enemy);
            enemyCreationTimer = new Timer(3.0f);
            bg = new Background(this, new Vector2(screenWidth, screenHeight/2));
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {
            // Update base game
            base.Update(gameTime);

            // Update the components
            player.Update(gameTime);
            bg.Update(gameTime, player.Position);
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                p.Update(gameTime);

                bool isPlayerProjectile = p.ProjectileType == ProjectileType.Player;

                if (!isPlayerProjectile && player.Overlap(p))
                {
                    projectiles.Remove(p);
                }
                else if (isPlayerProjectile)
                {
                    for (int x = enemies.Count - 1; x >= 0; x--)
                    {
                        Enemy e = enemies[x];
                        if (e.Overlap(p))
                        {
                            projectiles.Remove(p);
                            enemies.Remove(e);
                        }
                    }
                }
            }
            if(!enemyCreationTimer.Active)
            {
                Enemy e = new Enemy(this, new Vector2(screenWidth, 0));
                enemies.Add(e);
                enemyCreationTimer.StartTimer();
            }
            enemyCreationTimer.update(gameTime);
        }
        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.White);

            // Start batch draw
            spriteBatch.Begin();
            bg.Draw(gameTime, spriteBatch);
            // Draw the components
            player.Draw(gameTime, spriteBatch);

            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch);
            }

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            // End batch draw
            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            Texture2D texture;
            if (projectileType == ProjectileType.Player)
            {
                texture = playerProjectileSprite;
            }
            else
            {
                texture = enemyProjectileSprite;
            }
            Projectile firedProjectile = new Projectile(position, velocity, texture, projectileType);
            projectiles.Add(firedProjectile);
        }
    }
}