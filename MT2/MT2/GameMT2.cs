using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System; 

namespace MT2
{
    public class GameMT2 : RC_GameStateParent
    {

        string dir = @"C:\Users\juuso\OneDrive\Yliopisto\UC\Game Programming Techniques\MT1\Art\";

        int screenWidth = 1920;
        int screenHeight = 900;

        Sprite3 ship = null;
        Sprite3 block1 = null;
        Sprite3 missile = null;
        Sprite3 truck = null;
        Sprite3 fail = null;
        Sprite3 dot = null;
        Sprite3 winning = null;
        Texture2D texBack = null;
        Texture2D texBlock1 = null;
        Texture2D texShip = null;
        Texture2D texboom = null;
        Texture2D texMissile = null;
        Texture2D texTruck = null;
        Texture2D texFail = null;
        Texture2D texDot = null;
        Texture2D texWinning = null;
        ScrollBackGround back = null;
        Rectangle screenRect;
        

        SpriteList dots = null;
        SpriteList booms = null;

        KeyboardState k;
        KeyboardState prevK;
        bool showbb = false;
        Random random = new Random();
        int points = 4;
        int spot = 0;

        int starty = 500;
        int startx = 0;
        int top = 0;
        int bottom = 800;
        int right = 1900;
        int left = 0;
        int moveSpeed = 10;
        int shipSpeed = 8;

        int shipWidth = 128;
        int shipHeight = 64;
        int block1Width = 128;
        int block1Height = 256;
        int missileWidth = 16;
        int missileHeight = 32;
        int truckWidth = 128;
        int truckHeight = 64;
        int rndWidth = 128;
        int rndHeight = 256;

        bool gameState = true;

        

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
            texFail = Util.texFromFile(GraphicsDevice, dir + "gameover.png");
            texDot = Util.texFromFile(GraphicsDevice, dir + "Dot1.png");
            texWinning = Util.texFromFile(GraphicsDevice, dir + "youwin.png");

            back  = new ScrollBackGround(texBack, new Rectangle(0, 0, screenWidth, screenHeight), screenRect, -10f, 2);

            ship = new Sprite3(true, texShip, startx, starty);
            ship.setWidthHeight(shipWidth, shipHeight);
            ship.setBBToTexture();

            block1 = new Sprite3(true, texBlock1, 2000, 700);
            block1.setWidthHeight(block1Width, block1Height);
            block1.setBBToTexture();
            block1.setMoveAngleDegrees(-90);
            block1.setMoveSpeed(moveSpeed);

            missile = new Sprite3(false, texMissile, 0, 0);
            missile.setWidthHeight(missileWidth, missileHeight);
            missile.setHSoffset(new Vector2(0, 370));
            missile.setDisplayAngleDegrees(90);
            missile.setBBToTexture();

            truck = new Sprite3(true, texTruck, block1.getPosX(), block1.getPosY() - 64);
            truck.setWidthHeight(truckWidth, truckHeight);
            truck.setMoveAngleDegrees(-180);
            truck.setMoveSpeed(moveSpeed);
            truck.setBBToTexture();

            fail = new Sprite3(false, texFail, 700, 250);
            fail.setWidthHeight(500, 400);

            winning = new Sprite3(false, texWinning, 600, 400);

            dot = new Sprite3(false, texDot, 0, 0);
            dot.setWidthHeight(45, 45);
            dot.setMoveAngleDegrees(-180);
            dot.setMoveSpeed(moveSpeed);
            dot.setBBToTexture();

            dots = new SpriteList();
            for (int i = 0; i < 5; i++)
            {
                Sprite3 d = new Sprite3(true, texDot, spot, 0);
                d.setWidthHeight(16, 16);
                spot = spot + 24;

                dots.addSpriteReuse(d);
            }

            booms = new SpriteList();

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

            if (k.IsKeyDown(Keys.Up))
            {
                if (ship.getPosY() > top)
                {
                    ship.setPosY(ship.getPosY() - shipSpeed);
                }

            }

            if (k.IsKeyDown(Keys.Down))
            {
                if (ship.getPosY() < bottom)
                {
                    ship.setPosY(ship.getPosY() + shipSpeed);
                }
            }

            if (k.IsKeyDown(Keys.Right) && gameState == true)
            {
                block1.setMoveSpeed(15);
                truck.setMoveSpeed(15);
                dot.setMoveSpeed(15);
                back.setScrollSpeed(-15f);
            }

