using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ArcadeFlyer2D
{
    class Projectile : Sprite
    {
        private Vector2 velocity;
        private ArcadeFlyerGame root;
        public Projectile(Vector2 position, Vector2 velocity, Texture2D spriteImage) : base(position)
        {
            this.velocity = velocity;
            this.SpriteWidth = 32.0f;
            this.SpriteImage = spriteImage;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
        }
    }
}