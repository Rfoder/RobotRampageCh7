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

namespace RobotRampageCh7
   //Robert foder
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        //pg. 170
        Texture2D spriteSheet;
        Texture2D titleScreen;
        SpriteFont pericles14;
       
        //pg. 184
        //Temp code
        //Sprite tempSprite;
        //Sprite tempSprite2;
        //temp end

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here
            //pg. 170
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 600;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //pg. 170
            spriteSheet = Content.Load<Texture2D>(@"Textures\SpriteSheet");
            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
            pericles14 = Content.Load<SpriteFont>(@"Fonts\Pericles14");
            //pg. 174
            Camera.WorldRectangle = new Rectangle(0, 0, 1600, 1600);
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 600;
           
            //pg. 184
            //temp code
            //tempSprite = new Sprite(
            //    new Vector2(100, 100),
            //    spriteSheet,
            //    new Rectangle(0, 64, 32, 32),
            //    Vector2.Zero);

            //tempSprite2 = new Sprite(
            //    new Vector2(200, 200),
            //    spriteSheet,
            //    new Rectangle(0, 160, 32, 32),
            //    Vector2.Zero);

            //temp end

            //pg. 196
            TileMap.Initialize(spriteSheet);

            //pg. 203
            Player.Initialize(
                spriteSheet,
                new Rectangle(0, 64, 32, 32),
                6,
                new Rectangle(0, 96, 32, 32),
                1,
                new Vector2(300, 300));


            //pg. 227
            EffectsManager.Initialize(
                spriteSheet,
                new Rectangle(0, 288, 2, 2),
                new Rectangle(0, 256, 32, 32),
                3);

            //pg. 231
            WeaponManager.Texture = spriteSheet;

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //pg. 203
            Player.Update(gameTime);
            //pg. 227
            EffectsManager.Update(gameTime);
            //pg. 231
            WeaponManager.Update(gameTime);

            base.Update(gameTime);
        }

            // TODO: Add your update logic here

            //pg. 185
            //temp code
            //Vector2 spriteMove = Vector2.Zero;
            //Vector2 cameraMove = Vector2.Zero;

            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    spriteMove.X = -1;

            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    spriteMove.X = 1;

            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //    spriteMove.Y = -1;

            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    spriteMove.Y = 1;

            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //    cameraMove.X = -1;

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //    cameraMove.X = 1;

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //    cameraMove.Y = -1;

            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //    cameraMove.Y = 1;

            //Camera.Move(cameraMove);
            //tempSprite.Velocity = spriteMove * 60;

            //tempSprite.Update(gameTime);
            //tempSprite2.Update(gameTime);
            //temp end



            //base.Update(gameTime);
       // }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();
    TileMap.Draw(spriteBatch);
    Player.Draw(spriteBatch);
            //pg.227
    EffectsManager.Draw(spriteBatch);
            //pg. 231
    WeaponManager.Draw(spriteBatch);
    spriteBatch.End();

    base.Draw(gameTime);
//}

            // TODO: Add your drawing code here
            //pg. 184
            //spriteBatch.Begin();
            ////pg.197
            //TileMap.Draw(spriteBatch);
            //tempSprite.Draw(spriteBatch);
            //tempSprite2.Draw(spriteBatch);
            //spriteBatch.End();
            ////temp end
            //base.Draw(gameTime);
        }
    }
}

