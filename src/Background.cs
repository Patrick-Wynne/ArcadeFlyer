using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Background : Sprite
    {
        // A reference to the game that will contain this enemy
        private ArcadeFlyerGame root;

        // Initialize an enemy
        public Background(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            // Initialize values
            Vector2 offset = new Vector2(1600, 450);
            this.root = root;
            this.position = position - offset;
            this.SpriteWidth = 128.0f;
            // Load the content for this enemy
            LoadContent();
        }

        // Loads all the assets for this enemy
        public void LoadContent()
        {
            // Get the Enemy image
            this.SpriteImage = root.Content.Load<Texture2D>("Background");
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {

        }
    }
}
