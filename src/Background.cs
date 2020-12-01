using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Background : Sprite
    {
        // A reference to the game that will contain this enemy
        private ArcadeFlyerGame root;

        private string backgroundImage;
        // Initialize an enemy
        public Background(ArcadeFlyerGame root, Vector2 position, string backgroundImage, Vector2 offset) : base(position)
        {
            // Initialize values
            this.root = root;
            this.position = position - offset;
            this.SpriteWidth = 3200.0f;
            this.backgroundImage = backgroundImage;
            // Load the content for this enemy
            LoadContent();
        }

        // Loads all the assets for this enemy
        public void LoadContent()
        {
            // Get the Enemy image
            this.SpriteImage = root.Content.Load<Texture2D>(backgroundImage);
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {
            
        }
    }
}
