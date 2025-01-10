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


namespace BreakoutGame
{
    internal class splashScreen : RC_GameStateParent
    {
        static public Texture2D texSplash = null;
        ImageBackground back2 = null;
        int timerTicks = 300;

        public override void LoadContent() 
        {
            texSplash = Util.texFromFile(graphicsDevice, Breakout.dir + "Splash3.png");

            back2 = new ImageBackground(texSplash, Color.White, graphicsDevice);
        }
        public override void Update(GameTime gameTime) 
        {
            timerTicks--;
            if (timerTicks <= 0)
            {
                Breakout.levelManager.setLevel(0);
            }
        }
        public override void Draw(GameTime gameTime) 
        {
            spriteBatch.Begin();
            back2.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
