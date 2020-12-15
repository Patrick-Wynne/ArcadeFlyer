using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Coin : Sprite
    {
        public Coin(Vector2 position, Texture2D SpriteImage, Direction direction) : base(position, direction)
        {
            this.SpriteImage = SpriteImage;
            this.SpriteWidth = 128.0f;
        }
        
    }
}