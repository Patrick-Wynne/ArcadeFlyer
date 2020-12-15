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
        private Random spawnRandom;
        public int wave;
        public SpawnManager(ArcadeFlyerGame root, int maxEntities)
        {
            this.root = root;
            spawnTimer = new Timer(10.0f);
            this.maxEntities = maxEntities;
            spawnRandom = new Random();
        }
        public void Update(GameTime gameTime)
        {
                if(!spawnTimer.Active && root.enemies.Count < maxEntities)
                {
                    float rand = (float)spawnRandom.Next(1, 3);
                    if(rand==1)
                    {
                        rand = -1000;
                    }
                    else
                    {
                        rand = 2500;
                    }
                    Console.WriteLine(rand);
                    root.enemies.Add(new Enemy(root, new Vector2(rand, 392), Direction.Right));
                    spawnTimer.StartTimer();
                }
                else
                {
                    spawnTimer.update(gameTime);
                }
        }
        public void newWave()
        {
            wave++;
            maxEntities += (int)maxEntities/2;
        }
    }
}