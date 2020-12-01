using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArcadeFlyer2D
{
    // Sprite base class, contains basic sprite functionality
    class Sprite
    {
        // The current position of the sprite
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        // An image texture for the sprite
        private Texture2D spriteImage;
        public Texture2D SpriteImage
        {
            get { return spriteImage; }
            set { spriteImage = value; }
        }
        // The width of the sprite
        private float spriteWidth;
        public float SpriteWidth
        {
            get { return spriteWidth; }
            set { spriteWidth = value; }
        }

        // The height of the sprite
        public virtual float SpriteHeight
        {
            get
            {
                // Calculated based on the width
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }

        // The properly scaled position rectangle for the sprite
        public Rectangle PositionRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
            }
        }
        
        // Initialize a sprite
        public Sprite(Vector2 position)
        {
            // Initialize values
            this.position = position;
        }

        // Draw the sprite
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 playerPosition)
        {
            Vector2 drawPosition = new Vector2(position.X-playerPosition.X, position.Y);
            spriteBatch.Draw(spriteImage, drawPosition, Color.White);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 playerPosition, float depthMultiplier)
        {
            Vector2 drawPosition = new Vector2(position.X-(playerPosition.X*depthMultiplier), position.Y);
            spriteBatch.Draw(spriteImage, drawPosition, Color.White);
        }

        public bool Overlap(Sprite otherSprite)
        {
            bool doesOverlap = this.PositionRectangle.Intersects(otherSprite.PositionRectangle);
            return doesOverlap;
        }
    }
}