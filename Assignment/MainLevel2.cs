using Microsoft.Xna.Framework;
using RC_Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Assignment
{
    public class MainLevel2 : RC_GameStateParent
    {
        Sprite3 tower = null;
        Sprite3 tower2 = null;
        Sprite3 towerDead = null;
        Sprite3 player = null;
        Sprite3 enemy1 = null;
        Sprite3 enemy2 = null;
        Sprite3 enemy3a = null;
        Sprite3 enemy3b = null;
        Sprite3 sword = null;
        Sprite3 grass = null;
        Sprite3 back = null;
        Sprite3 border = null;
        SpriteList enemy1list = null;
        SpriteList enemy2list = null;
        SpriteList enemy3list = null;
        SpriteList health = null;
        Vector2 playerPos;
        Vector2 prevPos;

        int top = 225;
        int bottom = 900;
        int right = 1400;
        int left = 0;
        int moveSpeed = 3;
        int enemyticks = 300;
        int hp = 2000;
        int soundTick = 0;
        int soundTick2 = 0;
        int soundTick3 = 0;
        int playerHitTick = 50;
        int healthCount = 5;
        int kills = 0;
        int killLimit = 50;
        int enemy3FlagDirection = 1;

        bool animFlag = true;
        float scale = 2f;
        Random random = new Random();

        int playerWidth = 64;
        int playerHeight = 64;
        

        struct anims
        {
            static public Vector2[] animPlayer;
            static public Vector2[] animEnemy1;
            static public Vector2[] animEnemy2;
            static public Vector2[] animEnemy3;
        }

        

        public override void LoadContent()
        {
            // Reinitialization for resetting the game after death
            enemyticks = 225;
            hp = 2000;
            soundTick = 0;
            soundTick2 = 0;
            playerHitTick = 100;
            healthCount = 5;
            kills = 0;

            player = new Sprite3(true, Levels.texPlayer, 300, 400);
            player.setWidthHeightOfTex(70, 131);
            player.setWidthHeight(24 * scale, 32 * scale);
            player.setXframes(3);
            player.setYframes(4);
            player.setBB(0, 4, 24, 28);
            player.setHSoffset(new Vector2 (12, 16));

            // player animations
            anims.animPlayer = new Vector2[12];
            anims.animPlayer[0].X = 0; anims.animPlayer[0].Y = 0;
            anims.animPlayer[1].X = 1; anims.animPlayer[1].Y = 0;
            anims.animPlayer[2].X = 2; anims.animPlayer[2].Y = 0;
            anims.animPlayer[3].X = 0; anims.animPlayer[3].Y = 1;
            anims.animPlayer[4].X = 1; anims.animPlayer[4].Y = 1;
            anims.animPlayer[5].X = 2; anims.animPlayer[5].Y = 1;
            anims.animPlayer[6].X = 0; anims.animPlayer[6].Y = 2;
            anims.animPlayer[7].X = 1; anims.animPlayer[7].Y = 2;
            anims.animPlayer[8].X = 2; anims.animPlayer[8].Y = 2;
            anims.animPlayer[9].X = 0; anims.animPlayer[9].Y = 3;
            anims.animPlayer[10].X = 1; anims.animPlayer[10].Y = 3;
            anims.animPlayer[11].X = 2; anims.animPlayer[11].Y = 3;

            //enemy1 animations
            anims.animEnemy1 = new Vector2[9];
            anims.animEnemy1[0].X = 0; anims.animEnemy1[0].Y = 1;
            anims.animEnemy1[1].X = 1; anims.animEnemy1[1].Y = 1;
            anims.animEnemy1[2].X = 2; anims.animEnemy1[2].Y = 1;
            anims.animEnemy1[3].X = 3; anims.animEnemy1[3].Y = 1;
            anims.animEnemy1[4].X = 0; anims.animEnemy1[4].Y = 0;
            anims.animEnemy1[5].X = 1; anims.animEnemy1[5].Y = 0;
            anims.animEnemy1[6].X = 2; anims.animEnemy1[6].Y = 0;
            anims.animEnemy1[7].X = 3; anims.animEnemy1[7].Y = 0;
            anims.animEnemy1[8].X = 4; anims.animEnemy1[8].Y = 0;

            //enemy2 animations
            anims.animEnemy2 = new Vector2[10];
            anims.animEnemy2[0].X = 5; anims.animEnemy2[0].Y = 0;
            anims.animEnemy2[1].X = 4; anims.animEnemy2[1].Y = 0;
            anims.animEnemy2[2].X = 3; anims.animEnemy2[2].Y = 0;
            anims.animEnemy2[3].X = 2; anims.animEnemy2[3].Y = 0;
            anims.animEnemy2[4].X = 1; anims.animEnemy2[4].Y = 0;
            anims.animEnemy2[5].X = 0; anims.animEnemy2[5].Y = 0;
            anims.animEnemy2[6].X = 0; anims.animEnemy2[6].Y = 1;
            anims.animEnemy2[7].X = 1; anims.animEnemy2[7].Y = 1;
            anims.animEnemy2[8].X = 2; anims.animEnemy2[8].Y = 1;
            anims.animEnemy2[9].X = 3; anims.animEnemy2[9].Y = 1;

            //enemy3 animations
            anims.animEnemy3 = new Vector2[10];
            anims.animEnemy3[0].X = 0; anims.animEnemy3[0].Y = 0;
            anims.animEnemy3[1].X = 1; anims.animEnemy3[1].Y = 0;
            anims.animEnemy3[2].X = 2; anims.animEnemy3[2].Y = 0;
            anims.animEnemy3[3].X = 3; anims.animEnemy3[3].Y = 0;
            anims.animEnemy3[4].X = 4; anims.animEnemy3[4].Y = 0;
            anims.animEnemy3[5].X = 4; anims.animEnemy3[5].Y = 0;
            anims.animEnemy3[6].X = 3; anims.animEnemy3[6].Y = 0;
            anims.animEnemy3[7].X = 2; anims.animEnemy3[7].Y = 0;
            anims.animEnemy3[8].X = 1; anims.animEnemy3[8].Y = 0;
            anims.animEnemy3[9].X = 0; anims.animEnemy3[9].Y = 0;


            player.setAnimationSequence(anims.animPlayer, 7, 7, 15);
            player.animationStart();

            sword = new Sprite3(false, Levels.texSword, player.getPosX(), player.getPosY());
            sword.setWidthHeight(16 * scale, 16 * scale);
            sword.setBB(28, 0, 4, 4);
            sword.setHSoffset(new Vector2(0, 32));
            sword.setDisplayAngleDegrees(45);
            

            tower = new Sprite3(true, Levels.texTower, 700, 550);
            tower.setWidthHeight(50, 150);
            tower.setHSoffset(new Vector2(25, 75));
            tower.setBBToTexture();
            tower2 = new Sprite3(false, Levels.texTower, 700, 394);
            towerDead = new Sprite3(false, Levels.texTowerDead, 700, 731);

            HealthBarAttached h = new HealthBarAttached(Color.Aquamarine, Color.Green, Color.Red, 9, true);
            h.offset = new Vector2(0, -2);
            h.gapOfbar = 2;
            tower.hitPoints = hp;
            tower.maxHitPoints = hp;
            tower.attachedRenderable = h;

            back = new Sprite3(true,Levels.texBack, 0, 0);
            back.setWidth(Levels.screenWidth);

            health = new SpriteList();
            int spot = 10;
            for (int i = 0; i < healthCount; i++)
            {
                Sprite3 heart = new Sprite3(true, Levels.texHeart, spot, 10);
                heart.setWidthHeight(32, 32);
                spot = spot + 24;

                health.addSpriteReuse(heart);
            }

            enemy1list = new SpriteList();
            enemy2list = new SpriteList();
            enemy3list = new SpriteList();

            enemyticks = 300;
            tower.setPosY(731);

            tower2.setVisible(true);
            towerDead.setWidthHeight(50, 150);
            towerDead.setHSoffset(new Vector2(25, 75));

                
            tower2.setWidthHeight(50, 150);
            tower2.setHSoffset(new Vector2(25, 75));
            tower2.setBBToTexture();

            HealthBarAttached h2 = new HealthBarAttached(Color.Aquamarine, Color.Green, Color.Red, 9, true);
            h2.offset = new Vector2(0, -2);
            h2.gapOfbar = 2;
            tower2.hitPoints = hp;
            tower2.maxHitPoints = hp;
            tower2.attachedRenderable = h2;
            
        }

        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevPos = playerPos;
            playerPos = player.getPos();
            player.savePosition();

            if (keyState.IsKeyDown(Keys.Up))
            {
                changeAnim("up");
                sword.setDisplayAngleDegrees(-45);
                sword.setPos(new Vector2(player.getPosX(), player.getPosY()-playerHeight/4));
                if (player.getPosY() > top)
                {
                    player.setPosY(player.getPosY() - moveSpeed);
                }
            }
            if(keyState.IsKeyUp(Keys.Up))
            {
                animFlag = true;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                changeAnim("down");
                sword.setDisplayAngleDegrees(135);
                sword.setPos(new Vector2(player.getPosX(), player.getPosY()+playerHeight/4));
                if (player.getPosY() < bottom - playerHeight)
                {
                    player.setPosY(player.getPosY() + moveSpeed);
                }
            }
            if (keyState.IsKeyUp(Keys.Down))
            {
                animFlag = true;
            }
            if (keyState.IsKeyDown(Keys.Left) )
            {
                changeAnim("left");
                sword.setDisplayAngleDegrees(-135);
                sword.setPos(new Vector2(player.getPosX()-playerWidth/4, player.getPosY()+playerHeight/4));
                if (player.getPosX() > left)
                {
                    player.setPosX(player.getPosX() - moveSpeed);
                }
            }
            if (keyState.IsKeyUp(Keys.Left))
            {
                animFlag = true;
            }

            if (keyState.IsKeyDown(Keys.Right) )
            {
                changeAnim("right");
                sword.setDisplayAngleDegrees(45);
                sword.setPos(new Vector2(player.getPosX() + playerWidth/4, player.getPosY() + playerHeight / 4));
                if (player.getPosX() < right - playerWidth)
                {
                    player.setPosX(player.getPosX() + moveSpeed);
                }
            }
            if (keyState.IsKeyUp(Keys.Right))
            {
                animFlag = true;
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                sword.setActiveAndVisible(true);
            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                sword.setActiveAndVisible(false);
            }

            // Sword hits
            if (enemy1list.collisionAA(sword) != -1 && sword.getActive() == true)
            {
                enemy1list[enemy1list.collisionAA(sword)].setActiveAndVisible(false);
                Levels.limClashSound.playSoundIfOk();
                kills++;
            }
            if (enemy2list.collisionAA(sword) != -1 && sword.getActive() == true)
            {
                enemy2list[enemy2list.collisionAA(sword)].setActiveAndVisible(false);
                Levels.limClashSound.playSoundIfOk();
                kills++;
            }
            if (enemy3list.collisionAA(sword) != -1 && sword.getActive() == true)
            {
                enemy3list[enemy3list.collisionAA(sword)].setActiveAndVisible(false);
                Levels.limClashSound.playSoundIfOk();
                kills++;
            }
            if (kills == killLimit)
            {
                //transition to win screen
                Levels.levelManager.getCurrentLevel().ExitLevel();
                Levels.levelManager.getCurrentLevel().UnloadContent();
                Levels.levelManager.setLevel(7);
            }

            // Checking hits on towers
            checkTowerHit(tower);
            checkTowerHit(tower2);

            // Checking if towers are dead
            if (tower.hitPoints == 0)
            {
                tower.setVisible(false);
                towerDead.setVisible(true);
            }
            if (tower2.hitPoints == 0)
            {
                tower2.setVisible(false);
                towerDead.setPosY(394);
                towerDead.setVisible(true);
            }
            if (tower.hitPoints == 0 && tower2.hitPoints == 0)
            {
                Levels.levelManager.setLevel(4);
            }
            


            // Checking hits on player
            if (enemy1list.collisionAA(player) != -1 || enemy2list.collisionAA(player) != -1 || enemy3list.collisionAA(player) != -1)
            {
                if (healthCount == 0)
                {
                    Levels.levelManager.setLevel(4);
                }
                playerHitTick--;
                if (playerHitTick == 0)
                {
                    health[healthCount-1].setActiveAndVisible(false);
                    healthCount--;
                    playerHitTick = 100;
                    Levels.limHitSound.playSoundIfOk();
                }
            } else
            {
                playerHitTick = 100;
            }


            // Enemy spawner
            enemyticks--;
            if (enemyticks == 0)
            {
                createEnemy1();
                createEnemy2();
                createEnemy3();
                enemyticks = 225;
            }
           

            // Enemy movement
            for (int i = 0; i < enemy1list.count(); i++) {

                if (i % 2 == 1)
                {
                    if (tower.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy1list[i].moveTo(new Vector2(tower.getPosX() - 50, tower.getPosY()), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy1list[i].moveTo(new Vector2(tower.getPosX() - 50, tower.getPosY() - 50), 1, false);
                        }
                        else
                        {
                            enemy1list[i].moveTo(new Vector2(tower.getPosX() - 50, tower.getPosY() + 50), 1, false);
                        }
                    }
                    else enemy1list[i].moveTo(new Vector2(tower2.getPosX() - 50, tower2.getPosY()), 1, false);
                }
                else if (i % 2 == 0)
                {
                    if (tower2.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy1list[i].moveTo(new Vector2(tower2.getPosX() - 50, tower2.getPosY()), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy1list[i].moveTo(new Vector2(tower2.getPosX() - 50, tower2.getPosY() - 50), 1, false);
                        }
                        else
                        {
                            enemy1list[i].moveTo(new Vector2(tower2.getPosX() - 50, tower2.getPosY() + 50), 1, false);
                        }
                    } else enemy1list[i].moveTo(new Vector2(tower.getPosX() - 50, tower.getPosY()), 1, false);
            }
                
            }

            for (int i = 0; i < enemy2list.count(); i++)
            {
                if (i % 2 == 1)
                {
                    if (tower.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy2list[i].moveTo(new Vector2(tower.getPosX() + 50, tower.getPosY()), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy2list[i].moveTo(new Vector2(tower.getPosX() + 50, tower.getPosY() - 50), 1, false);
                        }
                        else
                        {
                            enemy2list[i].moveTo(new Vector2(tower.getPosX() + 50, tower.getPosY() + 50), 1, false);
                        }
                    }
                    else enemy2list[i].moveTo(new Vector2(tower2.getPosX() + 50, tower2.getPosY()), 1, false);
                }
                else if (i % 2 == 0)
                {
                    if (tower2.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy2list[i].moveTo(new Vector2(tower2.getPosX() + 50, tower2.getPosY()), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy2list[i].moveTo(new Vector2(tower2.getPosX() + 50, tower2.getPosY() - 50), 1, false);
                        }
                        else
                        {
                            enemy2list[i].moveTo(new Vector2(tower2.getPosX() + 50, tower2.getPosY() + 50), 1, false);
                        }
                    } else enemy2list[i].moveTo(new Vector2(tower.getPosX() + 50, tower.getPosY()), 1, false);
                }
            }
            for (int i = 0; i < enemy3list.count(); i++)
            {
                if (i % 2 == 1)
                {
                    if (tower.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy3list[i].moveTo(new Vector2(tower.getPosX(), tower.getPosY() - 75), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy3list[i].moveTo(new Vector2(tower.getPosX() + 15, tower.getPosY() - 75), 1, false);
                        }
                        else
                        {
                            enemy3list[i].moveTo(new Vector2(tower.getPosX() - 15, tower.getPosY() - 75), 1, false);
                        }
                    }
                    else enemy3list[i].moveTo(new Vector2(tower2.getPosX(), tower2.getPosY() - 75), 1, false);
                }
                else if (i % 2 == 0)
                {
                    if (tower2.hitPoints > 0)
                    {
                        if (i % 3 == 0)
                        {
                            enemy3list[i].moveTo(new Vector2(tower2.getPosX(), tower2.getPosY() - 75), 1, false);
                        }
                        else if (i % 3 == 1)
                        {
                            enemy3list[i].moveTo(new Vector2(tower2.getPosX() + 15, tower2.getPosY() - 75), 1, false);
                        }
                        else
                        {
                            enemy3list[i].moveTo(new Vector2(tower2.getPosX() - 15, tower2.getPosY() - 75), 1, false);
                        }
                    } else enemy3list[i].moveTo(new Vector2(tower.getPosX(), tower.getPosY() - 75), 1, false);
                }
            }

            enemy1list.animationTick(gameTime);
            enemy2list.animationTick(gameTime);
            enemy3list.animationTick(gameTime);
            player.animationTick(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            back.Draw(spriteBatch);

            int x = 0, y = 250;
            for (int i = 0; i < 1400/60 +1; i++)
            {
                for (int j = 0; j < 900/59 +1; j++)
                {
                    grass = new Sprite3(true, Levels.texGrass, x, y);
                    y = y + 59;
                    grass.Draw(spriteBatch);
                }
                x = x + 60;
                y = 250;
            }
            x = 0;
            for (int k=0; k < 1400/64 +1; k++)
            {
                border = new Sprite3(true, Levels.texBorder, x, 225);
                x = x + 64;
                border.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            sword.Draw(spriteBatch);
            tower.Draw(spriteBatch);
            tower2.Draw(spriteBatch);
            towerDead.Draw(spriteBatch);
            enemy3list.Draw(spriteBatch);
            enemy1list.Draw(spriteBatch);
            enemy2list.Draw(spriteBatch);

            if (Levels.showbb)
            {
                player.drawInfo(spriteBatch, Color.Red, Color.Black);
                tower.drawInfo(spriteBatch, Color.Red, Color.Black);
                enemy1list.drawInfo(spriteBatch, Color.Red, Color.Black);
                enemy2list.drawInfo(spriteBatch, Color.Red, Color.Black);
                tower2.drawInfo(spriteBatch, Color.Red, Color.Black);
                enemy3list.drawInfo(spriteBatch, Color.Red, Color.Black);
                sword.drawInfo(spriteBatch, Color.Red, Color.Black);
            }

            health.Draw(spriteBatch);
            spriteBatch.DrawString(Levels.font, "Kills: " + kills, new Vector2(600, 10), Color.Red);
            spriteBatch.DrawString(Levels.font, "Get " + killLimit + " kills to repel the attack", new Vector2(560, 40), Color.Red);

            spriteBatch.End();
        }

        void changeAnim(string state)
        {
            if (animFlag)
            {
                switch (state)
                {
                    case "up":
                        anims.animPlayer[0].X = 0; anims.animPlayer[0].Y = 0;
                        anims.animPlayer[1].X = 1; anims.animPlayer[1].Y = 0;
                        anims.animPlayer[2].X = 2; anims.animPlayer[2].Y = 0;
                        anims.animPlayer[3].X = 1; anims.animPlayer[3].Y = 0;
                        player.setAnimationSequence(anims.animPlayer, 0, 3, 15);
                        animFlag = false;
                        break;
                    case "down":
                        anims.animPlayer[0].X = 0; anims.animPlayer[0].Y = 2;
                        anims.animPlayer[1].X = 1; anims.animPlayer[1].Y = 2;
                        anims.animPlayer[2].X = 2; anims.animPlayer[2].Y = 2;
                        anims.animPlayer[3].X = 1; anims.animPlayer[3].Y = 2;
                        player.setAnimationSequence(anims.animPlayer, 0, 3, 15);
                        animFlag = false;
                        break;
                    case "left":
                        anims.animPlayer[0].X = 0; anims.animPlayer[0].Y = 3;
                        anims.animPlayer[1].X = 1; anims.animPlayer[1].Y = 3;
                        anims.animPlayer[2].X = 2; anims.animPlayer[2].Y = 3;
                        anims.animPlayer[3].X = 1; anims.animPlayer[3].Y = 3;
                        player.setAnimationSequence(anims.animPlayer, 0, 3, 15);
                        animFlag = false;
                        break;
                    case "right":
                        anims.animPlayer[0].X = 0; anims.animPlayer[0].Y = 1;
                        anims.animPlayer[1].X = 1; anims.animPlayer[1].Y = 1;
                        anims.animPlayer[2].X = 2; anims.animPlayer[2].Y = 1;
                        anims.animPlayer[3].X = 1; anims.animPlayer[3].Y = 1;
                        player.setAnimationSequence(anims.animPlayer, 0, 3, 15);
                        animFlag = false;
                        break;
                    default:
                        break;
                }
            }
        }

        void createEnemy1()
        {
            float scale = 1.5f; 

            enemy1 = new Sprite3(true, Levels.texEnemy1, - 50, 300 + random.NextInt64(600));
            enemy1.setWidthHeight(359 / 5 * scale, 115 / 2 * scale);
            enemy1.setWidthHeightOfTex(359, 115);
            enemy1.setXframes(5);
            enemy1.setYframes(2);
            enemy1.setHSoffset(new Vector2((359 / 5) / 2, 115 / 4));
            enemy1.setBB(0, 0, 359 / 5, 115 / 2);
            enemy1.setMoveSpeed(3);

            enemy1.setAnimationSequence(anims.animEnemy1, 0, 3, 15);
            enemy1.animationStart();

            enemy1list.addSpriteReuse(enemy1);
        }
        void createEnemy2()
        {
            float scale = 1.5f;

            enemy2 = new Sprite3(true, Levels.texEnemy2, 1500, 300 + random.NextInt64(600));
            enemy2.setWidthHeight(243 / 6 * scale, 80 / 2 * scale);
            enemy2.setWidthHeightOfTex(243, 80);
            enemy2.setXframes(6);
            enemy2.setYframes(2);
            enemy2.setHSoffset(new Vector2((243/ 5) / 2, 80 / 4));
            enemy2.setBB(0, 0, 243 / 5, 80 / 2);
            enemy2.setMoveSpeed(3);



            enemy2.setAnimationSequence(anims.animEnemy2, 0, 4, 10);
            enemy2.animationStart();

            enemy2list.addSpriteReuse(enemy2);
        }


        void createEnemy3()
        {
            float scale = 1.5f;

            if (enemy3FlagDirection == 1)
            {
                enemy3a = new Sprite3(true, Levels.texEnemy3a, random.NextInt64(700), - 50);
                enemy3a.setWidthHeight(230 / 5 * scale, 51 * scale);
                enemy3a.setWidthHeightOfTex(230, 51);
                enemy3a.setXframes(5);
                enemy3a.setYframes(1);
                enemy3a.setHSoffset(new Vector2((230 / 5) / 2, 51 / 2));
                enemy3a.setBB(0, 0, 230 / 5, 51);
                enemy3a.setMoveSpeed(3);

                enemy3a.setAnimationSequence(anims.animEnemy3, 0, 4, 15);
                enemy3a.animationStart();

                enemy3list.addSpriteReuse(enemy3a);
                enemy3FlagDirection = 2;
            }
            if (enemy3FlagDirection == 2)
            {
                enemy3b = new Sprite3(true, Levels.texEnemy3b, 700 + random.NextInt64(700), - 50);
                enemy3b.setWidthHeight(230 / 5 * scale, 51 * scale);
                enemy3b.setWidthHeightOfTex(230, 51);
                enemy3b.setXframes(5);
                enemy3b.setYframes(1);
                enemy3b.setHSoffset(new Vector2((230 / 5) / 2, 51 / 2));
                enemy3b.setBB(0, 0, 230 / 5, 51);
                enemy3b.setMoveSpeed(3);

                enemy3b.setAnimationSequence(anims.animEnemy3, 5, 8, 15);
                enemy3b.animationStart();

                enemy3list.addSpriteReuse(enemy3b);
                enemy3FlagDirection = 1;
            }
        }

        private void checkTowerHit(Sprite3 tower)
        {
            if (enemy1list.collisionAA(tower) != -1 && tower.hitPoints > 0)
            {
                for (int i = 0; i < enemy1list.count(); i++)
                {
                    Sprite3 e = enemy1list[i];
                    if (e.collision(tower))
                    {
                        e.setMoveSpeed(0);
                        hitAnimation(e, 1);
                    }
                }
                if (soundTick == 0)
                {
                    Levels.limSwordSound.playSoundIfOk();
                    soundTick = 50;
                }
                tower.hitPoints--;
                soundTick--;
            }

            if (enemy2list.collisionAA(tower) != -1 && tower.hitPoints > 0)
            {
                for (int i = 0; i < enemy2list.count(); i++)
                {
                    Sprite3 e = enemy2list[i];
                    if (e.collision(tower))
                    {
                        e.setMoveSpeed(0);
                        hitAnimation(e, 2);
                    }
                }
                if (soundTick2 == 0)
                {
                    Levels.limSwordSound.playSoundIfOk();
                    soundTick2 = 75;
                }
                tower.hitPoints--;
                soundTick2--;
            }

            if (enemy3list.collisionAA(tower) != -1 && tower.hitPoints > 0)
            {
                if (soundTick3 == 0)
                {
                    Levels.limSwordSound.playSoundIfOk();
                    soundTick3 = 90;
                }
                tower.hitPoints--;
                soundTick3--;
            }
        }

        private void hitAnimation(Sprite3 enemy, int n)
        {   
            if (n==1)
            {
                Vector2[] animStrike = new Vector2[9];
                animStrike[0].X = 0; animStrike[0].Y = 0;
                animStrike[1].X = 1; animStrike[1].Y = 0;
                animStrike[2].X = 2; animStrike[2].Y = 0;
                animStrike[3].X = 3; animStrike[3].Y = 0;
                animStrike[4].X = 4; animStrike[4].Y = 0;

                enemy.setAnimationSequence(animStrike, 0, 4, 10);
            }

            if(n==2)
            {
                enemy.setXframes(4);
                enemy.setYframes(2);
                enemy.setWidthHeight(243 / 4 * 1.5f, 80 / 2 * 1.5f);

                Vector2[] animStrike = new Vector2[9];
                animStrike[0].X = 0; animStrike[0].Y = 1;
                animStrike[1].X = 1; animStrike[1].Y = 1;
                animStrike[2].X = 2; animStrike[2].Y = 1;
                animStrike[3].X = 3; animStrike[3].Y = 1;


                enemy.setAnimationSequence(animStrike, 0, 3, 10);
            }
            
        }
    }
}

