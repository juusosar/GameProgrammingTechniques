using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using RC_Framework;
using System.Collections.Specialized;
using Microsoft.Xna.Framework.Audio;

namespace BreakoutGame
{
    public class Breakout : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static public Texture2D texBack = null;
        static public Texture2D texpaddle = null;
        static public Texture2D texball = null;
        static public Texture2D texblock1 = null;
        static public Texture2D texwinning = null;
        static public Texture2D texboom = null;
        static public SpriteList booms = null;
        

        static public SoundEffect boomSound;
        static public LimitSound limBoomSound;


        static public string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\BreakoutGame\";
        
        static public Rectangle playArea;
        static public bool showbb = false;

        static public RC_GameStateManager levelManager;



        public Breakout()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            LineBatch.init(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texBack = Util.texFromFile(GraphicsDevice, dir + "back3.png");
            texball = Util.texFromFile(GraphicsDevice, dir + "Ball2.png");
            texpaddle = Util.texFromFile(GraphicsDevice, dir + "red64x32.png");
            texblock1 = Util.texFromFile(GraphicsDevice, dir + "white64x32.png");
            texwinning = Util.texFromFile(GraphicsDevice, dir + "youwin.png");
            texboom = Util.texFromFile(GraphicsDevice, dir + "Boom3.png");


            boomSound = Content.Load<SoundEffect>("flack");
            limBoomSound = new LimitSound(boomSound, 3);

            levelManager = new RC_GameStateManager();

            levelManager.AddLevel(0, new playLevel()); // note play level is level 0
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();

            levelManager.AddLevel(1, new splashScreen()); // note splash screen is level 1
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();
            levelManager.setLevel(1);

            levelManager.AddLevel(2, new pause()); // note pause screen is level 2
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();



        }

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


            levelManager.getCurrentLevel().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            levelManager.getCurrentLevel().Draw(gameTime);

            base.Draw(gameTime);
        }

        


    }

}