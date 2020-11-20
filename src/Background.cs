using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Background : Sprite
    {
        // A reference to the game that will contain this enemy
        private ArcadeFlyerGame root;
        private Vector2 initialPosition;

        // Initialize an enemy
        public Background(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            // Initialize values
            this.root = root;
            this.position = position;
            this.SpriteWidth = 128.0f;
            initialPosition = position;
            // Load the content for this enemy
            LoadContent();
        }

        // Loads all the assets for this enemy
        public void LoadContent()
        {
            // Get the Enemy image
            this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
        }

        // Called each frame
        public void Update(GameTime gameTime, Vector2 playerPosition)
        {
            position.X = initialPosition.X - playerPosition.X;

        }
    }
}