            if (k.IsKeyDown(Keys.Left) && gameState == true)
            {
                block1.setMoveSpeed(5);
                truck.setMoveSpeed(5);
                dot.setMoveSpeed(5);
                back.setScrollSpeed(-5f);
            }

            if (k.IsKeyDown(Keys.Space) && gameState == true)
            {
                if (missile.getVisible() == false)
                {
                    missile.setActiveAndVisible(true);
                    missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                    missile.setDeltaSpeed(new Vector2(10, 0));
                }
            }

            if (missile.getPosX() > right)
            {
                missile.setActiveAndVisible(false);
            }

            Rectangle missileBB = missile.getBoundingBoxAA();
            Rectangle shipBB = ship.getBoundingBoxAA();

            if (shipBB.Intersects(block1.getBoundingBoxAA()) && ship.getActive() == true)
            {
                gameState = false;
                fail.setVisible(true);
                ship.setActiveAndVisible(false);
                createExplosion((int) ship.getPosX(), (int) ship.getPosY());
                block1.setMoveSpeed(0);
                truck.setMoveSpeed(0);
                dot.setMoveSpeed(0);
                back.setScrollSpeed(0);
            }

            if (shipBB.Intersects(truck.getBoundingBoxAA()) && truck.getActive() == true)
            {
                gameState = false;
                fail.setVisible(true);
                ship.setActiveAndVisible(false);
                truck.setActiveAndVisible(false);
                createExplosion((int)ship.getPosX() + shipWidth/2, (int)ship.getPosY());
                block1.setMoveSpeed(0);
                truck.setMoveSpeed(0);
                back.setScrollSpeed(0);
            }

            if (missileBB.Intersects(block1.getBoundingBoxAA()))
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                createExplosion((int) missile.getPosX(), (int) missile.getPosY());
            }

            if (missileBB.Intersects(truck.getBoundingBoxAA()) && missile.getActive() == true)
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                truck.setActiveAndVisible(false);
                createExplosion((int) missile.getPosX() + 16, (int) missile.getPosY() + 8);
                dot.setActiveAndVisible(true);
                dot.setPos(truck.getPosX(), truck.getPosY());
            }

            if (shipBB.Intersects(dot.getBoundingBoxAA()) && dot.getActive() == true)
            {
                dot.setActiveAndVisible(false);
                dots[points].setVisible(false);
                points--;
                
            }

            if (block1.getPosX() < left - rndWidth)
            {
                rndHeight = random.Next(64, 800);
                rndWidth = random.Next(128, 512);
                block1.setPos(2000, rndHeight);
                block1.setWidthHeight(rndWidth, rndHeight);
                truck.setPos(block1.getPosX(), block1.getPosY() - 64);
                truck.setActiveAndVisible(true);

            }

            if (points < 0) 
            {
                winning.setVisible(true);
                gameState = false;
                block1.setMoveSpeed(0);
                truck.setMoveSpeed(0);
                dot.setMoveSpeed(0);
                back.setScrollSpeed(0);
            }

            block1.moveByAngleSpeed();
            truck.moveByAngleSpeed();
            dot.moveByAngleSpeed();
            missile.moveByDeltaXY();
            back.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            back.Draw(spriteBatch);
            ship.Draw(spriteBatch);
            block1.Draw(spriteBatch);
            truck.Draw(spriteBatch);
            booms.Draw(spriteBatch);
            missile.Draw(spriteBatch);
            fail.Draw(spriteBatch);
            dot.Draw(spriteBatch);
            dots.Draw(spriteBatch);
            winning.Draw(spriteBatch);


            if (showbb)
            {
                ship.drawBB(spriteBatch, Color.Red);
                ship.drawHS(spriteBatch, Color.Green);
                block1.drawBB(spriteBatch, Color.Red);
                block1.drawHS(spriteBatch, Color.Green);
                missile.drawBB(spriteBatch, Color.Red);
                missile.drawHS(spriteBatch, Color.Green);
                truck.drawBB(spriteBatch, Color.Red);
                truck.drawHS(spriteBatch, Color.Green);
                dot.drawBB(spriteBatch, Color.Red);
                dot.drawHS(spriteBatch, Color.Green);
            }

            spriteBatch.End();

            booms.animationTick(gameTime);

            base.Draw(gameTime);
        }

        void createExplosion(int x, int y)
        {
            float scale = 1f;
            int xoffset = -2;
            int yoffset = -20;

            Sprite3 s = new Sprite3(true, texboom, x + xoffset, y + yoffset);
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

            booms.addSpriteReuse(s); // add the sprite
        }
    }
}