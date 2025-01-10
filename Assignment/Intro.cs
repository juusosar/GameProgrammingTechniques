using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;

namespace Assignment
{
    public class GameLevel_Intro : RC_GameStateParent
    {
        Texture2D texStart = null;
        ImageBackground start = null;

        public override void LoadContent()
        {
            texStart = Util.texFromFile(graphicsDevice, Levels.dir + "splash.png");
            start = new ImageBackground(texStart, Color.White, graphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {

            if (keyState.IsKeyDown(Keys.Space))
            {
                Levels.levelManager.setLevel(1);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            start.Draw(spriteBatch);
            spriteBatch.End();
        }
    }


}
