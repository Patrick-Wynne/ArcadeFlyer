using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace ArcadeFlyer2D
{
    class Projectile : Sprite
    {
        private float initial;
        private float initialY;
        private float target;
        private float speed = 15f;
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
        }
        
        
        
        public Projectile(Vector2 position, Texture2D spriteImage, float initial, float target, float initialY, Direction direction) : base(position, direction)
        {
            this.SpriteWidth = 32.0f;
            this.SpriteImage = spriteImage;
            this.initial = initial;
            this.target = target;
            this.initialY = initialY;
        }

        public void Update(GameTime gameTime)
        {
            if(target<initial)
            {
                speed = (target-initial)/25;
                position.X += speed;
            }
            else
            {
                speed = (initial-target)/25;
                position.X -= speed;
            }

            float zero = position.X;
            float median = (initial+target)/2;
            float k = 300f;
            float a = (initialY-k)/((initial-median)*(initial-median));
            
            position.Y = -2*a*((zero-median)*(zero-median))+k-100;
            if(direction == Direction.Right)
            {
                rotation = (zero-initial)/(target-initial);
            } 
            else
            {
                rotation = -(zero-initial)/(target-initial);
            }
        }
    }
}