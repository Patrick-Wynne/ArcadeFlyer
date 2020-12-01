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
        private float speed = 5f;
        private ProjectileType projectileType;
        public ProjectileType ProjectileType
        {
            get { return projectileType; }
            set { projectileType = value; }
        }
        
        
        public Projectile(Vector2 position, Texture2D spriteImage, ProjectileType projectileType, float initial, float target, float initialY) : base(position)
        {
            this.SpriteWidth = 32.0f;
            this.SpriteImage = spriteImage;
            this.projectileType = projectileType;
            this.initial = initial;
            this.target = target;
            this.initialY = initialY;
            Console.WriteLine("initial "+this.initial);
            Console.WriteLine("target "+this.target);
        }

        public void Update(GameTime gameTime)
        {
            float zero = position.X-initial;
            position.X += speed;
            position.Y = ((zero-initial)*(zero-target))/80+400;
            Console.WriteLine(position.Y);
        }
    }
}