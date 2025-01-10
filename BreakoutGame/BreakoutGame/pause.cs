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
    internal class pause : RC_GameStateParent
    {
        static public Texture2D texPause = null;

        ImageBackground pause1 = null;
        ColorField trans = null;

        public override void LoadContent()
        {
            texPause = Util.texFromFile(graphicsDevice, Breakout.dir + "Pause1.png");
            pause1 = new ImageBackground(texPause, Color.White, graphicsDevice);
            trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 800, 600));
        }

        public override void Update(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.P) && prevKeyState.IsKeyUp(Keys.P))
            {
                Breakout.levelManager.popLevel();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Breakout.levelManager.prevStatePlayLevel.Draw(gameTime);

            //spriteBatch.Begin();  // depending on version you may need this
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            trans.Draw(spriteBatch);
            pause1.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

}

