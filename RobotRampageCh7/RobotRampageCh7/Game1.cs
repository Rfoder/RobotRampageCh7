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

        //pg. 285
        enum GameStates {TitleScreen, Playing, WaveComplete, GameOver};
        GameStates gameState = GameStates.TitleScreen;

        float gameOverTimer = 0.0f;
        float gameOverDelay = 6.0f;

        float waveCompleteTimer = 0.0f;
        float waveCompleteDelay = 6.0f;
       
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
            //pg. 259
           // this.IsMouseVisible = true;
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
                //pg. 270
                new Vector2(32, 32));


            //pg. 227
            EffectsManager.Initialize(
                spriteSheet,
                new Rectangle(0, 288, 2, 2),
                new Rectangle(0, 256, 32, 32),
                3);

            //pg. 231
            WeaponManager.Texture = spriteSheet;

            //pg. 270
            GoalManager.Initialize(
                spriteSheet,
                new Rectangle(0, 7 * 32, 32, 32),
                new Rectangle(3 * 32, 7 * 32, 32, 32),
                3,
                1);
            //pg. 286
            //GoalManager.GenerateComputers(10);

            //pg. 279
            EnemyManager.Initialize(
                spriteSheet,
                new Rectangle(0, 160, 32, 32));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        //pg. 286

        private void checkPlayerDeath()
        {
            foreach (Enemy enemy in EnemyManager.Enemies)
            {
                if (enemy.EnemyBase.IsCircleColliding(
                    Player.BaseSprite.WorldCenter,
                    Player.BaseSprite.CollisionRadius))
                {
                    gameState = GameStates.GameOver;
                }
            }
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

            //pg. 286
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if ((GamePad.GetState(PlayerIndex.One).Buttons.A ==
                        ButtonState.Pressed) ||
                        (Keyboard.GetState().IsKeyDown(Keys.Space)))
                    {
                        GameManager.StartNewGame();
                        gameState = GameStates.Playing;
                    }
                    break;

                case GameStates.Playing:
                    //pg. 203
                    Player.Update(gameTime);
                    //pg. 231
                    WeaponManager.Update(gameTime);
                    //pg. 280
                    EnemyManager.Update(gameTime);
                    //pg. 227
                    EffectsManager.Update(gameTime);
                    //pg. 270
                    GoalManager.Update(gameTime);

                    if (GoalManager.ActiveTerminals == 0)
                    {
                        gameState = GameStates.WaveComplete;
                    }
                    break;

                case GameStates.WaveComplete:
                    waveCompleteTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (waveCompleteTimer > waveCompleteDelay)
                    {
                        GameManager.StartNewWave();
                        gameState = GameStates.Playing;
                        waveCompleteTimer = 0.0f;
                    }
                    break;

                case GameStates.GameOver:
                    gameOverTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (gameOverTimer > gameOverDelay)
                    {
                        gameState = GameStates.TitleScreen;
                        gameOverTimer = 0.0f;
                    }
                    break;
            }

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

        if (gameState == GameStates.TitleScreen)
        {
            spriteBatch.Draw(
                titleScreen,
                new Rectangle(0, 0, 800, 600),
                Color.White);
        }
            if (( gameState == GameStates.Playing) ||
                ( gameState == GameStates.WaveComplete) ||
                ( gameState == GameStates.GameOver))
            {
                TileMap.Draw(spriteBatch);
                WeaponManager.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            EnemyManager.Draw(spriteBatch);
            EffectsManager.Draw(spriteBatch);
            GoalManager.Draw(spriteBatch);

            checkPlayerDeath();

            spriteBatch.DrawString(
                pericles14,
                "Score: " + GameManager.Score.ToString(),
                new Vector2(30, 5),
                Color.White);

            spriteBatch.DrawString(
                pericles14,
                "Terminals Remaining: " +
                GoalManager.ActiveTerminals,
                new Vector2(520, 5),
                Color.White);
        }
            if (gameState == GameStates.WaveComplete)
            {
                spriteBatch.DrawString(
                pericles14,
                "Beginning Wave " +
                (GameManager.CurrentWave+1).ToString(),
                new Vector2(300, 300),
                Color.White);
            }
            if (gameState == GameStates.GameOver)
            {
                spriteBatch.DrawString(
                    pericles14,
                    "G A M E  O V E R!",
                    new Vector2(300,300),
                    Color.White);
            }
            spriteBatch.End();
            
    //TileMap.Draw(spriteBatch);
    ////pg. 231
    //WeaponManager.Draw(spriteBatch);
    //Player.Draw(spriteBatch);
    //        //pg. 280
    //EnemyManager.Draw(spriteBatch);
    //        //pg.227
    //EffectsManager.Draw(spriteBatch);
    //    //pg. 271
    //GoalManager.Draw(spriteBatch);


            //Temp code
    //Vector2 mouseLocation = new Vector2(
    //    Mouse.GetState().X, Mouse.GetState().Y);

    //mouseLocation += Camera.Position;

    //List<Vector2> path = PathFinder.FindPath(
    //    TileMap.GetSquareAtPixel(mouseLocation),
    //    TileMap.GetSquareAtPixel(Player.BaseSprite.WorldCenter));

    //if (!(path == null))
    //{
    //    foreach (Vector2 node in path)
    //    {
    //        spriteBatch.Draw(
    //            spriteSheet,
    //            TileMap.SquareScreenRectangle((int)node.X,
    //            (int)node.Y),
    //            new Rectangle(0, 288, 32, 32),
    //            new Color(128, 0, 0, 80));
    //    }
    //}

            //End Temporary code



    //spriteBatch.End();

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

