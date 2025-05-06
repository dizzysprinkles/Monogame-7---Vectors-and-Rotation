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

        Texture2D tankTexture, fireballTexture;
        Rectangle tankRect, window;
        Vector2 tankLocation;
        MouseState mouseState, prevMouseState;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tankTexture = Content.Load<Texture2D>("Images/tank");
            fireballTexture = Content.Load<Texture2D>("Images/fireball");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                fireballs.Add(new Fireball(fireballTexture, tankRect.Center.ToVector2(), mouseState.Position.ToVector2(), 10));
            }

            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update();
                if (!window.Intersects(fireballs[i].Rect))
                {
                    fireballs.RemoveAt(i);
                    i--;
                }
            }

            prevMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(tankTexture, tankRect, Color.White);

            foreach (Fireball fireball in fireballs)
                fireball.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
