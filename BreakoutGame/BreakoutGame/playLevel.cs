using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Specialized;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.Metadata;

namespace BreakoutGame
{
    public class playLevel : RC_GameStateParent
    {
        Sprite3 paddle = null;
        Sprite3 ball = null;
        Sprite3 winning = null;
        ImageBackground back1 = null;

        SpriteList spriteList = null;

        int blocksOffsetX = 30;
        int blocksOffsetY = 30;

        bool ballStuck = true;
        Vector2 ballOffset = new Vector2(32, -10);

        int paddleSpeed = 4;
        int top = 56;
        int lhs = 236;
        int rhs = 564;
        int bot = 543;
        float xx = 360;
        float yy = 543;


        public override void LoadContent()
        {
            paddle = new Sprite3(true, Breakout.texpaddle, xx, yy);
            paddle.setBBToTexture();

            ball = new Sprite3(true, Breakout.texball, xx, yy);
            ball.setBBandHSFractionOfTexCentered(0.7f);

            winning = new Sprite3(false, Breakout.texwinning, 50, 200);


            back1 = new ImageBackground(Breakout.texBack, Color.White, graphicsDevice);
            Breakout.playArea = new Rectangle(lhs, top, rhs - lhs, bot - top);

            spriteList = new SpriteList();
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 4; x++)
                {
                    Sprite3 s = new Sprite3(true, Breakout.texblock1, x * 68 + Breakout.playArea.X + blocksOffsetX, y * 36 + Breakout.playArea.Y + blocksOffsetY);
                    s.hitPoints = 1;

                    if (y == 0)
                    {
                        s.hitPoints = 2;
                        s.setColor(Color.LightBlue);
                    }
                    spriteList.addSpriteReuse(s);
                }

            Breakout.booms = new SpriteList();

        }

        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.Right))
            {
                if (paddle.getPosX() < rhs - Breakout.texpaddle.Width)
                {
                    paddle.setPosX(paddle.getPosX() + paddleSpeed);
                }
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (paddle.getPosX() > lhs)
                {
                    paddle.setPosX(paddle.getPosX() - paddleSpeed);
                }
            }

            if (ballStuck)
            {
                ball.setPos(paddle.getPos() + ballOffset);
                if (keyState.IsKeyDown(Keys.Space))
                {
                    ballStuck = false;
                    ball.setDeltaSpeed(new Vector2(2, -3));
                }
            }
            else
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
                        createExplosion((int)temp.getPosX(), (int)temp.getPosY());
                        Breakout.limBoomSound.playSoundIfOk();
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

            Breakout.booms.animationTick(gameTime);
            Breakout.limBoomSound.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();

            back1.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            Breakout.booms.Draw(spriteBatch);
            spriteList.Draw(spriteBatch);
            winning.Draw(spriteBatch);

            if (Breakout.showbb)
            {
                paddle.drawBB(spriteBatch, Color.Black);
                paddle.drawHS(spriteBatch, Color.Green);
                LineBatch.drawLineRectangle(spriteBatch, Breakout.playArea, Color.Blue);
                ball.drawInfo(spriteBatch, Color.Gray, Color.Green);
                spriteList.drawInfo(spriteBatch, Color.Brown, Color.Aqua);
            }

            spriteBatch.End();
        }

        void createExplosion(int x, int y)
        {
            float scale = 0.6f;
            int xoffset = -2;
            int yoffset = -20;

            Sprite3 s = new Sprite3(true, Breakout.texboom, x + xoffset, y + yoffset);
            s.setXframes(7);
            s.setYframes(3);
            s.setWidthHeight(896 / 7 * scale, 384 / 3 * scale);

            Vector2[] anim = new Vector2[21];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 0; anim[7].Y = 1;
            anim[8].X = 1; anim[8].Y = 1;
            anim[9].X = 2; anim[9].Y = 1;
            anim[10].X = 3; anim[10].Y = 1;
            anim[11].X = 4; anim[11].Y = 1;
            anim[12].X = 5; anim[12].Y = 1;
            anim[13].X = 6; anim[13].Y = 1;
            anim[14].X = 0; anim[14].Y = 2;
            anim[15].X = 1; anim[15].Y = 2;
            anim[16].X = 2; anim[16].Y = 2;
            anim[17].X = 3; anim[17].Y = 2;
            anim[18].X = 4; anim[18].Y = 2;
            anim[19].X = 5; anim[19].Y = 2;
            anim[20].X = 6; anim[20].Y = 2;
            s.setAnimationSequence(anim, 0, 20, 4);
            s.setAnimFinished(2); // make it inactive and invisible
            s.animationStart();

            Breakout.booms.addSpriteReuse(s); // add the sprite

        }
    }

}
