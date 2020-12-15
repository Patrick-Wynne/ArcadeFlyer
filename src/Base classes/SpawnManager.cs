using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
namespace ArcadeFlyer2D
{
    class SpawnManager
    {
        private Timer spawnTimer;
        private ArcadeFlyerGame root;
        private float targetPosition;
        private int maxEntities;
        private int currentEntities = 0;
        private Random spawnRandom;
        public int wave = 1;
        public SpawnManager(ArcadeFlyerGame root, int maxEntities)
        {
            this.root = root;
            spawnTimer = new Timer(15/maxEntities);
            this.maxEntities = maxEntities;
            spawnRandom = new Random();
        }
        public void Update(GameTime gameTime)
        {
            if(currentEntities<maxEntities)
            {
                if(!spawnTimer.Active)
                {
                    float rand = (float)spawnRandom.Next(1, 3);
                    Direction tempDirection;
                    if(rand==1)
                    {
                        rand = -1000;
                        tempDirection = Direction.Left;
                    }
                    else
                    {
                        rand = 2500;
                        tempDirection = Direction.Right;
                    }
                    root.enemies.Add(new Enemy(root, new Vector2(rand, 408), tempDirection));
                    currentEntities++;
                    spawnTimer = new Timer(15/maxEntities);
                    spawnTimer.StartTimer();
                }
                else
                {
                    spawnTimer.update(gameTime);
                }
            }
            else
            {
                if(root.enemies.Count == 0)
                {
                    newWave();
                }
            }
        }
        public void newWave()
        {
            wave++;
            maxEntities += (int)maxEntities/4;
            currentEntities = 0;
        }
    }
}