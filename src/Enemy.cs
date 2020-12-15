using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ArcadeFlyer2D
{
    // The Game itself
    class Enemy : Pathfinder
    {
        private ArcadeFlyerGame root;
        public Enemy(ArcadeFlyerGame root, Vector2 position, Direction direction) : base(position, root, direction)
        {
            this.position = position;
            this.SpriteWidth = 128f;
            this.root = root;
            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
        }

    }
}