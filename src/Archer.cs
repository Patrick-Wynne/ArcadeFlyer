using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ArcadeFlyer2D
{
    // The Game itself
    class Archer : Pathfinder
    {
        private ArcadeFlyerGame root;
        private Timer cooldown;
        public int frameIndex = 0;
        private Timer animationTimer;
        private Random arrowRand;
        public Archer(ArcadeFlyerGame root, Vector2 position, Direction direction) : base(position, root, direction)
        {
            this.position = position;
            this.SpriteWidth = 128f;
            this.root = root;
            cooldown = new Timer(3.0f);
            animationTimer = new Timer(0.2f);
            arrowRand = new Random();
            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Archer");
        }
        
        public override void Update(GameTime gameTime)
        {
            foreach(Enemy b in root.enemies)
            {
                if(Math.Abs(position.X -b.Position.X) < 1000 && !cooldown.Active)
                {
                    float targetPos = b.Position.X+2*b.SpriteWidth*((float)arrowRand.Next(1,20)/10.0f);
                    root.FireProjectile(position+new Vector2(SpriteWidth, 0), position.X, targetPos);
                    cooldown.StartTimer();
                }
            }
            cooldown.update(gameTime);
            if(position.X == targetPosition)
            {
                if(!movementTimer.Active)
                {
                    float rand = (float)pathfinderRandom.Next(-500, 500);
                    targetPosition = position.X + rand;
                    while (targetPosition>1600 || targetPosition<0)
                    {
                        rand = (float)pathfinderRandom.Next(-500, 500);
                        targetPosition = position.X + rand;
                    }
                    rand = (float)pathfinderRandom.Next(5,10);
                    movementTimer = new Timer(rand);
                    movementTimer.StartTimer();
                }
                else
                {
                    movementTimer.update(gameTime);
                }
            }
            else
            {
                animationTimer.update(gameTime);
                if(!animationTimer.Active)
                {
                animationTimer.StartTimer();
                if(frameIndex==2)
                {
                    frameIndex = 0;
                }
                else
                {
                    frameIndex++;
                }   
                }
                if(position.X>targetPosition)
                    {
                        direction = Direction.Right;
                        position.X -= speed;
                    }
                    else
                    {
                        direction = Direction.Left;
                        position.X += speed;
                    }
            }
        }
    }
}