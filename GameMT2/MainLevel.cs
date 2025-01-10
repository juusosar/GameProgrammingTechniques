using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace GameMT2
{
    /*
     POINTS SYSTEM:
    
     HITTING WITH SHIP:
     Boom truck -300
     Friendly truck -1000
     Mountain -200
     
     SHOOTING WITH MISSILE:
     Boom truck +200
     Friendly truck -500

     COLLECTING DOTS:
     Dot + 100
     
     WINNING THE GAME:
     Get over 1000 points in the end
    */
    public class MainLevel : RC_GameStateParent
    {

        Sprite3 ship = null;
        Sprite3 missile = null;
        Sprite3 truck = null;
        Sprite3 truck2 = null;
        Sprite3 truckF = null;
        Sprite3 dot = null;
        ScrollBackGround back = null;
        SpriteList dots = null;
        SpriteList booms = null;
        SpriteList blocks = null;


        static public int score = 0;
        int points = 4;
        int spot = 0;

        int starty = 500;
        int startx = 0;
        int top = 0;
        int bottom = 800;
        int right = 1900;
        int left = 0;
        int shipSpeed = 8;

        int shipWidth = 128;
        int shipHeight = 64;
        int blockWidth = 1400;
        int block1Height = 256;
        int missileWidth = 16;
        int missileHeight = 32;
        int truckWidth = 128;
        int truckHeight = 64;
        int blockx = 1900;
        int blocky = 700;
        int blockFlag = 0;
        int blockn = 2;
        int blockn2 = 3;

        bool gameState = true;
        int endTimer = 100;

        

        public override void LoadContent()
        {
            font1 = Content.Load<SpriteFont>("C:/Users/juuso/source/repos/GameMT2/Content/SpriteFont1");


            back = new ScrollBackGround(MT2.texBack, new Rectangle(0, 0, MT2.screenWidth, MT2.screenHeight), MT2.screenRect, -10f, 2);

            ship = new Sprite3(true, MT2.texShip, startx, starty);
            ship.setWidthHeight(shipWidth, shipHeight);
            ship.setBBToTexture();

            missile = new Sprite3(false, MT2.texMissile, 0, 0);
            missile.setWidthHeight(missileWidth, missileHeight);
            missile.setHSoffset(new Vector2(0, 370));
            missile.setDisplayAngleDegrees(90);
            missile.setBBToTexture();

            truck = new Sprite3(true, MT2.texTruck, blockx, blocky - 64);
            truck.setWidthHeight(truckWidth, truckHeight);
            truck.setDeltaSpeed(new Vector2(-10, 0));
            truck.setBBToTexture();

            truck2 = new Sprite3(true, MT2.texTruck, blockx + blockWidth, 450 - 64);
            truck2.setWidthHeight(truckWidth, truckHeight);
            truck2.setDeltaSpeed(new Vector2(-10, 0));
            truck2.setBBToTexture();

            truckF = new Sprite3(true, MT2.texTruck2, blockx + truckWidth + 64, blocky - 64);
            truckF.setWidthHeight(truckWidth, truckHeight);
            truckF.setDeltaSpeed(new Vector2(-10, 0));
            truckF.setBBToTexture();

            dot = new Sprite3(false, MT2.texDot, 0, 0);
            dot.setWidthHeight(45, 45);
            dot.setDeltaSpeed(new Vector2(-10, 0));
            dot.setBBToTexture();

            dots = new SpriteList();
            for (int i = 0; i < 5; i++)
            {
                Sprite3 d = new Sprite3(true, MT2.texDot, spot, 0);
                d.setWidthHeight(16, 16);
                spot = spot + 24;

                dots.addSpriteReuse(d);
            }

            blocks = new SpriteList();
            for (int i =0; i < 12; i++)
            {
                Sprite3 b = new Sprite3(true, MT2.texBlock1, blockx, blocky);
                b.setWidthHeight(blockWidth, block1Height);
                b.setBBToTexture();
                b.setDeltaSpeed(new Vector2(-10, 0));
                if (blockFlag == 0)
                {
                    block1Height += 400;
                    blocky = 450;
                    
                }
                if (blockFlag == 1)
                {
                    block1Height = block1Height + 200;
                    blocky = 250;
                    
                } 
                if (blockFlag == 2)
                {
                    block1Height = 256;
                    blocky = 700;
                    blockFlag = -1;
                }


                blockFlag++;
                blockx += blockWidth;
                blocks.addSpriteReuse(b);

            }

            booms = new SpriteList();

        }

        public override void Update(GameTime gameTime)
        {

            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up))
            {
                if (ship.getPosY() > top)
                {
                    ship.setPosY(ship.getPosY() - shipSpeed);
                    if (missile.getActive() == false) missile.setPosY(ship.getPosY() - shipSpeed);
                }

            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                if (ship.getPosY() < bottom)
                {
                    ship.setPosY(ship.getPosY() + shipSpeed);
                    if (missile.getActive() == false) missile.setPosY(ship.getPosY() + shipSpeed);
                }
            }

            if (keyState.IsKeyDown(Keys.Right) && gameState == true)
            {
                blocks.setDeltaXYSpeed(new Vector2(-15, 0));

                truck.setDeltaSpeed(new Vector2(-15, 0));
                truck2.setDeltaSpeed(new Vector2(-15, 0));
                truckF.setDeltaSpeed(new Vector2(-15, 0));
                dot.setDeltaSpeed(new Vector2(-15, 0));
                back.setScrollSpeed(-15f);
            }

            if (keyState.IsKeyDown(Keys.Left) && gameState == true)
            {
                blocks.setDeltaXYSpeed(new Vector2(-5, 0));

                truck.setDeltaSpeed(new Vector2(-5, 0));
                truck2.setDeltaSpeed(new Vector2(-5, 0));
                truckF.setDeltaSpeed(new Vector2(-5, 0));
                dot.setDeltaSpeed(new Vector2(-5, 0));
                back.setScrollSpeed(-5f);
            }

            // Shoot a missile
            if (keyState.IsKeyDown(Keys.Space) && gameState == true)
            {
                if (missile.getVisible() == false)
                {
                    missile.setActiveAndVisible(true);
                    missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                    missile.setDeltaSpeed(new Vector2(10, 0));
                }
            }

            // Missile flies away
            if (missile.getPosX() > right)
            {
                missile.setActiveAndVisible(false);
                missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                missile.setDeltaSpeed(new Vector2(0, 0));
            }

            Rectangle missileBB = missile.getBoundingBoxAA();
            Rectangle shipBB = ship.getBoundingBoxAA();

            // Ship collides with blocks
            if (blocks.collisionWithRect(shipBB) != -1 && ship.getActive() == true)
            {
                ship.setActiveAndVisible(false);
                createExplosion((int)ship.getPosX(), (int)ship.getPosY());
                MT2.limBoomSound.playSoundIfOk();
                blocks.setDeltaXYSpeed(new Vector2(0, 0));
                truck.setDeltaSpeed(new Vector2(0, 0));
                truck2.setDeltaSpeed(new Vector2(0, 0));
                truckF.setDeltaSpeed(new Vector2(0, 0));
                dot.setMoveSpeed(0);
                back.setScrollSpeed(0);
                score -= 200;
                gameState = false;
            }

            // Ship collides with truck1
            if (shipBB.Intersects(truck.getBoundingBoxAA()) && truck.getActive() == true && ship.getActive() == true)
            {
                ship.setActiveAndVisible(false);
                truck.setActiveAndVisible(false);
                createExplosion((int)ship.getPosX() + shipWidth / 2, (int)ship.getPosY());
                MT2.limBoomSound.playSoundIfOk();
                blocks.setDeltaXYSpeed(new Vector2(0, 0));
                truck.setDeltaSpeed(new Vector2(0, 0));
                truck2.setDeltaSpeed(new Vector2(0, 0));
                truckF.setDeltaSpeed(new Vector2(0, 0));
                back.setScrollSpeed(0);
                score -= 300;
                gameState = false;
            }

            // Ship collides with truck2
            if (shipBB.Intersects(truck2.getBoundingBoxAA()) && truck2.getActive() == true && ship.getActive() == true)
            {
                ship.setActiveAndVisible(false);
                truck2.setActiveAndVisible(false);
                createExplosion((int)ship.getPosX() + shipWidth / 2, (int)ship.getPosY());
                MT2.limBoomSound.playSoundIfOk();
                blocks.setDeltaXYSpeed(new Vector2(0, 0));
                truck.setDeltaSpeed(new Vector2(0, 0));
                truck2.setDeltaSpeed(new Vector2(0, 0));
                truckF.setDeltaSpeed(new Vector2(0, 0));
                back.setScrollSpeed(0);
                score -= 300;
                gameState = false;
            }

            //Ship collides with truckF
            if (shipBB.Intersects(truckF.getBoundingBoxAA()) && truckF.getActive() == true && ship.getActive() == true)
            {
                ship.setActiveAndVisible(false);
                truckF.setActiveAndVisible(false);
                createExplosion((int)ship.getPosX() + shipWidth / 2, (int)ship.getPosY());
                MT2.limBoomSound.playSoundIfOk();
                blocks.setDeltaXYSpeed(new Vector2(0, 0));
                truck.setDeltaSpeed(new Vector2(0, 0));
                truckF.setDeltaSpeed(new Vector2(0, 0));
                back.setScrollSpeed(0);
                score -= 1000;
                gameState = false;
            }

            // Missile collides with blocks
            if (blocks.collisionWithRect(missileBB) != -1 && missile.getActive() == true)
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                createExplosion((int)missile.getPosX() - 32, (int)missile.getPosY());
                missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                MT2.limBoomSound.playSoundIfOk();
            }

            // Missile collides with truck1
            if (missileBB.Intersects(truck.getBoundingBoxAA()) && truck.getActive() == true)
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                truck.setActiveAndVisible(false);
                createExplosion((int)truck.getPosX(), (int)truck.getPosY());
                missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                MT2.limBoomSound.playSoundIfOk();
                if (points >= 0)
                {
                    dot.setActiveAndVisible(true);
                    dot.setPos(truck.getPosX(), truck.getPosY());
                }
                score += 200;
            }

            // Missile collides with truck2
            if (missileBB.Intersects(truck2.getBoundingBoxAA()) && truck2.getActive() == true)
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                truck2.setActiveAndVisible(false);
                createExplosion((int)truck2.getPosX(), (int)truck2.getPosY());
                missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                MT2.limBoomSound.playSoundIfOk();
                if (points >= 0)
                {
                    dot.setActiveAndVisible(true);
                    dot.setPos(truck2.getPosX(), truck2.getPosY());
                }
                score += 200;
            }

            // Missile collides with truckF
            if (missileBB.Intersects(truckF.getBoundingBoxAA()) && truckF.getActive() == true)
            {
                missile.setActiveAndVisible(false);
                missile.setDeltaSpeed(new(0, 0));
                truckF.setActiveAndVisible(false);
                createExplosion((int)truckF.getPosX(), (int)truckF.getPosY());
                missile.setPos(ship.getPosX() + 100, ship.getPosY() + 32);
                MT2.limBoomSound.playSoundIfOk();
                score -= 500;
            }

            // Collect a dot
            if (shipBB.Intersects(dot.getBoundingBoxAA()) && dot.getActive() == true)
            {
                dot.setActiveAndVisible(false);
                dots[points].setVisible(false);
                points--;
                score += 100;

            }

            // Reposition trucks
            if (truckF.getPosX() < left - truckWidth)
            {
                if (blockn < blocks.count())
                {
                    truck.setPos(blocks[blockn].getPosX(), blocks[blockn].getPosY() - 64);
                    truck.setActiveAndVisible(true);
                    truckF.setPos(blocks[blockn].getPosX() + truckWidth + 64, blocks[blockn].getPosY() - 64);
                    truckF.setActiveAndVisible(true);
                }

                blockn += 2;
            }

            // Reposition truck2
            if (truck2.getPosX() < left - truckWidth)
            {
                if (blockn2 < blocks.count())
                {
                    truck2.setPos(blocks[blockn2].getPosX(), blocks[blockn2].getPosY() - 64);
                    truck2.setActiveAndVisible(true);
                }

                blockn2 += 2;
            }

            // End game when blocks end
            if (blocks[blocks.getHighestUsed()].getPosX() < left - blockWidth)
            {
                MT2.levelManager.setLevel(2);
            }

            if (gameState == false)
            {
                endTimer--;
                if (endTimer <= 0)
                {
                    MT2.levelManager.setLevel(2);
                }
            }

            blocks.moveDeltaXY();
            truck.moveByDeltaXY();
            truck2.moveByDeltaXY();
            truckF.moveByDeltaXY();
            dot.moveByDeltaXY();
            missile.moveByDeltaXY();
            back.Update(gameTime);
            MT2.limBoomSound.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            back.Draw(spriteBatch);
            ship.Draw(spriteBatch);
            blocks.Draw(spriteBatch);
            truck.Draw(spriteBatch);
            truck2.Draw(spriteBatch);
            truckF.Draw(spriteBatch);
            booms.Draw(spriteBatch);
            missile.Draw(spriteBatch);
            dot.Draw(spriteBatch);
            dots.Draw(spriteBatch);

            spriteBatch.DrawString(font1, "Score: " + score, new Vector2(600, 10), Color.Red);


            if (MT2.showbb)
            {
                ship.drawBB(spriteBatch, Color.Red);
                ship.drawHS(spriteBatch, Color.Green);
                blocks.drawInfo(spriteBatch, Color.Red, Color.Green);
                missile.drawBB(spriteBatch, Color.Red);
                missile.drawHS(spriteBatch, Color.Green);
                truck.drawBB(spriteBatch, Color.Red);
                truck.drawHS(spriteBatch, Color.Green);
                truck2.drawBB(spriteBatch, Color.Red);
                truck2.drawHS(spriteBatch, Color.Green);
                truckF.drawBB(spriteBatch, Color.Red);
                truckF.drawHS(spriteBatch, Color.Green);
                dot.drawBB(spriteBatch, Color.Red);
                dot.drawHS(spriteBatch, Color.Green);
              
            }

            spriteBatch.End();

            booms.animationTick(gameTime);
        }

        void createExplosion(int x, int y)
        {
            float scale = 1f;
            int xoffset = -2;
            int yoffset = -20;

            Sprite3 s = new Sprite3(true, MT2.texboom, x + xoffset, y + yoffset);
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