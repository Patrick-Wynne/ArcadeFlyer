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
        public Player player;


        private List<Projectile> projectiles;
        

        private Texture2D playerProjectileSprite;
        private Texture2D coinOutline;
        private Background bgGround;
        private Background bgFoliage;
        private Background bgSky;
        private List<Coin> coins;
        private List<Interactable> interactables;
        private Interactable treeO;
        private bool inZone = false;
        private SpriteFont textFont;
        private Enemy enemy;
        public List<Enemy> enemies;
        public Archer archer;
        private int coinCount;
        private Target target;
        public List<Archer> archers;
        public bool gameEnd = false;
        // Screen width
        private int screenWidth = 1600;
        SpawnManager spawnManager;
        private Sprite tree1;
        private Sprite tree2;
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
            player = new Player(this, new Vector2(780, 175), new Vector2(ScreenWidth/2, screenHeight/2), Direction.Right);

            // Initialize an enemy to be on the right side

            projectiles = new List<Projectile>();
            bgGround = new Background(this, new Vector2(screenWidth, screenHeight/2), "Background", new Vector2(1600, -70), Direction.Right);
            bgFoliage = new Background(this, new Vector2(screenWidth, screenHeight/2), "BackgroundFoliage", new Vector2(1600, 450), Direction.Right);
            bgSky = new Background(this, new Vector2(screenWidth, screenHeight/2), "BackgroundSky", new Vector2(1600, 450), Direction.Right);
            coins = new List<Coin>();
            interactables = new List<Interactable>();
            treeO = new Interactable(this, new Vector2(780, 240), Direction.Right);
            interactables.Add(treeO);
            enemies = new List<Enemy> ();
            //enemy = new Enemy(this, new Vector2(-10, 392), Direction.Right);
            //enemies.Add(enemy);
            archer = new Archer(this, new Vector2(1000, 408), Direction.Right);
            archers = new List<Archer>();
            archers.Add(archer);
            coinCount = 0;
            spawnManager = new SpawnManager(this, 5);
            target = new Target(this, new Vector2(0,460), Direction.Right);
            tree1 = new Sprite(new Vector2(-140,240), Direction.Right);
            tree1.SpriteImage = Content.Load<Texture2D>("Tree");
            tree2 = new Sprite(new Vector2(1560,240), Direction.Right);
            tree2.SpriteImage = Content.Load<Texture2D>("Tree");
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
            spawnManager.Update(gameTime);
            for (int i = enemies.Count -1; i >= 0; i--)
            {
                if(enemies[i].Overlap(player))
                {
                    gameEnd = true;
                }
                enemies[i].speed = spawnManager.wave*.75f;
                enemies[i].Update(gameTime);
                for (int k = archers.Count -1; k >= 0; k--)
                {
                    if(enemies[i].Overlap(archers[k]))
                    {
                        archers.Remove(archers[k]);
                    }
                }
                
            }

            for (int i = archers.Count -1; i >= 0; i--)
            {
                archers[i].Update(gameTime);
            }
            bgGround.Update(gameTime);
            bgFoliage.Update(gameTime);
            bgSky.Update(gameTime);

            for (int i = interactables.Count -1; i >= 0; i--)
            {
                Interactable inter = interactables[i];
                if(player.upPressed && coinCount>=inter.cost)
                {
                    coinCount -= inter.cost;
                    inter.Upgrade();
                }
                if(inter.Overlap(player))
                {
                    if(!inZone)
                    {
                        Coin coin = new Coin(inter.Position+new Vector2(inter.SpriteWidth/2-coinOutline.Width/2, inter.SpriteHeight-450), coinOutline, Direction.Right);
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

            if(player.drawTarget)
            {
                target.position.X = player.position.X+(screenWidth/2)+player.mouseClickInitial-player.mousePosition;
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                p.Update(gameTime);
                    for (int x = enemies.Count - 1; x >= 0; x--)
                    {
                        Enemy b = enemies[x];
                        if (b.Overlap(p))
                        {
                            projectiles.Remove(p);
                            enemies.Remove(b);
                            coinCount++;
                        }
                    }
            }
            if(gameEnd)
            {
                Console.WriteLine("game over");
                return;
            }
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

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2), 1f, p.Rotation, p.Direction);
            }

            foreach (Interactable i in interactables)
            {
                i.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            }

            foreach (Coin c in coins)
            {
                c.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            }
            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2), e.Direction, e.frameIndex);
            }
            foreach (Archer a in archers)
            {
                a.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2), a.Direction, a.frameIndex);
            }
            if(player.drawTarget)
            {
                target.Draw(gameTime, spriteBatch, player.Position);
            }
            player.Draw(gameTime, spriteBatch, new Vector2(screenWidth/2, screenHeight/2-42));
            tree1.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            tree2.Draw(gameTime, spriteBatch, player.Position - new Vector2(ScreenWidth/2, screenHeight/2));
            spriteBatch.DrawString(textFont, "Coins: "+coinCount, new Vector2(0,0), Color.Black);
            spriteBatch.DrawString(textFont, "wave: "+spawnManager.wave, new Vector2(0,30), Color.Black);
            // End batch draw
            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, float initialPosition, float targetPosition)
        {
            Texture2D texture = playerProjectileSprite;
            Direction direction = Direction.Right;
            if(targetPosition<initialPosition)
            {
                direction = Direction.Left;
            }
            Projectile firedProjectile = new Projectile(position, texture, initialPosition, targetPosition, player.Position.Y, direction);
            projectiles.Add(firedProjectile);
        }
    }
}