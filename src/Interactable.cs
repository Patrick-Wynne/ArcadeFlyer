using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Interactable : Sprite
    {
        private ArcadeFlyerGame root;
        int cost;
        public Interactable(ArcadeFlyerGame root, Vector2 position) : base(position)
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
    }
}