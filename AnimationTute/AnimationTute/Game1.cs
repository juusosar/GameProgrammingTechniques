using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace AnimationTute
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\Tutorials\AnimationTuteImages\";
        public static KeyboardState keystate;
        public static KeyboardState prevkeystate;


        Texture2D texfan1;
        Texture2D texfan2;
        Texture2D texfan3;
        Texture2D texFredA;

        Vector2[] anim1;
        Vector2[] anim2;

        public Sprite3 fan = null;
        public Sprite3 fred = null;

       

        public Game1()
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LineBatch.init(GraphicsDevice);
            //string dir = Util.findDirWithFile("Fan1.png");

            texfan1 = Util.texFromFile(GraphicsDevice, dir + "Fan1.png");
            texfan2 = Util.texFromFile(GraphicsDevice, dir + "Fan2.png");
            texfan3 = Util.texFromFile(GraphicsDevice, dir + "Fan3.png");
            texFredA = Util.texFromFile(GraphicsDevice, dir + "fredStrip128x128transA.png");

            fan = new Sprite3(true, texfan1, 100, 100);
            fan.varInt0 = 0;

            fred = new Sprite3(true, texFredA, 300, 100);
            fred.setWidthHeightOfTex(1152, 128);
            fred.setXframes(9);
            fred.setYframes(1);
            fred.setWidthHeight(128, 128);
            fred.setMoveAngleDegrees(180);
            fred.setMoveSpeed(1.2f);
            fred.state = 0;

            anim1 = new Vector2[8];
            anim2 = new Vector2[9];
            /*
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 7; anim[7].Y = 0;
            */

            // init anim1
            for (int i = 0; i <= 7; i++)
            {
                anim1[i].X = i; anim1[i].Y = 0;
            }

            // init anim2
            anim2[0].X = 8; anim2[0].Y = 0;
            anim2[1].X = 8; anim2[1].Y = 0;
            anim2[2].X = 8; anim2[2].Y = 0;
            anim2[3].X = 2; anim2[3].Y = 0;
            anim2[4].X = 8; anim2[4].Y = 0;
            anim2[5].X = 8; anim2[5].Y = 0;
            anim2[6].X = 5; anim2[6].Y = 0;
            anim2[7].X = 8; anim2[7].Y = 0;
            anim2[8].X = 8; anim2[8].Y = 0;

            fred.setAnimationSequence(anim1, 0, 6, 9);
            fred.setAnimationSequence(anim2, 0, 8, 17);
            fred.setAnimFinished(0);
            fred.setPos(500, 100);
            fred.animationStart();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevkeystate = keystate;
            keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.R) && prevkeystate.IsKeyUp(Keys.R))
            {
                fred.state = 1;
                fred.setPos(500, 100);
                fred.setAnimationSequence(anim1, 0, 6, 9);
                fred.animationStart();
            }

            if (keystate.IsKeyDown(Keys.S) && prevkeystate.IsKeyUp(Keys.S))
            {
                fred.state = 0;
                fred.setAnimationSequence(anim2, 0, 8, 17);
                fred.animationStart();
            }
            
            if(fred.state == 1) { fred.moveByAngleSpeed(); }

            int timingVar = 4;

            fan.varInt0 += 1;
            
            if(fan.varInt0 == 1) { fan.setTexture(texfan1, false); }
            if(fan.varInt0 == 1 + timingVar) { fan.setTexture(texfan2, false); }
            if (fan.varInt0 == 1 + 2 * timingVar - 1){ fan.setTexture(texfan3, false); }
            if (fan.varInt0 == 1 + 3 * timingVar) { fan.varInt0 = 0; }

            fred.animationTick(gameTime);
        

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            fan.Draw(spriteBatch);
            fred.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}