using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using RC_Framework;
using System.Collections.Specialized;

namespace BreakoutGame
{
    public class Breakout_W3 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D texBack = null;
        Texture2D texpaddle = null;
        Texture2D texball = null;
        Texture2D texblock1 = null;
        Texture2D texwinning = null;
        ImageBackground back1 = null;

        string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\BreakoutGame\";
        Sprite3 paddle = null;
        Sprite3 ball = null;
        Sprite3 winning = null;

        SpriteList spriteList = null;
        int blocksOffsetX = 30;
        int blocksOffsetY = 30;

        bool ballStuck = true;
        Vector2 ballOffset = new Vector2(32, -10);

        Rectangle playArea;
        bool showbb = false;
        
        KeyboardState k;
        KeyboardState prevK;

        int paddleSpeed = 4;
        int top = 56;
        int lhs = 236;
        int rhs = 564;
        int bot = 543;
        float xx = 360;
        float yy = 543;


        public Breakout_W3()
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

            // TODO: use this.Content to load your game content here

            texBack = Util.texFromFile(GraphicsDevice, dir + "back3.png");
            texball = Util.texFromFile(GraphicsDevice, dir + "Ball2.png");
            texpaddle = Util.texFromFile(GraphicsDevice, dir + "red64x32.png");
            texblock1 = Util.texFromFile(GraphicsDevice, dir + "white64x32.png");
            texwinning = Util.texFromFile(GraphicsDevice, dir + "youwin.png");

            paddle = new Sprite3(true, texpaddle, xx, yy);
            paddle.setBBToTexture();

            ball = new Sprite3(true, texball, xx, yy);
            ball.setBBandHSFractionOfTexCentered(0.7f);

            winning = new Sprite3(false, texwinning, 50, 200);
            

            back1 = new ImageBackground(texBack, Color.White, GraphicsDevice);
            playArea = new Rectangle(lhs, top, rhs - lhs, bot - top);

            spriteList = new SpriteList();
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 4; x++)
                {
                    Sprite3 s = new Sprite3(true, texblock1, x * 68 + playArea.X + blocksOffsetX, y * 36 + playArea.Y + blocksOffsetY);
                    s.hitPoints = 1;
                    
                    if (y == 0)
                    {
                        s.hitPoints = 2;
                        s.setColor(Color.LightBlue);
                    }
                    spriteList.addSpriteReuse(s);
                }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevK = k;
            k = Keyboard.GetState();

            if (k.IsKeyDown(Keys.B) && prevK.IsKeyUp(Keys.B))
            {
                showbb = !showbb;
            }

            if (k.IsKeyDown(Keys.Right))
            {
                if (paddle.getPosX() < rhs - texpaddle.Width)
                {
                    paddle.setPosX(paddle.getPosX() + paddleSpeed);
                }
            }
            if (k.IsKeyDown(Keys.Left))
            {
                if (paddle.getPosX() > lhs)
                {
                    paddle.setPosX(paddle.getPosX() - paddleSpeed);
                }
            }

            if (ballStuck)
            {
                ball.setPos(paddle.getPos() + ballOffset);
                if (k.IsKeyDown(Keys.Space) && prevK.IsKeyUp(Keys.Space))
                {
                    ballStuck = false;
                    ball.setDeltaSpeed(new Vector2(2, -3));
                }
            } else
            {
                //move ball
                ball.savePosition();
                ball.moveByDeltaXY();
                Rectangle ballbb = ball.getBoundingBoxAA();

                if (ballbb.X + ball.getWidth() > rhs)
                {
                    ball.setDeltaSpeed(ball.getDeltaSpeed() * new Vector2(-1, 1));
                }

                if (ballbb.X < lhs)
                {
                    ball.setDeltaSpeed(ball.getDeltaSpeed() * new Vector2(-1, 1));
                }

                if (ballbb.Y < top)
                {
                    ball.setDeltaSpeed(ball.getDeltaSpeed() * new Vector2(1, -1));
                }

                if (ballbb.Intersects(paddle.getBoundingBoxAA()))
                {
                    ball.setDeltaSpeed(ball.getDeltaSpeed() * new Vector2(1, -1));
                }


                Rectangle ballbbnow = ball.getBoundingBoxAA();

                int rc = spriteList.collisionWithRect(ballbbnow);
                if (rc != -1)
                {
                    ball.setDeltaSpeed(ball.getDeltaSpeed() * new Vector2(1, -1)); // reflect the ball
                    Sprite3 temp = spriteList.getSprite(rc);
                    temp.hitPoints = temp.hitPoints - 1;

                    if (temp.hitPoints <= 0)
                    {
                        temp.active = false;
                        temp.visible = false;
                    }
                    else
                    {
                        temp.setColor(Color.Salmon); // change its colour
                    }
                }
            }

            int count = spriteList.countActive();
            if (count <= 0)
            {
                ball.setDeltaSpeed(new(0, 0));
                winning.setActiveAndVisible(true);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            back1.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            spriteList.Draw(spriteBatch);
            winning.Draw(spriteBatch);
            

            if (showbb)
            {
                paddle.drawBB(spriteBatch, Color.Black);
                paddle.drawHS(spriteBatch, Color.Green);
                LineBatch.drawLineRectangle(spriteBatch, playArea, Color.Blue);
                ball.drawInfo(spriteBatch, Color.Gray, Color.Green);
                spriteList.drawInfo(spriteBatch, Color.Brown, Color.Aqua);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}