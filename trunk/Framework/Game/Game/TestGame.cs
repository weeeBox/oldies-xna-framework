using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using flipstones;
using Flipstones2.app;
using Flipstones2.gfx;
using asap.core;

namespace Flipstones2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestGame : Microsoft.Xna.Framework.Game
    {
        private const int WIDTH = 320;
        private const int HEIGHT = 480;

        GraphicsDeviceManager graphics;
        TestApp app;        

        GamePadState gamePadState;
#if WINDOWS
        KeyboardState keyboardState;
#endif

        bool mousePressed;
        int mouseLastX, mouseLastY;

        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;

            Content.RootDirectory = "Content";            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);

#if WINDOWS
            IsMouseVisible = true;
            keyboardState = Keyboard.GetState();
#endif

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {            
            app = new TestApp(WIDTH, HEIGHT, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            updateInput();
                        
            if (app.isRunning())
            {
                int deltaTime = (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                app.Tick(deltaTime);
            }
            else
            {
                Exit();
            }

            base.Update(gameTime);
        }

        private void updateInput()
        {
            if (mousePressed)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    mousePressed = false;
                    mouseLastX = Mouse.GetState().X;
                    mouseLastY = Mouse.GetState().Y;
                    app.PointerReleased(mouseLastX, mouseLastY);
                }
                else
                {
                    int mouseX = Mouse.GetState().X;
                    int mouseY = Mouse.GetState().Y;
                    if (mouseX != mouseLastX || mouseY != mouseLastY)
                    {
                        app.PointerDragged(mouseX, mouseY);
                    }
                    mouseLastX = mouseX;
                    mouseLastY = mouseY;
                }
            }
            else
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    mousePressed = true;
                    mouseLastX = Mouse.GetState().X;
                    mouseLastY = Mouse.GetState().Y;
                    app.PointerPressed(mouseLastX, mouseLastY);
                }
            }

            // check for back press
            GamePadState oldGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsButtonDown(Buttons.Back) && oldGamePadState.IsButtonUp(Buttons.Back))
            {
                backPressed();
            }

            // check windows key press
#if WINDOWS
            KeyboardState oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                backPressed();
            }
#endif
        }

        private void backPressed()
        {
            app.BackPressed();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            app.Draw(GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}
