using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ArcadeFlyer2D
{
    // The Game itself
    class Target : Sprite
    {
        private ArcadeFlyerGame root;
        public Target(ArcadeFlyerGame root, Vector2 position, Direction direction) : base(position, direction)
        {
            this.position = position;
            this.SpriteWidth = 32.0f;
            this.root = root;
            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("PlayerFire");
        }

    }
}