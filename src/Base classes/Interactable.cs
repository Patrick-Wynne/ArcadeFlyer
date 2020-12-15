using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Interactable : Sprite
    {
        private ArcadeFlyerGame root;
        public int cost = 0;
        private int level;
        public Interactable(ArcadeFlyerGame root, Vector2 position, Direction direction) : base(position, direction)
        {
            this.SpriteWidth = 280.0f;
            this.root = root;
            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Tree");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Upgrade()
        {
            level++;
        }
    }
}