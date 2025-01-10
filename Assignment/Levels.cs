
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RC_Framework;

namespace Assignment
{
    public class Levels : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static public RC_GameStateManager levelManager;

        static public string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\Assignment\Art\";

        static public int screenWidth = 1400;
        static public int screenHeight = 900;

        static public Texture2D texBack = null;
        static public Texture2D texTower = null;
        static public Texture2D texTowerDead = null;
        static public Texture2D texPlayer = null;
        static public Texture2D texEnemy1 = null;
        static public Texture2D texEnemy2 = null;
        static public Texture2D texEnemy3a = null;
        static public Texture2D texEnemy3b = null;
        static public Texture2D texSword = null;
        static public Texture2D texGrass = null;
        static public Texture2D texBorder = null;
        static public Texture2D texHeart = null;    
        static public Rectangle screenRect;

        static public SpriteFont font;
        static public SoundEffect swordSound;
        static public LimitSound limSwordSound;
        static public SoundEffect clashSound;
        static public LimitSound limClashSound;
        static public SoundEffect hitSound;
        static public LimitSound limHitSound;


        static public bool level2flag = false;
        static public bool showbb = false;


        public Levels()
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
            screenRect = new Rectangle(0, 225, screenWidth, screenHeight);

            font = Content.Load<SpriteFont>("gamefont");

            texTower = Util.texFromFile(GraphicsDevice, dir + "tower.png");
            texTowerDead = Util.texFromFile(GraphicsDevice, dir + "tower_dead.png");
            texPlayer = Util.texFromFile(GraphicsDevice, dir + "redknight.png");
            texSword = Util.texFromFile(GraphicsDevice, dir + "sword.png");
            texEnemy1 = Util.texFromFile(GraphicsDevice, dir + "enemy1.png");
            texEnemy2 = Util.texFromFile(GraphicsDevice, dir + "enemy2.png");
            texEnemy3a = Util.texFromFile(GraphicsDevice, dir + "enemy3a.png");
            texEnemy3b = Util.texFromFile(GraphicsDevice, dir + "enemy3b.png");
            texGrass = Util.texFromFile(GraphicsDevice, dir + "grass.png");
            texBack = Util.texFromFile(GraphicsDevice, dir + "bg.png");
            texBorder = Util.texFromFile(GraphicsDevice, dir + "grassborder.png");
            texHeart = Util.texFromFile(GraphicsDevice, dir + "heart.png");

            

            swordSound = Content.Load<SoundEffect>("sword sound");
            limSwordSound = new LimitSound(swordSound, 3);

            clashSound = Content.Load<SoundEffect>("sword_hit");
            limClashSound = new LimitSound(clashSound, 3);

            hitSound = Content.Load<SoundEffect>("take_hit");
            limHitSound = new LimitSound(hitSound, 3);

            levelManager = new RC_GameStateManager();


            levelManager.AddLevel(0, new GameLevel_Intro());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();
            levelManager.setLevel(0);

            levelManager.AddLevel(1, new MainLevel());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();

            levelManager.AddLevel(6, new transition());
            levelManager.getLevel(6).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(6).LoadContent();

            levelManager.AddLevel(5, new MainLevel2());
            levelManager.getLevel(5).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(5).LoadContent();
            //levelManager.setLevel(5);

            levelManager.AddLevel(2, new pause());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

            levelManager.AddLevel(3, new help());
            levelManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(3).LoadContent();

            levelManager.AddLevel(4, new GameLevel_End());
            levelManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(4).LoadContent();

            levelManager.AddLevel(7, new GameLevel_Win());
            levelManager.getLevel(7).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(7).LoadContent();

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

            if (RC_GameStateParent.keyState.IsKeyDown(Keys.P) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.P))
            {
                levelManager.pushLevel(2);
            }

            if (RC_GameStateParent.keyState.IsKeyDown(Keys.H) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.H))
            {
                levelManager.pushLevel(3);
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