
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RC_Framework;

namespace GameMT2
{
    public class MT2 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static public RC_GameStateManager levelManager;

        static public string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\MT\Art\";

        static public int screenWidth = 1920;
        static public int screenHeight = 900;

        static public Texture2D texBack = null;
        static public Texture2D texBlock1 = null;
        static public Texture2D texShip = null;
        static public Texture2D texboom = null;
        static public Texture2D texMissile = null;
        static public Texture2D texTruck = null;
        static public Texture2D texTruck2 = null;
        static public Texture2D texFail = null;
        static public Texture2D texDot = null;
        static public Texture2D texWinning = null;
        static public Rectangle screenRect;

        static public SoundEffect boomSound;
        static public LimitSound limBoomSound;


        static public bool showbb = false;


        public MT2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LineBatch.init(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenRect = new Rectangle(0, 0, screenWidth, screenHeight);

            texBack = Util.texFromFile(GraphicsDevice, dir + "Back4.png");
            texShip = Util.texFromFile(GraphicsDevice, dir + "Spaceship3a.png");
            texBlock1 = Util.texFromFile(GraphicsDevice, dir + "Mountain2a.png");
            texboom = Util.texFromFile(GraphicsDevice, dir + "Boom6.png");
            texMissile = Util.texFromFile(GraphicsDevice, dir + "missile2.png");
            texTruck = Util.texFromFile(GraphicsDevice, dir + "Truck2.png");
            texTruck2 = Util.texFromFile(GraphicsDevice, dir + "Truck3a.png");
            texFail = Util.texFromFile(GraphicsDevice, dir + "gameover.png");
            texDot = Util.texFromFile(GraphicsDevice, dir + "Dot1.png");
            texWinning = Util.texFromFile(GraphicsDevice, dir + "youwin.png");

            boomSound = Content.Load<SoundEffect>("flack");
            limBoomSound = new LimitSound(boomSound, 3);


            levelManager = new RC_GameStateManager();

            levelManager.AddLevel(1, new MainLevel());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();

            levelManager.AddLevel(0, new GameLevel_Intro());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();
            levelManager.setLevel(0);

            levelManager.AddLevel(2, new GameLevel_End());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

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

            RC_GameStateParent.getKeyboardAndMouse();

            if (RC_GameStateParent.keyState.IsKeyDown(Keys.B) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.B))
            {
                showbb = !showbb;
            }

            levelManager.getCurrentLevel().Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            levelManager.getCurrentLevel().Draw(gameTime);

            base.Draw(gameTime);
        }


    }
}