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
        private Texture2D coinOutline;
        private Timer enemyCreationTimer;
        private Background bgGround;
        private Background bgFoliage;
        private Background bgSky;
        private List<Coin> coins;
        private List<Interactable> interactables;
        private Interactable treeO;
        private bool inZone = false;
        private SpriteFont textFont;
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

            Vector2 screenCenter = new Vector2(screenWidth/2, screenHeight/2);


            // Make mouse visible
            IsMouseVisible = true;

            // Initialize the player to be in the top left
            player = new Player(this, new Vector2(0.0f, 175), new Vector2(ScreenWidth/2, screenHeight/2));

            // Initialize an enemy to be on the right side
            enemy = new Enemy(this, new Vector2(screenWidth, 0));

            projectiles = new List<Projectile>();

            enemies = new List<Enemy>();
            enemies.Add(enemy);
            enemyCreationTimer = new Timer(3.0f);
            bgGround = new Background(this, new Vector2(screenWidth, screenHeight/2), "Background", new Vector2(1600, -70));
            bgFoliage = new Background(this, new Vector2(screenWidth, screenHeight/2), "BackgroundFoliage", new Vector2(1600, 450));
            bgSky = new Background(this, new Vector2(screenWidth, screenHeight/2), "BackgroundSky", new Vector2(1600, 450));
            coins = new List<Coin>();
            interactables = new List<Interactable>();
            treeO = new Interactable(this, new Vector2(100, 240));
            interactables.Add(treeO);
            
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
            coinOutline = Content.Load<Texture2D>("CoinOutline");

            textFont = Content.Load<SpriteFont>("Text");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {
            // Update base game
            base.Update(gameTime);
            // Update the components
            player.Update(gameTime);
            bgGround.Update(gameTime);
            bgFoliage.Update(gameTime);
            bgSky.Update(gameTime);
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);
            }

            for (int i = interactables.Count -1; i >= 0; i--)
            {
                Interactable inter = interactables[i];
                
                if(inter.Overlap(player))
                {
                    if(!inZone)
                    {
                        Coin coin = new Coin(inter.Position+new Vector2(inter.SpriteWidth/2-coinOutline.Width/2, inter.SpriteHeight-450), coinOutline);
                        coins.Add(coin);
                        inZone = true;
                    }
                }
                else
                {
                    inZone = false;
                    coins = new List<Coin>();
                    inZone = false;
                }
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
            bgGround.Draw(gameTime, spriteBatch, player.Position, 1.0f);
            bgSky.Draw(gameTime, spriteBatch, player.Position, 0.3f);
            bgFoliage.Draw(gameTime, spriteBatch, player.Position, 0.8f);
            // Draw the components
            player.Draw(gameTime, spriteBatch, new Vector2(screenWidth/2, screenHeight/2-58));
            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2), 1.0f);
            }

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            }

            foreach (Interactable i in interactables)
            {
                i.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            }

            foreach (Coin c in coins)
            {
                c.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            }

            spriteBatch.DrawString(textFont, "hello", new Vector2(0,0), Color.Blue);
            // End batch draw
            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, ProjectileType projectileType, float initialPosition, float targetPosition)
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
            Projectile firedProjectile = new Projectile(position, texture, projectileType, initialPosition, targetPosition, player.Position.Y);
            projectiles.Add(firedProjectile);
        }
    }
}