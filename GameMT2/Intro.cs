using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;

namespace GameMT2
{
    public class GameLevel_Intro : RC_GameStateParent
    {
        Texture2D texStart = null;
        ImageBackground start = null;
        int timerTicks = 200;
        public override void LoadContent()
        {
            texStart = Util.texFromFile(graphicsDevice, MT2.dir + "ss2.png");
            start = new ImageBackground(texStart, Color.White, graphicsDevice);
            
        }

        public override void Update(GameTime gameTime)
        {

            timerTicks--;
            if (timerTicks <= 0)
            {
                MT2.levelManager.setLevel(1);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            start.Draw(spriteBatch);
            //spriteBatch.DrawString(font1, "level 0 - press n to go to next level", new Vector2(100, 100), Color.Brown);
            spriteBatch.End();
        }
    }


}
