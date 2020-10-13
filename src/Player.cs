using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ArcadeFlyer2D
{
    class Player
    {
        private float spriteWidth;
        private ArcadeFlyerGame root;
        private Vector2 posititon;
        private Texture2D spriteImage;
        public float SpriteHeight
        {
            get{
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
        }

        public Rectangle PositionRectangle
        {
            get{
                Rectangle rec = new Rectangle((int)posititon.X, (int)posititon.Y, (int)spriteWidth, (int)SpriteHeight);
                return rec;
            }
        }

        public Player(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.posititon = position;
            this.spriteWidth = 128.0f;

            LoadContent();
        }

        public void LoadContent() 
        {
            this.spriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        public void update(GameTime gameTime) 
        {
            //define this
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}