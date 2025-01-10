using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;

namespace GameMT2
{
    public class GameLevel_End : RC_GameStateParent
    {
        Texture2D texWin = null;
        Texture2D texLose = null;
        ImageBackground win = null;
        ImageBackground lose = null;
        Sprite3 winning = null;
        Sprite3 fail = null;
        public override void LoadContent()
        {
            texWin = Util.texFromFile(graphicsDevice, MT2.dir + "screen2.png");
            texLose = Util.texFromFile(graphicsDevice, MT2.dir + "screen1.png");
            win = new ImageBackground(texWin, Color.White, graphicsDevice);
            lose = new ImageBackground(texLose, Color.White, graphicsDevice);

            fail = new Sprite3(true, MT2.texFail, 700, 250);
            fail.setWidthHeight(500, 400);

            winning = new Sprite3(true, MT2.texWinning, 600, 400);


        }

        public override void Update(GameTime gameTime)
        {

            
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Blue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            if (MainLevel.score > 1000)
            {
                win.Draw(spriteBatch);
                winning.Draw(spriteBatch);
            }
            else
            {
                lose.Draw(spriteBatch);
                fail.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }


}
