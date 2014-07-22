using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Hnefatafl
{
    public class Tafl : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private TaflBoard _gameBoard;
        private Texture2D _piece;

        public Tafl()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
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

            _gameBoard = new TaflBoard();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _piece = Content.Load<Texture2D>("Piece");
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            const int pieceSize = 45;

            _spriteBatch.Begin();
            for (var x = 0; x < _gameBoard.Positions.GetLength(0); x++)
            {
                for (var y = 0; y < _gameBoard.Positions.GetLength(1); y++)
                {
                    var row = _gameBoard.Positions[x, y];

                    var drawPosX = (x * pieceSize) + 20;
                    var drawPosY = (y * pieceSize) + 20;

                    var colour = Color.Brown;
                    if (row is Defender)
                    {
                        colour = Color.White;
                    }                    
                    else if (row is Attacker)
                    {
                        colour = Color.Black;
                    }
                    else if (row is DefenderKing)
                    {
                        colour = Color.BlanchedAlmond;
                    }

                    _spriteBatch.Draw(_piece, new Rectangle(drawPosX, drawPosY, pieceSize, pieceSize), colour);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }


    public class TaflBoard
    {
        public object[,] Positions { get; set; }

        public TaflBoard()
        {
            Positions = new object[9,9];

            Positions[0, 3] = new Attacker();
            Positions[0, 4] = new Attacker();
            Positions[0, 5] = new Attacker();
            Positions[1, 4] = new Attacker();

            Positions[3, 8] = new Attacker();
            Positions[4, 8] = new Attacker();
            Positions[5, 8] = new Attacker();
            Positions[4, 7] = new Attacker();

            Positions[8, 3] = new Attacker();
            Positions[8, 4] = new Attacker();
            Positions[8, 5] = new Attacker();
            Positions[7, 4] = new Attacker();

            Positions[3, 0] = new Attacker();
            Positions[4, 0] = new Attacker();
            Positions[5, 0] = new Attacker();
            Positions[4, 1] = new Attacker();

            Positions[2, 4] = new Defender();
            Positions[3, 4] = new Defender();
            Positions[5, 4] = new Defender();
            Positions[6, 4] = new Defender();
            Positions[4, 2] = new Defender();
            Positions[4, 3] = new Defender();
            Positions[4, 5] = new Defender();
            Positions[4, 6] = new Defender();

            Positions[4, 4] = new DefenderKing();
        }
    }

    public class Defender
    {
    }

    public class DefenderKing
    {
    }

    public class Attacker
    {
    }
}
