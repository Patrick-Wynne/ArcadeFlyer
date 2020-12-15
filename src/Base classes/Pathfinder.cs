using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ArcadeFlyer2D
{
    // The Game itself
    class Pathfinder : Sprite
    {
        private ArcadeFlyerGame root;
        public Timer movementTimer;
        public Random pathfinderRandom;
        public float speed = 1f;
        public float targetPosition;
                public int frameIndex = 0;
        private Timer animationTimer;
        public Pathfinder(Vector2 position, ArcadeFlyerGame root, Direction direction) : base(position, direction)
        {
            this.position = position;
            movementTimer = new Timer(5.0f);
            pathfinderRandom = new Random();
            targetPosition = position.X;
            animationTimer = new Timer(0.2f);
            this.root = root;
        }
        public virtual void Update(GameTime gameTime)
        {
            /*
            if(position.X == targetPosition)
            {
                if(!movementTimer.Active)
                {
                    float rand = (float)pathfinderRandom.Next(-500, 500);
                    targetPosition = position.X + rand;
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
            */
                if(position.X>root.player.position.X)
                    {
                        position.X -= speed;
                    }
                    else
                    {
                        position.X += speed;
                    }
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
            //}
                }
        }
    }
}