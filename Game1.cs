using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Monogame_7___Vectors_and_Rotation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D tankTexture, fireballTexture, shipTexture, rectTexture;
        Rectangle tankRect, window, shipRect;
        MouseState mouseState, prevMouseState;

        float shipAngle, shipSpeed;
        Vector2 shipPosition, shipDirection;
        KeyboardState keyboardState, prevKeyboardState;


        List<Fireball> fireballs;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            fireballs = new List<Fireball>();

            tankRect = new Rectangle(350, 250, 75, 75);
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            shipAngle = 0;
            shipSpeed = 2;
            shipPosition = new Vector2(100,100);

            shipRect = new Rectangle(shipPosition.ToPoint(), new Point(50,50));

            shipDirection = Vector2.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tankTexture = Content.Load<Texture2D>("Images/tank");
            fireballTexture = Content.Load<Texture2D>("Images/fireball");
            shipTexture = Content.Load<Texture2D>("Images/enterprise");
            rectTexture = Content.Load<Texture2D>("Images/rectangle");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //Angular motion
            //if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            //{
            //    fireballs.Add(new Fireball(fireballTexture, tankRect.Center.ToVector2(), mouseState.Position.ToVector2(), 30));
            //}

            //for (int i = 0; i < fireballs.Count; i++)
            //{
            //    fireballs[i].Update();
                
            //    if (!window.Intersects(fireballs[i].Rect))
            //    {
            //        fireballs.RemoveAt(i);
            //        i--;
            //    }
            //}

            //Rotating w/Keyboard

            if (keyboardState.IsKeyDown(Keys.Left) && prevKeyboardState.IsKeyUp(Keys.Left))
            {
                shipAngle -= 0.1f;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && prevKeyboardState.IsKeyUp(Keys.Right))
            {
                shipAngle += 0.1f;
            }

            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(tankTexture, tankRect, Color.White);

            foreach (Fireball fireball in fireballs)
                fireball.Draw(_spriteBatch);


            _spriteBatch.Draw(rectTexture, shipRect, Color.Red); //Just to show where the ship is rotating around....
            _spriteBatch.Draw(shipTexture, new Rectangle(shipRect.Center, shipRect.Size), null, Color.White, shipAngle, new Vector2(shipTexture.Width / 2, shipTexture.Height / 2), SpriteEffects.None, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
