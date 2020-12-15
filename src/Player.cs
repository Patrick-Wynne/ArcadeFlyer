using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ArcadeFlyer2D
{
    // The player, controlled by the keyboard
    class Player : Sprite
    {
        // A reference to the game that will contain the player
        private ArcadeFlyerGame root;

        // The speed at which the player can move
        private float movementSpeed = 4.0f;
        private Timer coolDownTimer;
        private bool isUp = false;
        private bool isUp2 = false;
        private int frameIndex = 0;
        private Timer animationTimer;
        public bool upPressed;
        public bool mouseDown = false;
        public bool mouseUp = false;
        public float mouseClickInitial;
        private float mouseClickFinal;
        public bool drawTarget;
        public float mousePosition;
        // Initialize a player
        public Player(ArcadeFlyerGame root, Vector2 position, Vector2 screenCenter, Direction direction) : base(position, direction)
        {
            // Initialize values
            this.root = root;
            this.position = position;
            this.SpriteWidth = 128.0f;
            animationTimer = new Timer(0.2f);
            coolDownTimer = new Timer(0.5f);
            // Load the content for the player
            LoadContent();
        }

        // Loads all the assets for the player
        public void LoadContent()
        {
            // Get the MainChar image
            this.SpriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        // Update position based on input
        private void HandleInput(KeyboardState currentKeyboardState, GameTime gameTime, MouseState mouseState)
        {
            // Get all the key states
            bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.W);
            bool downKeyPressed = currentKeyboardState.IsKeyDown(Keys.S);
            bool leftKeyPressed = currentKeyboardState.IsKeyDown(Keys.A);
            bool rightKeyPressed = currentKeyboardState.IsKeyDown(Keys.D);
            bool spaceKeyPressed = currentKeyboardState.IsKeyDown(Keys.Space);
            bool spaceKeyUp = currentKeyboardState.IsKeyUp(Keys.Space);
            bool upKeyUp = currentKeyboardState.IsKeyUp(Keys.W);
            bool isMouseDown = mouseState.LeftButton == ButtonState.Pressed;
            bool isMouseUp = mouseState.LeftButton == ButtonState.Released;
            // If Up is pressed, decrease position Y
            if (upKeyPressed && isUp2)
            {
                isUp2 = false;
                upPressed = true;
                //position.Y -= movementSpeed;
            }
            else
            {
                upPressed = false;
            }
            if(upKeyUp)
            {
                isUp2 = true;
            }
            
            if(isMouseDown && !mouseDown)
            {
                drawTarget = true;
                mouseUp = false;
                mouseDown = true;
                mouseClickInitial = Mouse.GetState(root.Window).X;
            }

            if(isMouseUp)
            {
                drawTarget = false;
                if(!mouseUp)
                {
                    mouseUp = true;
                    mouseClickFinal = Mouse.GetState(root.Window).X;
                    calculateProjectile();
                }

                mouseDown = false;

            }
            
            // If Left is pressed, decrease position X
            if (leftKeyPressed)
            {
                
                if(position.X - movementSpeed>=0)
                {
                    position.X -= movementSpeed;
                    animationTimer.update(gameTime);
                }
                
                direction = Direction.Left;
            }
            
            // If Right is pressed, increase position X
            if (rightKeyPressed)
            {
                if(position.X + movementSpeed<=1600)
                {
                    position.X += movementSpeed;
                    animationTimer.update(gameTime);
                }
                direction = Direction.Right;
            }
            /*
            if(spaceKeyUp)
            {
                isUp = true;
            }

            if (spaceKeyPressed && !coolDownTimer.Active && isUp)
            {
                
            }
            */
        }

        // Called each frame
        public void Update(GameTime gameTime)
        {   
            mousePosition = Mouse.GetState(root.Window).X;
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

            // Get current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();
            coolDownTimer.update(gameTime);
            // Handle any movement input
            MouseState currentMouseState = Mouse.GetState();
            HandleInput(currentKeyboardState, gameTime, currentMouseState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 screenCenter)
        {
            //spriteBatch.Draw(SpriteImage, screenCenter, Color.White);
            Rectangle frame = new Rectangle(0, 128*frameIndex, 128, 128);
            if(direction == Direction.Right)
            {
                spriteBatch.Draw(SpriteImage, screenCenter, frame, Color.White, 0, new Vector2(16,16), new Vector2(1,1), SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(SpriteImage, screenCenter, frame, Color.White, 0, new Vector2(16,16), new Vector2(1,1), SpriteEffects.None, 0f);
            }
        }

        private void calculateProjectile()
        {
            if(!coolDownTimer.Active)
            {
                float targetPosition = position.X+mouseClickInitial-mouseClickFinal;
                    Vector2 projectilePosition = new Vector2(position.X, position.Y);
                    //float targetPosition = position.X + root.ScreenWidth/2 - (Mouse.GetState(root.Window).X);
                    root.FireProjectile(projectilePosition, projectilePosition.X, targetPosition);
                    coolDownTimer.StartTimer();
            }

        }

    }
}
